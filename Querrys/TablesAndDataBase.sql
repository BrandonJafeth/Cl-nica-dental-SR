USE master;
GO
DROP DATABASE IF EXISTS ClinicaDental;
GO

/*
    Justificación de los Tamaños de la Base de Datos y Filegroups:
    - PacientesFileGroup: Para almacenar datos de pacientes, proyectando 1,000 pacientes nuevos por año y 500 bytes de almacenamiento por paciente. Tamaño inicial: 50MB.
    - CitasFileGroup: Para almacenar citas. Se estima almacenar 50,000 citas en 10 años, con un tamaño de 30MB para crecimiento.
    - TratamientosFileGroup: Para tratamientos y procedimientos, estimado en 50MB.
    - FinancierosFileGroup: Para facturación y pagos, asignado 30MB.
    - AuditoriaFileGroup: Para registros de auditoría, asignado 20MB.
    - RolesPermisosFileGroup: Para datos de roles y permisos, asignado 10MB.
*/

-- 1. Crear la base de datos con Filegroups adecuados
CREATE DATABASE ClinicaDental
ON PRIMARY (
    NAME = 'ClinicaDental_Data',       
    FILENAME = 'C:\SQLData\ClinicaDental_Data.mdf',  
    SIZE = 50MB,                       
    MAXSIZE = 500MB,                   
    FILEGROWTH = 20MB                  
),
FILEGROUP PacientesFileGroup (
    NAME = 'Pacientes_Data',       
    FILENAME = 'C:\SQLData\Pacientes_Data.ndf',  
    SIZE = 50MB,                       
    MAXSIZE = 200MB,                   
    FILEGROWTH = 10MB                   
),
FILEGROUP CitasFileGroup (
    NAME = 'Citas_Data',       
    FILENAME = 'C:\SQLData\Citas_Data.ndf',  
    SIZE = 30MB,                       
    MAXSIZE = 100MB,                   
    FILEGROWTH = 10MB                   
),
FILEGROUP TratamientosFileGroup (
    NAME = 'Tratamientos_Data',       
    FILENAME = 'C:\SQLData\Tratamientos_Data.ndf',  
    SIZE = 50MB,                       
    MAXSIZE = 150MB,                   
    FILEGROWTH = 10MB                   
),
FILEGROUP FinancierosFileGroup (
    NAME = 'Financieros_Data',       
    FILENAME = 'C:\SQLData\Financieros_Data.ndf',  
    SIZE = 30MB,                       
    MAXSIZE = 100MB,                   
    FILEGROWTH = 10MB                   
),
FILEGROUP AuditoriaFileGroup (
    NAME = 'Auditoria_Data',       
    FILENAME = 'C:\SQLData\Auditoria_Data.ndf',  
    SIZE = 20MB,                       
    MAXSIZE = 50MB,                    
    FILEGROWTH = 5MB                   
),
FILEGROUP RolesPermisosFileGroup (
    NAME = 'RolesPermisos_Data',       
    FILENAME = 'C:\SQLData\RolesPermisos_Data.ndf',  
    SIZE = 10MB,                       
    MAXSIZE = 50MB,                    
    FILEGROWTH = 5MB                   
)
LOG ON (
    NAME = 'ClinicaDental_Log',        
    FILENAME = 'C:\SQLLog\ClinicaDental_Log.ldf',   
    SIZE = 10MB,                       
    MAXSIZE = 50MB,                    
    FILEGROWTH = 5MB                   
);
GO

-- 2. Usar la base de datos
USE ClinicaDental;
GO

-- 3. Crear tablas en los filegroups correspondientes

-- Tabla: Estado_Pago
CREATE TABLE Estado_Pago (
    ID_EstadoPago CHAR(8) PRIMARY KEY,
    Nombre_EP VARCHAR(20),
    Descripcion_EP VARCHAR(200)
) ON FinancierosFileGroup;

-- Tabla: Tipo_Tratamiento
CREATE TABLE Tipo_Tratamiento (
    ID_TipoTratamiento CHAR(8) PRIMARY KEY,
    Nombre_Tipo_Tratamiento VARCHAR(20),
    Descripcion_Tipo_Tratamiento VARCHAR(200)
) ON TratamientosFileGroup;

-- Tabla: Estado_Tratamiento
CREATE TABLE Estado_Tratamiento (
    ID_EstadoTratamiento CHAR(8) PRIMARY KEY,
    Nombre_Estado VARCHAR(20),
    Descripcion_Estado VARCHAR(200)
) ON TratamientosFileGroup;

