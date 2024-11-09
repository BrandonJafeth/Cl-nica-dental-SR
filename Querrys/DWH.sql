USE master;
GO
DROP DATABASE IF EXISTS ClinicaDentalDWH;
GO

-- Create the Data Warehouse Database
CREATE DATABASE ClinicaDentalDWH;
GO

-- Use the Data Warehouse Database
USE ClinicaDentalDWH;
GO

-- Create Dimensions

-- DimFecha
CREATE TABLE DimFecha (
    Fecha DATE PRIMARY KEY,
    Año INT,
    Mes INT,
    Dia INT,
    Trimestre INT,
    Semana INT,
    DiaSemana VARCHAR(10)
);

-- DimPaciente
CREATE TABLE DimPaciente (
    ID_Paciente CHAR(8) PRIMARY KEY,
    Nombre_Pac VARCHAR(50),
    Apellido1_Pac VARCHAR(50),
    Apellido2_Pac VARCHAR(50),
    Fecha_Nacimiento_Pac DATE,
    Telefono_Pac VARCHAR(20),
    Correo_Pac VARCHAR(50),
    Direccion_Pac VARCHAR(200)
);

-- DimDentista
CREATE TABLE DimDentista (
    ID_Dentista CHAR(8) PRIMARY KEY,
    Nombre_Den VARCHAR(50),
    Apellido1_Den VARCHAR(50),
    Apellido2_Den VARCHAR(50),
    Direccion_Den VARCHAR(200),
    FechaNacimiento_Den DATE,
    Telefono_Den VARCHAR(20),
    Correo_Den VARCHAR(50)
);

-- DimFuncionario
CREATE TABLE DimFuncionario (
    ID_Funcionario CHAR(8) PRIMARY KEY,
    Nombre VARCHAR(50),
    Apellido1 VARCHAR(50),
    Apellido2 VARCHAR(50),
    Email VARCHAR(50),
    Contraseña VARCHAR(50)
);

-- DimEstadoCita
CREATE TABLE DimEstadoCita (
    ID_EstadoCita CHAR(8) PRIMARY KEY,
    Nombre_Estado VARCHAR(50),
    Descripcion_Estado VARCHAR(200)
);

-- DimTipoCita
CREATE TABLE DimTipoCita (
    ID_TipoCita CHAR(8) PRIMARY KEY,
    Nombre_TipoCita VARCHAR(50),
    Descripcion_TipoCita VARCHAR(200)
);

-- DimTipoTratamiento
CREATE TABLE DimTipoTratamiento (
    ID_TipoTratamiento CHAR(8) PRIMARY KEY,
    Nombre_Tipo_Tratamiento VARCHAR(50),
    Descripcion_Tipo_Tratamiento VARCHAR(200)
);

-- DimEstadoTratamiento
CREATE TABLE DimEstadoTratamiento (
    ID_EstadoTratamiento CHAR(8) PRIMARY KEY,
    Nombre_Estado VARCHAR(50),
    Descripcion_Estado VARCHAR(200)
);

-- DimTratamiento
CREATE TABLE DimTratamiento (
    ID_Tratamiento CHAR(8) PRIMARY KEY,
    Nombre_Tra VARCHAR(50),
    Descripcion_Tra VARCHAR(200),
    ID_TipoTratamiento CHAR(8),
    ID_EstadoTratamiento CHAR(8),
    FOREIGN KEY (ID_TipoTratamiento) REFERENCES DimTipoTratamiento(ID_TipoTratamiento),
    FOREIGN KEY (ID_EstadoTratamiento) REFERENCES DimEstadoTratamiento(ID_EstadoTratamiento)
);

-- DimProcedimiento
CREATE TABLE DimProcedimiento (
    ID_Procedimiento CHAR(8) PRIMARY KEY,
    Fecha_Proc DATE,
    Detalles_Proc VARCHAR(500),
    Hora_Inicio_Proc TIME,
    Hora_Fin_Proc TIME,
    ID_Tratamiento CHAR(8),
    FOREIGN KEY (ID_Tratamiento) REFERENCES DimTratamiento(ID_Tratamiento),
    FOREIGN KEY (Fecha_Proc) REFERENCES DimFecha(Fecha)
);

