-- 1. Vista: Historial Completo de Pacientes
IF OBJECT_ID('vw_HistorialPaciente') IS NOT NULL
    DROP VIEW vw_HistorialPaciente;
GO

CREATE VIEW vw_HistorialPaciente AS
SELECT 
    P.ID_Paciente, P.Nombre_Pac, P.Apellido1_Pac, P.Apellido2_Pac,
    P.Fecha_Nacimiento_Pac, P.Telefono_Pac, P.Correo_Pac, P.Direccion_Pac,
    C.ID_Cita, C.Fecha_Cita, C.Motivo, C.Hora_Inicio, C.Hora_Fin,
    D.Nombre_Den, D.Apellido1_Den, D.Apellido2_Den,
    HT.ID_Historial_Tratamiento, HT.Fecha_Tratamiento, 
    T.Nombre_Tra, T.Descripcion_Tra
FROM 
    Paciente P
INNER JOIN Cita C ON P.ID_Paciente = C.ID_Paciente
INNER JOIN Dentista D ON C.ID_Dentista = D.ID_Dentista
INNER JOIN Historial_Tratamiento HT ON P.ID_HistorialMedico = HT.ID_HistorialMedico
INNER JOIN Tratamiento T ON HT.ID_Tratamiento = T.ID_Tratamiento;
GO

-- 2. Vista: Facturas Pendientes de Pago
IF OBJECT_ID('vw_FacturasPendientes') IS NOT NULL
    DROP VIEW vw_FacturasPendientes;
GO

CREATE VIEW vw_FacturasPendientes AS
SELECT 
    F.ID_Factura, F.MontoTotal_Fa, F.FechaEmision_Fa, 
    E.Nombre_EP AS Estado_Pago,
    P.Nombre_Pac, P.Apellido1_Pac, P.Apellido2_Pac
FROM 
    Factura F
INNER JOIN Estado_Pago E ON F.ID_EstadoPago = E.ID_EstadoPago
INNER JOIN Cuenta C ON F.ID_Factura = C.ID_Factura
INNER JOIN Paciente P ON C.ID_Paciente = P.ID_Paciente
WHERE 
    E.Nombre_EP = 'Pendiente';
GO

-- 3. Vista: Próximas Citas
IF OBJECT_ID('vw_ProximasCitas') IS NOT NULL
    DROP VIEW vw_ProximasCitas;
GO

CREATE VIEW vw_ProximasCitas AS
SELECT 
    C.ID_Cita, C.Fecha_Cita, C.Motivo, C.Hora_Inicio, C.Hora_Fin,
    P.Nombre_Pac, P.Apellido1_Pac, P.Apellido2_Pac,
    D.Nombre_Den, D.Apellido1_Den, D.Apellido2_Den
FROM 
    Cita C
INNER JOIN Paciente P ON C.ID_Paciente = P.ID_Paciente
INNER JOIN Dentista D ON C.ID_Dentista = D.ID_Dentista
WHERE 
    C.Fecha_Cita >= GETDATE();
GO

-- 4. Vista: Tratamientos Realizados por Dentista
IF OBJECT_ID('vw_TratamientosPorDentista') IS NOT NULL
    DROP VIEW vw_TratamientosPorDentista;
GO

CREATE VIEW vw_TratamientosPorDentista AS
SELECT 
    D.ID_Dentista, D.Nombre_Den, D.Apellido1_Den, D.Apellido2_Den,
    HT.ID_Historial_Tratamiento, HT.Fecha_Tratamiento, 
    T.Nombre_Tra, T.Descripcion_Tra,
    P.Nombre_Pac, P.Apellido1_Pac, P.Apellido2_Pac
FROM 
    Dentista D
INNER JOIN Cita C ON D.ID_Dentista = C.ID_Dentista
INNER JOIN Paciente P ON C.ID_Paciente = P.ID_Paciente
INNER JOIN Historial_Tratamiento HT ON P.ID_HistorialMedico = HT.ID_HistorialMedico
INNER JOIN Tratamiento T ON HT.ID_Tratamiento = T.ID_Tratamiento;
GO

-- 5. Vista: Resumen Financiero por Paciente
IF OBJECT_ID('vw_ResumenFinancieroPaciente') IS NOT NULL
    DROP VIEW vw_ResumenFinancieroPaciente;
GO

CREATE VIEW vw_ResumenFinancieroPaciente AS
SELECT 
    P.ID_Paciente, P.Nombre_Pac, P.Apellido1_Pac, P.Apellido2_Pac,
    SUM(F.MontoTotal_Fa) AS TotalFacturado,
    SUM(ISNULL(PG.Monto_Pago, 0)) AS TotalPagado,
    SUM(F.MontoTotal_Fa) - SUM(ISNULL(PG.Monto_Pago, 0)) AS SaldoPendiente
FROM 
    Paciente P
INNER JOIN Cuenta C ON P.ID_Paciente = C.ID_Paciente
INNER JOIN Factura F ON C.ID_Factura = F.ID_Factura
LEFT JOIN Pago PG ON F.ID_Factura = PG.ID_Factura
GROUP BY 
    P.ID_Paciente, P.Nombre_Pac, P.Apellido1_Pac, P.Apellido2_Pac;
