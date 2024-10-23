-- Trigger: tr_AuditarInsercionPaciente
-- Prop�sito: Auditar la inserci�n de nuevos pacientes en la tabla Paciente y registrar la acci�n en Auditoria.

CREATE TRIGGER tr_AuditarInsercionPaciente
ON Paciente
AFTER INSERT
AS
BEGIN
    DECLARE @ID_Paciente CHAR(8), @Nombre VARCHAR(20), @Apellido1 VARCHAR(20), @Apellido2 VARCHAR(20), @Fecha_Hora DATETIME, @ID_Usuario CHAR(8);

    -- Obtener los datos del paciente insertado
    SELECT @ID_Paciente = i.ID_Paciente, @Nombre = i.Nombre_Pac, @Apellido1 = i.Apellido1_Pac, @Apellido2 = i.Apellido2_Pac, @Fecha_Hora = GETDATE(), @ID_Usuario = 'ID_ADMIN' -- O cualquier l�gica para obtener el ID del usuario
    FROM inserted i;

    -- Insertar el registro en la tabla Auditoria
    INSERT INTO Auditoria (ID_Auditoria, Fecha_Hora_Accion, Descripcion_Accion, ID_TipoAccion, ID_Usuario)
    VALUES (NEWID(), @Fecha_Hora, 'Inserci�n de paciente: ' + @Nombre + ' ' + @Apellido1 + ' ' + @Apellido2, 'INSERCION', @ID_Usuario);
END;
GO



-- Trigger: tr_AuditarActualizacionPaciente
-- Prop�sito: Auditar las actualizaciones de datos de pacientes, comparando valores antiguos y nuevos, y registrar los cambios en Auditoria.

CREATE TRIGGER tr_AuditarActualizacionPaciente
ON Paciente
AFTER UPDATE
AS
BEGIN
    DECLARE @ID_Paciente CHAR(8), @NombreAntiguo VARCHAR(20), @NombreNuevo VARCHAR(20), @Fecha_Hora DATETIME, @ID_Usuario CHAR(8);

    -- Obtener el ID del paciente y el nombre antiguo y nuevo
    SELECT @ID_Paciente = i.ID_Paciente, @NombreAntiguo = d.Nombre_Pac, @NombreNuevo = i.Nombre_Pac, @Fecha_Hora = GETDATE(), @ID_Usuario = 'ID_ADMIN'
    FROM inserted i
    JOIN deleted d ON i.ID_Paciente = d.ID_Paciente;

    -- Comparar valores y auditar solo si el nombre ha cambiado
    IF @NombreAntiguo <> @NombreNuevo
    BEGIN
        INSERT INTO Auditoria (ID_Auditoria, Fecha_Hora_Accion, Descripcion_Accion, ID_TipoAccion, ID_Usuario)
        VALUES (NEWID(), @Fecha_Hora, 'Actualizaci�n de paciente: Cambio de Nombre de ' + @NombreAntiguo + ' a ' + @NombreNuevo, 'ACTUALIZACION', @ID_Usuario);
    END;
END;
GO



-- Trigger: tr_AuditarEliminacionPaciente
-- Prop�sito: Auditar la eliminaci�n de pacientes y registrar la acci�n en Auditoria.

CREATE TRIGGER tr_AuditarEliminacionPaciente
ON Paciente
AFTER DELETE
AS
BEGIN
    DECLARE @ID_Paciente CHAR(8), @Nombre VARCHAR(20), @Apellido1 VARCHAR(20), @Fecha_Hora DATETIME, @ID_Usuario CHAR(8);

    -- Obtener los datos del paciente eliminado
    SELECT @ID_Paciente = d.ID_Paciente, @Nombre = d.Nombre_Pac, @Apellido1 = d.Apellido1_Pac, @Fecha_Hora = GETDATE(), @ID_Usuario = 'ID_ADMIN'
    FROM deleted d;

    -- Insertar registro en la tabla Auditoria
    INSERT INTO Auditoria (ID_Auditoria, Fecha_Hora_Accion, Descripcion_Accion, ID_TipoAccion, ID_Usuario)
    VALUES (NEWID(), @Fecha_Hora, 'Eliminaci�n de paciente: ' + @Nombre + ' ' + @Apellido1, 'ELIMINACION', @ID_Usuario);
