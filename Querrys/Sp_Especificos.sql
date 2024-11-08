-- Procedimiento almacenado para buscar un paciente por nombre
CREATE PROCEDURE sp_BuscarPacientePorNombre
    @Nombre_Pac VARCHAR(20)
AS
BEGIN
    IF @Nombre_Pac = ''
    BEGIN
        RAISERROR('El nombre del paciente no puede estar vacío', 16, 1);
        RETURN;
    END

    SELECT * FROM Paciente
    WHERE Nombre_Pac LIKE '%' + @Nombre_Pac + '%';
END;
GO

-- Procedimiento almacenado para agendar una cita
CREATE PROCEDURE sp_AgendarCita
    @ID_Cita CHAR(8),
    @Fecha_Cita DATE,
    @Hora_Inicio TIME,
    @Hora_Fin TIME,
    @ID_Paciente CHAR(8),
    @ID_Dentista CHAR(8),
    @ID_Funcionario CHAR(8),
    @ID_EstadoCita CHAR(8)
AS
BEGIN
    BEGIN TRY
        BEGIN TRANSACTION;

        IF @ID_Cita = '' OR @Fecha_Cita IS NULL OR @Hora_Inicio IS NULL OR @Hora_Fin IS NULL OR @ID_Paciente = '' OR @ID_Dentista = '' OR @ID_Funcionario = '' OR @ID_EstadoCita = ''
        BEGIN
            RAISERROR('No se permiten valores nulos o vacíos', 16, 1);
            ROLLBACK TRANSACTION;
            RETURN;
        END

        IF @Hora_Fin <= @Hora_Inicio
        BEGIN
            RAISERROR('La hora de fin no puede ser anterior o igual a la hora de inicio', 16, 1);
            ROLLBACK TRANSACTION;
            RETURN;
        END

        IF @Fecha_Cita < CAST(GETDATE() AS DATE)
        BEGIN
            RAISERROR('No se pueden agendar citas en fechas anteriores a la fecha actual', 16, 1);
            ROLLBACK TRANSACTION;
            RETURN;
        END

        -- Comprobar conflictos de horarios
        IF EXISTS (
            SELECT 1 FROM Cita
            WHERE ID_Dentista = @ID_Dentista
            AND Fecha_Cita = @Fecha_Cita
            AND ((Hora_Inicio <= @Hora_Inicio AND Hora_Fin > @Hora_Inicio) OR (Hora_Inicio < @Hora_Fin AND Hora_Fin >= @Hora_Fin))
        )
        BEGIN
            RAISERROR('Conflicto de horarios para el dentista', 16, 1);
            ROLLBACK TRANSACTION;
            RETURN;
        END

        INSERT INTO Cita (ID_Cita, Fecha_Cita, Hora_Inicio, Hora_Fin, ID_Paciente, ID_Dentista, ID_Funcionario, ID_EstadoCita)
        VALUES (@ID_Cita, @Fecha_Cita, @Hora_Inicio, @Hora_Fin, @ID_Paciente, @ID_Dentista, @ID_Funcionario, @ID_EstadoCita);

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION;
        DECLARE @ErrorMessage NVARCHAR(4000), @ErrorSeverity INT, @ErrorState INT;
        SELECT @ErrorMessage = ERROR_MESSAGE(), @ErrorSeverity = ERROR_SEVERITY(), @ErrorState = ERROR_STATE();
        RAISERROR (@ErrorMessage, @ErrorSeverity, @ErrorState);
    END CATCH
END;
GO

-- Procedimiento almacenado para cancelar una cita
CREATE PROCEDURE sp_CancelarCita
    @ID_Cita CHAR(8)
AS
BEGIN
    IF @ID_Cita = ''
    BEGIN
        RAISERROR('El ID de la cita no puede estar vacío', 16, 1);
        RETURN;
    END

    UPDATE Cita
    SET ID_EstadoCita = 'CANCELADA' 
    WHERE ID_Cita = @ID_Cita; -- Agregar esta línea para especificar la cita a cancelar
