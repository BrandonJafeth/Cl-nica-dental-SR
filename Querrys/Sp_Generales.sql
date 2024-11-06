-- Procedimiento almacenado para insertar un registro en la tabla Paciente
CREATE PROCEDURE InsertPaciente
    @ID_Paciente CHAR(8),
    @Nombre_Pac VARCHAR(20),
    @Apellido1_Pac VARCHAR(20),
    @Apellido2_Pac VARCHAR(20),
    @Fecha_Nacimiento_Pac DATE,
    @Edad_Pac INT,
    @Telefono_Pac VARCHAR(20),
    @Correo_Pac VARCHAR(30),
    @Direccion_Pac VARCHAR(200),
    @ID_HistorialMedico CHAR(8)
AS
BEGIN
    -- Validar que ninguno de los parámetros sea nulo o vacío
    IF @ID_Paciente = '' OR @Nombre_Pac = '' OR @Apellido1_Pac = '' OR @Apellido2_Pac = '' OR @Fecha_Nacimiento_Pac IS NULL OR @Edad_Pac IS NULL OR @Telefono_Pac = '' OR @Correo_Pac = '' OR @Direccion_Pac = '' OR @ID_HistorialMedico = ''
    BEGIN
        -- Lanzar un error si algún parámetro es nulo o vacío
        RAISERROR('No se permiten valores nulos o vacíos', 16, 1);
        RETURN;
    END

    -- Insertar el nuevo registro en la tabla Paciente
    INSERT INTO Paciente (ID_Paciente, Nombre_Pac, Apellido1_Pac, Apellido2_Pac, Fecha_Nacimiento_Pac, Edad_Pac, Telefono_Pac, Correo_Pac, Direccion_Pac, ID_HistorialMedico)
    VALUES (@ID_Paciente, @Nombre_Pac, @Apellido1_Pac, @Apellido2_Pac, @Fecha_Nacimiento_Pac, @Edad_Pac, @Telefono_Pac, @Correo_Pac, @Direccion_Pac, @ID_HistorialMedico);
END;
GO

-- Procedimiento almacenado para actualizar un registro en la tabla Paciente
CREATE PROCEDURE UpdatePaciente
    @ID_Paciente CHAR(8),
    @Nombre_Pac VARCHAR(20),
    @Apellido1_Pac VARCHAR(20),
    @Apellido2_Pac VARCHAR(20),
    @Fecha_Nacimiento_Pac DATE,
    @Edad_Pac INT,
    @Telefono_Pac VARCHAR(20),
    @Correo_Pac VARCHAR(30),
    @Direccion_Pac VARCHAR(200),
    @ID_HistorialMedico CHAR(8)
AS
BEGIN
    -- Validar que ninguno de los parámetros sea nulo o vacío
    IF @ID_Paciente = '' OR @Nombre_Pac = '' OR @Apellido1_Pac = '' OR @Apellido2_Pac = '' OR @Fecha_Nacimiento_Pac IS NULL OR @Edad_Pac IS NULL OR @Telefono_Pac = '' OR @Correo_Pac = '' OR @Direccion_Pac = '' OR @ID_HistorialMedico = ''
    BEGIN
        -- Lanzar un error si algún parámetro es nulo o vacío
        RAISERROR('No se permiten valores nulos o vacíos', 16, 1);
        RETURN;
    END

    -- Actualizar el registro en la tabla Paciente
    UPDATE Paciente
    SET Nombre_Pac = @Nombre_Pac,
        Apellido1_Pac = @Apellido1_Pac,
        Apellido2_Pac = @Apellido2_Pac,
        Fecha_Nacimiento_Pac = @Fecha_Nacimiento_Pac,
        Edad_Pac = @Edad_Pac,
        Telefono_Pac = @Telefono_Pac,
        Correo_Pac = @Correo_Pac,
        Direccion_Pac = @Direccion_Pac,
        ID_HistorialMedico = @ID_HistorialMedico
    WHERE ID_Paciente = @ID_Paciente;
END;
GO

-- Procedimiento almacenado para eliminar un registro de la tabla Paciente
CREATE PROCEDURE DeletePaciente
    @ID_Paciente CHAR(8)
AS
BEGIN
    -- Validar que el parámetro no sea nulo o vacío
    IF @ID_Paciente = ''
    BEGIN
        -- Lanzar un error si el parámetro es nulo o vacío
        RAISERROR('No se permiten valores nulos o vacíos', 16, 1);
        RETURN;
    END

    -- Eliminar el registro de la tabla Paciente
    DELETE FROM Paciente
    WHERE ID_Paciente = @ID_Paciente;
END;
GO

-- Procedimiento almacenado para insertar un registro en la tabla Estado_Pago
CREATE PROCEDURE InsertEstadoPago
    @ID_EstadoPago CHAR(8),
    @Nombre_EP VARCHAR(20),
    @Descripcion_EP VARCHAR(200)
AS
BEGIN
    IF @ID_EstadoPago = '' OR @Nombre_EP = '' OR @Descripcion_EP = ''
    BEGIN
        RAISERROR('No se permiten valores nulos o vacíos', 16, 1);
        RETURN;
    END

    INSERT INTO Estado_Pago (ID_EstadoPago, Nombre_EP, Descripcion_EP)
    VALUES (@ID_EstadoPago, @Nombre_EP, @Descripcion_EP);
END;
GO

-- Procedimiento almacenado para actualizar un registro en la tabla Estado_Pago
CREATE PROCEDURE UpdateEstadoPago
    @ID_EstadoPago CHAR(8),
    @Nombre_EP VARCHAR(20),
    @Descripcion_EP VARCHAR(200)
