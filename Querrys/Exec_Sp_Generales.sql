--exec sp generales

-- Insertar Historial_Medico 1
EXEC InsertHistorialMedico 
    @ID_HistorialMedico = 'HIS001',
    @Fecha_Historial = '2023-01-01',
    @Diagnostico = 'Caries en molar izquierdo',
    @Tratamientos_Medicos = 'Empaste dental';

-- Insertar Historial_Medico 2
EXEC InsertHistorialMedico 
    @ID_HistorialMedico = 'HIS002',
    @Fecha_Historial = '2023-02-15',
    @Diagnostico = 'Gingivitis',
    @Tratamientos_Medicos = 'Limpieza profunda';

-- Actualizar Historial_Medico
EXEC UpdateHistorialMedico 
    @ID_HistorialMedico = 'HIS001',
    @Fecha_Historial = '2023-01-01',
    @Diagnostico = 'Caries en molar derecho',
    @Tratamientos_Medicos = 'Empaste dental';

-- Eliminar Historial_Medico
EXEC DeleteHistorialMedico @ID_HistorialMedico = 'HIS002';



-- Insertar Paciente 1
EXEC InsertPaciente 
    @ID_Paciente = 'PAC001',
    @Nombre_Pac = 'Juan',
    @Apellido1_Pac = 'Pérez',
    @Apellido2_Pac = 'Gómez',
    @Fecha_Nacimiento_Pac = '1990-01-01',
    @Telefono_Pac = '1234567890',
    @Correo_Pac = 'juan.perez@example.com',
    @Direccion_Pac = '123 Calle Falsa',
    @ID_HistorialMedico = 'HIS001';

-- Insertar Paciente 2
EXEC InsertPaciente 
    @ID_Paciente = 'PAC002',
    @Nombre_Pac = 'Maria',
    @Apellido1_Pac = 'López',
    @Apellido2_Pac = 'Martínez',
    @Fecha_Nacimiento_Pac = '1985-06-15',
    @Telefono_Pac = '0987654321',
    @Correo_Pac = 'maria.lopez@example.com',
    @Direccion_Pac = '456 Calle Real',
    @ID_HistorialMedico = 'HIS002';

-- Actualizar Paciente
EXEC UpdatePaciente 
    @ID_Paciente = 'PAC001',
    @Nombre_Pac = 'Juan Carlos',
    @Apellido1_Pac = 'Pérez',
    @Apellido2_Pac = 'Gómez',
    @Fecha_Nacimiento_Pac = '1990-01-01',
    @Telefono_Pac = '1234567890',
    @Correo_Pac = 'juancarlos.perez@example.com',
    @Direccion_Pac = '123 Calle Falsa',
    @ID_HistorialMedico = 'HIS001';

-- Eliminar Paciente
EXEC DeletePaciente @ID_Paciente = 'PAC002';



-- Insertar Estado_Pago 1
EXEC InsertEstadoPago 
    @ID_EstadoPago = 'EST001',
    @Nombre_EP = 'Pendiente',
    @Descripcion_EP = 'Pago pendiente de confirmación';

-- Insertar Estado_Pago 2
EXEC InsertEstadoPago 
    @ID_EstadoPago = 'EST002',
    @Nombre_EP = 'Pagado',
    @Descripcion_EP = 'Pago confirmado';

-- Actualizar Estado_Pago
EXEC UpdateEstadoPago 
    @ID_EstadoPago = 'EST001',
    @Nombre_EP = 'En proceso',
    @Descripcion_EP = 'Pago en proceso de confirmación';

-- Eliminar Estado_Pago
EXEC DeleteEstadoPago @ID_EstadoPago = 'EST002';




-- Insertar Tipo_Tratamiento 1
EXEC InsertTipoTratamiento 
    @ID_TipoTratamiento = 'TIP001',
    @Nombre_Tipo_Tratamiento = 'Ortopedia',
    @Descripcion_Tipo_Tratamiento = 'Tratamiento de ortopedia dental';