-- Tabla: Paciente
CREATE TABLE Paciente (
    ID_Paciente CHAR(8) PRIMARY KEY,
    Nombre_Pac VARCHAR(20),
    Apellido1_Pac VARCHAR(20),
    Apellido2_Pac VARCHAR(20),
    Fecha_Nacimiento_Pac DATE,
    Telefono_Pac VARCHAR(20),
    Correo_Pac VARCHAR(30),
    Direccion_Pac VARCHAR(200)
) ON PacientesFileGroup;

-- Tabla: Historial_Medico con ON DELETE CASCADE
CREATE TABLE Historial_Medico (
    ID_HistorialMedico CHAR(8) PRIMARY KEY,
    Fecha_Historial DATE,
    Diagnostico VARCHAR(100),
    ID_Paciente CHAR(8) UNIQUE,
    FOREIGN KEY (ID_Paciente) REFERENCES Paciente(ID_Paciente) ON DELETE CASCADE
) ON PacientesFileGroup;

-- Tabla: Tratamiento con ON DELETE CASCADE en referencias
CREATE TABLE Tratamiento (
    ID_Tratamiento CHAR(8) PRIMARY KEY,
    Nombre_Tra VARCHAR(20),
    Descripcion_Tra VARCHAR(200),
    ID_TipoTratamiento CHAR(8),
    ID_EstadoTratamiento CHAR(8),
    FOREIGN KEY (ID_TipoTratamiento) REFERENCES Tipo_Tratamiento(ID_TipoTratamiento) ON DELETE CASCADE,
    FOREIGN KEY (ID_EstadoTratamiento) REFERENCES Estado_Tratamiento(ID_EstadoTratamiento) ON DELETE CASCADE
) ON TratamientosFileGroup;

-- Tabla: Usuarios
CREATE TABLE Usuarios (
    ID_Usuario CHAR(8) PRIMARY KEY,
    Nombre VARCHAR(20),
    Apellido1 VARCHAR(200),
    Apellido2 VARCHAR(200),
    Email VARCHAR(50),
    Contraseña CHAR(12),
    Token VARCHAR(100)
) ON AuditoriaFileGroup;

-- Tabla: Funcionario con ON DELETE CASCADE en relación a Usuarios
CREATE TABLE Funcionario (
    ID_Funcionario CHAR(8) PRIMARY KEY,
    Nombre VARCHAR(20),
    Apellido1 VARCHAR(200),
    Apellido2 VARCHAR(200),
    Email VARCHAR(20),
    ID_Usuario CHAR(8),
    FOREIGN KEY (ID_Usuario) REFERENCES Usuarios(ID_Usuario) ON DELETE CASCADE
) ON AuditoriaFileGroup;

-- Tabla: Dentista con ON DELETE CASCADE en relación a Funcionario
CREATE TABLE Dentista (
    ID_Dentista CHAR(8) PRIMARY KEY,
    Nombre_Den VARCHAR(20),
    Apellido1_Den VARCHAR(20),
    Apellido2_Den VARCHAR(20),
    Direccion_Den VARCHAR(200),
    FechaNacimiento_Den DATE,
    Telefono_Den VARCHAR(20),
    Correo_Den VARCHAR(30),
    ID_Funcionario CHAR(8),
    FOREIGN KEY (ID_Funcionario) REFERENCES Funcionario(ID_Funcionario) ON DELETE CASCADE
) ON CitasFileGroup;

-- Tabla: Especialidad
CREATE TABLE Especialidad (
    ID_Especialidad CHAR(8) PRIMARY KEY,
    Nombre_Esp VARCHAR(20),
    Descripcion_Esp VARCHAR(200)
) ON CitasFileGroup;

-- Tabla: Dentista_Especialidad con ON DELETE CASCADE
CREATE TABLE Dentista_Especialidad (
    ID_Dentista_Especialidad CHAR(8) PRIMARY KEY,
    ID_Dentista CHAR(8),
    ID_Especialidad CHAR(8),
    FOREIGN KEY (ID_Dentista) REFERENCES Dentista(ID_Dentista) ON DELETE CASCADE,
    FOREIGN KEY (ID_Especialidad) REFERENCES Especialidad(ID_Especialidad) ON DELETE CASCADE
) ON CitasFileGroup;