AS
BEGIN
    IF @ID_EstadoPago = '' OR @Nombre_EP = '' OR @Descripcion_EP = ''
    BEGIN
        RAISERROR('No se permiten valores nulos o vacíos', 16, 1);
        RETURN;
    END

    UPDATE Estado_Pago
    SET Nombre_EP = @Nombre_EP,
        Descripcion_EP = @Descripcion_EP
    WHERE ID_EstadoPago = @ID_EstadoPago;
END;
GO

-- Procedimiento almacenado para eliminar un registro de la tabla Estado_Pago
CREATE PROCEDURE DeleteEstadoPago
    @ID_EstadoPago CHAR(8)
AS
BEGIN
    IF @ID_EstadoPago = ''
    BEGIN
        RAISERROR('No se permiten valores nulos o vacíos', 16, 1);
        RETURN;
    END

    DELETE FROM Estado_Pago
    WHERE ID_EstadoPago = @ID_EstadoPago;
END;
GO

-- Procedimiento almacenado para insertar un registro en la tabla Tipo_Tratamiento
CREATE PROCEDURE InsertTipoTratamiento
    @ID_TipoTratamiento CHAR(8),
    @Nombre_Tipo_Tratamiento VARCHAR(20),
    @Descripcion_Tipo_Tratamiento VARCHAR(200)
AS
BEGIN
    IF @ID_TipoTratamiento = '' OR @Nombre_Tipo_Tratamiento = '' OR @Descripcion_Tipo_Tratamiento = ''
    BEGIN
        RAISERROR('No se permiten valores nulos o vacíos', 16, 1);
        RETURN;
    END

    INSERT INTO Tipo_Tratamiento (ID_TipoTratamiento, Nombre_Tipo_Tratamiento, Descripcion_Tipo_Tratamiento)
    VALUES (@ID_TipoTratamiento, @Nombre_Tipo_Tratamiento, @Descripcion_Tipo_Tratamiento);
END;
GO

-- Procedimiento almacenado para actualizar un registro en la tabla Tipo_Tratamiento
CREATE PROCEDURE UpdateTipoTratamiento
    @ID_TipoTratamiento CHAR(8),
    @Nombre_Tipo_Tratamiento VARCHAR(20),
    @Descripcion_Tipo_Tratamiento VARCHAR(200)
AS
BEGIN
    IF @ID_TipoTratamiento = '' OR @Nombre_Tipo_Tratamiento = '' OR @Descripcion_Tipo_Tratamiento = ''
    BEGIN
        RAISERROR('No se permiten valores nulos o vacíos', 16, 1);
        RETURN;
    END

    UPDATE Tipo_Tratamiento
    SET Nombre_Tipo_Tratamiento = @Nombre_Tipo_Tratamiento,
        Descripcion_Tipo_Tratamiento = @Descripcion_Tipo_Tratamiento
    WHERE ID_TipoTratamiento = @ID_TipoTratamiento;
END;
GO

-- Procedimiento almacenado para eliminar un registro de la tabla Tipo_Tratamiento
CREATE PROCEDURE DeleteTipoTratamiento
    @ID_TipoTratamiento CHAR(8)
AS
BEGIN
    IF @ID_TipoTratamiento = ''
    BEGIN
        RAISERROR('No se permiten valores nulos o vacíos', 16, 1);
        RETURN;
    END

    DELETE FROM Tipo_Tratamiento
    WHERE ID_TipoTratamiento = @ID_TipoTratamiento;
END;
GO

-- Procedimiento almacenado para insertar un registro en la tabla Tratamiento
CREATE PROCEDURE InsertTratamiento
    @ID_Tratamiento CHAR(8),
    @Nombre_Tra VARCHAR(20),
    @Descripcion_Tra VARCHAR(200),
    @ID_TipoTratamiento CHAR(8)
AS
BEGIN
    IF @ID_Tratamiento = '' OR @Nombre_Tra = '' OR @Descripcion_Tra = '' OR @ID_TipoTratamiento = ''
    BEGIN
        RAISERROR('No se permiten valores nulos o vacíos', 16, 1);
        RETURN;
    END

    INSERT INTO Tratamiento (ID_Tratamiento, Nombre_Tra, Descripcion_Tra, ID_TipoTratamiento)
    VALUES (@ID_Tratamiento, @Nombre_Tra, @Descripcion_Tra, @ID_TipoTratamiento);
END;
GO

-- Procedimiento almacenado para actualizar un registro en la tabla Tratamiento
CREATE PROCEDURE UpdateTratamiento
    @ID_Tratamiento CHAR(8),
    @Nombre_Tra VARCHAR(20),
    @Descripcion_Tra VARCHAR(200),
    @ID_TipoTratamiento CHAR(8)
AS
BEGIN
    IF @ID_Tratamiento = '' OR @Nombre_Tra = '' OR @Descripcion_Tra = '' OR @ID_TipoTratamiento = ''
    BEGIN
        RAISERROR('No se permiten valores nulos o vacíos', 16, 1);
        RETURN;
    END

    UPDATE Tratamiento
    SET Nombre_Tra = @Nombre_Tra,
        Descripcion_Tra = @Descripcion_Tra,
        ID_TipoTratamiento = @ID_TipoTratamiento
    WHERE ID_Tratamiento = @ID_Tratamiento;
END;
GO

-- Procedimiento almacenado para eliminar un registro de la tabla Tratamiento
CREATE PROCEDURE DeleteTratamiento
    @ID_Tratamiento CHAR(8)
AS
BEGIN
    IF @ID_Tratamiento = ''
    BEGIN
        RAISERROR('No se permiten valores nulos o vacíos', 16, 1);
        RETURN;
    END

    DELETE FROM Tratamiento
    WHERE ID_Tratamiento = @ID_Tratamiento;
END;
GO