-- Insertar Tipo_Tratamiento 2
EXEC InsertTipoTratamiento 
    @ID_TipoTratamiento = 'TIP002',
    @Nombre_Tipo_Tratamiento = 'Endodoncia',
    @Descripcion_Tipo_Tratamiento = 'Tratamiento de endodoncia';

-- Actualizar Tipo_Tratamiento
EXEC UpdateTipoTratamiento 
    @ID_TipoTratamiento = 'TIP001',
    @Nombre_Tipo_Tratamiento = 'Ortodoncia',
    @Descripcion_Tipo_Tratamiento = 'Tratamiento de ortodoncia dental';

-- Eliminar Tipo_Tratamiento
EXEC DeleteTipoTratamiento @ID_TipoTratamiento = 'TIP002';



-- Insertar Tratamiento 1
EXEC InsertTratamiento 
    @ID_Tratamiento = 'TRA001',
    @Nombre_Tra = 'Brackets',
    @Descripcion_Tra = 'Aplicación de brackets',
    @ID_TipoTratamiento = 'TIP001';

-- Insertar Tratamiento 2
EXEC InsertTratamiento 
    @ID_Tratamiento = 'TRA002',
    @Nombre_Tra = 'Implante dental',
    @Descripcion_Tra = 'Implante de diente artificial',
    @ID_TipoTratamiento = 'TIP002';

-- Actualizar Tratamiento
EXEC UpdateTratamiento 
    @ID_Tratamiento = 'TRA001',
    @Nombre_Tra = 'Brackets Metálicos',
    @Descripcion_Tra = 'Aplicación de brackets metálicos',
    @ID_TipoTratamiento = 'TIP001';

-- Eliminar Tratamiento
EXEC DeleteTratamiento @ID_Tratamiento = 'TRA002';




-- Insertar Procedimiento 1
EXEC InsertProcedimiento 
    @ID_Procedimiento = 'PROC001',
    @Fecha_Proc = '2024-11-10',
    @Detalles_Proc = 'Aplicación de anestesia local',
    @Hora_Inicio_Proc = '09:00',
    @Hora_Fin_Proc = '09:30',
    @ID_Tratamiento = 'TRA001';

-- Insertar Procedimiento 2
EXEC InsertProcedimiento 
    @ID_Procedimiento = 'PROC002',
    @Fecha_Proc = '2024-11-11',
    @Detalles_Proc = 'Extracción dental',
    @Hora_Inicio_Proc = '10:00',
    @Hora_Fin_Proc = '10:45',
    @ID_Tratamiento = 'TRA002';

-- Actualizar Procedimiento
EXEC UpdateProcedimiento 
    @ID_Procedimiento = 'PROC001',
    @Fecha_Proc = '2024-11-10',
    @Detalles_Proc = 'Aplicación de anestesia general',
    @Hora_Inicio_Proc = '09:00',
    @Hora_Fin_Proc = '09:45',
    @ID_Tratamiento = 'TRA001';

-- Eliminar Procedimiento
EXEC DeleteProcedimiento @ID_Procedimiento = 'PROC002';




-- Insertar Factura 1
EXEC InsertFactura 
    @ID_Factura = 'FAC001',
    @MontoTotal_Fa = 2500.00,
    @FechaEmision_Fa = '2024-11-05',
    @ID_EstadoPago = 'EST001';

-- Insertar Factura 2
EXEC InsertFactura 
    @ID_Factura = 'FAC002',
    @MontoTotal_Fa = 1500.00,
    @FechaEmision_Fa = '2024-11-06',
    @ID_EstadoPago = 'EST002';

-- Actualizar Factura
EXEC UpdateFactura 
    @ID_Factura = 'FAC001',
    @MontoTotal_Fa = 2700.00,
    @FechaEmision_Fa = '2024-11-07',
    @ID_EstadoPago = 'EST002';

-- Eliminar Factura
EXEC DeleteFactura @ID_Factura = 'FAC002';




