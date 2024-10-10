-- Usar la base de datos master
USE master;
GO

-- Crear la base de datos ClinicaDentalSP con control sobre los archivos de datos y log
CREATE DATABASE ClinicaDentalSP
ON PRIMARY
(
    NAME = 'ClinicaDentalSP_data',
    FILENAME = 'D:\SQLData\ClinicaDentalSP_data.mdf',
    SIZE = 50MB,
    MAXSIZE = 500MB,
    FILEGROWTH = 10MB
)
LOG ON
(
    NAME = 'ClinicaDentalSP_log',
    FILENAME = 'D:\SQLLog\ClinicaDentalSP_log.ldf',
    SIZE = 20MB,
    MAXSIZE = 200MB,
    FILEGROWTH = 5MB
);
GO

-- Usar la base de datos recien creada
USE ClinicaDentalSP;
GO

-- Crear la tabla Paciente primero, ya que otras tablas hacen referencia a ella
CREATE TABLE Paciente (
    ID_Paciente INT PRIMARY KEY,
    Nombre VARCHAR(20) NOT NULL,
    Apellido1 VARCHAR(20) NOT NULL,
    Apellido2 VARCHAR(20),
    Fecha_Nacimiento DATE,
    Edad INT,
    Telefono VARCHAR(20),
    Correo VARCHAR(30),
    Direccion VARCHAR(200),
    ID_HistorialMedico INT
);
GO

-- Crear la tabla Historial_Medico, que no depende de otras tablas
CREATE TABLE Historial_Medico (
    ID_HistorialMedico INT PRIMARY KEY,
    Fecha_Historial DATE NOT NULL,
    Diagnostico VARCHAR(100),
    Tratamientos_Medicos VARCHAR(200)
);
GO

-- Ahora que Paciente y Historial_Medico están creadas, podemos agregar la llave foránea en Paciente
ALTER TABLE Paciente
ADD CONSTRAINT FK_HistorialMedico FOREIGN KEY (ID_HistorialMedico) REFERENCES Historial_Medico(ID_HistorialMedico);
GO

-- Crear la tabla Estado_Pago
CREATE TABLE Estado_Pago (
    ID_EstadoPago INT PRIMARY KEY,
    Nombre VARCHAR(20) NOT NULL,
    Descripcion VARCHAR(200)
);
GO

-- Crear la tabla Factura que hace referencia a Paciente y Estado_Pago
CREATE TABLE Factura (
    ID_Factura INT PRIMARY KEY,
    MontoTotal MONEY NOT NULL,
    FechaEmision DATE NOT NULL,
    ID_Paciente INT,
    ID_EstadoPago INT,
    CONSTRAINT FK_Paciente_Factura FOREIGN KEY (ID_Paciente) REFERENCES Paciente(ID_Paciente),
    CONSTRAINT FK_EstadoPago_Factura FOREIGN KEY (ID_EstadoPago) REFERENCES Estado_Pago(ID_EstadoPago)
);
GO

-- Crear la tabla Estado_Cita
CREATE TABLE Estado_Cita (
    ID_EstadoCita INT PRIMARY KEY,
    NombreEstado VARCHAR(20) NOT NULL,
    DescripcionEstado VARCHAR(200)
);
GO

-- Crear la tabla Cita que hace referencia a Paciente y Estado_Cita
CREATE TABLE Cita (
    ID_Cita INT PRIMARY KEY,
    Fecha_Hora DATETIME NOT NULL,
    ID_Paciente INT,
    ID_EstadoCita INT,
    CONSTRAINT FK_Paciente_Cita FOREIGN KEY (ID_Paciente) REFERENCES Paciente(ID_Paciente),
    CONSTRAINT FK_EstadoCita_Cita FOREIGN KEY (ID_EstadoCita) REFERENCES Estado_Cita(ID_EstadoCita)
);
GO

-- Crear la tabla Dentista
CREATE TABLE Dentista (
    ID_Dentista INT PRIMARY KEY,
    Nombre VARCHAR(20) NOT NULL,
    Apellido1 VARCHAR(20) NOT NULL,
    Apellido2 VARCHAR(20),
    Direccion VARCHAR(200),
    FechaNacimiento DATE,
    Telefono VARCHAR(20),
    Correo VARCHAR(30)
);
GO

-- Crear la tabla Especialidad
CREATE TABLE Especialidad (
    ID_Especialidad INT PRIMARY KEY,
    NombreEspecialidad VARCHAR(20),
    Descripcion VARCHAR(200)
);
GO

-- Crear la tabla Dentista_Especialidad que hace referencia a Dentista y Especialidad
CREATE TABLE Dentista_Especialidad (
    ID_Dentista_Especialidad INT PRIMARY KEY,
    ID_Dentista INT,
    ID_Especialidad INT,
    CONSTRAINT FK_Dentista_Especialidad FOREIGN KEY (ID_Dentista) REFERENCES Dentista(ID_Dentista),
    CONSTRAINT FK_Especialidad_Dentista FOREIGN KEY (ID_Especialidad) REFERENCES Especialidad(ID_Especialidad)
);
GO

-- Crear la tabla Tipo_Tratamiento
CREATE TABLE Tipo_Tratamiento (
    ID_TipoTratamiento INT PRIMARY KEY,
    Nombre_Tipo_Tratamiento VARCHAR(20),
    Descripcion_Tipo_Tratamiento VARCHAR(200)
);
GO

-- Crear la tabla Tratamiento que hace referencia a Tipo_Tratamiento
CREATE TABLE Tratamiento (
    ID_Tratamiento INT PRIMARY KEY,
    Nombre_Tratamiento VARCHAR(20),
    Descripcion_Tratamiento VARCHAR(200),
    ID_TipoTratamiento INT,
    CONSTRAINT FK_TipoTratamiento FOREIGN KEY (ID_TipoTratamiento) REFERENCES Tipo_Tratamiento(ID_TipoTratamiento)
);
GO

-- Crear la tabla Procedimiento que hace referencia a Tratamiento
CREATE TABLE Procedimiento (
    ID_Procedimiento INT PRIMARY KEY,
    FechaProcedimiento DATE,
    DetallesProcedimiento VARCHAR(200),
    ID_Tratamiento INT,
    CONSTRAINT FK_Tratamiento_Procedimiento FOREIGN KEY (ID_Tratamiento) REFERENCES Tratamiento(ID_Tratamiento)
);
GO

-- Crear la tabla Paciente_Procedimiento que hace referencia a Paciente y Procedimiento
CREATE TABLE Paciente_Procedimiento (
    ID_Paciente_Procedimiento INT PRIMARY KEY,
    ID_Paciente INT,
    ID_Procedimiento INT,
    CONSTRAINT FK_Paciente_Proc FOREIGN KEY (ID_Paciente) REFERENCES Paciente(ID_Paciente),
    CONSTRAINT FK_Procedimiento_Paciente FOREIGN KEY (ID_Procedimiento) REFERENCES Procedimiento(ID_Procedimiento)
);
GO