-- Procedimiento almacenado para insertar un registro en la tabla Procedimiento
CREATE PROCEDURE InsertProcedimiento
    @ID_Procedimiento CHAR(8),
    @Fecha_Proc DATE,
    @Detalles_Proc VARCHAR(200),
    @Hora_Inicio_Proc TIME,
    @Hora_Fin_Proc TIME,
    @ID_Tratamiento CHAR(8)
AS
BEGIN
    IF @ID_Procedimiento = '' OR @Fecha_Proc IS NULL OR @Detalles_Proc = '' OR @Hora_Inicio_Proc IS NULL OR @Hora_Fin_Proc IS NULL OR @ID_Tratamiento = ''
    BEGIN
        RAISERROR('No se permiten valores nulos o vacíos', 16, 1);
        RETURN;
    END

    INSERT INTO Procedimiento (ID_Procedimiento, Fecha_Proc, Detalles_Proc, Hora_Inicio_Proc, Hora_Fin_Proc, ID_Tratamiento)
    VALUES (@ID_Procedimiento, @Fecha_Proc, @Detalles_Proc, @Hora_Inicio_Proc, @Hora_Fin_Proc, @ID_Tratamiento);
END;
GO

-- Procedimiento almacenado para actualizar un registro en la tabla Procedimiento
CREATE PROCEDURE UpdateProcedimiento
    @ID_Procedimiento CHAR(8),
    @Fecha_Proc DATE,
    @Detalles_Proc VARCHAR(200),
    @Hora_Inicio_Proc TIME,
    @Hora_Fin_Proc TIME,
    @ID_Tratamiento CHAR(8)
AS
BEGIN
    IF @ID_Procedimiento = '' OR @Fecha_Proc IS NULL OR @Detalles_Proc = '' OR @Hora_Inicio_Proc IS NULL OR @Hora_Fin_Proc IS NULL OR @ID_Tratamiento = ''
    BEGIN
        RAISERROR('No se permiten valores nulos o vacíos', 16, 1);
        RETURN;
    END

    UPDATE Procedimiento
    SET Fecha_Proc = @Fecha_Proc,
        Detalles_Proc = @Detalles_Proc,
        Hora_Inicio_Proc = @Hora_Inicio_Proc,
        Hora_Fin_Proc = @Hora_Fin_Proc,
        ID_Tratamiento = @ID_Tratamiento
    WHERE ID_Procedimiento = @ID_Procedimiento;
END;
GO

-- Procedimiento almacenado para eliminar un registro de la tabla Procedimiento
CREATE PROCEDURE DeleteProcedimiento
    @ID_Procedimiento CHAR(8)
AS
BEGIN
    IF @ID_Procedimiento = ''
    BEGIN
        RAISERROR('No se permiten valores nulos o vacíos', 16, 1);
        RETURN;
    END

    DELETE FROM Procedimiento
    WHERE ID_Procedimiento = @ID_Procedimiento;
END;
GO

-- Procedimiento almacenado para insertar un registro en la tabla Factura
CREATE PROCEDURE InsertFactura
    @ID_Factura CHAR(8),
    @MontoTotal_Fa MONEY,
    @FechaEmision_Fa DATE,
    @ID_EstadoPago CHAR(8)
AS
BEGIN
    IF @ID_Factura = '' OR @MontoTotal_Fa IS NULL OR @FechaEmision_Fa IS NULL OR @ID_EstadoPago = ''
    BEGIN
        RAISERROR('No se permiten valores nulos o vacíos', 16, 1);
        RETURN;
    END

    INSERT INTO Factura (ID_Factura, MontoTotal_Fa, FechaEmision_Fa, ID_EstadoPago)
    VALUES (@ID_Factura, @MontoTotal_Fa, @FechaEmision_Fa, @ID_EstadoPago);
END;
GO

-- Procedimiento almacenado para actualizar un registro en la tabla Factura
CREATE PROCEDURE UpdateFactura
    @ID_Factura CHAR(8),
    @MontoTotal_Fa MONEY,
    @FechaEmision_Fa DATE,
    @ID_EstadoPago CHAR(8)
AS
BEGIN
    IF @ID_Factura = '' OR @MontoTotal_Fa IS NULL OR @FechaEmision_Fa IS NULL OR @ID_EstadoPago = ''
    BEGIN
        RAISERROR('No se permiten valores nulos o vacíos', 16, 1);
        RETURN;
    END

    UPDATE Factura
    SET MontoTotal_Fa = @MontoTotal_Fa,
        FechaEmision_Fa = @FechaEmision_Fa,
        ID_EstadoPago = @ID_EstadoPago
    WHERE ID_Factura = @ID_Factura;
END;
GO

-- Procedimiento almacenado para eliminar un registro de la tabla Factura
CREATE PROCEDURE DeleteFactura
    @ID_Factura CHAR(8)
AS
BEGIN
    IF @ID_Factura = ''
    BEGIN
        RAISERROR('No se permiten valores nulos o vacíos', 16, 1);
        RETURN;
    END

    DELETE FROM Factura
    WHERE ID_Factura = @ID_Factura;
END;
GO

-- Procedimiento almacenado para insertar un registro en la tabla Tipo_Pago
CREATE PROCEDURE InsertTipoPago
    @ID_Tipo_Pago CHAR(8),
    @Nombre_TP VARCHAR(20),
    @Descripcion_TP VARCHAR(200)
AS
BEGIN
    IF @ID_Tipo_Pago = '' OR @Nombre_TP = '' OR @Descripcion_TP = ''
    BEGIN
        RAISERROR('No se permiten valores nulos o vacíos', 16, 1);
        RETURN;
    END

    INSERT INTO Tipo_Pago (ID_Tipo_Pago, Nombre_TP, Descripcion_TP)
    VALUES (@ID_Tipo_Pago, @Nombre_TP, @Descripcion_TP);