-- Tabla: Estado_Citas
CREATE TABLE Estado_Citas (
    ID_EstadoCita CHAR(8) PRIMARY KEY,
    Nombre_Estado VARCHAR(20),
    Descripcion_Estado VARCHAR(200)
) ON CitasFileGroup;

-- Tabla: Estado_Cuenta
CREATE TABLE Estado_Cuenta (
    ID_Estado_Cuenta CHAR(8) PRIMARY KEY,
    Nombre_EC VARCHAR(20),
    Descripcion_EC VARCHAR(200)
) ON FinancierosFileGroup;

-- Tabla: Tipo_Pago
CREATE TABLE Tipo_Pago (
    ID_Tipo_Pago CHAR(8) PRIMARY KEY,
    Nombre_TP VARCHAR(20),
    Descripcion_TP VARCHAR(200)
) ON FinancierosFileGroup;

-- Tabla: Factura
CREATE TABLE Factura (
    ID_Factura CHAR(8) PRIMARY KEY,
    MontoTotal_Fa MONEY,
    FechaEmision_Fa DATE,
    ID_EstadoPago CHAR(8),
    FOREIGN KEY (ID_EstadoPago) REFERENCES Estado_Pago(ID_EstadoPago)
) ON FinancierosFileGroup;

-- Tabla: Pago
CREATE TABLE Pago (
    ID_Pago UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    Monto_Pago MONEY,
    Fecha_Pago DATE,
    ID_Factura CHAR(8),
    ID_Tipo_Pago CHAR(8),
    FOREIGN KEY (ID_Factura) REFERENCES Factura(ID_Factura) ON DELETE CASCADE,
    FOREIGN KEY (ID_Tipo_Pago) REFERENCES Tipo_Pago(ID_Tipo_Pago)
) ON FinancierosFileGroup;

-- Tabla: Cuenta con ON DELETE CASCADE en relación a Paciente y Factura
CREATE TABLE Cuenta (
    ID_Cuenta CHAR(8) PRIMARY KEY,
    Saldo_Total MONEY,
    Fecha_Apertura DATE,
    Fecha_Cierre DATE,
    Fecha_Ultima_Actualizacion DATE,
    Observaciones VARCHAR(255),
    ID_Estado_Cuenta CHAR(8),
    ID_Factura CHAR(8),
    ID_Paciente CHAR(8),
    FOREIGN KEY (ID_Estado_Cuenta) REFERENCES Estado_Cuenta(ID_Estado_Cuenta) ON DELETE CASCADE,
    FOREIGN KEY (ID_Factura) REFERENCES Factura(ID_Factura) ON DELETE CASCADE,
    FOREIGN KEY (ID_Paciente) REFERENCES Paciente(ID_Paciente) ON DELETE CASCADE
) ON FinancierosFileGroup;

-- Tabla: Procedimiento con ON DELETE CASCADE en relación a Tratamiento y Paciente
CREATE TABLE Procedimiento (
    ID_Procedimiento CHAR(8) PRIMARY KEY,
    Fecha_Proc DATE,
    Detalles_Proc VARCHAR(200),
    Hora_Inicio_Proc TIME,
    Hora_Fin_Proc TIME,
    ID_Tratamiento CHAR(8),
    FOREIGN KEY (ID_Tratamiento) REFERENCES Tratamiento(ID_Tratamiento) ON DELETE CASCADE,
) ON TratamientosFileGroup;

-- Tabla intermedia Paciente_Procedimiento con ON DELETE CASCADE
CREATE TABLE Paciente_Procedimiento (
    ID_Paciente_Procedimiento CHAR(8) PRIMARY KEY,
    ID_Paciente CHAR(8),
    ID_Procedimiento CHAR(8),
    FOREIGN KEY (ID_Paciente) REFERENCES Paciente(ID_Paciente) ON DELETE CASCADE,
    FOREIGN KEY (ID_Procedimiento) REFERENCES Procedimiento(ID_Procedimiento) ON DELETE CASCADE
) ON TratamientosFileGroup;

