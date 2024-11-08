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
CREATE OR ALTER TRIGGER tr_AuditarInsercionPaciente
ON Paciente
AFTER INSERT
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO Auditoria (
        ID_Auditoria, 
        Fecha_Hora_Accion, 
        Acción, 
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
        Acción, 
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
        Acción, 
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
            Acción, 
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


-- Trigger: tr_EvitarPagosNegativos
CREATE TRIGGER tr_EvitarPagosNegativos
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

-- Auditoría de inserción, actualización y eliminación para la tabla Estado_Pago
CREATE OR ALTER TRIGGER tr_AuditarInsercionEstadoPago
ON Estado_Pago
AFTER INSERT
AS
BEGIN
    SET NOCOUNT ON;
    INSERT INTO Auditoria (ID_Auditoria, Fecha_Hora_Accion, Acción, DispositivoQueRealizo, Usuario)
    SELECT NEWID(), GETDATE(), 'Inserción en Estado_Pago: ' + i.ID_EstadoPago, HOST_NAME(), dbo.fn_UsuarioActual()
    FROM inserted i;
END;
GO

CREATE OR ALTER TRIGGER tr_AuditarActualizacionEstadoPago
ON Estado_Pago
AFTER UPDATE
AS
BEGIN
    SET NOCOUNT ON;
    INSERT INTO Auditoria (ID_Auditoria, Fecha_Hora_Accion, Acción, DispositivoQueRealizo, Usuario)
    SELECT NEWID(), GETDATE(), 'Actualización en Estado_Pago: ' + d.ID_EstadoPago + ' Cambios aplicados.', HOST_NAME(), dbo.fn_UsuarioActual()
    FROM deleted d;
END;
GO

CREATE OR ALTER TRIGGER tr_AuditarEliminacionEstadoPago
ON Estado_Pago
AFTER DELETE
AS
BEGIN
    SET NOCOUNT ON;
    INSERT INTO Auditoria (ID_Auditoria, Fecha_Hora_Accion, Acción, DispositivoQueRealizo, Usuario)
    SELECT NEWID(), GETDATE(), 'Eliminación en Estado_Pago: ' + d.ID_EstadoPago, HOST_NAME(), dbo.fn_UsuarioActual()
    FROM deleted d;
END;
GO

-- Auditoría de inserción, actualización y eliminación para la tabla Tipo_Tratamiento
CREATE OR ALTER TRIGGER tr_AuditarInsercionTipoTratamiento
ON Tipo_Tratamiento
AFTER INSERT
AS
BEGIN
    SET NOCOUNT ON;
    INSERT INTO Auditoria (ID_Auditoria, Fecha_Hora_Accion, Acción, DispositivoQueRealizo, Usuario)
    SELECT NEWID(), GETDATE(), 'Inserción en Tipo_Tratamiento: ' + i.ID_TipoTratamiento, HOST_NAME(), dbo.fn_UsuarioActual()
    FROM inserted i;
END;
GO

CREATE OR ALTER TRIGGER tr_AuditarActualizacionTipoTratamiento
ON Tipo_Tratamiento
AFTER UPDATE
AS
BEGIN
    SET NOCOUNT ON;
    INSERT INTO Auditoria (ID_Auditoria, Fecha_Hora_Accion, Acción, DispositivoQueRealizo, Usuario)
    SELECT NEWID(), GETDATE(), 'Actualización en Tipo_Tratamiento: ' + d.ID_TipoTratamiento + ' Cambios aplicados.', HOST_NAME(), dbo.fn_UsuarioActual()
    FROM deleted d;
END;
GO

CREATE OR ALTER TRIGGER tr_AuditarEliminacionTipoTratamiento
ON Tipo_Tratamiento
AFTER DELETE
AS
BEGIN
    SET NOCOUNT ON;
    INSERT INTO Auditoria (ID_Auditoria, Fecha_Hora_Accion, Acción, DispositivoQueRealizo, Usuario)
    SELECT NEWID(), GETDATE(), 'Eliminación en Tipo_Tratamiento: ' + d.ID_TipoTratamiento, HOST_NAME(), dbo.fn_UsuarioActual()
    FROM deleted d;
END;
GO

-- Auditoría de inserción, actualización y eliminación para la tabla Tratamiento
CREATE OR ALTER TRIGGER tr_AuditarInsercionTratamiento
ON Tratamiento
AFTER INSERT
AS
BEGIN
    SET NOCOUNT ON;
    INSERT INTO Auditoria (ID_Auditoria, Fecha_Hora_Accion, Acción, DispositivoQueRealizo, Usuario)
    SELECT NEWID(), GETDATE(), 'Inserción en Tratamiento: ' + i.ID_Tratamiento, HOST_NAME(), dbo.fn_UsuarioActual()
    FROM inserted i;
END;
GO

CREATE OR ALTER TRIGGER tr_AuditarActualizacionTratamiento
ON Tratamiento
AFTER UPDATE
AS
BEGIN
    SET NOCOUNT ON;
    INSERT INTO Auditoria (ID_Auditoria, Fecha_Hora_Accion, Acción, DispositivoQueRealizo, Usuario)
    SELECT NEWID(), GETDATE(), 'Actualización en Tratamiento: ' + d.ID_Tratamiento + ' Cambios aplicados.', HOST_NAME(), dbo.fn_UsuarioActual()
    FROM deleted d;
END;
GO

CREATE OR ALTER TRIGGER tr_AuditarEliminacionTratamiento
ON Tratamiento
AFTER DELETE
AS
BEGIN
    SET NOCOUNT ON;
    INSERT INTO Auditoria (ID_Auditoria, Fecha_Hora_Accion, Acción, DispositivoQueRealizo, Usuario)
    SELECT NEWID(), GETDATE(), 'Eliminación en Tratamiento: ' + d.ID_Tratamiento, HOST_NAME(), dbo.fn_UsuarioActual()
    FROM deleted d;
END;
GO

-- Auditoría de inserción, actualización y eliminación para la tabla Procedimiento
CREATE OR ALTER TRIGGER tr_AuditarInsercionProcedimiento
ON Procedimiento
AFTER INSERT
AS
BEGIN
    SET NOCOUNT ON;
    INSERT INTO Auditoria (ID_Auditoria, Fecha_Hora_Accion, Acción, DispositivoQueRealizo, Usuario)
    SELECT NEWID(), GETDATE(), 'Inserción en Procedimiento: ' + i.ID_Procedimiento, HOST_NAME(), dbo.fn_UsuarioActual()
    FROM inserted i;
END;
GO

CREATE OR ALTER TRIGGER tr_AuditarActualizacionProcedimiento
ON Procedimiento
AFTER UPDATE
AS
BEGIN
    SET NOCOUNT ON;
    INSERT INTO Auditoria (ID_Auditoria, Fecha_Hora_Accion, Acción, DispositivoQueRealizo, Usuario)
    SELECT NEWID(), GETDATE(), 'Actualización en Procedimiento: ' + d.ID_Procedimiento + ' Cambios aplicados.', HOST_NAME(), dbo.fn_UsuarioActual()
    FROM deleted d;
END;
GO

CREATE OR ALTER TRIGGER tr_AuditarEliminacionProcedimiento
ON Procedimiento
AFTER DELETE
AS
BEGIN
    SET NOCOUNT ON;
    INSERT INTO Auditoria (ID_Auditoria, Fecha_Hora_Accion, Acción, DispositivoQueRealizo, Usuario)
    SELECT NEWID(), GETDATE(), 'Eliminación en Procedimiento: ' + d.ID_Procedimiento, HOST_NAME(), dbo.fn_UsuarioActual()
    FROM deleted d;
END;
GO

-- Auditoría de inserción, actualización y eliminación para la tabla Factura
CREATE OR ALTER TRIGGER tr_AuditarInsercionFactura
ON Factura
AFTER INSERT
AS
BEGIN
    SET NOCOUNT ON;
    INSERT INTO Auditoria (ID_Auditoria, Fecha_Hora_Accion, Acción, DispositivoQueRealizo, Usuario)
    SELECT NEWID(), GETDATE(), 'Inserción en Factura: ' + i.ID_Factura, HOST_NAME(), dbo.fn_UsuarioActual()
    FROM inserted i;
END;
GO

CREATE OR ALTER TRIGGER tr_AuditarActualizacionFactura
ON Factura
AFTER UPDATE
AS
BEGIN
    SET NOCOUNT ON;
    INSERT INTO Auditoria (ID_Auditoria, Fecha_Hora_Accion, Acción, DispositivoQueRealizo, Usuario)
    SELECT NEWID(), GETDATE(), 'Actualización en Factura: ' + d.ID_Factura + ' Cambios aplicados.', HOST_NAME(), dbo.fn_UsuarioActual()
    FROM deleted d;
END;
GO

CREATE OR ALTER TRIGGER tr_AuditarEliminacionFactura
ON Factura
AFTER DELETE
AS
BEGIN
    SET NOCOUNT ON;
    INSERT INTO Auditoria (ID_Auditoria, Fecha_Hora_Accion, Acción, DispositivoQueRealizo, Usuario)
    SELECT NEWID(), GETDATE(), 'Eliminación en Factura: ' + d.ID_Factura, HOST_NAME(), dbo.fn_UsuarioActual()
    FROM deleted d;
END;
GO


-- Función de Auditoría
CREATE OR ALTER FUNCTION dbo.fn_UsuarioActual()
RETURNS VARCHAR(128)
AS
BEGIN
    RETURN SYSTEM_USER;
END;
GO

-- Trigger de auditoría para Historial_Medico
CREATE OR ALTER TRIGGER tr_AuditarInsercionHistorialMedico
ON Historial_Medico
AFTER INSERT
AS
BEGIN
    SET NOCOUNT ON;
    INSERT INTO Auditoria (ID_Auditoria, Fecha_Hora_Accion, Acción, DispositivoQueRealizo, Usuario)
    SELECT NEWID(), GETDATE(), 'Inserción en Historial_Medico: ' + i.ID_HistorialMedico, HOST_NAME(), dbo.fn_UsuarioActual()
    FROM inserted i;
END;
GO

CREATE OR ALTER TRIGGER tr_AuditarActualizacionHistorialMedico
ON Historial_Medico
AFTER UPDATE
AS
BEGIN
    SET NOCOUNT ON;
    INSERT INTO Auditoria (ID_Auditoria, Fecha_Hora_Accion, Acción, DispositivoQueRealizo, Usuario)
    SELECT NEWID(), GETDATE(), 'Actualización en Historial_Medico: ' + d.ID_HistorialMedico + ' Cambios aplicados.', HOST_NAME(), dbo.fn_UsuarioActual()
    FROM deleted d;
END;
GO

CREATE OR ALTER TRIGGER tr_AuditarEliminacionHistorialMedico
ON Historial_Medico
AFTER DELETE
AS
BEGIN
    SET NOCOUNT ON;
    INSERT INTO Auditoria (ID_Auditoria, Fecha_Hora_Accion, Acción, DispositivoQueRealizo, Usuario)
    SELECT NEWID(), GETDATE(), 'Eliminación en Historial_Medico: ' + d.ID_HistorialMedico, HOST_NAME(), dbo.fn_UsuarioActual()
    FROM deleted d;
END;
GO

-- Trigger de auditoría para Cuenta
CREATE OR ALTER TRIGGER tr_AuditarInsercionCuenta
ON Cuenta
AFTER INSERT
AS
BEGIN
    SET NOCOUNT ON;
    INSERT INTO Auditoria (ID_Auditoria, Fecha_Hora_Accion, Acción, DispositivoQueRealizo, Usuario)
    SELECT NEWID(), GETDATE(), 'Inserción en Cuenta: ' + i.ID_Cuenta, HOST_NAME(), dbo.fn_UsuarioActual()
    FROM inserted i;
END;
GO

CREATE OR ALTER TRIGGER tr_AuditarActualizacionCuenta
ON Cuenta
AFTER UPDATE
AS
BEGIN
    SET NOCOUNT ON;
    INSERT INTO Auditoria (ID_Auditoria, Fecha_Hora_Accion, Acción, DispositivoQueRealizo, Usuario)
    SELECT NEWID(), GETDATE(), 'Actualización en Cuenta: ' + d.ID_Cuenta + ' Cambios aplicados.', HOST_NAME(), dbo.fn_UsuarioActual()
    FROM deleted d;
END;
GO

CREATE OR ALTER TRIGGER tr_AuditarEliminacionCuenta
ON Cuenta
AFTER DELETE
AS
BEGIN
    SET NOCOUNT ON;
    INSERT INTO Auditoria (ID_Auditoria, Fecha_Hora_Accion, Acción, DispositivoQueRealizo, Usuario)
    SELECT NEWID(), GETDATE(), 'Eliminación en Cuenta: ' + d.ID_Cuenta, HOST_NAME(), dbo.fn_UsuarioActual()
    FROM deleted d;
END;
GO

-- Trigger de auditoría para Factura_Procedimiento
CREATE OR ALTER TRIGGER tr_AuditarInsercionFacturaProcedimiento
ON Factura_Procedimiento
AFTER INSERT
AS
BEGIN
    SET NOCOUNT ON;
    INSERT INTO Auditoria (ID_Auditoria, Fecha_Hora_Accion, Acción, DispositivoQueRealizo, Usuario)
    SELECT NEWID(), GETDATE(), 'Inserción en Factura_Procedimiento: ' + i.ID_Factura_Procedimiento, HOST_NAME(), dbo.fn_UsuarioActual()
    FROM inserted i;
END;
GO

CREATE OR ALTER TRIGGER tr_AuditarActualizacionFacturaProcedimiento
ON Factura_Procedimiento
AFTER UPDATE
AS
BEGIN
    SET NOCOUNT ON;
    INSERT INTO Auditoria (ID_Auditoria, Fecha_Hora_Accion, Acción, DispositivoQueRealizo, Usuario)
    SELECT NEWID(), GETDATE(), 'Actualización en Factura_Procedimiento: ' + d.ID_Factura_Procedimiento + ' Cambios aplicados.', HOST_NAME(), dbo.fn_UsuarioActual()
    FROM deleted d;
END;
GO

CREATE OR ALTER TRIGGER tr_AuditarEliminacionFacturaProcedimiento
ON Factura_Procedimiento
AFTER DELETE
AS
BEGIN
    SET NOCOUNT ON;
    INSERT INTO Auditoria (ID_Auditoria, Fecha_Hora_Accion, Acción, DispositivoQueRealizo, Usuario)
    SELECT NEWID(), GETDATE(), 'Eliminación en Factura_Procedimiento: ' + d.ID_Factura_Procedimiento, HOST_NAME(), dbo.fn_UsuarioActual()
    FROM deleted d;
END;
GO

-- Trigger de auditoría para Factura_Tratamiento
CREATE OR ALTER TRIGGER tr_AuditarInsercionFacturaTratamiento
ON Factura_Tratamiento
AFTER INSERT
AS
BEGIN
    SET NOCOUNT ON;
    INSERT INTO Auditoria (ID_Auditoria, Fecha_Hora_Accion, Acción, DispositivoQueRealizo, Usuario)
    SELECT NEWID(), GETDATE(), 'Inserción en Factura_Tratamiento: ' + i.ID_Factura_Tratamiento, HOST_NAME(), dbo.fn_UsuarioActual()
    FROM inserted i;
END;
GO

CREATE OR ALTER TRIGGER tr_AuditarActualizacionFacturaTratamiento
ON Factura_Tratamiento
AFTER UPDATE
AS
BEGIN
    SET NOCOUNT ON;
    INSERT INTO Auditoria (ID_Auditoria, Fecha_Hora_Accion, Acción, DispositivoQueRealizo, Usuario)
    SELECT NEWID(), GETDATE(), 'Actualización en Factura_Tratamiento: ' + d.ID_Factura_Tratamiento + ' Cambios aplicados.', HOST_NAME(), dbo.fn_UsuarioActual()
    FROM deleted d;
END;
GO

CREATE OR ALTER TRIGGER tr_AuditarEliminacionFacturaTratamiento
ON Factura_Tratamiento
AFTER DELETE
AS
BEGIN
    SET NOCOUNT ON;
    INSERT INTO Auditoria (ID_Auditoria, Fecha_Hora_Accion, Acción, DispositivoQueRealizo, Usuario)
    SELECT NEWID(), GETDATE(), 'Eliminación en Factura_Tratamiento: ' + d.ID_Factura_Tratamiento, HOST_NAME(), dbo.fn_UsuarioActual()
    FROM deleted d;
END;
GO

-- Trigger de auditoría para Roles
CREATE OR ALTER TRIGGER tr_AuditarInsercionRoles
ON Roles
AFTER INSERT
AS
BEGIN
    SET NOCOUNT ON;
    INSERT INTO Auditoria (ID_Auditoria, Fecha_Hora_Accion, Acción, DispositivoQueRealizo, Usuario)
    SELECT NEWID(), GETDATE(), 'Inserción en Roles: ' + i.ID_Roles, HOST_NAME(), dbo.fn_UsuarioActual()
    FROM inserted i;
END;
GO

CREATE OR ALTER TRIGGER tr_AuditarActualizacionRoles
ON Roles
AFTER UPDATE
AS
BEGIN
    SET NOCOUNT ON;
    INSERT INTO Auditoria (ID_Auditoria, Fecha_Hora_Accion, Acción, DispositivoQueRealizo, Usuario)
    SELECT NEWID(), GETDATE(), 'Actualización en Roles: ' + d.ID_Roles + ' Cambios aplicados.', HOST_NAME(), dbo.fn_UsuarioActual()
    FROM deleted d;
END;
GO

CREATE OR ALTER TRIGGER tr_AuditarEliminacionRoles
ON Roles
AFTER DELETE
AS
BEGIN
    SET NOCOUNT ON;
    INSERT INTO Auditoria (ID_Auditoria, Fecha_Hora_Accion, Acción, DispositivoQueRealizo, Usuario)
    SELECT NEWID(), GETDATE(), 'Eliminación en Roles: ' + d.ID_Roles, HOST_NAME(), dbo.fn_UsuarioActual()
    FROM deleted d;
END;
GO

-- Trigger de auditoría para Permisos
CREATE OR ALTER TRIGGER tr_AuditarInsercionPermisos
ON Permisos
AFTER INSERT
AS
BEGIN
    SET NOCOUNT ON;
    INSERT INTO Auditoria (ID_Auditoria, Fecha_Hora_Accion, Acción, DispositivoQueRealizo, Usuario)
    SELECT NEWID(), GETDATE(), 'Inserción en Permisos: ' + i.ID_Permisos, HOST_NAME(), dbo.fn_UsuarioActual()
    FROM inserted i;
END;
GO

CREATE OR ALTER TRIGGER tr_AuditarActualizacionPermisos
ON Permisos
AFTER UPDATE
AS
BEGIN
    SET NOCOUNT ON;
    INSERT INTO Auditoria (ID_Auditoria, Fecha_Hora_Accion, Acción, DispositivoQueRealizo, Usuario)
    SELECT NEWID(), GETDATE(), 'Actualización en Permisos: ' + d.ID_Permisos + ' Cambios aplicados.', HOST_NAME(), dbo.fn_UsuarioActual()
    FROM deleted d;
END;
GO

CREATE OR ALTER TRIGGER tr_AuditarEliminacionPermisos
ON Permisos
AFTER DELETE
AS
BEGIN
    SET NOCOUNT ON;
    INSERT INTO Auditoria (ID_Auditoria, Fecha_Hora_Accion, Acción, DispositivoQueRealizo, Usuario)
    SELECT NEWID(), GETDATE(), 'Eliminación en Permisos: ' + d.ID_Permisos, HOST_NAME(), dbo.fn_UsuarioActual()
    FROM deleted d;
END;
GO

-- Trigger de auditoría para Usuario_Roles
CREATE OR ALTER TRIGGER tr_AuditarInsercionUsuarioRoles
ON Usuario_Roles
AFTER INSERT
AS
BEGIN
    SET NOCOUNT ON;
    INSERT INTO Auditoria (ID_Auditoria, Fecha_Hora_Accion, Acción, DispositivoQueRealizo, Usuario)
    SELECT NEWID(), GETDATE(), 'Inserción en Usuario_Roles: ' + i.ID_Usuario_Roles, HOST_NAME(), dbo.fn_UsuarioActual()
    FROM inserted i;
END;
GO

CREATE OR ALTER TRIGGER tr_AuditarActualizacionUsuarioRoles
ON Usuario_Roles
AFTER UPDATE
AS
BEGIN
    SET NOCOUNT ON;
    INSERT INTO Auditoria (ID_Auditoria, Fecha_Hora_Accion, Acción, DispositivoQueRealizo, Usuario)
    SELECT NEWID(), GETDATE(), 'Actualización en Usuario_Roles: ' + d.ID_Usuario_Roles + ' Cambios aplicados.', HOST_NAME(), dbo.fn_UsuarioActual()
    FROM deleted d;
END;
GO

CREATE OR ALTER TRIGGER tr_AuditarEliminacionUsuarioRoles
ON Usuario_Roles
AFTER DELETE
AS
BEGIN
    SET NOCOUNT ON;
    INSERT INTO Auditoria (ID_Auditoria, Fecha_Hora_Accion, Acción, DispositivoQueRealizo, Usuario)
    SELECT NEWID(), GETDATE(), 'Eliminación en Usuario_Roles: ' + d.ID_Usuario_Roles, HOST_NAME(), dbo.fn_UsuarioActual()
    FROM deleted d;
END;
GO


-- Trigger de auditoría para Funcionario
CREATE OR ALTER TRIGGER tr_AuditarInsercionFuncionario
ON Funcionario
AFTER INSERT
AS
BEGIN
    SET NOCOUNT ON;
    INSERT INTO Auditoria (ID_Auditoria, Fecha_Hora_Accion, Acción, DispositivoQueRealizo, Usuario)
    SELECT NEWID(), GETDATE(), 'Inserción en Funcionario: ' + i.ID_Funcionario, HOST_NAME(), dbo.fn_UsuarioActual()
    FROM inserted i;
END;
GO

CREATE OR ALTER TRIGGER tr_AuditarActualizacionFuncionario
ON Funcionario
AFTER UPDATE
AS
BEGIN
    SET NOCOUNT ON;
    INSERT INTO Auditoria (ID_Auditoria, Fecha_Hora_Accion, Acción, DispositivoQueRealizo, Usuario)
    SELECT NEWID(), GETDATE(), 'Actualización en Funcionario: ' + d.ID_Funcionario + ' Cambios aplicados.', HOST_NAME(), dbo.fn_UsuarioActual()
    FROM deleted d;
END;
GO

CREATE OR ALTER TRIGGER tr_AuditarEliminacionFuncionario
ON Funcionario
AFTER DELETE
AS
BEGIN
    SET NOCOUNT ON;
    INSERT INTO Auditoria (ID_Auditoria, Fecha_Hora_Accion, Acción, DispositivoQueRealizo, Usuario)
    SELECT NEWID(), GETDATE(), 'Eliminación en Funcionario: ' + d.ID_Funcionario, HOST_NAME(), dbo.fn_UsuarioActual()
    FROM deleted d;
END;
GO

-- Trigger de auditoría para Usuarios
CREATE OR ALTER TRIGGER tr_AuditarInsercionUsuarios
ON Usuarios
AFTER INSERT
AS
BEGIN
    SET NOCOUNT ON;
    INSERT INTO Auditoria (ID_Auditoria, Fecha_Hora_Accion, Acción, DispositivoQueRealizo, Usuario)
    SELECT NEWID(), GETDATE(), 'Inserción en Usuarios: ' + i.ID_Usuario, HOST_NAME(), dbo.fn_UsuarioActual()
    FROM inserted i;
END;
GO

CREATE OR ALTER TRIGGER tr_AuditarActualizacionUsuarios
ON Usuarios
AFTER UPDATE
AS
BEGIN
    SET NOCOUNT ON;
    INSERT INTO Auditoria (ID_Auditoria, Fecha_Hora_Accion, Acción, DispositivoQueRealizo, Usuario)
    SELECT NEWID(), GETDATE(), 'Actualización en Usuarios: ' + d.ID_Usuario + ' Cambios aplicados.', HOST_NAME(), dbo.fn_UsuarioActual()
    FROM deleted d;
END;
GO

CREATE OR ALTER TRIGGER tr_AuditarEliminacionUsuarios
ON Usuarios
AFTER DELETE
AS
BEGIN
    SET NOCOUNT ON;
    INSERT INTO Auditoria (ID_Auditoria, Fecha_Hora_Accion, Acción, DispositivoQueRealizo, Usuario)
    SELECT NEWID(), GETDATE(), 'Eliminación en Usuarios: ' + d.ID_Usuario, HOST_NAME(), dbo.fn_UsuarioActual()
    FROM deleted d;
END;
GO

-- Trigger de auditoría para Dentista
CREATE OR ALTER TRIGGER tr_AuditarInsercionDentista
ON Dentista
AFTER INSERT
AS
BEGIN
    SET NOCOUNT ON;
    INSERT INTO Auditoria (ID_Auditoria, Fecha_Hora_Accion, Acción, DispositivoQueRealizo, Usuario)
    SELECT NEWID(), GETDATE(), 'Inserción en Dentista: ' + i.ID_Dentista, HOST_NAME(), dbo.fn_UsuarioActual()
    FROM inserted i;
END;
GO

CREATE OR ALTER TRIGGER tr_AuditarActualizacionDentista
ON Dentista
AFTER UPDATE
AS
BEGIN
    SET NOCOUNT ON;
    INSERT INTO Auditoria (ID_Auditoria, Fecha_Hora_Accion, Acción, DispositivoQueRealizo, Usuario)
    SELECT NEWID(), GETDATE(), 'Actualización en Dentista: ' + d.ID_Dentista + ' Cambios aplicados.', HOST_NAME(), dbo.fn_UsuarioActual()
    FROM deleted d;
END;
GO

CREATE OR ALTER TRIGGER tr_AuditarEliminacionDentista
ON Dentista
AFTER DELETE
AS
BEGIN
    SET NOCOUNT ON;
    INSERT INTO Auditoria (ID_Auditoria, Fecha_Hora_Accion, Acción, DispositivoQueRealizo, Usuario)
    SELECT NEWID(), GETDATE(), 'Eliminación en Dentista: ' + d.ID_Dentista, HOST_NAME(), dbo.fn_UsuarioActual()
    FROM deleted d;
END;
GO

-- Trigger de auditoría para Especialidad
CREATE OR ALTER TRIGGER tr_AuditarInsercionEspecialidad
ON Especialidad
AFTER INSERT
AS
BEGIN
    SET NOCOUNT ON;
    INSERT INTO Auditoria (ID_Auditoria, Fecha_Hora_Accion, Acción, DispositivoQueRealizo, Usuario)
    SELECT NEWID(), GETDATE(), 'Inserción en Especialidad: ' + i.ID_Especialidad, HOST_NAME(), dbo.fn_UsuarioActual()
    FROM inserted i;
END;
GO

CREATE OR ALTER TRIGGER tr_AuditarActualizacionEspecialidad
ON Especialidad
AFTER UPDATE
AS
BEGIN
    SET NOCOUNT ON;
    INSERT INTO Auditoria (ID_Auditoria, Fecha_Hora_Accion, Acción, DispositivoQueRealizo, Usuario)
    SELECT NEWID(), GETDATE(), 'Actualización en Especialidad: ' + d.ID_Especialidad + ' Cambios aplicados.', HOST_NAME(), dbo.fn_UsuarioActual()
    FROM deleted d;
END;
GO

CREATE OR ALTER TRIGGER tr_AuditarEliminacionEspecialidad
ON Especialidad
AFTER DELETE
AS
BEGIN
    SET NOCOUNT ON;
    INSERT INTO Auditoria (ID_Auditoria, Fecha_Hora_Accion, Acción, DispositivoQueRealizo, Usuario)
    SELECT NEWID(), GETDATE(), 'Eliminación en Especialidad: ' + d.ID_Especialidad, HOST_NAME(), dbo.fn_UsuarioActual()
    FROM deleted d;
END;
GO

-- Trigger de auditoría para Dentista_Especialidad
CREATE OR ALTER TRIGGER tr_AuditarInsercionDentistaEspecialidad
ON Dentista_Especialidad
AFTER INSERT
AS
BEGIN
    SET NOCOUNT ON;
    INSERT INTO Auditoria (ID_Auditoria, Fecha_Hora_Accion, Acción, DispositivoQueRealizo, Usuario)
    SELECT NEWID(), GETDATE(), 'Inserción en Dentista_Especialidad: ' + i.ID_Dentista_Especialidad, HOST_NAME(), dbo.fn_UsuarioActual()
    FROM inserted i;
END;
GO

CREATE OR ALTER TRIGGER tr_AuditarActualizacionDentistaEspecialidad
ON Dentista_Especialidad
AFTER UPDATE
AS
BEGIN
    SET NOCOUNT ON;
    INSERT INTO Auditoria (ID_Auditoria, Fecha_Hora_Accion, Acción, DispositivoQueRealizo, Usuario)
    SELECT NEWID(), GETDATE(), 'Actualización en Dentista_Especialidad: ' + d.ID_Dentista_Especialidad + ' Cambios aplicados.', HOST_NAME(), dbo.fn_UsuarioActual()
    FROM deleted d;
END;
GO

CREATE OR ALTER TRIGGER tr_AuditarEliminacionDentistaEspecialidad
ON Dentista_Especialidad
AFTER DELETE
AS
BEGIN
    SET NOCOUNT ON;
    INSERT INTO Auditoria (ID_Auditoria, Fecha_Hora_Accion, Acción, DispositivoQueRealizo, Usuario)
    SELECT NEWID(), GETDATE(), 'Eliminación en Dentista_Especialidad: ' + d.ID_Dentista_Especialidad, HOST_NAME(), dbo.fn_UsuarioActual()
    FROM deleted d;
END;
GO

-- Trigger de auditoría para Estado_Citas
CREATE OR ALTER TRIGGER tr_AuditarInsercionEstadoCitas
ON Estado_Citas
AFTER INSERT
AS
BEGIN
    SET NOCOUNT ON;
    INSERT INTO Auditoria (ID_Auditoria, Fecha_Hora_Accion, Acción, DispositivoQueRealizo, Usuario)
    SELECT NEWID(), GETDATE(), 'Inserción en Estado_Citas: ' + i.ID_EstadoCita, HOST_NAME(), dbo.fn_UsuarioActual()
    FROM inserted i;
END;
GO

CREATE OR ALTER TRIGGER tr_AuditarActualizacionEstadoCitas
ON Estado_Citas
AFTER UPDATE
AS
BEGIN
    SET NOCOUNT ON;
    INSERT INTO Auditoria (ID_Auditoria, Fecha_Hora_Accion, Acción, DispositivoQueRealizo, Usuario)
    SELECT NEWID(), GETDATE(), 'Actualización en Estado_Citas: ' + d.ID_EstadoCita + ' Cambios aplicados.', HOST_NAME(), dbo.fn_UsuarioActual()
    FROM deleted d;
END;
GO

CREATE OR ALTER TRIGGER tr_AuditarEliminacionEstadoCitas
ON Estado_Citas
AFTER DELETE
AS
BEGIN
    SET NOCOUNT ON;
    INSERT INTO Auditoria (ID_Auditoria, Fecha_Hora_Accion, Acción, DispositivoQueRealizo, Usuario)
    SELECT NEWID(), GETDATE(), 'Eliminación en Estado_Citas: ' + d.ID_EstadoCita, HOST_NAME(), dbo.fn_UsuarioActual()
    FROM deleted d;
END;
GO

-- Trigger de auditoría para Cita
CREATE OR ALTER TRIGGER tr_AuditarInsercionCita
ON Cita
AFTER INSERT
AS
BEGIN
    SET NOCOUNT ON;
    INSERT INTO Auditoria (ID_Auditoria, Fecha_Hora_Accion, Acción, DispositivoQueRealizo, Usuario)
    SELECT NEWID(), GETDATE(), 'Inserción en Cita: ' + i.ID_Cita, HOST_NAME(), dbo.fn_UsuarioActual()
    FROM inserted i;
END;
GO

CREATE OR ALTER TRIGGER tr_AuditarActualizacionCita
ON Cita
AFTER UPDATE
AS
BEGIN
    SET NOCOUNT ON;
    INSERT INTO Auditoria (ID_Auditoria, Fecha_Hora_Accion, Acción, DispositivoQueRealizo, Usuario)
    SELECT NEWID(), GETDATE(), 'Actualización en Cita: ' + d.ID_Cita + ' Cambios aplicados.', HOST_NAME(), dbo.fn_UsuarioActual()
    FROM deleted d;
END;
GO

CREATE OR ALTER TRIGGER tr_AuditarEliminacionCita
ON Cita
AFTER DELETE
AS
BEGIN
    SET NOCOUNT ON;
    INSERT INTO Auditoria (ID_Auditoria, Fecha_Hora_Accion, Acción, DispositivoQueRealizo, Usuario)
    SELECT NEWID(), GETDATE(), 'Eliminación en Cita: ' + d.ID_Cita, HOST_NAME(), dbo.fn_UsuarioActual()
    FROM deleted d;
END;
GO



USE ClinicaDental;
GO

-- Auditoría de inserción, actualización y eliminación para la tabla Estado_Pago
CREATE OR ALTER TRIGGER tr_AuditarInsercionEstadoPago
ON Estado_Pago
AFTER INSERT
AS
BEGIN
    SET NOCOUNT ON;
    INSERT INTO Auditoria (ID_Auditoria, Fecha_Hora_Accion, Acción, DispositivoQueRealizo, Usuario)
    SELECT NEWID(), GETDATE(), 'Inserción en Estado_Pago: ' + i.ID_EstadoPago, HOST_NAME(), dbo.fn_UsuarioActual()
    FROM inserted i;
END;
GO

CREATE OR ALTER TRIGGER tr_AuditarActualizacionEstadoPago
ON Estado_Pago
AFTER UPDATE
AS
BEGIN
    SET NOCOUNT ON;
    INSERT INTO Auditoria (ID_Auditoria, Fecha_Hora_Accion, Acción, DispositivoQueRealizo, Usuario)
    SELECT NEWID(), GETDATE(), 'Actualización en Estado_Pago: ' + d.ID_EstadoPago + ' Cambios aplicados.', HOST_NAME(), dbo.fn_UsuarioActual()
    FROM deleted d;
END;
GO

CREATE OR ALTER TRIGGER tr_AuditarEliminacionEstadoPago
ON Estado_Pago
AFTER DELETE
AS
BEGIN
    SET NOCOUNT ON;
    INSERT INTO Auditoria (ID_Auditoria, Fecha_Hora_Accion, Acción, DispositivoQueRealizo, Usuario)
    SELECT NEWID(), GETDATE(), 'Eliminación en Estado_Pago: ' + d.ID_EstadoPago, HOST_NAME(), dbo.fn_UsuarioActual()
    FROM deleted d;
END;
GO

-- Auditoría de inserción, actualización y eliminación para la tabla Tipo_Tratamiento
CREATE OR ALTER TRIGGER tr_AuditarInsercionTipoTratamiento
ON Tipo_Tratamiento
AFTER INSERT
AS
BEGIN
    SET NOCOUNT ON;
    INSERT INTO Auditoria (ID_Auditoria, Fecha_Hora_Accion, Acción, DispositivoQueRealizo, Usuario)
    SELECT NEWID(), GETDATE(), 'Inserción en Tipo_Tratamiento: ' + i.ID_TipoTratamiento, HOST_NAME(), dbo.fn_UsuarioActual()
    FROM inserted i;
END;
GO

CREATE OR ALTER TRIGGER tr_AuditarActualizacionTipoTratamiento
ON Tipo_Tratamiento
AFTER UPDATE
AS
BEGIN
    SET NOCOUNT ON;
    INSERT INTO Auditoria (ID_Auditoria, Fecha_Hora_Accion, Acción, DispositivoQueRealizo, Usuario)
    SELECT NEWID(), GETDATE(), 'Actualización en Tipo_Tratamiento: ' + d.ID_TipoTratamiento + ' Cambios aplicados.', HOST_NAME(), dbo.fn_UsuarioActual()
    FROM deleted d;
END;
GO

CREATE OR ALTER TRIGGER tr_AuditarEliminacionTipoTratamiento
ON Tipo_Tratamiento
AFTER DELETE
AS
BEGIN
    SET NOCOUNT ON;
    INSERT INTO Auditoria (ID_Auditoria, Fecha_Hora_Accion, Acción, DispositivoQueRealizo, Usuario)
    SELECT NEWID(), GETDATE(), 'Eliminación en Tipo_Tratamiento: ' + d.ID_TipoTratamiento, HOST_NAME(), dbo.fn_UsuarioActual()
    FROM deleted d;
END;
GO

-- Auditoría de inserción, actualización y eliminación para la tabla Tratamiento
CREATE OR ALTER TRIGGER tr_AuditarInsercionTratamiento
ON Tratamiento
AFTER INSERT
AS
BEGIN
    SET NOCOUNT ON;
    INSERT INTO Auditoria (ID_Auditoria, Fecha_Hora_Accion, Acción, DispositivoQueRealizo, Usuario)
    SELECT NEWID(), GETDATE(), 'Inserción en Tratamiento: ' + i.ID_Tratamiento, HOST_NAME(), dbo.fn_UsuarioActual()
    FROM inserted i;
END;
GO

CREATE OR ALTER TRIGGER tr_AuditarActualizacionTratamiento
ON Tratamiento
AFTER UPDATE
AS
BEGIN
    SET NOCOUNT ON;
    INSERT INTO Auditoria (ID_Auditoria, Fecha_Hora_Accion, Acción, DispositivoQueRealizo, Usuario)
    SELECT NEWID(), GETDATE(), 'Actualización en Tratamiento: ' + d.ID_Tratamiento + ' Cambios aplicados.', HOST_NAME(), dbo.fn_UsuarioActual()
    FROM deleted d;
END;
GO

CREATE OR ALTER TRIGGER tr_AuditarEliminacionTratamiento
ON Tratamiento
AFTER DELETE
AS
BEGIN
    SET NOCOUNT ON;
    INSERT INTO Auditoria (ID_Auditoria, Fecha_Hora_Accion, Acción, DispositivoQueRealizo, Usuario)
    SELECT NEWID(), GETDATE(), 'Eliminación en Tratamiento: ' + d.ID_Tratamiento, HOST_NAME(), dbo.fn_UsuarioActual()
    FROM deleted d;
END;
GO

-- Auditoría de inserción, actualización y eliminación para la tabla Procedimiento
CREATE OR ALTER TRIGGER tr_AuditarInsercionProcedimiento
ON Procedimiento
AFTER INSERT
AS
BEGIN
    SET NOCOUNT ON;
    INSERT INTO Auditoria (ID_Auditoria, Fecha_Hora_Accion, Acción, DispositivoQueRealizo, Usuario)
    SELECT NEWID(), GETDATE(), 'Inserción en Procedimiento: ' + i.ID_Procedimiento, HOST_NAME(), dbo.fn_UsuarioActual()
    FROM inserted i;
END;
GO

CREATE OR ALTER TRIGGER tr_AuditarActualizacionProcedimiento
ON Procedimiento
AFTER UPDATE
AS
BEGIN
    SET NOCOUNT ON;
    INSERT INTO Auditoria (ID_Auditoria, Fecha_Hora_Accion, Acción, DispositivoQueRealizo, Usuario)
    SELECT NEWID(), GETDATE(), 'Actualización en Procedimiento: ' + d.ID_Procedimiento + ' Cambios aplicados.', HOST_NAME(), dbo.fn_UsuarioActual()
    FROM deleted d;
END;
GO

CREATE OR ALTER TRIGGER tr_AuditarEliminacionProcedimiento
ON Procedimiento
AFTER DELETE
AS
BEGIN
    SET NOCOUNT ON;
    INSERT INTO Auditoria (ID_Auditoria, Fecha_Hora_Accion, Acción, DispositivoQueRealizo, Usuario)
    SELECT NEWID(), GETDATE(), 'Eliminación en Procedimiento: ' + d.ID_Procedimiento, HOST_NAME(), dbo.fn_UsuarioActual()
    FROM deleted d;
END;
GO

-- Auditoría de inserción, actualización y eliminación para la tabla Factura
CREATE OR ALTER TRIGGER tr_AuditarInsercionFactura
ON Factura
AFTER INSERT
AS
BEGIN
    SET NOCOUNT ON;
    INSERT INTO Auditoria (ID_Auditoria, Fecha_Hora_Accion, Acción, DispositivoQueRealizo, Usuario)
    SELECT NEWID(), GETDATE(), 'Inserción en Factura: ' + i.ID_Factura, HOST_NAME(), dbo.fn_UsuarioActual()
    FROM inserted i;
END;
GO

CREATE OR ALTER TRIGGER tr_AuditarActualizacionFactura
ON Factura
AFTER UPDATE
AS
BEGIN
    SET NOCOUNT ON;
    INSERT INTO Auditoria (ID_Auditoria, Fecha_Hora_Accion, Acción, DispositivoQueRealizo, Usuario)
    SELECT NEWID(), GETDATE(), 'Actualización en Factura: ' + d.ID_Factura + ' Cambios aplicados.', HOST_NAME(), dbo.fn_UsuarioActual()
    FROM deleted d;
END;
GO

CREATE OR ALTER TRIGGER tr_AuditarEliminacionFactura
ON Factura
AFTER DELETE
AS
BEGIN
    SET NOCOUNT ON;
    INSERT INTO Auditoria (ID_Auditoria, Fecha_Hora_Accion, Acción, DispositivoQueRealizo, Usuario)
    SELECT NEWID(), GETDATE(), 'Eliminación en Factura: ' + d.ID_Factura, HOST_NAME(), dbo.fn_UsuarioActual()
    FROM deleted d;
END;
GO

-- Trigger de auditoría para Funcionario
CREATE OR ALTER TRIGGER tr_AuditarInsercionFuncionario
ON Funcionario
AFTER INSERT
AS
BEGIN
    SET NOCOUNT ON;
    INSERT INTO Auditoria (ID_Auditoria, Fecha_Hora_Accion, Acción, DispositivoQueRealizo, Usuario)
    SELECT NEWID(), GETDATE(), 'Inserción en Funcionario: ' + i.ID_Funcionario, HOST_NAME(), dbo.fn_UsuarioActual()
    FROM inserted i;
END;
GO

CREATE OR ALTER TRIGGER tr_AuditarActualizacionFuncionario
ON Funcionario
AFTER UPDATE
AS
BEGIN
    SET NOCOUNT ON;
    INSERT INTO Auditoria (ID_Auditoria, Fecha_Hora_Accion, Acción, DispositivoQueRealizo, Usuario)
    SELECT NEWID(), GETDATE(), 'Actualización en Funcionario: ' + d.ID_Funcionario + ' Cambios aplicados.', HOST_NAME(), dbo.fn_UsuarioActual()
    FROM deleted d;
END;
GO

CREATE OR ALTER TRIGGER tr_AuditarEliminacionFuncionario
ON Funcionario
AFTER DELETE
AS
BEGIN
    SET NOCOUNT ON;
    INSERT INTO Auditoria (ID_Auditoria, Fecha_Hora_Accion, Acción, DispositivoQueRealizo, Usuario)
    SELECT NEWID(), GETDATE(), 'Eliminación en Funcionario: ' + d.ID_Funcionario, HOST_NAME(), dbo.fn_UsuarioActual()
    FROM deleted d;
END;
GO

-- Trigger de auditoría para Usuarios
CREATE OR ALTER TRIGGER tr_AuditarInsercionUsuarios
ON Usuarios
AFTER INSERT
AS
BEGIN
    SET NOCOUNT ON;
    INSERT INTO Auditoria (ID_Auditoria, Fecha_Hora_Accion, Acción, DispositivoQueRealizo, Usuario)
    SELECT NEWID(), GETDATE(), 'Inserción en Usuarios: ' + i.ID_Usuario, HOST_NAME(), dbo.fn_UsuarioActual()
    FROM inserted i;
END;
GO

CREATE OR ALTER TRIGGER tr_AuditarActualizacionUsuarios
ON Usuarios
AFTER UPDATE
AS
BEGIN
    SET NOCOUNT ON;
    INSERT INTO Auditoria (ID_Auditoria, Fecha_Hora_Accion, Acción, DispositivoQueRealizo, Usuario)
    SELECT NEWID(), GETDATE(), 'Actualización en Usuarios: ' + d.ID_Usuario + ' Cambios aplicados.', HOST_NAME(), dbo.fn_UsuarioActual()
    FROM deleted d;
END;
GO

CREATE OR ALTER TRIGGER tr_AuditarEliminacionUsuarios
ON Usuarios
AFTER DELETE
AS
BEGIN
    SET NOCOUNT ON;
    INSERT INTO Auditoria (ID_Auditoria, Fecha_Hora_Accion, Acción, DispositivoQueRealizo, Usuario)
    SELECT NEWID(), GETDATE(), 'Eliminación en Usuarios: ' + d.ID_Usuario, HOST_NAME(), dbo.fn_UsuarioActual()
    FROM deleted d;
END;
GO

-- Trigger de auditoría para Dentista
CREATE OR ALTER TRIGGER tr_AuditarInsercionDentista
ON Dentista
AFTER INSERT
AS
BEGIN
    SET NOCOUNT ON;
    INSERT INTO Auditoria (ID_Auditoria, Fecha_Hora_Accion, Acción, DispositivoQueRealizo, Usuario)
    SELECT NEWID(), GETDATE(), 'Inserción en Dentista: ' + i.ID_Dentista, HOST_NAME(), dbo.fn_UsuarioActual()
    FROM inserted i;
END;
GO

CREATE OR ALTER TRIGGER tr_AuditarActualizacionDentista
ON Dentista
AFTER UPDATE
AS
BEGIN
    SET NOCOUNT ON;
    INSERT INTO Auditoria (ID_Auditoria, Fecha_Hora_Accion, Acción, DispositivoQueRealizo, Usuario)
    SELECT NEWID(), GETDATE(), 'Actualización en Dentista: ' + d.ID_Dentista + ' Cambios aplicados.', HOST_NAME(), dbo.fn_UsuarioActual()
    FROM deleted d;
END;
GO

CREATE OR ALTER TRIGGER tr_AuditarEliminacionDentista
ON Dentista
AFTER DELETE
AS
BEGIN
    SET NOCOUNT ON;
    INSERT INTO Auditoria (ID_Auditoria, Fecha_Hora_Accion, Acción, DispositivoQueRealizo, Usuario)
    SELECT NEWID(), GETDATE(), 'Eliminación en Dentista: ' + d.ID_Dentista, HOST_NAME(), dbo.fn_UsuarioActual()
    FROM deleted d;
END;
GO

-- Trigger de auditoría para Especialidad
CREATE OR ALTER TRIGGER tr_AuditarInsercionEspecialidad
ON Especialidad
AFTER INSERT
AS
BEGIN
    SET NOCOUNT ON;
    INSERT INTO Auditoria (ID_Auditoria, Fecha_Hora_Accion, Acción, DispositivoQueRealizo, Usuario)
    SELECT NEWID(), GETDATE(), 'Inserción en Especialidad: ' + i.ID_Especialidad, HOST_NAME(), dbo.fn_UsuarioActual()
    FROM inserted i;
END;
GO

CREATE OR ALTER TRIGGER tr_AuditarActualizacionEspecialidad
ON Especialidad
AFTER UPDATE
AS
BEGIN
    SET NOCOUNT ON;
    INSERT INTO Auditoria (ID_Auditoria, Fecha_Hora_Accion, Acción, DispositivoQueRealizo, Usuario)
    SELECT NEWID(), GETDATE(), 'Actualización en Especialidad: ' + d.ID_Especialidad + ' Cambios aplicados.', HOST_NAME(), dbo.fn_UsuarioActual()
    FROM deleted d;
END;
GO

CREATE OR ALTER TRIGGER tr_AuditarEliminacionEspecialidad
ON Especialidad
AFTER DELETE
AS
BEGIN
    SET NOCOUNT ON;
    INSERT INTO Auditoria (ID_Auditoria, Fecha_Hora_Accion, Acción, DispositivoQueRealizo, Usuario)
    SELECT NEWID(), GETDATE(), 'Eliminación en Especialidad: ' + d.ID_Especialidad, HOST_NAME(), dbo.fn_UsuarioActual()
    FROM deleted d;
END;
GO

-- Trigger de auditoría para Dentista_Especialidad
CREATE OR ALTER TRIGGER tr_AuditarInsercionDentistaEspecialidad
ON Dentista_Especialidad
AFTER INSERT
AS
BEGIN
    SET NOCOUNT ON;
    INSERT INTO Auditoria (ID_Auditoria, Fecha_Hora_Accion, Acción, DispositivoQueRealizo, Usuario)
    SELECT NEWID(), GETDATE(), 'Inserción en Dentista_Especialidad: ' + i.ID_Dentista_Especialidad, HOST_NAME(), dbo.fn_UsuarioActual()
    FROM inserted i;
END;
GO

CREATE OR ALTER TRIGGER tr_AuditarActualizacionDentistaEspecialidad
ON Dentista_Especialidad
AFTER UPDATE
AS
BEGIN
    SET NOCOUNT ON;
    INSERT INTO Auditoria (ID_Auditoria, Fecha_Hora_Accion, Acción, DispositivoQueRealizo, Usuario)
    SELECT NEWID(), GETDATE(), 'Actualización en Dentista_Especialidad: ' + d.ID_Dentista_Especialidad + ' Cambios aplicados.', HOST_NAME(), dbo.fn_UsuarioActual()
    FROM deleted d;
END;
GO

CREATE OR ALTER TRIGGER tr_AuditarEliminacionDentistaEspecialidad
ON Dentista_Especialidad
AFTER DELETE
AS
BEGIN
    SET NOCOUNT ON;
    INSERT INTO Auditoria (ID_Auditoria, Fecha_Hora_Accion, Acción, DispositivoQueRealizo, Usuario)
    SELECT NEWID(), GETDATE(), 'Eliminación en Dentista_Especialidad: ' + d.ID_Dentista_Especialidad, HOST_NAME(), dbo.fn_UsuarioActual()
    FROM deleted d;
END;
GO

-- Trigger de auditoría para Estado_Citas
CREATE OR ALTER TRIGGER tr_AuditarInsercionEstadoCitas
ON Estado_Citas
AFTER INSERT
AS
BEGIN
    SET NOCOUNT ON;
    INSERT INTO Auditoria (ID_Auditoria, Fecha_Hora_Accion, Acción, DispositivoQueRealizo, Usuario)
    SELECT NEWID(), GETDATE(), 'Inserción en Estado_Citas: ' + i.ID_EstadoCita, HOST_NAME(), dbo.fn_UsuarioActual()
    FROM inserted i;
END;
GO

CREATE OR ALTER TRIGGER tr_AuditarActualizacionEstadoCitas
ON Estado_Citas
AFTER UPDATE
AS
BEGIN
    SET NOCOUNT ON;
    INSERT INTO Auditoria (ID_Auditoria, Fecha_Hora_Accion, Acción, DispositivoQueRealizo, Usuario)
    SELECT NEWID(), GETDATE(), 'Actualización en Estado_Citas: ' + d.ID_EstadoCita + ' Cambios aplicados.', HOST_NAME(), dbo.fn_UsuarioActual()
    FROM deleted d;
END;
GO

CREATE OR ALTER TRIGGER tr_AuditarEliminacionEstadoCitas
ON Estado_Citas
AFTER DELETE
AS
BEGIN
    SET NOCOUNT ON;
    INSERT INTO Auditoria (ID_Auditoria, Fecha_Hora_Accion, Acción, DispositivoQueRealizo, Usuario)
    SELECT NEWID(), GETDATE(), 'Eliminación en Estado_Citas: ' + d.ID_EstadoCita, HOST_NAME(), dbo.fn_UsuarioActual()
    FROM deleted d;
END;
GO

-- Trigger de auditoría para Cita
CREATE OR ALTER TRIGGER tr_AuditarInsercionCita
ON Cita
AFTER INSERT
AS
BEGIN
    SET NOCOUNT ON;
    INSERT INTO Auditoria (ID_Auditoria, Fecha_Hora_Accion, Acción, DispositivoQueRealizo, Usuario)
    SELECT NEWID(), GETDATE(), 'Inserción en Cita: ' + i.ID_Cita, HOST_NAME(), dbo.fn_UsuarioActual()
    FROM inserted i;
END;
GO

CREATE OR ALTER TRIGGER tr_AuditarActualizacionCita
ON Cita
AFTER UPDATE
AS
BEGIN
    SET NOCOUNT ON;
    INSERT INTO Auditoria (ID_Auditoria, Fecha_Hora_Accion, Acción, DispositivoQueRealizo, Usuario)
    SELECT NEWID(), GETDATE(), 'Actualización en Cita: ' + d.ID_Cita + ' Cambios aplicados.', HOST_NAME(), dbo.fn_UsuarioActual()
    FROM deleted d;
END;
GO

CREATE OR ALTER TRIGGER tr_AuditarEliminacionCita
ON Cita
AFTER DELETE
AS
BEGIN
    SET NOCOUNT ON;
    INSERT INTO Auditoria (ID_Auditoria, Fecha_Hora_Accion, Acción, DispositivoQueRealizo, Usuario)
    SELECT NEWID(), GETDATE(), 'Eliminación en Cita: ' + d.ID_Cita, HOST_NAME(), dbo.fn_UsuarioActual()
    FROM deleted d;
END;
GO