END;
GO

-- Procedimiento almacenado para actualizar un registro en la tabla Tipo_Pago
CREATE PROCEDURE UpdateTipoPago
    @ID_Tipo_Pago CHAR(8),
    @Nombre_TP VARCHAR(20),
    @Descripcion_TP VARCHAR(200)
AS
BEGIN
    IF @ID_Tipo_Pago = '' OR @Nombre_TP = '' OR @Descripcion_TP = ''
    BEGIN
        RAISERROR('No se permiten valores nulos o vacíos', 16, 1);
        RETURN;
    END

    UPDATE Tipo_Pago
    SET Nombre_TP = @Nombre_TP,
        Descripcion_TP = @Descripcion_TP
    WHERE ID_Tipo_Pago = @ID_Tipo_Pago;
END;
GO

-- Procedimiento almacenado para eliminar un registro de la tabla Tipo_Pago
CREATE PROCEDURE DeleteTipoPago
    @ID_Tipo_Pago CHAR(8)
AS
BEGIN
    IF @ID_Tipo_Pago = ''
    BEGIN
        RAISERROR('No se permiten valores nulos o vacíos', 16, 1);
        RETURN;
    END

    DELETE FROM Tipo_Pago
    WHERE ID_Tipo_Pago = @ID_Tipo_Pago;
END;
GO

-- Procedimiento almacenado para insertar un registro en la tabla Pago
CREATE PROCEDURE InsertPago
    @ID_Pago INT,
    @Monto_Pago MONEY,
    @Fecha_Pago DATE,
    @ID_Factura CHAR(8),
    @ID_Tipo_Pago CHAR(8)
AS
BEGIN
    IF @ID_Pago IS NULL OR @Monto_Pago IS NULL OR @Fecha_Pago IS NULL OR @ID_Factura = '' OR @ID_Tipo_Pago = ''
    BEGIN
        RAISERROR('No se permiten valores nulos o vacíos', 16, 1);
        RETURN;
    END

    INSERT INTO Pago (ID_Pago, Monto_Pago, Fecha_Pago, ID_Factura, ID_Tipo_Pago)
    VALUES (@ID_Pago, @Monto_Pago, @Fecha_Pago, @ID_Factura, @ID_Tipo_Pago);
END;
GO

-- Procedimiento almacenado para actualizar un registro en la tabla Pago
CREATE PROCEDURE UpdatePago
    @ID_Pago INT,
    @Monto_Pago MONEY,
    @Fecha_Pago DATE,
    @ID_Factura CHAR(8),
    @ID_Tipo_Pago CHAR(8)
AS
BEGIN
    IF @ID_Pago IS NULL OR @Monto_Pago IS NULL OR @Fecha_Pago IS NULL OR @ID_Factura = '' OR @ID_Tipo_Pago = ''
    BEGIN
        RAISERROR('No se permiten valores nulos o vacíos', 16, 1);
        RETURN;
    END

    UPDATE Pago
    SET Monto_Pago = @Monto_Pago,
        Fecha_Pago = @Fecha_Pago,
        ID_Factura = @ID_Factura,
        ID_Tipo_Pago = @ID_Tipo_Pago
    WHERE ID_Pago = @ID_Pago;
END;
GO

-- Procedimiento almacenado para eliminar un registro de la tabla Pago
CREATE PROCEDURE DeletePago
    @ID_Pago INT
AS
BEGIN
    IF @ID_Pago IS NULL
    BEGIN
        RAISERROR('No se permiten valores nulos o vacíos', 16, 1);
        RETURN;
    END

    DELETE FROM Pago
    WHERE ID_Pago = @ID_Pago;
END;
GO

-- Procedimiento almacenado para insertar un registro en la tabla Estado_Cuenta
CREATE PROCEDURE InsertEstadoCuenta
    @ID_Estado_Cuenta CHAR(8),
    @Nombre_EC VARCHAR(20),
    @Descripcion_EC VARCHAR(200)
AS
BEGIN
    IF @ID_Estado_Cuenta = '' OR @Nombre_EC = '' OR @Descripcion_EC = ''
    BEGIN
        RAISERROR('No se permiten valores nulos o vacíos', 16, 1);
        RETURN;
    END

    INSERT INTO Estado_Cuenta (ID_Estado_Cuenta, Nombre_EC, Descripcion_EC)
    VALUES (@ID_Estado_Cuenta, @Nombre_EC, @Descripcion_EC);
END;
GO

-- Procedimiento almacenado para actualizar un registro en la tabla Estado_Cuenta
CREATE PROCEDURE UpdateEstadoCuenta
    @ID_Estado_Cuenta CHAR(8),
    @Nombre_EC VARCHAR(20),
    @Descripcion_EC VARCHAR(200)
AS
BEGIN
    IF @ID_Estado_Cuenta = '' OR @Nombre_EC = '' OR @Descripcion_EC = ''
    BEGIN
        RAISERROR('No se permiten valores nulos o vacíos', 16, 1);
        RETURN;
    END

    UPDATE Estado_Cuenta
    SET Nombre_EC = @Nombre_EC,
        Descripcion_EC = @Descripcion_EC
    WHERE ID_Estado_Cuenta = @ID_Estado_Cuenta;
END;
GO

-- Procedimiento almacenado para eliminar un registro de la tabla Estado_Cuenta
CREATE PROCEDURE DeleteEstadoCuenta
    @ID_Estado_Cuenta CHAR(8)
AS
BEGIN
    IF @ID_Estado_Cuenta = ''
    BEGIN
        RAISERROR('No se permiten valores nulos o vacíos', 16, 1);
        RETURN;
    END

    DELETE FROM Estado_Cuenta
    WHERE ID_Estado_Cuenta = @ID_Estado_Cuenta;