-- Tabla: Cita
CREATE TABLE Cita (
    ID_Cita CHAR(8) PRIMARY KEY,
    Fecha_Cita DATE,
    Motivo VARCHAR(200),
    Hora_Inicio TIME,
    Hora_Fin TIME,
    ID_Paciente CHAR(8),
    ID_Dentista CHAR(8),
    ID_Funcionario CHAR(8),
    ID_EstadoCita CHAR(8),
    FOREIGN KEY (ID_Paciente) REFERENCES Paciente(ID_Paciente) ON DELETE CASCADE,
    FOREIGN KEY (ID_Dentista) REFERENCES Dentista(ID_Dentista) ON DELETE CASCADE,
    FOREIGN KEY (ID_Funcionario) REFERENCES Funcionario(ID_Funcionario),
    FOREIGN KEY (ID_EstadoCita) REFERENCES Estado_Citas(ID_EstadoCita)
) ON CitasFileGroup;

-- Tabla: Historial_Tratamiento con ON DELETE CASCADE
CREATE TABLE Historial_Tratamiento (
    ID_Historial_Tratamiento CHAR(8) PRIMARY KEY,
    ID_HistorialMedico CHAR(8) NOT NULL,
    ID_Tratamiento CHAR(8) NOT NULL,
    Fecha_Tratamiento DATE,
    FOREIGN KEY (ID_HistorialMedico) REFERENCES Historial_Medico(ID_HistorialMedico) ON DELETE CASCADE,
    FOREIGN KEY (ID_Tratamiento) REFERENCES Tratamiento(ID_Tratamiento) ON DELETE CASCADE
) ON TratamientosFileGroup;

-- Tabla: Roles
CREATE TABLE Roles (
    ID_Roles CHAR(8) PRIMARY KEY,
    Nombre VARCHAR(20),
    Descripcion VARCHAR(200)
) ON RolesPermisosFileGroup;

-- Tabla: Permisos
CREATE TABLE Permisos (
    ID_Permisos CHAR(8) PRIMARY KEY,
    Nombre VARCHAR(20),
    Descripcion VARCHAR(200)
) ON RolesPermisosFileGroup;

-- Tabla: Roles_Permisos con ON DELETE CASCADE
CREATE TABLE Roles_Permisos (
    ID_Roles_Permisos CHAR(8) PRIMARY KEY,
    ID_Roles CHAR(8),
    ID_Permisos CHAR(8),
    FOREIGN KEY (ID_Roles) REFERENCES Roles(ID_Roles) ON DELETE CASCADE,
    FOREIGN KEY (ID_Permisos) REFERENCES Permisos(ID_Permisos) ON DELETE CASCADE
) ON RolesPermisosFileGroup;

-- Tabla: Usuario_Roles con ON DELETE CASCADE
CREATE TABLE Usuario_Roles (
    ID_Usuario_Roles CHAR(8) PRIMARY KEY,
    ID_Usuario CHAR(8),
    ID_Roles CHAR(8),
    FOREIGN KEY (ID_Usuario) REFERENCES Usuarios(ID_Usuario) ON DELETE CASCADE,
    FOREIGN KEY (ID_Roles) REFERENCES Roles(ID_Roles) ON DELETE CASCADE
) ON RolesPermisosFileGroup;

-- Tabla: Factura_Procedimiento con ON DELETE CASCADE
CREATE TABLE Factura_Procedimiento (
    ID_Factura_Procedimiento CHAR(8) PRIMARY KEY,
    ID_Factura CHAR(8),
    ID_Procedimiento CHAR(8),
    FOREIGN KEY (ID_Factura) REFERENCES Factura(ID_Factura) ON DELETE CASCADE,
    FOREIGN KEY (ID_Procedimiento) REFERENCES Procedimiento(ID_Procedimiento) ON DELETE CASCADE
) ON FinancierosFileGroup;

-- Tabla: Factura_Tratamiento con ON DELETE CASCADE
CREATE TABLE Factura_Tratamiento (
    ID_Factura_Tratamiento CHAR(8) PRIMARY KEY,
    ID_Factura CHAR(8),
    ID_Tratamiento CHAR(8),
    FOREIGN KEY (ID_Factura) REFERENCES Factura(ID_Factura) ON DELETE CASCADE,
    FOREIGN KEY (ID_Tratamiento) REFERENCES Tratamiento(ID_Tratamiento) ON DELETE CASCADE
) ON FinancierosFileGroup;

-- Tabla: Auditoria
CREATE TABLE Auditoria (
    ID_Auditoria UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    Fecha_Hora_Accion DATETIME NOT NULL,
    Accion VARCHAR(255) NOT NULL,
    DispositivoQueRealizo VARCHAR(50) NOT NULL,
    Usuario VARCHAR(128) NOT NULL
) ON AuditoriaFileGroup;
GO