-- DimEstadoPago
CREATE TABLE DimEstadoPago (
    ID_EstadoPago CHAR(8) PRIMARY KEY,
    Nombre_EP VARCHAR(50),
    Descripcion_EP VARCHAR(200)
);

-- DimTipoPago
CREATE TABLE DimTipoPago (
    ID_Tipo_Pago CHAR(8) PRIMARY KEY,
    Nombre_TP VARCHAR(50),
    Descripcion_TP VARCHAR(200)
);

-- DimHistorialMedico
CREATE TABLE DimHistorialMedico (
    ID_HistorialMedico CHAR(8) PRIMARY KEY,
    ID_Paciente CHAR(8),
    Fecha_Creacion DATE,
    Observaciones VARCHAR(500),
    FOREIGN KEY (ID_Paciente) REFERENCES DimPaciente(ID_Paciente),
    FOREIGN KEY (Fecha_Creacion) REFERENCES DimFecha(Fecha)
);

-- DimUsuario
CREATE TABLE DimUsuario (
    ID_Usuario CHAR(8) PRIMARY KEY,
    Nombre_Usuario VARCHAR(50),
    Rol VARCHAR(50)
);

-- DimFactura
CREATE TABLE DimFactura (
    ID_Factura UNIQUEIDENTIFIER PRIMARY KEY,
    MontoTotal_Fa MONEY,
    FechaEmision_Fa DATE,
    ID_EstadoPago CHAR(8),
    FOREIGN KEY (ID_EstadoPago) REFERENCES DimEstadoPago(ID_EstadoPago),
    FOREIGN KEY (FechaEmision_Fa) REFERENCES DimFecha(Fecha)
);

-- DimPago
CREATE TABLE DimPago (
    ID_Pago UNIQUEIDENTIFIER PRIMARY KEY,
    Monto_Pago MONEY,
    Fecha_Pago DATE,
    ID_Factura UNIQUEIDENTIFIER,
    ID_Tipo_Pago CHAR(8),
    FOREIGN KEY (ID_Factura) REFERENCES DimFactura(ID_Factura),
    FOREIGN KEY (ID_Tipo_Pago) REFERENCES DimTipoPago(ID_Tipo_Pago),
    FOREIGN KEY (Fecha_Pago) REFERENCES DimFecha(Fecha)
);

-- DimAuditoria
CREATE TABLE DimAuditoria (
    ID_Auditoria UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    Fecha_Hora_Accion DATETIME NOT NULL,
    Accion VARCHAR(255) NOT NULL,
    DispositivoQueRealizo VARCHAR(50) NOT NULL,
    Usuario VARCHAR(128) NOT NULL
);

-- DimCita
CREATE TABLE DimCita (
    ID_Cita CHAR(8) PRIMARY KEY,
    Fecha_Cita DATE,
    Hora_Inicio TIME,
    Hora_Fin TIME,
    ID_Paciente CHAR(8),
    ID_Dentista CHAR(8),
    ID_Funcionario CHAR(8),
    ID_EstadoCita CHAR(8),
    ID_TipoCita CHAR(8),
    FOREIGN KEY (ID_Paciente) REFERENCES DimPaciente(ID_Paciente),
    FOREIGN KEY (ID_Dentista) REFERENCES DimDentista(ID_Dentista),
    FOREIGN KEY (ID_Funcionario) REFERENCES DimFuncionario(ID_Funcionario),
    FOREIGN KEY (ID_EstadoCita) REFERENCES DimEstadoCita(ID_EstadoCita),
    FOREIGN KEY (ID_TipoCita) REFERENCES DimTipoCita(ID_TipoCita),
    FOREIGN KEY (Fecha_Cita) REFERENCES DimFecha(Fecha)
);

-- Create Fact Tables