END;
GO

-- Procedimiento almacenado para insertar un registro en la tabla Historial_Medico
CREATE PROCEDURE InsertHistorialMedico
    @ID_HistorialMedico CHAR(8),
    @Fecha_Historial DATE,
    @Diagnostico VARCHAR(100),
    @Tratamientos_Medicos VARCHAR(200)
AS
BEGIN
    IF @ID_HistorialMedico = '' OR @Fecha_Historial IS NULL OR @Diagnostico = '' OR @Tratamientos_Medicos = ''
    BEGIN
        RAISERROR('No se permiten valores nulos o vacíos', 16, 1);
        RETURN;
    END

    INSERT INTO Historial_Medico (ID_HistorialMedico, Fecha_Historial, Diagnostico, Tratamientos_Medicos)
    VALUES (@ID_HistorialMedico, @Fecha_Historial, @Diagnostico, @Tratamientos_Medicos);
END;
GO

-- Procedimiento almacenado para actualizar un registro en la tabla Historial_Medico
CREATE PROCEDURE UpdateHistorialMedico
    @ID_HistorialMedico CHAR(8),
    @Fecha_Historial DATE,
    @Diagnostico VARCHAR(100),
    @Tratamientos_Medicos VARCHAR(200)
AS
BEGIN
    IF @ID_HistorialMedico = '' OR @Fecha_Historial IS NULL OR @Diagnostico = '' OR @Tratamientos_Medicos = ''
    BEGIN
        RAISERROR('No se permiten valores nulos o vacíos', 16, 1);
        RETURN;
    END

    UPDATE Historial_Medico
    SET Fecha_Historial = @Fecha_Historial,
        Diagnostico = @Diagnostico,
        Tratamientos_Medicos = @Tratamientos_Medicos
    WHERE ID_HistorialMedico = @ID_HistorialMedico;
END;
GO

-- Procedimiento almacenado para eliminar un registro en la tabla Historial_Medico
CREATE PROCEDURE DeleteHistorialMedico
    @ID_HistorialMedico CHAR(8)
AS
BEGIN
    IF @ID_HistorialMedico = ''
    BEGIN
        RAISERROR('No se permiten valores nulos o vacíos', 16, 1);
        RETURN;
    END

    DELETE FROM Historial_Medico
    WHERE ID_HistorialMedico = @ID_HistorialMedico;
END;
GO

-- Procedimiento almacenado para insertar un registro en la tabla Estado_Citas
CREATE PROCEDURE InsertEstadoCitas
    @ID_EstadoCita CHAR(8),
    @Nombre_Estado VARCHAR(20),
    @Descripcion_Estado VARCHAR(200)
AS
BEGIN
    IF @ID_EstadoCita = '' OR @Nombre_Estado = '' OR @Descripcion_Estado = ''
    BEGIN
        RAISERROR('No se permiten valores nulos o vacíos', 16, 1);
        RETURN;
    END

    INSERT INTO Estado_Citas (ID_EstadoCita, Nombre_Estado, Descripcion_Estado)
    VALUES (@ID_EstadoCita, @Nombre_Estado, @Descripcion_Estado);
END;
GO

-- Procedimiento almacenado para actualizar un registro en la tabla Estado_Citas
CREATE PROCEDURE UpdateEstadoCitas
    @ID_EstadoCita CHAR(8),
    @Nombre_Estado VARCHAR(20),
    @Descripcion_Estado VARCHAR(200)
AS
BEGIN
    IF @ID_EstadoCita = '' OR @Nombre_Estado = '' OR @Descripcion_Estado = ''
    BEGIN
        RAISERROR('No se permiten valores nulos o vacíos', 16, 1);
        RETURN;
    END

    UPDATE Estado_Citas
    SET Nombre_Estado = @Nombre_Estado,
        Descripcion_Estado = @Descripcion_Estado
    WHERE ID_EstadoCita = @ID_EstadoCita;
END;
GO

-- Procedimiento almacenado para eliminar un registro de la tabla Estado_Citas
CREATE PROCEDURE DeleteEstadoCitas
    @ID_EstadoCita CHAR(8)
AS
BEGIN
    IF @ID_EstadoCita = ''
    BEGIN
        RAISERROR('No se permiten valores nulos o vacíos', 16, 1);
        RETURN;
    END

    DELETE FROM Estado_Citas
    WHERE ID_EstadoCita = @ID_EstadoCita;
END;
GO

-- Procedimiento almacenado para insertar un registro en la tabla Dentista_Especialidad
CREATE PROCEDURE InsertDentistaEspecialidad
    @ID_Dentista_Especialidad CHAR(8),
    @ID_Dentista CHAR(8),
    @ID_Especialidad CHAR(8)
AS
BEGIN
    IF @ID_Dentista_Especialidad = '' OR @ID_Dentista = '' OR @ID_Especialidad = ''
    BEGIN
        RAISERROR('No se permiten valores nulos o vacíos', 16, 1);
        RETURN;
    END

    INSERT INTO Dentista_Especialidad (ID_Dentista_Especialidad, ID_Dentista, ID_Especialidad)
    VALUES (@ID_Dentista_Especialidad, @ID_Dentista, @ID_Especialidad);
END;
GO

-- Procedimiento almacenado para actualizar un registro en la tabla Dentista_Especialidad
CREATE PROCEDURE UpdateDentistaEspecialidad
    @ID_Dentista_Especialidad CHAR(8),
    @ID_Dentista CHAR(8),
    @ID_Especialidad CHAR(8)
