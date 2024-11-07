-- Función: fn_UsuarioActual
-- Descripción: Devuelve el nombre del usuario actual conectado a la base de datos.
USE ClinicaDental;
GO

-- Eliminar la función si existe en el esquema dbo
DROP FUNCTION IF EXISTS dbo.fn_UsuarioActual;
GO

-- Crear la función en el esquema dbo
CREATE FUNCTION dbo.fn_UsuarioActual()
RETURNS VARCHAR(128)
AS
BEGIN
    RETURN SYSTEM_USER;
END;
GO


USE ClinicaDental;
GO

-- Trigger: tr_AuditarInsercionPaciente


-- Trigger: tr_EvitarDuplicadosCita
CREATE OR ALTER TRIGGER  tr_EvitarDuplicadosCita
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
CREATE OR ALTER TRIGGER  tr_EvitarFacturaConMontoCero
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


-- Trigger: tr_EvitarPagosNegativos
CREATE OR ALTER TRIGGER  tr_EvitarPagosNegativos
ON Pago
AFTER INSERT
AS
BEGIN
    DECLARE @Monto_Pago MONEY, @ID_Pago UNIQUEIDENTIFIER;

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


-- TRIGGERS Generales de Tablas 

CREATE OR ALTER TRIGGER tr_AuditarInsercionPaciente
ON Paciente
AFTER INSERT
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO Auditoria (
        ID_Auditoria, 
        Fecha_Hora_Accion, 
        Accion, 
        DispositivoQueRealizo, 
        Usuario
    )
    SELECT
        NEWID(),
        GETDATE(),
        'Inserción de paciente: ' + i.Nombre_Pac + ' ' + i.Apellido1_Pac + ' ' + i.Apellido2_Pac,
        HOST_NAME(),
        dbo.fn_UsuarioActual()
    FROM inserted i;
END;
GO

-- Trigger: tr_AuditarActualizacionPaciente
CREATE OR ALTER TRIGGER tr_AuditarActualizacionPaciente
ON Paciente
AFTER UPDATE
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO Auditoria (
        ID_Auditoria, 
        Fecha_Hora_Accion, 
        Accion, 
        DispositivoQueRealizo, 
        Usuario
    )
    SELECT
        NEWID(),
        GETDATE(),
        'Actualización de paciente: Cambio de Nombre de ' + d.Nombre_Pac + ' a ' + i.Nombre_Pac,
        HOST_NAME(),
        dbo.fn_UsuarioActual()
    FROM inserted i
    JOIN deleted d ON i.ID_Paciente = d.ID_Paciente
    WHERE d.Nombre_Pac <> i.Nombre_Pac;
END;
GO

-- Trigger: tr_AuditarEliminacionPaciente
CREATE OR ALTER TRIGGER tr_AuditarEliminacionPaciente
ON Paciente
AFTER DELETE
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO Auditoria (
        ID_Auditoria, 
        Fecha_Hora_Accion, 
        Accion, 
        DispositivoQueRealizo, 
        Usuario
    )
    SELECT
        NEWID(),
        GETDATE(),
        'Eliminación de paciente: ' + d.Nombre_Pac + ' ' + d.Apellido1_Pac,
        HOST_NAME(),
        dbo.fn_UsuarioActual()
    FROM deleted d;
END;
GO





-- Trigger: tr_AuditarInsercionTratamiento
CREATE TRIGGER tr_AuditarInsercionTratamiento
ON Tratamiento
AFTER INSERT
AS
BEGIN
    INSERT INTO Auditoria (
        ID_Auditoria, 
        Fecha_Hora_Accion, 
        Accion, 
        DispositivoQueRealizo, 
        Usuario
    )
    SELECT 
        NEWID(), 
        GETDATE(), 
        'Inserción en Tratamiento: ' + i.ID_Tratamiento, 
        HOST_NAME(), 
        dbo.fn_UsuarioActual()
    FROM inserted i;
END;
GO




-- Trigger: tr_AuditarInsercionFactura
CREATE TRIGGER tr_AuditarInsercionFactura
ON Factura
AFTER INSERT
AS
BEGIN
    INSERT INTO Auditoria (
        ID_Auditoria, 
        Fecha_Hora_Accion, 
        Accion, 
        DispositivoQueRealizo, 
        Usuario
    )
    SELECT 
        NEWID(), 
        GETDATE(), 
        'Inserción en Factura: ' + i.ID_Factura, 
        HOST_NAME(), 
        dbo.fn_UsuarioActual()
    FROM inserted i;