-- Insertar Tipo_Pago 1
EXEC InsertTipoPago 
    @ID_Tipo_Pago = 'TP001',
    @Nombre_TP = 'Efectivo',
    @Descripcion_TP = 'Pago en efectivo';

-- Insertar Tipo_Pago 2
EXEC InsertTipoPago 
    @ID_Tipo_Pago = 'TP002',
    @Nombre_TP = 'Tarjeta de crédito',
    @Descripcion_TP = 'Pago con tarjeta de crédito';

-- Actualizar Tipo_Pago
EXEC UpdateTipoPago 
    @ID_Tipo_Pago = 'TP001',
    @Nombre_TP = 'Efectivo Dólares',
    @Descripcion_TP = 'Pago en efectivo en dólares';

-- Eliminar Tipo_Pago
EXEC DeleteTipoPago @ID_Tipo_Pago = 'TP002';


-- Insertar Pago 1
EXEC InsertPago 

    @Monto_Pago = 500.00,
    @Fecha_Pago = '2024-11-05',
    @ID_Factura = 'FAC001',
    @ID_Tipo_Pago = 'TP001';

-- Insertar Pago 2
EXEC InsertPago 

    @Monto_Pago = 300.00,
    @Fecha_Pago = '2024-11-06',
    @ID_Factura = 'FAC002',
    @ID_Tipo_Pago = 'TP002';

-- Actualizar Pago
EXEC UpdatePago 
    @ID_Pago = 1,
    @Monto_Pago = 600.00,
    @Fecha_Pago = '2024-11-05',
    @ID_Factura = 'FAC001',
    @ID_Tipo_Pago = 'TP001';

-- Eliminar Pago
EXEC DeletePago @ID_Pago = 2;




-- Insertar Estado_Cuenta 1
EXEC InsertEstadoCuenta 
    @ID_Estado_Cuenta = 'EC001',
    @Nombre_EC = 'Activa',
    @Descripcion_EC = 'Cuenta activa';

-- Insertar Estado_Cuenta 2
EXEC InsertEstadoCuenta 
    @ID_Estado_Cuenta = 'EC002',
    @Nombre_EC = 'Inactiva',
    @Descripcion_EC = 'Cuenta inactiva';

-- Actualizar Estado_Cuenta
EXEC UpdateEstadoCuenta 
    @ID_Estado_Cuenta = 'EC001',
    @Nombre_EC = 'Suspendida',
    @Descripcion_EC = 'Cuenta suspendida temporalmente';

-- Eliminar Estado_Cuenta
EXEC DeleteEstadoCuenta @ID_Estado_Cuenta = 'EC002';








-- Insertar Estado_Citas 1
EXEC InsertEstadoCitas 
    @ID_EstadoCita = 'CIT001',
    @Nombre_Estado = 'Pendiente',
    @Descripcion_Estado = 'Cita pendiente de confirmación';

-- Insertar Estado_Citas 2
EXEC InsertEstadoCitas 
    @ID_EstadoCita = 'CIT002',
    @Nombre_Estado = 'Confirmada',
    @Descripcion_Estado = 'Cita confirmada';

-- Actualizar Estado_Citas
EXEC UpdateEstadoCitas 
    @ID_EstadoCita = 'CIT001',
    @Nombre_Estado = 'Reprogramada',
    @Descripcion_Estado = 'Cita reprogramada por el paciente';

-- Eliminar Estado_Citas
EXEC DeleteEstadoCitas @ID_EstadoCita = 'CIT002';



-- Insertar Especialidad 1
EXEC InsertEspecialidad 
    @ID_Especialidad = 'ESP001',
    @Nombre_Esp = 'Ortodoncia',
    @Descripcion_Esp = 'Tratamiento de alineación dental';

-- Insertar Especialidad 2
EXEC InsertEspecialidad 
    @ID_Especialidad = 'ESP002',
    @Nombre_Esp = 'Endodoncia',
    @Descripcion_Esp = 'Tratamiento de conductos';