AS
BEGIN
    IF @ID_Dentista_Especialidad = '' OR @ID_Dentista = '' OR @ID_Especialidad = ''
    BEGIN
        RAISERROR('No se permiten valores nulos o vacíos', 16, 1);
        RETURN;
    END

    UPDATE Dentista_Especialidad
    SET ID_Dentista = @ID_Dentista,
        ID_Especialidad = @ID_Especialidad
    WHERE ID_Dentista_Especialidad = @ID_Dentista_Especialidad;
END;
GO

-- Procedimiento almacenado para eliminar un registro de la tabla Dentista_Especialidad
CREATE PROCEDURE DeleteDentistaEspecialidad
    @ID_Dentista_Especialidad CHAR(8)
AS
BEGIN
    IF @ID_Dentista_Especialidad = ''
    BEGIN
        RAISERROR('No se permiten valores nulos o vacíos', 16, 1);
        RETURN;
    END

    DELETE FROM Dentista_Especialidad
    WHERE ID_Dentista_Especialidad = @ID_Dentista_Especialidad;
END;
GO

-- Procedimiento almacenado para insertar un registro en la tabla Especialidad
CREATE PROCEDURE InsertEspecialidad
    @ID_Especialidad CHAR(8),
    @Nombre_Esp VARCHAR(20),
    @Descripcion_Esp VARCHAR(200)
AS
BEGIN
    IF @ID_Especialidad = '' OR @Nombre_Esp = '' OR @Descripcion_Esp = ''
    BEGIN
        RAISERROR('No se permiten valores nulos o vacíos', 16, 1);
        RETURN;
    END

    INSERT INTO Especialidad (ID_Especialidad, Nombre_Esp, Descripcion_Esp)
    VALUES (@ID_Especialidad, @Nombre_Esp, @Descripcion_Esp);
END;
GO

-- Procedimiento almacenado para actualizar un registro en la tabla Especialidad
CREATE PROCEDURE UpdateEspecialidad
    @ID_Especialidad CHAR(8),
    @Nombre_Esp VARCHAR(20),
    @Descripcion_Esp VARCHAR(200)
AS
BEGIN
    IF @ID_Especialidad = '' OR @Nombre_Esp = '' OR @Descripcion_Esp = ''
    BEGIN
        RAISERROR('No se permiten valores nulos o vacíos', 16, 1);
        RETURN;
    END

    UPDATE Especialidad
    SET Nombre_Esp = @Nombre_Esp,
        Descripcion_Esp = @Descripcion_Esp
    WHERE ID_Especialidad = @ID_Especialidad;
END;
GO

-- Procedimiento almacenado para eliminar un registro de la tabla Especialidad
CREATE PROCEDURE DeleteEspecialidad
    @ID_Especialidad CHAR(8)
AS
BEGIN
    IF @ID_Especialidad = ''
    BEGIN
        RAISERROR('No se permiten valores nulos o vacíos', 16, 1);
        RETURN;
    END

    DELETE FROM Especialidad
    WHERE ID_Especialidad = @ID_Especialidad;
END;
GO

-- Procedimiento almacenado para insertar un registro en la tabla Usuarios
CREATE PROCEDURE InsertUsuario
    @ID_Usuario CHAR(8),
    @Nombre VARCHAR(20),
    @Apellido1 VARCHAR(200),
    @Apellido2 VARCHAR(200),
    @Email VARCHAR(20),
    @Contraseña CHAR(12),
    @Token VARCHAR(100),
    @ID_Funcionario CHAR(8)
AS
BEGIN
    IF @ID_Usuario = '' OR @Nombre = '' OR @Apellido1 = '' OR @Apellido2 = '' OR @Email = '' OR @Contraseña = '' OR @Token = '' OR @ID_Funcionario = ''
    BEGIN
        RAISERROR('No se permiten valores nulos o vacíos', 16, 1);
        RETURN;
    END

    INSERT INTO Usuarios (ID_Usuario, Nombre, Apellido1, Apellido2, Email, Contraseña, Token, ID_Funcionario)
    VALUES (@ID_Usuario, @Nombre, @Apellido1, @Apellido2, @Email, @Contraseña, @Token, @ID_Funcionario);
END;
GO

-- Procedimiento almacenado para actualizar un registro en la tabla Usuarios
CREATE PROCEDURE UpdateUsuario
    @ID_Usuario CHAR(8),
    @Nombre VARCHAR(20),
    @Apellido1 VARCHAR(200),
    @Apellido2 VARCHAR(200),
    @Email VARCHAR(20),
    @Contraseña CHAR(12),
    @Token VARCHAR(100),
    @ID_Funcionario CHAR(8)
AS
BEGIN
    IF @ID_Usuario = '' OR @Nombre = '' OR @Apellido1 = '' OR @Apellido2 = '' OR @Email = '' OR @Contraseña = '' OR @Token = '' OR @ID_Funcionario = ''
    BEGIN
        RAISERROR('No se permiten valores nulos o vacíos', 16, 1);
        RETURN;
    END

    UPDATE Usuarios
    SET Nombre = @Nombre,
        Apellido1 = @Apellido1,
        Apellido2 = @Apellido2,
        Email = @Email,
        Contraseña = @Contraseña,
        Token = @Token,
        ID_Funcionario = @ID_Funcionario
    WHERE ID_Usuario = @ID_Usuario;
END;
GO

-- Procedimiento almacenado para eliminar un registro de la tabla Usuarios
CREATE PROCEDURE DeleteUsuario
    @ID_Usuario CHAR(8)
AS
BEGIN
    IF @ID_Usuario = ''
    BEGIN
        RAISERROR('No se permiten valores nulos o vacíos', 16, 1);
        RETURN;
    END

    DELETE FROM Usuarios
    WHERE ID_Usuario = @ID_Usuario;
END;
GO