END;
GO



-- Trigger: tr_EvitarDuplicadosCita
-- Prop�sito: Evitar que se inserten citas duplicadas para el mismo dentista en la misma fecha y hora.

CREATE TRIGGER tr_EvitarDuplicadosCita
ON Cita
INSTEAD OF INSERT
AS
BEGIN
    DECLARE @ID_Dentista CHAR(8), @Fecha_Cita DATE, @Hora_Inicio TIME;

    -- Obtener los datos de la cita nueva
    SELECT @ID_Dentista = i.ID_Dentista, @Fecha_Cita = i.Fecha_Cita, @Hora_Inicio = i.Hora_Inicio
    FROM inserted i;

    -- Verificar si ya existe una cita en el mismo horario para el dentista
    IF EXISTS (
        SELECT 1
        FROM Cita
        WHERE ID_Dentista = @ID_Dentista
        AND Fecha_Cita = @Fecha_Cita
        AND Hora_Inicio = @Hora_Inicio
    )
    BEGIN
        -- Si existe, lanzamos un error
        RAISERROR('Ya existe una cita programada para el dentista en esta fecha y hora.', 16, 1);
        ROLLBACK TRANSACTION;
    END
    ELSE
    BEGIN
        -- Insertar la nueva cita
        INSERT INTO Cita (ID_Cita, Fecha_Cita, Motivo, Hora_Inicio, ID_Paciente, ID_Dentista, ID_Funcionario, ID_EstadoCita)
        SELECT ID_Cita, Fecha_Cita, Motivo, Hora_Inicio, ID_Paciente, ID_Dentista, ID_Funcionario, ID_EstadoCita
        FROM inserted;
    END;
END;
GO




-- Trigger: tr_EvitarFacturaConMontoCero
-- Prop�sito: Evitar que se inserten facturas con un monto total de cero o negativo.

CREATE TRIGGER tr_EvitarFacturaConMontoCero
ON Factura
AFTER INSERT
AS
BEGIN
    DECLARE @MontoTotal MONEY, @ID_Factura CHAR(8);

    -- Obtener el monto total de la factura reci�n insertada
    SELECT @MontoTotal = i.MontoTotal_Fa, @ID_Factura = i.ID_Factura
    FROM inserted i;

    -- Si el monto es cero o negativo, eliminar la factura y lanzar un error
    IF @MontoTotal <= 0
    BEGIN
        -- Eliminar la factura insertada
        DELETE FROM Factura WHERE ID_Factura = @ID_Factura;

        -- Lanzar un error para notificar del problema
        RAISERROR('No se permite la inserci�n de facturas con monto cero o negativo.', 16, 1);
    END;
END;
GO



-- Trigger: tr_ActualizarEstadoFactura
-- Prop�sito: Actualizar el estado de la factura a "Pagada" cuando el monto pagado sea igual o mayor al monto total de la factura.

CREATE TRIGGER tr_ActualizarEstadoFactura
ON Pago
AFTER INSERT
AS
BEGIN
    DECLARE @ID_Factura CHAR(8), @MontoTotal MONEY, @MontoPagado MONEY;

    -- Obtener el ID de la factura pagada
    SELECT @ID_Factura = i.ID_Factura
    FROM inserted i;

    -- Obtener el monto total de la factura y la suma de los pagos realizados
    SELECT @MontoTotal = MontoTotal_Fa
    FROM Factura
    WHERE ID_Factura = @ID_Factura;

    SELECT @MontoPagado = SUM(Monto_Pago)
    FROM Pago
    WHERE ID_Factura = @ID_Factura;

    -- Actualizar el estado de la factura a "Pagada" si se ha cubierto el monto total
    IF @MontoPagado >= @MontoTotal
    BEGIN
        UPDATE Factura
        SET ID_EstadoPago = 'PAGADO'
        WHERE ID_Factura = @ID_Factura;
    END;