END;
GO

-- Procedimiento almacenado para consultar citas por dentista en una fecha determinada
CREATE PROCEDURE sp_ConsultarTratamientosPorPaciente
    @ID_Paciente CHAR(8)
AS
BEGIN
    IF @ID_Paciente = ''
    BEGIN
        RAISERROR('El ID del paciente no puede estar vacío', 16, 1);
        RETURN;
    END

    SELECT t.*
    FROM Tratamiento t
    JOIN Historial_Tratamiento ht ON t.ID_Tratamiento = ht.ID_Tratamiento
    JOIN Historial_Medico hm ON ht.ID_HistorialMedico = hm.ID_HistorialMedico
    JOIN Paciente p ON hm.ID_HistorialMedico = p.ID_HistorialMedico
    WHERE p.ID_Paciente = @ID_Paciente;
END;
GO

-- Procedimiento almacenado para reprogramar una cita
CREATE PROCEDURE sp_ReprogramarCita
    @ID_Cita CHAR(8),
    @Nueva_Fecha_Cita DATE,
    @Nueva_Hora_Inicio TIME,
    @Nueva_Hora_Fin TIME
AS
BEGIN
    BEGIN TRY
        BEGIN TRANSACTION;

        IF @ID_Cita = '' OR @Nueva_Fecha_Cita IS NULL OR @Nueva_Hora_Inicio IS NULL OR @Nueva_Hora_Fin IS NULL
        BEGIN
            RAISERROR('No se permiten valores nulos o vacíos', 16, 1);
            ROLLBACK TRANSACTION;
            RETURN;
        END

        IF @Nueva_Hora_Fin <= @Nueva_Hora_Inicio
        BEGIN
            RAISERROR('La hora de fin no puede ser anterior o igual a la hora de inicio', 16, 1);
            ROLLBACK TRANSACTION;
            RETURN;
        END

        IF @Nueva_Fecha_Cita < CAST(GETDATE() AS DATE)
        BEGIN
            RAISERROR('No se pueden reprogramar citas a fechas anteriores a la fecha actual', 16, 1);
            ROLLBACK TRANSACTION;
            RETURN;
        END

        -- Comprobar conflictos de horarios
        DECLARE @ID_Dentista CHAR(8);
        SELECT @ID_Dentista = ID_Dentista FROM Cita WHERE ID_Cita = @ID_Cita;

        IF EXISTS (
            SELECT 1 FROM Cita
            WHERE ID_Dentista = @ID_Dentista
            AND Fecha_Cita = @Nueva_Fecha_Cita
            AND ((Hora_Inicio <= @Nueva_Hora_Inicio AND Hora_Fin > @Nueva_Hora_Inicio) OR (Hora_Inicio < @Nueva_Hora_Fin AND Hora_Fin >= @Nueva_Hora_Fin))
        )
        BEGIN
            RAISERROR('Conflicto de horarios para el dentista', 16, 1);
            ROLLBACK TRANSACTION;
            RETURN;
        END

        UPDATE Cita
        SET Fecha_Cita = @Nueva_Fecha_Cita,
            Hora_Inicio = @Nueva_Hora_Inicio,
            Hora_Fin = @Nueva_Hora_Fin
        WHERE ID_Cita = @ID_Cita;

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION;
        DECLARE @ErrorMessage NVARCHAR(4000), @ErrorSeverity INT, @ErrorState INT;
        SELECT @ErrorMessage = ERROR_MESSAGE(), @ErrorSeverity = ERROR_SEVERITY(), @ErrorState = ERROR_STATE();
        RAISERROR (@ErrorMessage, @ErrorSeverity, @ErrorState);
    END CATCH
END;
GO

-- Procedimiento almacenado para consultar citas por fecha
CREATE PROCEDURE sp_ConsultarCitasPorFecha
    @Fecha_Cita DATE