-- Actualizar Especialidad
EXEC UpdateEspecialidad 
    @ID_Especialidad = 'ESP001',
    @Nombre_Esp = 'Ortodoncia Infantil',
    @Descripcion_Esp = 'Tratamiento de alineación dental para niños';

-- Eliminar Especialidad
EXEC DeleteEspecialidad @ID_Especialidad = 'ESP002';


-- Insertar Funcionario 1
EXEC InsertFuncionario 
    @ID_Funcionario = 'FUN001',
    @Nombre = 'Roberto',
    @Apellido1 = 'González',
    @Apellido2 = 'Pérez',
    @Email = 'roberto@example.com',
    @Contraseña = 'func1234';

-- Insertar Funcionario 2
EXEC InsertFuncionario 
    @ID_Funcionario = 'FUN002',
    @Nombre = 'Lucía',
    @Apellido1 = 'Ramírez',
    @Apellido2 = 'López',
    @Email = 'lucia@example.com',
    @Contraseña = 'func5678';

-- Actualizar Funcionario
EXEC UpdateFuncionario 
    @ID_Funcionario = 'FUN001',
    @Nombre = 'Roberto',
    @Apellido1 = 'González',
    @Apellido2 = 'Martínez',
    @Email = 'roberto.new@example.com',
    @Contraseña = 'newpass123';

-- Eliminar Funcionario
EXEC DeleteFuncionario @ID_Funcionario = 'FUN002';

-- Ejemplo de inserción
EXEC InsertDentista 
    @ID_Dentista = 'DENT0001',
    @Nombre_Den = 'Carlos',
    @Apellido1_Den = 'Pérez',
    @Apellido2_Den = 'Lopez',
    @Direccion_Den = 'Av. Las Flores 123',
    @FechaNacimiento_Den = '1980-05-20',
    @Telefono_Den = '555-1234',
    @Correo_Den = 'carlos.perez@example.com',
    @ID_Funcionario = 'FUN001';

	EXEC InsertDentista 
    @ID_Dentista = 'DENT0002',
    @Nombre_Den = 'Alvaro',
    @Apellido1_Den = 'Gómez',
    @Apellido2_Den = 'Arrieta',
    @Direccion_Den = 'Av. Las Flores 123',
    @FechaNacimiento_Den = '1985-07-20',
    @Telefono_Den = '555-1234',
    @Correo_Den = 'alvaro.gomez@example.com',
    @ID_Funcionario = 'FUN002';

-- Ejemplo de actualización
EXEC UpdateDentista 
    @ID_Dentista = 'DENT0001',
    @Nombre_Den = 'Carlos',
    @Apellido1_Den = 'Pérez',
    @Apellido2_Den = 'Martinez',
    @Direccion_Den = 'Av. Las Flores 456',
    @FechaNacimiento_Den = '1980-05-20',
    @Telefono_Den = '555-5678',
    @Correo_Den = 'carlos.perez@example.com',
    @ID_Funcionario = 'FUN001';

-- Ejemplo de eliminación
EXEC DeleteDentista 
    @ID_Dentista = 'DENT0001';



-- Insertar Dentista_Especialidad 1
EXEC InsertDentistaEspecialidad 
    @ID_Dentista_Especialidad = 'DE001',
    @ID_Dentista = 'DENT0001',
    @ID_Especialidad = 'ESP001';

-- Insertar Dentista_Especialidad 2
EXEC InsertDentistaEspecialidad 
    @ID_Dentista_Especialidad = 'DE002',
    @ID_Dentista = 'DENT0002',
    @ID_Especialidad = 'ESP002';

-- Actualizar Dentista_Especialidad
EXEC UpdateDentistaEspecialidad 
    @ID_Dentista_Especialidad = 'DE001',
    @ID_Dentista = 'DENT0001',
    @ID_Especialidad = 'ESP002';

-- Eliminar Dentista_Especialidad
EXEC DeleteDentistaEspecialidad @ID_Dentista_Especialidad = 'DENT0002';