END;
GO



-- Trigger: tr_ActualizarMontoFactura
-- Prop�sito: Actualizar el monto total de la factura autom�ticamente cuando se modifiquen los tratamientos asociados.

CREATE TRIGGER tr_ActualizarMontoFactura
ON Factura_Tratamiento
AFTER UPDATE
AS
BEGIN
    DECLARE @ID_Factura CHAR(8), @NuevoMonto MONEY;

    -- Obtener el ID de la factura afectada
    SELECT @ID_Factura = i.ID_Factura
    FROM inserted i;

    -- Calcular el nuevo monto total basado en los tratamientos asociados a la factura
    SELECT @NuevoMonto = SUM(t.Costo)
    FROM Tratamiento t
    JOIN Factura_Tratamiento ft ON t.ID_Tratamiento = ft.ID_Tratamiento
    WHERE ft.ID_Factura = @ID_Factura;

    -- Actualizar el monto total en la factura
    UPDATE Factura
    SET MontoTotal_Fa = @NuevoMonto
    WHERE ID_Factura = @ID_Factura;
END;
GO

-- Trigger: tr_ActualizarFechaUltimaActualizacionCuenta
-- Prop�sito: Registrar la fecha y hora de la �ltima actualizaci�n en la cuenta del paciente.

CREATE TRIGGER tr_ActualizarFechaUltimaActualizacionCuenta
ON Cuenta
AFTER UPDATE
AS
BEGIN
    DECLARE @ID_Cuenta CHAR(8);

    -- Obtener el ID de la cuenta actualizada
    SELECT @ID_Cuenta = i.ID_Cuenta
    FROM inserted i;

    -- Actualizar la fecha de �ltima actualizaci�n a la fecha actual
    UPDATE Cuenta
    SET Fecha_Ultima_Actualizacion = GETDATE()
    WHERE ID_Cuenta = @ID_Cuenta;
END;
GO

-- Trigger: tr_ActualizarEdadPaciente
-- Prop�sito: Actualizar autom�ticamente la edad del paciente basado en su fecha de nacimiento.

CREATE TRIGGER tr_ActualizarEdadPaciente
ON Paciente
AFTER INSERT, UPDATE
AS
BEGIN
    DECLARE @ID_Paciente CHAR(8), @Fecha_Nacimiento DATE, @Edad INT;

    -- Obtener los datos del paciente reci�n insertado o actualizado
    SELECT @ID_Paciente = i.ID_Paciente, @Fecha_Nacimiento = i.Fecha_Nacimiento_Pac
    FROM inserted i;

    -- Calcular la edad en a�os
    SET @Edad = DATEDIFF(YEAR, @Fecha_Nacimiento, GETDATE());

    -- Actualizar la edad del paciente
    UPDATE Paciente
    SET Edad_Pac = @Edad
    WHERE ID_Paciente = @ID_Paciente;
END;
GO



-- Trigger: tr_ValidarCorreoElectronico
-- Prop�sito: Validar que el correo electr�nico del paciente tenga un formato v�lido (contenga "@" y ".").

CREATE TRIGGER tr_ValidarCorreoElectronico
ON Paciente
BEFORE INSERT, UPDATE
AS
BEGIN
    DECLARE @Correo VARCHAR(100);

    -- Obtener el correo electr�nico de la fila insertada o actualizada
    SELECT @Correo = i.Correo_Pac
    FROM inserted i;

    -- Validar que el correo contenga "@" y "."
    IF @Correo NOT LIKE '%@%.%'
    BEGIN
        -- Lanzar un error si el formato del correo es incorrecto
        RAISERROR('El formato del correo electr�nico es inv�lido.', 16, 1);
        ROLLBACK TRANSACTION;
    END;
END;
GO




-- Trigger: tr_EvitarCitasDiasNoLaborales
-- Prop�sito: Evitar que se inserten citas en d�as no laborables (por ejemplo, fines de semana).

