-- 1. Insertar Funcionario
INSERT INTO Funcionario (
    ID_Funcionario, Nombre, Apellido1, Apellido2, Email, Contraseña
)
VALUES (
    'FUNC0001', 'Pedro', 'López', 'Ramírez', 
    'pedro.lopez@mail.com', 'pass1234'
);
GO

-- 2. Insertar Estado de Pago
INSERT INTO Estado_Pago (ID_EstadoPago, Nombre_EP, Descripcion_EP)
VALUES ('EST00001', 'Pendiente', 'Pago aún no realizado');
GO

-- 3. Insertar Estado de Cuenta
INSERT INTO Estado_Cuenta (ID_Estado_Cuenta, Nombre_EC, Descripcion_EC)
VALUES ('ESTC0001', 'Abierta', 'Cuenta activa sin cierre');
GO

-- 4. Insertar Estado de Cita
INSERT INTO Estado_Citas (ID_EstadoCita, Nombre_Estado, Descripcion_Estado)
VALUES ('ESTCITA1', 'Programada', 'Cita programada para tratamiento');
GO

-- 5. Insertar Tipo de Pago
INSERT INTO Tipo_Pago (ID_Tipo_Pago, Nombre_TP, Descripcion_TP)
VALUES ('TIPO0002', 'Efectivo', 'Pago en efectivo realizado en caja');
GO

-- 6. Insertar Dentista
INSERT INTO Dentista (
    ID_Dentista, Nombre_Den, Apellido1_Den, Apellido2_Den, 
    Direccion_Den, FechaNacimiento_Den, Telefono_Den, Correo_Den, ID_Funcionario
)
VALUES (
    'DEN00001', 'Juan', 'Pérez', 'García', 
    'Calle Principal 456, Ciudad X', '1980-04-22', 
    '555-987654', 'juan.perez@mail.com', 'FUNC0001'
);
GO

-- 7. Insertar Historial Médico
INSERT INTO Historial_Medico (ID_HistorialMedico, Fecha_Historial, Diagnostico, Tratamientos_Medicos)
VALUES ('HIS00001', '2024-10-10', 'Revisión general', 'Limpieza dental y ortodoncia');
GO

-- 8. Insertar Paciente
INSERT INTO Paciente (
    ID_Paciente, Nombre_Pac, Apellido1_Pac, Apellido2_Pac, 
    Fecha_Nacimiento_Pac, Edad_Pac, Telefono_Pac, 
    Correo_Pac, Direccion_Pac, ID_HistorialMedico
)
VALUES (
    'PAC00001', 'Carlos', 'Pérez', 'Gómez', 
    '1990-05-15', dbo.fn_EdadPaciente('1990-05-15'), 
    '555-1234567', 'carlos.perez@mail.com', 
    'Calle Falsa 123, Ciudad X', 'HIS00001'
);
GO

-- 9. Insertar Tipo de Tratamiento
INSERT INTO Tipo_Tratamiento (ID_TipoTratamiento, Nombre_Tipo_Tratamiento, Descripcion_Tipo_Tratamiento)
VALUES ('TIPO0001', 'Ortodoncia', 'Corrección dental');
GO

-- 10. Insertar Tratamiento
INSERT INTO Tratamiento (ID_Tratamiento, Nombre_Tra, Descripcion_Tra, ID_TipoTratamiento)
VALUES ('TRA00001', 'Ortodoncia básica', 'Colocación de brackets', 'TIPO0001');
GO

-- 11. Insertar Factura
INSERT INTO Factura (ID_Factura, MontoTotal_Fa, FechaEmision_Fa, ID_EstadoPago)
VALUES ('FAC00001', 300.00, '2024-10-15', 'EST00001');
GO

-- 12. Insertar Cuenta del Paciente
INSERT INTO Cuenta (
    ID_Cuenta, Saldo_Total, Fecha_Apertura, 
    Fecha_Cierre, Monto_Total_Facturado, 
    Fecha_Ultima_Actualizacion, Observaciones, 
    ID_Estado_Cuenta, ID_Factura, ID_Paciente
)
VALUES (
    'CUEN0001', 300.00, '2024-10-15', NULL, 
    300.00, '2024-10-15', 'Primera factura', 
    'ESTC0001', 'FAC00001', 'PAC00001'
);
GO

-- 13. Insertar Procedimiento
INSERT INTO Procedimiento (
    ID_Procedimiento, Fecha_Proc, Detalles_Proc, 
    Hora_Inicio_Proc, Hora_Fin_Proc, ID_Tratamiento
)
VALUES (
    'PROC0001', '2024-10-16', 'Colocación inicial de brackets', 
    '10:00:00', '11:30:00', 'TRA00001'
);
GO

-- 14. Insertar Cita para el Paciente
INSERT INTO Cita (
    ID_Cita, Fecha_Cita, Motivo, Hora_Inicio, 
    ID_Paciente, ID_Dentista, ID_Funcionario, ID_EstadoCita
)
VALUES (
    'CITA0001', '2024-10-16', 'Revisión y ortodoncia', '10:00:00', 
    'PAC00001', 'DEN00001', 'FUNC0001', 'ESTCITA1'
);
GO

-- 15. Insertar Pago
INSERT INTO Pago (ID_Pago, Monto_Pago, Fecha_Pago, ID_Factura, ID_Tipo_Pago)
VALUES (1, 100.00, '2024-10-17', 'FAC00001', 'TIPO0002');
GO

-- 16. Relacionar Factura y Tratamiento
INSERT INTO Factura_Tratamiento (ID_Factura_Tratamiento, ID_Factura, ID_Tratamiento)
VALUES ('FT000001', 'FAC00001', 'TRA00001');
GO

-- 17. Relacionar Factura y Procedimiento
INSERT INTO Factura_Procedimiento (ID_Factura_Procedimiento, ID_Factura, ID_Procedimiento)
VALUES ('FP000001', 'FAC00001', 'PROC0001');
GO