END;
GO

-- Trigger: tr_ActualizarEstadoFactura
CREATE OR ALTER TRIGGER tr_ActualizarEstadoFactura
ON Pago
AFTER INSERT
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @ID_Factura CHAR(8), 
            @MontoTotal MONEY, 
            @MontoPagado MONEY;

    -- Obtener ID de factura pagada
    SELECT @ID_Factura = i.ID_Factura
    FROM inserted i;

    -- Obtener monto total de la factura
    SELECT @MontoTotal = MontoTotal_Fa
    FROM Factura
    WHERE ID_Factura = @ID_Factura;

    -- Calcular monto pagado
    SELECT @MontoPagado = SUM(Monto_Pago)
    FROM Pago
    WHERE ID_Factura = @ID_Factura;

    -- Actualizar el estado si el monto total está cubierto
    IF @MontoPagado >= @MontoTotal
    BEGIN
        UPDATE Factura
        SET ID_EstadoPago = 'PAGADO'
        WHERE ID_Factura = @ID_Factura;

        INSERT INTO Auditoria (
            ID_Auditoria, 
            Fecha_Hora_Accion, 
            Accion, 
            DispositivoQueRealizo, 
            Usuario
        )
        VALUES (
            NEWID(), 
            GETDATE(), 
            'Actualizar Estado Factura ID: ' + @ID_Factura, 
            HOST_NAME(), 
            dbo.fn_UsuarioActual()
        );
    END;
END;
GO



-- Trigger: tr_AuditarInsercionPago
CREATE TRIGGER tr_AuditarInsercionPago
ON Pago
AFTER INSERT
AS
BEGIN
    INSERT INTO Auditoria (
        ID_Auditoria, 
        Fecha_Hora_Accion, 
        Accion, 
        DispositivoQueRealizo, 
        Usuario
    )
    SELECT 
        NEWID(), 
        GETDATE(), 
        'Inserción en Pago: ' + CONVERT(VARCHAR(36), i.ID_Pago), 
        HOST_NAME(), 
        dbo.fn_UsuarioActual()
    FROM inserted i;
END;
GO



-- Trigger: tr_AuditarInsercionCuenta
CREATE TRIGGER tr_AuditarInsercionCuenta
ON Cuenta
AFTER INSERT
AS
BEGIN
    INSERT INTO Auditoria (
        ID_Auditoria, 
        Fecha_Hora_Accion, 
        Accion, 
        DispositivoQueRealizo, 
        Usuario
    )
    SELECT 
        NEWID(), 
        GETDATE(), 
        'Inserción en Cuenta: ' + i.ID_Cuenta, 
        HOST_NAME(), 
        dbo.fn_UsuarioActual()
    FROM inserted i;
END;
GO



-- Trigger: tr_AuditarInsercionCita
CREATE TRIGGER tr_AuditarInsercionCita
ON Cita
AFTER INSERT
AS
BEGIN
    INSERT INTO Auditoria (
        ID_Auditoria, 
        Fecha_Hora_Accion, 
        Accion, 
        DispositivoQueRealizo, 
        Usuario
    )
    SELECT 
        NEWID(), 
        GETDATE(), 
        'Inserción en Cita: ' + i.ID_Cita, 
        HOST_NAME(), 
        dbo.fn_UsuarioActual()
    FROM inserted i;
END;
GO



-- Trigger: tr_AuditarInsercionProcedimiento
CREATE TRIGGER tr_AuditarInsercionProcedimiento
ON Procedimiento
AFTER INSERT
AS
BEGIN
    INSERT INTO Auditoria (
        ID_Auditoria, 
        Fecha_Hora_Accion, 
        Accion, 
        DispositivoQueRealizo, 
        Usuario
    )
    SELECT 
        NEWID(), 
        GETDATE(), 
        'Inserción en Procedimiento: ' + i.ID_Procedimiento, 
        HOST_NAME(), 
        dbo.fn_UsuarioActual()
    FROM inserted i;
END;
GO