AS
BEGIN
    IF @Fecha_Cita IS NULL
    BEGIN
        RAISERROR('La fecha de la cita no puede estar vacía', 16, 1);
        RETURN;
    END

    SELECT * FROM Cita
    WHERE Fecha_Cita = @Fecha_Cita;
END;
GO

CREATE PROCEDURE sp_ActualizarEstadoTratamiento
    @ID_Tratamiento CHAR(8),
    @Nuevo_Estado VARCHAR(20)
AS
BEGIN
    -- Validar que los parámetros no sean nulos o vacíos
    IF @ID_Tratamiento = '' OR @Nuevo_Estado = ''
    BEGIN
        RAISERROR('No se permiten valores nulos o vacíos', 16, 1);
        RETURN;
    END

    DECLARE @ID_EstadoTratamiento CHAR(8);

    -- Obtener el ID_EstadoTratamiento basado en el nombre del estado
    SELECT @ID_EstadoTratamiento = ID_EstadoTratamiento
    FROM Estado_Tratamiento
    WHERE Nombre_Estado = @Nuevo_Estado;

    -- Verificar si el estado existe
    IF @ID_EstadoTratamiento IS NULL
    BEGIN
        RAISERROR('El estado especificado no existe', 16, 1);
        RETURN;
    END

    -- Actualizar el estado del tratamiento
    UPDATE Tratamiento
    SET ID_EstadoTratamiento = @ID_EstadoTratamiento
    WHERE ID_Tratamiento = @ID_Tratamiento;
END;
GO

-- Procedimiento almacenado para generar una factura
CREATE PROCEDURE sp_GenerarFactura
    @ID_Factura CHAR(8),
    @MontoTotal MONEY,
    @FechaEmision DATE,
    @ID_EstadoPago CHAR(8),
    @ID_Cuenta CHAR(8)
AS
BEGIN
    BEGIN TRY
        BEGIN TRANSACTION;

        IF @ID_Factura = '' OR @MontoTotal IS NULL OR @FechaEmision IS NULL OR @ID_EstadoPago = '' OR @ID_Cuenta = ''
        BEGIN
            RAISERROR('No se permiten valores nulos o vacíos', 16, 1);
            ROLLBACK TRANSACTION;
            RETURN;
        END

        INSERT INTO Factura (ID_Factura, MontoTotal_Fa, FechaEmision_Fa, ID_EstadoPago)
        VALUES (@ID_Factura, @MontoTotal, @FechaEmision, @ID_EstadoPago);

        -- Relacionar la factura con la cuenta
        UPDATE Cuenta
        SET ID_Factura = @ID_Factura
        WHERE ID_Cuenta = @ID_Cuenta;

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION;
        DECLARE @ErrorMessage NVARCHAR(4000), @ErrorSeverity INT, @ErrorState INT;
        SELECT @ErrorMessage = ERROR_MESSAGE(), @ErrorSeverity = ERROR_SEVERITY(), @ErrorState = ERROR_STATE();
        RAISERROR (@ErrorMessage, @ErrorSeverity, @ErrorState);
    END CATCH
END;
GO

-- Procedimiento almacenado para registrar un pago
CREATE PROCEDURE sp_RegistrarPago
    @ID_Pago UNIQUEIDENTIFIER,
    @ID_Factura CHAR(8),
    @Monto_Pago MONEY,
    @Fecha_Pago DATE,
    @ID_Tipo_Pago CHAR(8)