-- Procedimiento almacenado para insertar un registro en la tabla Funcionario
CREATE PROCEDURE InsertFuncionario
    @ID_Funcionario CHAR(8),
    @Nombre VARCHAR(20),
    @Apellido1 VARCHAR(200),
    @Apellido2 VARCHAR(200),
    @Email VARCHAR(20),
    @Contraseña CHAR(12)
AS
BEGIN
    IF @ID_Funcionario = '' OR @Nombre = '' OR @Apellido1 = '' OR @Apellido2 = '' OR @Email = '' OR @Contraseña = ''
    BEGIN
        RAISERROR('No se permiten valores nulos o vacíos', 16, 1);
        RETURN;
    END

    INSERT INTO Funcionario (ID_Funcionario, Nombre, Apellido1, Apellido2, Email, Contraseña)
    VALUES (@ID_Funcionario, @Nombre, @Apellido1, @Apellido2, @Email, @Contraseña);
END;
GO

-- Procedimiento almacenado para actualizar un registro en la tabla Funcionario
CREATE PROCEDURE UpdateFuncionario
    @ID_Funcionario CHAR(8),
    @Nombre VARCHAR(20),
    @Apellido1 VARCHAR(200),
    @Apellido2 VARCHAR(200),
    @Email VARCHAR(20),
    @Contraseña CHAR(12)
AS
BEGIN
    IF @ID_Funcionario = '' OR @Nombre = '' OR @Apellido1 = '' OR @Apellido2 = '' OR @Email = '' OR @Contraseña = ''
    BEGIN
        RAISERROR('No se permiten valores nulos o vacíos', 16, 1);
        RETURN;
    END

    UPDATE Funcionario
    SET Nombre = @Nombre,
        Apellido1 = @Apellido1,
        Apellido2 = @Apellido2,
        Email = @Email,
        Contraseña = @Contraseña
    WHERE ID_Funcionario = @ID_Funcionario;
END;
GO

-- Procedimiento almacenado para eliminar un registro de la tabla Funcionario
CREATE PROCEDURE DeleteFuncionario
    @ID_Funcionario CHAR(8)
AS
BEGIN
    IF @ID_Funcionario = ''
    BEGIN
        RAISERROR('No se permiten valores nulos o vacíos', 16, 1);
        RETURN;
    END

    DELETE FROM Funcionario
    WHERE ID_Funcionario = @ID_Funcionario;
END;
GO

-- Procedimiento almacenado para insertar un registro en la tabla Factura_Procedimiento
CREATE PROCEDURE InsertFacturaProcedimiento
    @ID_Factura_Procedimiento CHAR(8),
    @ID_Factura CHAR(8),
    @ID_Procedimiento CHAR(8)
AS
BEGIN
    IF @ID_Factura_Procedimiento = '' OR @ID_Factura = '' OR @ID_Procedimiento = ''
    BEGIN
        RAISERROR('No se permiten valores nulos o vacíos', 16, 1);
        RETURN;
    END

    INSERT INTO Factura_Procedimiento (ID_Factura_Procedimiento, ID_Factura, ID_Procedimiento)
    VALUES (@ID_Factura_Procedimiento, @ID_Factura, @ID_Procedimiento);
END;
GO

-- Procedimiento almacenado para actualizar un registro en la tabla Factura_Procedimiento
CREATE PROCEDURE UpdateFacturaProcedimiento
    @ID_Factura_Procedimiento CHAR(8),
    @ID_Factura CHAR(8),
    @ID_Procedimiento CHAR(8)
AS
BEGIN
    IF @ID_Factura_Procedimiento = '' OR @ID_Factura = '' OR @ID_Procedimiento = ''
    BEGIN
        RAISERROR('No se permiten valores nulos o vacíos', 16, 1);
        RETURN;
    END

    UPDATE Factura_Procedimiento
    SET ID_Factura = @ID_Factura,
        ID_Procedimiento = @ID_Procedimiento
    WHERE ID_Factura_Procedimiento = @ID_Factura_Procedimiento;
END;
GO

-- Procedimiento almacenado para eliminar un registro de la tabla Factura_Procedimiento
CREATE PROCEDURE DeleteFacturaProcedimiento
    @ID_Factura_Procedimiento CHAR(8)
AS
BEGIN
    IF @ID_Factura_Procedimiento = ''
    BEGIN
        RAISERROR('No se permiten valores nulos o vacíos', 16, 1);
        RETURN;
    END

    DELETE FROM Factura_Procedimiento
    WHERE ID_Factura_Procedimiento = @ID_Factura_Procedimiento;
END;
GO

-- Procedimiento almacenado para insertar un registro en la tabla Factura_Tratamiento
CREATE PROCEDURE InsertFacturaTratamiento
    @ID_Factura_Tratamiento CHAR(8),
    @ID_Factura CHAR(8),
    @ID_Tratamiento CHAR(8)
AS
BEGIN
    IF @ID_Factura_Tratamiento = '' OR @ID_Factura = '' OR @ID_Tratamiento = ''
    BEGIN
        RAISERROR('No se permiten valores nulos o vacíos', 16, 1);
        RETURN;
    END

    INSERT INTO Factura_Tratamiento (ID_Factura_Tratamiento, ID_Factura, ID_Tratamiento)
    VALUES (@ID_Factura_Tratamiento, @ID_Factura, @ID_Tratamiento);
END;
GO

-- Procedimiento almacenado para actualizar un registro en la tabla Factura_Tratamiento
CREATE PROCEDURE UpdateFacturaTratamiento
    @ID_Factura_Tratamiento CHAR(8),
    @ID_Factura CHAR(8),
    @ID_Tratamiento CHAR(8)