-- Insertar Usuario 1
EXEC InsertUsuario 
    @ID_Usuario = 'USR001',
    @Nombre = 'Carlos',
    @Apellido1 = 'Fernández',
    @Apellido2 = 'Ruiz',
    @Email = 'carlos@example.com',
    @Contraseña = 'pass1234',
    @Token = 'abc123token',
    @ID_Funcionario = 'FUN001';

-- Insertar Usuario 2
EXEC InsertUsuario 
    @ID_Usuario = 'USR002',
    @Nombre = 'Ana',
    @Apellido1 = 'Sánchez',
    @Apellido2 = 'García',
    @Email = 'ana@example.com',
    @Contraseña = 'pass5678',
    @Token = 'def456token',
    @ID_Funcionario = 'FUN002';

-- Actualizar Usuario
EXEC UpdateUsuario 
    @ID_Usuario = 'USR001',
    @Nombre = 'Carlos',
    @Apellido1 = 'Fernández',
    @Apellido2 = 'Gómez',
    @Email = 'carlos@example.com',
    @Contraseña = 'newpass123',
    @Token = 'xyz789token',
    @ID_Funcionario = 'FUN001';

-- Eliminar Usuario
EXEC DeleteUsuario @ID_Usuario = 'USR002';








-- Insertar Factura_Procedimiento 1
EXEC InsertFacturaProcedimiento 
    @ID_Factura_Procedimiento = 'FP001',
    @ID_Factura = 'FAC001',
    @ID_Procedimiento = 'PROC001';

-- Insertar Factura_Procedimiento 2
EXEC InsertFacturaProcedimiento 
    @ID_Factura_Procedimiento = 'FP002',
    @ID_Factura = 'FAC002',
    @ID_Procedimiento = 'PROC002';

-- Actualizar Factura_Procedimiento
EXEC UpdateFacturaProcedimiento 
    @ID_Factura_Procedimiento = 'FP001',
    @ID_Factura = 'FAC001',
    @ID_Procedimiento = 'PROC002';

-- Eliminar Factura_Procedimiento
EXEC DeleteFacturaProcedimiento @ID_Factura_Procedimiento = 'FP002';



-- Insertar Factura_Tratamiento 1
EXEC InsertFacturaTratamiento 
    @ID_Factura_Tratamiento = 'FT001',
    @ID_Factura = 'FAC001',
    @ID_Tratamiento = 'TRA001';

-- Insertar Factura_Tratamiento 2
EXEC InsertFacturaTratamiento 
    @ID_Factura_Tratamiento = 'FT002',
    @ID_Factura = 'FAC002',
    @ID_Tratamiento = 'TRA002';

-- Actualizar Factura_Tratamiento
EXEC UpdateFacturaTratamiento 
    @ID_Factura_Tratamiento = 'FT001',
    @ID_Factura = 'FAC001',
    @ID_Tratamiento = 'TRA002';

-- Eliminar Factura_Tratamiento
EXEC DeleteFacturaTratamiento @ID_Factura_Tratamiento = 'FT002';



-- Insertar Historial_Tratamiento 1
EXEC InsertHistorialTratamiento 
    @ID_Historial_Tratamiento = 'HT001',
    @ID_HistorialMedico = 'HIS001',
    @ID_Tratamiento = 'TRA001',
    @Fecha_Tratamiento = '2023-03-01';

-- Insertar Historial_Tratamiento 2
EXEC InsertHistorialTratamiento 
    @ID_Historial_Tratamiento = 'HT002',
    @ID_HistorialMedico = 'HIS002',
    @ID_Tratamiento = 'TRA002',
    @Fecha_Tratamiento = '2023-04-01';

-- Actualizar Historial_Tratamiento
EXEC UpdateHistorialTratamiento 
    @ID_Historial_Tratamiento = 'HT001',
    @ID_HistorialMedico = 'HIS001',
    @ID_Tratamiento = 'TRA002',
    @Fecha_Tratamiento = '2023-03-15';

-- Eliminar Historial_Tratamiento
EXEC DeleteHistorialTratamiento @ID_Historial_Tratamiento = 'HT002';