AS
BEGIN
    BEGIN TRY
        BEGIN TRANSACTION;

        IF @ID_Pago IS NULL OR @ID_Factura = '' OR @Monto_Pago IS NULL OR @Fecha_Pago IS NULL OR @ID_Tipo_Pago = ''
        BEGIN
            RAISERROR('No se permiten valores nulos o vacíos', 16, 1);
            ROLLBACK TRANSACTION;
            RETURN;
        END

        INSERT INTO Pago (ID_Pago, ID_Factura, Monto_Pago, Fecha_Pago, ID_Tipo_Pago)
        VALUES (@ID_Pago, @ID_Factura, @Monto_Pago, @Fecha_Pago, @ID_Tipo_Pago);

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION;
        DECLARE @ErrorMessage NVARCHAR(4000), @ErrorSeverity INT, @ErrorState INT;
        SELECT @ErrorMessage = ERROR_MESSAGE(), @ErrorSeverity = ERROR_SEVERITY(), @ErrorState = ERROR_STATE();
        RAISERROR (@ErrorMessage, @ErrorSeverity, @ErrorState);
    END CATCH
END;
GO

-- Procedimiento almacenado para actualizar el saldo de la cuenta del paciente
CREATE PROCEDURE sp_ActualizarSaldoCuenta
    @ID_Cuenta CHAR(8)
AS
BEGIN
    DECLARE @NuevoSaldo MONEY;

    -- Calcular el nuevo saldo basado en las facturas y pagos asociados a la cuenta
    SELECT @NuevoSaldo = SUM(f.MontoTotal_Fa) - ISNULL(SUM(p.Monto_Pago), 0)
    FROM Cuenta c
    JOIN Factura f ON c.ID_Factura = f.ID_Factura
    LEFT JOIN Pago p ON f.ID_Factura = p.ID_Factura
    WHERE c.ID_Cuenta = @ID_Cuenta;

    -- Actualizar el saldo en la cuenta
    UPDATE Cuenta
    SET Saldo_Total = @NuevoSaldo
    WHERE ID_Cuenta = @ID_Cuenta;
END;
GO

-- Procedimiento almacenado para actualizar la información de contacto del paciente
CREATE PROCEDURE sp_ActualizarContactoPaciente
    @ID_Paciente CHAR(8),
    @Telefono VARCHAR(20),
    @Correo VARCHAR(30),
    @Direccion VARCHAR(200)
AS
BEGIN
    IF @ID_Paciente = '' OR @Telefono = '' OR @Correo = '' OR @Direccion = ''
    BEGIN
        RAISERROR('No se permiten valores nulos o vacíos', 16, 1);
        RETURN;
    END

    UPDATE Paciente
    SET Telefono_Pac = @Telefono,
        Correo_Pac = @Correo,
        Direccion_Pac = @Direccion
    WHERE ID_Paciente = @ID_Paciente;
END;
GO

-- Procedimiento almacenado para consultar citas por paciente
CREATE PROCEDURE sp_ConsultarCitasPorPaciente
    @ID_Paciente CHAR(8)
AS
BEGIN
    IF @ID_Paciente = ''
    BEGIN
        RAISERROR('El ID del paciente no puede estar vacío', 16, 1);
        RETURN;
    END

    SELECT * FROM Cita
    WHERE ID_Paciente = @ID_Paciente;
END;
GO

-- Procedimiento almacenado para generar reportes de facturación
CREATE PROCEDURE sp_GenerarReporteFacturacion
    @FechaInicio DATE,
    @FechaFin DATE
AS
BEGIN
    IF @FechaInicio IS NULL OR @FechaFin IS NULL
    BEGIN
        RAISERROR('Las fechas de inicio y fin no pueden estar vacías', 16, 1);
        RETURN;
    END

    SELECT f.ID_Factura, f.MontoTotal_Fa, f.FechaEmision_Fa,
           p.Nombre_Pac, p.Apellido1_Pac, p.Apellido2_Pac
    FROM Factura f
    JOIN Cuenta c ON c.ID_Factura = f.ID_Factura
    JOIN Paciente p ON p.ID_Paciente = c.ID_Paciente
    WHERE f.FechaEmision_Fa BETWEEN @FechaInicio AND @FechaFin;
END;
GO