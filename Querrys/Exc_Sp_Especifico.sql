-- Ejecutar sp_BuscarPacientePorNombre
EXEC sp_BuscarPacientePorNombre @Nombre_Pac = 'Juan';

-- Ejecutar sp_AgendarCita
EXEC sp_AgendarCita 
    @ID_Cita = 'CIT001',
    @Fecha_Cita = '2024-12-01',
    @Hora_Inicio = '10:00',
    @Hora_Fin = '10:30',
    @ID_Paciente = 'PAC001',
    @ID_Dentista = 'DENT0001',
    @ID_Funcionario = 'FUN001',
    @ID_EstadoCita = 'CIT001';

-- Ejecutar sp_CancelarCita
EXEC sp_CancelarCita @ID_Cita = 'CIT001';

-- Ejecutar sp_ConsultarTratamientosPorPaciente
EXEC sp_ConsultarTratamientosPorPaciente @ID_Paciente = 'PAC001';

-- Ejecutar sp_ReprogramarCita
EXEC sp_ReprogramarCita 
    @ID_Cita = 'CIT001',
    @Nueva_Fecha_Cita = '2024-12-05',
    @Nueva_Hora_Inicio = '11:00',
    @Nueva_Hora_Fin = '11:30';

-- Ejecutar sp_ConsultarCitasPorFecha
EXEC sp_ConsultarCitasPorFecha @Fecha_Cita = '2024-12-01';

-- Ejecutar sp_ActualizarEstadoTratamiento
EXEC sp_ActualizarEstadoTratamiento 
    @ID_Tratamiento = 'TRA001',
    @Nuevo_Estado = 'Completado';

-- Ejecutar sp_GenerarFactura
EXEC sp_GenerarFactura 
    @ID_Factura = 'FAC003',
    @MontoTotal = 2000.00,
    @FechaEmision = '2024-12-01',
    @ID_EstadoPago = 'EST001',
    @ID_Cuenta = 'EC001';


-- Ejecutar sp_ActualizarSaldoCuenta
EXEC sp_ActualizarSaldoCuenta @ID_Cuenta = 'EC001';

-- Ejecutar sp_ActualizarContactoPaciente
EXEC sp_ActualizarContactoPaciente 
    @ID_Paciente = 'PAC001',
    @Telefono = '9876543210',
    @Correo = 'juan.updated@example.com',
    @Direccion = '456 Calle Nueva';

-- Ejecutar sp_ConsultarCitasPorPaciente
EXEC sp_ConsultarCitasPorPaciente @ID_Paciente = 'PAC001';

-- Ejecutar sp_GenerarReporteFacturacion
EXEC sp_GenerarReporteFacturacion 
    @FechaInicio = '2024-01-01',
    @FechaFin = '2024-12-31';