-- FactCita
CREATE TABLE FactCita (
    ID_Cita CHAR(8),
    ID_Paciente CHAR(8),
    ID_Dentista CHAR(8),
    ID_Funcionario CHAR(8),
    ID_TipoCita CHAR(8),
    ID_EstadoCita CHAR(8),
    Fecha_Cita DATE,
    Hora_Inicio TIME,
    Hora_Fin TIME,
    PRIMARY KEY (ID_Cita),
    FOREIGN KEY (ID_Cita) REFERENCES DimCita(ID_Cita),
    FOREIGN KEY (ID_Paciente) REFERENCES DimPaciente(ID_Paciente),
    FOREIGN KEY (ID_Dentista) REFERENCES DimDentista(ID_Dentista),
    FOREIGN KEY (ID_Funcionario) REFERENCES DimFuncionario(ID_Funcionario),
    FOREIGN KEY (ID_TipoCita) REFERENCES DimTipoCita(ID_TipoCita),
    FOREIGN KEY (ID_EstadoCita) REFERENCES DimEstadoCita(ID_EstadoCita),
    FOREIGN KEY (Fecha_Cita) REFERENCES DimFecha(Fecha)
);

-- FactTratamiento
CREATE TABLE FactTratamiento (
    ID_Tratamiento CHAR(8),
    ID_Paciente CHAR(8),
    ID_HistorialMedico CHAR(8),
    Fecha_Tratamiento DATE,
    PRIMARY KEY (ID_Tratamiento),
    FOREIGN KEY (ID_Tratamiento) REFERENCES DimTratamiento(ID_Tratamiento),
    FOREIGN KEY (ID_Paciente) REFERENCES DimPaciente(ID_Paciente),
    FOREIGN KEY (ID_HistorialMedico) REFERENCES DimHistorialMedico(ID_HistorialMedico),
    FOREIGN KEY (Fecha_Tratamiento) REFERENCES DimFecha(Fecha)
);

-- FactProcedimiento
CREATE TABLE FactProcedimiento (
    ID_Procedimiento CHAR(8),
    ID_Tratamiento CHAR(8),
    Fecha_Proc DATE,
    Hora_Inicio_Proc TIME,
    Hora_Fin_Proc TIME,
    PRIMARY KEY (ID_Procedimiento),
    FOREIGN KEY (ID_Procedimiento) REFERENCES DimProcedimiento(ID_Procedimiento),
    FOREIGN KEY (ID_Tratamiento) REFERENCES DimTratamiento(ID_Tratamiento),
    FOREIGN KEY (Fecha_Proc) REFERENCES DimFecha(Fecha)
);

-- FactFactura
CREATE TABLE FactFactura (
    ID_Factura UNIQUEIDENTIFIER,
    ID_Paciente CHAR(8),
    MontoTotal_Fa MONEY,
    FechaEmision_Fa DATE,
    ID_EstadoPago CHAR(8),
    PRIMARY KEY (ID_Factura),
    FOREIGN KEY (ID_Factura) REFERENCES DimFactura(ID_Factura),
    FOREIGN KEY (ID_Paciente) REFERENCES DimPaciente(ID_Paciente),
    FOREIGN KEY (ID_EstadoPago) REFERENCES DimEstadoPago(ID_EstadoPago),
    FOREIGN KEY (FechaEmision_Fa) REFERENCES DimFecha(Fecha)
);

-- FactPago
CREATE TABLE FactPago (
    ID_Pago UNIQUEIDENTIFIER,
    ID_Factura UNIQUEIDENTIFIER,
    Monto_Pago MONEY,
    Fecha_Pago DATE,
    ID_Tipo_Pago CHAR(8),
    PRIMARY KEY (ID_Pago),
    FOREIGN KEY (ID_Pago) REFERENCES DimPago(ID_Pago),
    FOREIGN KEY (ID_Factura) REFERENCES DimFactura(ID_Factura),
    FOREIGN KEY (ID_Tipo_Pago) REFERENCES DimTipoPago(ID_Tipo_Pago),
    FOREIGN KEY (Fecha_Pago) REFERENCES DimFecha(Fecha)
);

-- FactAccionUsuario
CREATE TABLE FactAccionUsuario (
    ID_AccionUsuario INT PRIMARY KEY IDENTITY(1,1),
    ID_Auditoria UNIQUEIDENTIFIER,
    Fecha DATE,
    Detalle VARCHAR(500),
    FOREIGN KEY (ID_Auditoria) REFERENCES DimAuditoria(ID_Auditoria),
    FOREIGN KEY (Fecha) REFERENCES DimFecha(Fecha)
);