-- 1. Insertar en Estado_Pago
INSERT INTO Estado_Pago (ID_EstadoPago, Nombre_EP, Descripcion_EP)
VALUES ('EP000001', 'Pendiente', 'Pago aún no realizado');
GO

-- 2. Insertar en Estado_Tratamiento
INSERT INTO Estado_Tratamiento (ID_EstadoTratamiento, Nombre_Estado, Descripcion_Estado)
VALUES ('ET000001', 'En Proceso', 'Tratamiento en curso');
GO

-- 3. Insertar en Tipo_Tratamiento
INSERT INTO Tipo_Tratamiento (ID_TipoTratamiento, Nombre_Tipo_Tratamiento, Descripcion_Tipo_Tratamiento)
VALUES ('TT000001', 'Ortodoncia', 'Tratamientos de ortodoncia');
GO

-- 4. Insertar en Estado_Citas
INSERT INTO Estado_Citas (ID_EstadoCita, Nombre_Estado, Descripcion_Estado)
VALUES ('EC000001', 'Programada', 'Cita programada');
GO

-- 5. Insertar en Estado_Cuenta
INSERT INTO Estado_Cuenta (ID_Estado_Cuenta, Nombre_EC, Descripcion_EC)
VALUES ('ECU0001', 'Abierta', 'Cuenta activa');
GO

-- 6. Insertar en Tipo_Pago
INSERT INTO Tipo_Pago (ID_Tipo_Pago, Nombre_TP, Descripcion_TP)
VALUES ('TP000001', 'Efectivo', 'Pago en efectivo');
GO

-- 7. Insertar en Funcionario
INSERT INTO Funcionario (ID_Funcionario, Nombre, Apellido1, Apellido2, Email, Contraseña)
VALUES ('FU000001', 'Pedro', 'López', 'Ramírez', 'pedro.lopez@mail.com', 'pass1234');
GO

-- 8. Insertar en Paciente
INSERT INTO Paciente (
    ID_Paciente, Nombre_Pac, Apellido1_Pac, Apellido2_Pac,
    Fecha_Nacimiento_Pac, Telefono_Pac, Correo_Pac, Direccion_Pac
)
VALUES (
    'PA000001', 'Ana', 'Gómez', 'Fernández',
    '1990-05-15', '5551234567', 'ana.gomez@mail.com', 'Calle Falsa 123'
);
GO

-- 9. Insertar en Historial_Medico
INSERT INTO Historial_Medico (
    ID_HistorialMedico, Fecha_Historial, Diagnostico, ID_Paciente
)
VALUES (
    'HM000001', '2023-10-01', 'Sin observaciones', 'PA000001'
);
GO

-- 11. Insertar en Dentista
INSERT INTO Dentista (
    ID_Dentista, Nombre_Den, Apellido1_Den, Apellido2_Den,
    Direccion_Den, FechaNacimiento_Den, Telefono_Den, Correo_Den, ID_Funcionario
)
VALUES (
    'DE000001', 'Juan', 'Pérez', 'García',
    'Calle Principal 456', '1980-04-22', '5559876543', 'juan.perez@mail.com', 'FU000001'
);
GO

-- 12. Insertar en Usuarios
INSERT INTO Usuarios (
    ID_Usuario, Nombre, Apellido1, Apellido2, Email, Contraseña, Token, ID_Funcionario
)
VALUES (
    'US000001', 'Laura', 'González', 'Martínez',
    'laura.gonzalez@mail.com', 'pwd12345', 'token123', 'FU000001'
);
GO

-- 13. Insertar en Tratamiento
INSERT INTO Tratamiento (
    ID_Tratamiento, Nombre_Tra, Descripcion_Tra,
    ID_TipoTratamiento, ID_EstadoTratamiento
)
VALUES (
    'TR000001', 'Brackets', 'Colocación de brackets',
    'TT000001', 'ET000001'
);
GO

-- 14. Insertar en Procedimiento
INSERT INTO Procedimiento (
    ID_Procedimiento, Fecha_Proc, Detalles_Proc,
    Hora_Inicio_Proc, Hora_Fin_Proc, ID_Tratamiento, ID_Paciente
)
VALUES (
    'PR000001', '2023-10-02', 'Colocación de brackets',
    '10:00:00', '11:00:00', 'TR000001', 'PA000001'
);
GO

-- 15. Insertar Factura
INSERT INTO Factura (ID_Factura, MontoTotal_Fa, FechaEmision_Fa, ID_EstadoPago)
VALUES ('FAC00001', 300.00, '2024-10-15', 'EP000001');
GO

-- 16. Insertar Cuenta
INSERT INTO Cuenta (ID_Cuenta, Saldo_Total, Fecha_Apertura, Fecha_Cierre, 
                    Fecha_Ultima_Actualizacion, Observaciones, 
                    ID_Estado_Cuenta, ID_Factura, ID_Paciente)
VALUES ('CUEN0001', 300.00, '2024-10-15', NULL, '2024-10-15', 
        'Primera factura', 'ECU0001', 'FAC00001', 'PA000001');
GO

-- 17. Insertar Pago
INSERT INTO Pago (Monto_Pago, Fecha_Pago, ID_Factura, ID_Tipo_Pago)
VALUES (100.00, '2024-10-17', 'FAC00001', 'TP000001');
GO

-- 18. Insertar en Cita
INSERT INTO Cita (
    ID_Cita, Fecha_Cita, Motivo,
    Hora_Inicio, Hora_Fin, ID_Paciente,
    ID_Dentista, ID_Funcionario, ID_EstadoCita
)
VALUES (
    'CI000001', '2023-10-03', 'Revisión de brackets',
    '09:00:00', '09:30:00', 'PA000001',
    'DE000001', 'FU000001', 'EC000001'
);
GO

-- 19. Insertar en Factura_Procedimiento
INSERT INTO Factura_Procedimiento (
    ID_Factura_Procedimiento, ID_Factura, ID_Procedimiento
)
VALUES (
    'FP000001', 'FAC00001', 'PR000001'
);
GO

-- 20. Insertar en Factura_Tratamiento
INSERT INTO Factura_Tratamiento (
    ID_Factura_Tratamiento, ID_Factura, ID_Tratamiento
)
VALUES (
    'FT000001', 'FAC00001', 'TR000001'
);
GO

-- 21. Insertar en Roles
INSERT INTO Roles (ID_Roles, Nombre, Descripcion)
VALUES ('RO000001', 'Administrador', 'Acceso total');
GO

-- 22. Insertar en Permisos
INSERT INTO Permisos (ID_Permisos, Nombre, Descripcion)
VALUES ('PE000001', 'Ver Pacientes', 'Permite ver información de pacientes');
GO

-- 23. Insertar en Roles_Permisos
INSERT INTO Roles_Permisos (
    ID_Roles_Permisos, ID_Roles, ID_Permisos
)
VALUES (
    'RP000001', 'RO000001', 'PE000001'
);
GO

-- 24. Insertar en Usuario_Roles
INSERT INTO Usuario_Roles (
    ID_Usuario_Roles, ID_Usuario, ID_Roles
)
VALUES (
    'UR000001', 'US000001', 'RO000001'
);
GO

-- 25. Insertar en Auditoria
INSERT INTO Auditoria (
    ID_Auditoria, Fecha_Hora_Accion, Accion,
    DispositivoQueRealizo, Usuario
)
VALUES (
    NEWID(), GETDATE(), 'Inserción de datos iniciales',
    'Equipo1', 'US000001'
);
GO