GO

-- 6. Vista: Citas por Estado
IF OBJECT_ID('vw_CitasPorEstado') IS NOT NULL
    DROP VIEW vw_CitasPorEstado;
GO

CREATE VIEW vw_CitasPorEstado AS
SELECT 
    C.ID_Cita, C.Fecha_Cita, C.Motivo, C.Hora_Inicio, C.Hora_Fin,
    P.Nombre_Pac, P.Apellido1_Pac, P.Apellido2_Pac,
    D.Nombre_Den, D.Apellido1_Den, D.Apellido2_Den,
    E.Nombre_Estado
FROM 
    Cita C
INNER JOIN Paciente P ON C.ID_Paciente = P.ID_Paciente
INNER JOIN Dentista D ON C.ID_Dentista = D.ID_Dentista
INNER JOIN Estado_Citas E ON C.ID_EstadoCita = E.ID_EstadoCita;
GO

-- 7. Vista: Facturación por Fecha
IF OBJECT_ID('vw_FacturacionPorFecha') IS NOT NULL
    DROP VIEW vw_FacturacionPorFecha;
GO

CREATE VIEW vw_FacturacionPorFecha AS
SELECT 
    F.ID_Factura, F.MontoTotal_Fa, F.FechaEmision_Fa, 
    E.Nombre_EP AS Estado_Pago,
    P.Nombre_Pac, P.Apellido1_Pac, P.Apellido2_Pac
FROM 
    Factura F
INNER JOIN Estado_Pago E ON F.ID_EstadoPago = E.ID_EstadoPago
INNER JOIN Cuenta C ON F.ID_Factura = C.ID_Factura
INNER JOIN Paciente P ON C.ID_Paciente = P.ID_Paciente
WHERE 
    F.FechaEmision_Fa BETWEEN '2024-01-01' AND '2024-12-31';
GO

-- 8. Vista: Pacientes con Tratamientos Activos
IF OBJECT_ID('vw_PacientesConTratamientosActivos') IS NOT NULL
    DROP VIEW vw_PacientesConTratamientosActivos;
GO

CREATE VIEW vw_PacientesConTratamientosActivos AS
SELECT 
    P.ID_Paciente, P.Nombre_Pac, P.Apellido1_Pac, P.Apellido2_Pac,
    T.Nombre_Tra, T.Descripcion_Tra, HT.Fecha_Tratamiento
FROM 
    Paciente P
INNER JOIN Historial_Tratamiento HT ON P.ID_HistorialMedico = HT.ID_HistorialMedico
INNER JOIN Tratamiento T ON HT.ID_Tratamiento = T.ID_Tratamiento
WHERE 
    HT.Fecha_Tratamiento >= GETDATE();
GO

-- 9. Vista: Dentistas con Más Tratamientos Realizados
-- Eliminar la vista si ya existe
IF OBJECT_ID('vw_DentistasConMasTratamientos', 'V') IS NOT NULL
    DROP VIEW vw_DentistasConMasTratamientos;
GO

-- Crear la vista
CREATE VIEW vw_DentistasConMasTratamientos AS
SELECT 
    D.ID_Dentista, 
    D.Nombre_Den, 
    D.Apellido1_Den, 
    D.Apellido2_Den,
    COUNT(HT.ID_Historial_Tratamiento) AS TotalTratamientos
FROM 
    Dentista D
INNER JOIN Cita C ON D.ID_Dentista = C.ID_Dentista
INNER JOIN Paciente P ON C.ID_Paciente = P.ID_Paciente
INNER JOIN Historial_Medico HM ON P.ID_HistorialMedico = HM.ID_HistorialMedico
INNER JOIN Historial_Tratamiento HT ON HM.ID_HistorialMedico = HT.ID_HistorialMedico
GROUP BY 
    D.ID_Dentista, 
    D.Nombre_Den, 
    D.Apellido1_Den, 
    D.Apellido2_Den
WITH CHECK OPTION;
GO

-- Consultar la vista de Historial Completo de Pacientes
SELECT * FROM vw_HistorialPaciente;
GO

-- Consultar la vista de Facturas Pendientes de Pago
SELECT * FROM vw_FacturasPendientes;
GO

-- Consultar la vista de Próximas Citas
SELECT * FROM vw_ProximasCitas;
GO

-- Consultar la vista de Tratamientos Realizados por Dentista
SELECT * FROM vw_TratamientosPorDentista;
GO

-- Consultar la vista de Resumen Financiero por Paciente
SELECT * FROM vw_ResumenFinancieroPaciente;
GO

-- Consultar la vista de Citas por Estado
SELECT * FROM vw_CitasPorEstado;
GO

-- Consultar la vista de Facturación por Fecha
SELECT * FROM vw_FacturacionPorFecha;
GO

-- Consultar la vista de Pacientes con Tratamientos Activos
SELECT * FROM vw_PacientesConTratamientosActivos;
GO

-- Consultar la vista de Dentistas con Más Tratamientos Realizados
SELECT * FROM vw_DentistasConMasTratamientos;
GO