AS
BEGIN
    IF @ID_Factura_Tratamiento = '' OR @ID_Factura = '' OR @ID_Tratamiento = ''
    BEGIN
        RAISERROR('No se permiten valores nulos o vacíos', 16, 1);
        RETURN;
    END

    UPDATE Factura_Tratamiento
    SET ID_Factura = @ID_Factura,
        ID_Tratamiento = @ID_Tratamiento
    WHERE ID_Factura_Tratamiento = @ID_Factura_Tratamiento;
END;
GO

-- Procedimiento almacenado para eliminar un registro de la tabla Factura_Tratamiento
CREATE PROCEDURE DeleteFacturaTratamiento
    @ID_Factura_Tratamiento CHAR(8)
AS
BEGIN
    IF @ID_Factura_Tratamiento = ''
    BEGIN
        RAISERROR('No se permiten valores nulos o vacíos', 16, 1);
        RETURN;
    END

    DELETE FROM Factura_Tratamiento
    WHERE ID_Factura_Tratamiento = @ID_Factura_Tratamiento;
END;
GO

-- Procedimiento almacenado para insertar un registro en la tabla Tipo_Accion
CREATE PROCEDURE InsertTipoAccion
    @ID_TipoAccion CHAR(8),
    @Nombre_Accion VARCHAR(20),
    @Descripcion_Tipo_Accion VARCHAR(200)
AS
BEGIN
    IF @ID_TipoAccion = '' OR @Nombre_Accion = '' OR @Descripcion_Tipo_Accion = ''
    BEGIN
        RAISERROR('No se permiten valores nulos o vacíos', 16, 1);
        RETURN;
    END

    INSERT INTO Tipo_Accion (ID_TipoAccion, Nombre_Accion, Descripcion_Tipo_Accion)
    VALUES (@ID_TipoAccion, @Nombre_Accion, @Descripcion_Tipo_Accion);
END;
GO

-- Procedimiento almacenado para actualizar un registro en la tabla Tipo_Accion
CREATE PROCEDURE UpdateTipoAccion
    @ID_TipoAccion CHAR(8),
    @Nombre_Accion VARCHAR(20),
    @Descripcion_Tipo_Accion VARCHAR(200)
AS
BEGIN
    IF @ID_TipoAccion = '' OR @Nombre_Accion = '' OR @Descripcion_Tipo_Accion = ''
    BEGIN
        RAISERROR('No se permiten valores nulos o vacíos', 16, 1);
        RETURN;
    END

    UPDATE Tipo_Accion
    SET Nombre_Accion = @Nombre_Accion,
        Descripcion_Tipo_Accion = @Descripcion_Tipo_Accion
    WHERE ID_TipoAccion = @ID_TipoAccion;
END;
GO

-- Procedimiento almacenado para eliminar un registro de la tabla Tipo_Accion
CREATE PROCEDURE DeleteTipoAccion
    @ID_TipoAccion CHAR(8)
AS
BEGIN
    IF @ID_TipoAccion = ''
    BEGIN
        RAISERROR('No se permiten valores nulos o vacíos', 16, 1);
        RETURN;
    END

    DELETE FROM Tipo_Accion
    WHERE ID_TipoAccion = @ID_TipoAccion;
END;
GO

-- Procedimiento almacenado para insertar un registro en la tabla Historial_Tratamiento
CREATE PROCEDURE InsertHistorialTratamiento
    @ID_Historial_Tratamiento CHAR(8),
    @ID_HistorialMedico CHAR(8),
    @ID_Tratamiento CHAR(8),
    @Fecha_Tratamiento DATE
AS
BEGIN
    IF @ID_Historial_Tratamiento = '' OR @ID_HistorialMedico = '' OR @ID_Tratamiento = '' OR @Fecha_Tratamiento IS NULL
    BEGIN
        RAISERROR('No se permiten valores nulos o vacíos', 16, 1);
        RETURN;
    END

    INSERT INTO Historial_Tratamiento (ID_Historial_Tratamiento, ID_HistorialMedico, ID_Tratamiento, Fecha_Tratamiento)
    VALUES (@ID_Historial_Tratamiento, @ID_HistorialMedico, @ID_Tratamiento, @Fecha_Tratamiento);
END;
GO

-- Procedimiento almacenado para actualizar un registro en la tabla Historial_Tratamiento
CREATE PROCEDURE UpdateHistorialTratamiento
    @ID_Historial_Tratamiento CHAR(8),
    @ID_HistorialMedico CHAR(8),
    @ID_Tratamiento CHAR(8),
    @Fecha_Tratamiento DATE
AS
BEGIN
    IF @ID_Historial_Tratamiento = '' OR @ID_HistorialMedico = '' OR @ID_Tratamiento = '' OR @Fecha_Tratamiento IS NULL
    BEGIN
        RAISERROR('No se permiten valores nulos o vacíos', 16, 1);
        RETURN;
    END

    UPDATE Historial_Tratamiento
    SET ID_HistorialMedico = @ID_HistorialMedico,
        ID_Tratamiento = @ID_Tratamiento,
        Fecha_Tratamiento = @Fecha_Tratamiento
    WHERE ID_Historial_Tratamiento = @ID_Historial_Tratamiento;
END;
GO

-- Procedimiento almacenado para eliminar un registro de la tabla Historial_Tratamiento
CREATE PROCEDURE DeleteHistorialTratamiento
    @ID_Historial_Tratamiento CHAR(8)
AS
BEGIN
    IF @ID_Historial_Tratamiento = ''
    BEGIN
        RAISERROR('No se permiten valores nulos o vacíos', 16, 1);
        RETURN;
    END

    DELETE FROM Historial_Tratamiento
    WHERE ID_Historial_Tratamiento = @ID_Historial_Tratamiento;
END;
GO