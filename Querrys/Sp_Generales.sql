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