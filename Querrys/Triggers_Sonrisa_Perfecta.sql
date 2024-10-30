use ClinicaDental
go


-- Trigger: tr_AuditarInsercionPaciente
CREATE TRIGGER tr_AuditarInsercionPaciente
ON Paciente
AFTER INSERT
AS
BEGIN
    DECLARE @ID_Paciente CHAR(8), @Nombre VARCHAR(20), @Apellido1 VARCHAR(20), @Apellido2 VARCHAR(20), @Fecha_Hora DATETIME, @ID_Usuario CHAR(8);

    -- Obtener datos del paciente insertado
    SELECT @ID_Paciente = i.ID_Paciente, @Nombre = i.Nombre_Pac, @Apellido1 = i.Apellido1_Pac, @Apellido2 = i.Apellido2_Pac, @Fecha_Hora = GETDATE(), @ID_Usuario = 'ID_ADMIN'
    FROM inserted i;

    -- Insertar en Auditoria
    INSERT INTO Auditoria (ID_Auditoria, Fecha_Hora_Accion, Descripcion_Accion, ID_TipoAccion, ID_Usuario)
    VALUES (NEWID(), @Fecha_Hora, 'Inserción de paciente: ' + @Nombre + ' ' + @Apellido1 + ' ' + @Apellido2, 'INSERCION', @ID_Usuario);
END;
GO

-- Trigger: tr_AuditarActualizacionPaciente
CREATE TRIGGER tr_AuditarActualizacionPaciente
ON Paciente
AFTER UPDATE
AS
BEGIN
    DECLARE @ID_Paciente CHAR(8), @NombreAntiguo VARCHAR(20), @NombreNuevo VARCHAR(20), @Fecha_Hora DATETIME, @ID_Usuario CHAR(8);

    -- Obtener ID del paciente y nombres antiguos y nuevos
    SELECT @ID_Paciente = i.ID_Paciente, @NombreAntiguo = d.Nombre_Pac, @NombreNuevo = i.Nombre_Pac, @Fecha_Hora = GETDATE(), @ID_Usuario = 'ID_ADMIN'
    FROM inserted i
    JOIN deleted d ON i.ID_Paciente = d.ID_Paciente;

    -- Auditar si el nombre cambió
    IF @NombreAntiguo <> @NombreNuevo
    BEGIN
        INSERT INTO Auditoria (ID_Auditoria, Fecha_Hora_Accion, Descripcion_Accion, ID_TipoAccion, ID_Usuario)
        VALUES (NEWID(), @Fecha_Hora, 'Actualización de paciente: Cambio de Nombre de ' + @NombreAntiguo + ' a ' + @NombreNuevo, 'ACTUALIZACION', @ID_Usuario);
    END;
END;
GO

-- Trigger: tr_AuditarEliminacionPaciente
CREATE TRIGGER tr_AuditarEliminacionPaciente
ON Paciente
AFTER DELETE
AS
BEGIN
    DECLARE @ID_Paciente CHAR(8), @Nombre VARCHAR(20), @Apellido1 VARCHAR(20), @Fecha_Hora DATETIME, @ID_Usuario CHAR(8);

    -- Obtener datos del paciente eliminado
    SELECT @ID_Paciente = d.ID_Paciente, @Nombre = d.Nombre_Pac, @Apellido1 = d.Apellido1_Pac, @Fecha_Hora = GETDATE(), @ID_Usuario = 'ID_ADMIN'
    FROM deleted d;

    -- Insertar en Auditoria
    INSERT INTO Auditoria (ID_Auditoria, Fecha_Hora_Accion, Descripcion_Accion, ID_TipoAccion, ID_Usuario)
    VALUES (NEWID(), @Fecha_Hora, 'Eliminación de paciente: ' + @Nombre + ' ' + @Apellido1, 'ELIMINACION', @ID_Usuario);
END;
GO