CREATE TRIGGER tr_EvitarCitasDiasNoLaborales
ON Cita
AFTER INSERT
AS
BEGIN
    DECLARE @Fecha_Cita DATE;

    -- Obtener la fecha de la cita
    SELECT @Fecha_Cita = i.Fecha_Cita
    FROM inserted i;

    -- Verificar si la fecha es un s�bado o domingo
    IF DATENAME(WEEKDAY, @Fecha_Cita) IN ('Saturday', 'Sunday')
    BEGIN
        -- Si es un d�a no laborable, cancelar la cita
        RAISERROR('No se pueden agendar citas los fines de semana.', 16, 1);
        DELETE FROM Cita WHERE Fecha_Cita = @Fecha_Cita;
    END;
END;
GO





-- Trigger: tr_EvitarPagosNegativos
-- Prop�sito: Evitar la inserci�n de pagos con montos negativos.

CREATE TRIGGER tr_EvitarPagosNegativos
ON Pago
AFTER INSERT
AS
BEGIN
    DECLARE @Monto_Pago MONEY;

    -- Obtener el monto del pago insertado
    SELECT @Monto_Pago = i.Monto_Pago
    FROM inserted i;

    -- Si el monto es negativo, eliminar el pago y lanzar un error
    IF @Monto_Pago < 0
    BEGIN
        RAISERROR('No se pueden registrar pagos con monto negativo.', 16, 1);
        DELETE FROM Pago WHERE Monto_Pago = @Monto_Pago;
    END;
END;
GO



-- Trigger: tr_EvitarPagosDuplicados
-- Prop�sito: Evitar que se inserten pagos duplicados para la misma factura en la misma fecha.

CREATE TRIGGER tr_EvitarPagosDuplicados
ON Pago
AFTER INSERT
AS
BEGIN
    DECLARE @ID_Factura CHAR(8), @Fecha_Pago DATE, @CuentaPagosDuplicados INT;

    -- Obtener la factura y la fecha del pago
    SELECT @ID_Factura = i.ID_Factura, @Fecha_Pago = i.Fecha_Pago
    FROM inserted i;

    -- Contar si ya existe un pago para la misma factura y la misma fecha
    SELECT @CuentaPagosDuplicados = COUNT(*)
    FROM Pago
    WHERE ID_Factura = @ID_Factura AND Fecha_Pago = @Fecha_Pago;

    -- Si ya existe un pago en la misma fecha, eliminar el nuevo pago e informar el error
    IF @CuentaPagosDuplicados > 1
    BEGIN
        RAISERROR('No se pueden registrar pagos duplicados para la misma factura en la misma fecha.', 16, 1);
        DELETE FROM Pago WHERE ID_Factura = @ID_Factura AND Fecha_Pago = @Fecha_Pago;
    END;
END;
GO





-- Trigger: tr_CancelarCitasPasadas
-- Prop�sito: Cambiar el estado de la cita a 'Cancelada' si la fecha de la cita ha pasado y a�n no se ha cambiado el estado.

CREATE TRIGGER tr_CancelarCitasPasadas
ON Cita
AFTER INSERT, UPDATE
AS
BEGIN
    DECLARE @ID_Cita CHAR(8), @Fecha_Cita DATE, @Estado CHAR(8);

    -- Obtener la fecha de la cita y su estado actual
    SELECT @ID_Cita = i.ID_Cita, @Fecha_Cita = i.Fecha_Cita, @Estado = i.ID_EstadoCita
    FROM inserted i;

    -- Si la cita ya pas� y no ha sido actualizada, cambiar el estado a 'Cancelada'
    IF @Fecha_Cita < GETDATE() AND @Estado NOT IN ('CANCELADA', 'COMPLETADA') -- Ajusta los valores seg�n tu tabla Estado_Citas
    BEGIN
        UPDATE Cita
        SET ID_EstadoCita = 'CANCELADA'  -- Suponiendo que 'CANCELADA' es el estado para citas canceladas
        WHERE ID_Cita = @ID_Cita;
    END;
END;
GO