-- Trigger: tr_EvitarDuplicadosCita
CREATE TRIGGER tr_EvitarDuplicadosCita
ON Cita
INSTEAD OF INSERT
AS
BEGIN
    DECLARE @ID_Dentista CHAR(8), @Fecha_Cita DATE, @Hora_Inicio TIME;

    -- Obtener datos de la cita nueva
    SELECT @ID_Dentista = i.ID_Dentista, @Fecha_Cita = i.Fecha_Cita, @Hora_Inicio = i.Hora_Inicio
    FROM inserted i;

    -- Verificar si ya existe una cita en el mismo horario
    IF EXISTS (
        SELECT 1
        FROM Cita
        WHERE ID_Dentista = @ID_Dentista
        AND Fecha_Cita = @Fecha_Cita
        AND Hora_Inicio = @Hora_Inicio
    )
    BEGIN
        RAISERROR('Ya existe una cita programada para el dentista en esta fecha y hora.', 16, 1);
        ROLLBACK TRANSACTION;
    END
    ELSE
    BEGIN
        INSERT INTO Cita (ID_Cita, Fecha_Cita, Motivo, Hora_Inicio, Hora_Fin, ID_Paciente, ID_Dentista, ID_Funcionario, ID_EstadoCita)
        SELECT ID_Cita, Fecha_Cita, Motivo, Hora_Inicio, Hora_Fin, ID_Paciente, ID_Dentista, ID_Funcionario, ID_EstadoCita
        FROM inserted;
    END;
END;
GO

-- Trigger: tr_EvitarFacturaConMontoCero
CREATE TRIGGER tr_EvitarFacturaConMontoCero
ON Factura
AFTER INSERT
AS
BEGIN
    DECLARE @MontoTotal MONEY, @ID_Factura CHAR(8);

    -- Obtener monto total de la factura
    SELECT @MontoTotal = i.MontoTotal_Fa, @ID_Factura = i.ID_Factura
    FROM inserted i;

    -- Si el monto es cero o negativo
    IF @MontoTotal <= 0
    BEGIN
        DELETE FROM Factura WHERE ID_Factura = @ID_Factura;
        RAISERROR('No se permite la inserción de facturas con monto cero o negativo.', 16, 1);
    END;
END;
GO

-- Trigger: tr_ActualizarEstadoFactura
CREATE TRIGGER tr_ActualizarEstadoFactura
ON Pago
AFTER INSERT
AS
BEGIN
    DECLARE @ID_Factura CHAR(8), @MontoTotal MONEY, @MontoPagado MONEY;

    -- Obtener ID de factura pagada
    SELECT @ID_Factura = i.ID_Factura
    FROM inserted i;

    -- Obtener monto total de la factura y pagos realizados
    SELECT @MontoTotal = MontoTotal_Fa
    FROM Factura
    WHERE ID_Factura = @ID_Factura;

    SELECT @MontoPagado = SUM(Monto_Pago)
    FROM Pago
    WHERE ID_Factura = @ID_Factura;

    -- Actualizar el estado si el monto total está cubierto
    IF @MontoPagado >= @MontoTotal
    BEGIN
        UPDATE Factura
        SET ID_EstadoPago = 'PAGADO'
        WHERE ID_Factura = @ID_Factura;
    END;
END;
GO


-- Trigger: tr_EvitarPagosNegativos
CREATE TRIGGER tr_EvitarPagosNegativos
ON Pago
AFTER INSERT
AS
BEGIN
    DECLARE @Monto_Pago MONEY, @ID_Pago INT;

    -- Obtener monto del pago insertado
    SELECT @Monto_Pago = i.Monto_Pago, @ID_Pago = i.ID_Pago
    FROM inserted i;

    -- Si el monto es negativo, eliminar y lanzar error
    IF @Monto_Pago < 0
    BEGIN
        DELETE FROM Pago WHERE ID_Pago = @ID_Pago;
        RAISERROR('No se pueden registrar pagos con monto negativo.', 16, 1);
    END;
END;
GO
