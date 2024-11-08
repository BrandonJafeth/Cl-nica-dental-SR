USE master;
GO

-- Crear logins a nivel de servidor
CREATE LOGIN usrAdmin WITH PASSWORD = 'ContraseñaSegura123!';
CREATE LOGIN usrDentista WITH PASSWORD = 'ContraseñaSegura123!';
CREATE LOGIN usrRecepcionista WITH PASSWORD = 'ContraseñaSegura123!';
CREATE LOGIN usrAsistenteDental WITH PASSWORD = 'ContraseñaSegura123!';
GO

-- Cambiar al contexto de la base de datos ClinicaDental
USE ClinicaDental;
GO

-- Crear usuarios en la base de datos y asignarlos a los logins
CREATE USER usrAdmin FOR LOGIN usrAdmin;
CREATE USER usrDentista FOR LOGIN usrDentista;
CREATE USER usrRecepcionista FOR LOGIN usrRecepcionista;
CREATE USER usrAsistenteDental FOR LOGIN usrAsistenteDental;
GO




USE ClinicaDental;
GO

-- Crear roles de base de datos
CREATE ROLE [db_Admin];
CREATE ROLE [db_Dentista];
CREATE ROLE [db_Recepcionista];
CREATE ROLE [db_AsistenteDental];
GO

-- Permiso de control total para el administrador
GRANT CONTROL ON DATABASE::ClinicaDental TO [db_Admin];
GO

-- Asignación de permisos específicos a cada rol

-- Permisos para db_Recepcionista (manejo de citas, búsqueda de pacientes, registro y gestión de pagos)
GRANT EXECUTE ON OBJECT::[sp_BuscarPacientePorNombre] TO [db_Recepcionista];
GRANT EXECUTE ON OBJECT::[sp_AgendarCita] TO [db_Recepcionista];
GRANT EXECUTE ON OBJECT::[sp_ReprogramarCita] TO [db_Recepcionista];
GRANT EXECUTE ON OBJECT::[sp_ConsultarCitasPorFecha] TO [db_Recepcionista];
GRANT EXECUTE ON OBJECT::[InsertPaciente] TO [db_Recepcionista];
GRANT EXECUTE ON OBJECT::[UpdatePaciente] TO [db_Recepcionista];
GRANT EXECUTE ON OBJECT::[DeletePaciente] TO [db_Recepcionista];
GRANT EXECUTE ON OBJECT::[InsertEstadoCitas] TO [db_Recepcionista];
GRANT EXECUTE ON OBJECT::[UpdateEstadoCitas] TO [db_Recepcionista];
GRANT EXECUTE ON OBJECT::[DeleteEstadoCitas] TO [db_Recepcionista];
GRANT EXECUTE ON OBJECT::[sp_GenerarFactura] TO [db_Recepcionista];
GRANT EXECUTE ON OBJECT::[sp_RegistrarPago] TO [db_Recepcionista];
GRANT EXECUTE ON OBJECT::[sp_ActualizarSaldoCuenta] TO [db_Recepcionista];
GRANT EXECUTE ON OBJECT::[InsertPago] TO [db_Recepcionista];
GRANT EXECUTE ON OBJECT::[UpdatePago] TO [db_Recepcionista];
GRANT SELECT ON [vw_ProximasCitas] TO [db_Recepcionista];
GRANT SELECT ON [vw_CitasPorEstado] TO [db_Recepcionista];
GRANT SELECT ON [vw_HistorialPaciente] TO [db_Recepcionista];
GRANT SELECT ON [vw_FacturasPendientes] TO [db_Recepcionista];
GO

-- Permisos de tablas para db_Recepcionista
GRANT SELECT, INSERT, UPDATE ON Paciente TO db_Recepcionista;
GRANT SELECT ON Tratamiento TO db_Recepcionista;
GRANT SELECT ON Procedimiento TO db_Recepcionista;
GRANT SELECT ON Estado_Cuenta TO db_Recepcionista;
GRANT SELECT, INSERT, UPDATE ON Pago TO db_Recepcionista;
GRANT SELECT, INSERT, UPDATE ON Factura TO db_Recepcionista;
GRANT SELECT ON Estado_Pago TO db_Recepcionista;
GRANT SELECT ON Estado_Citas TO db_Recepcionista;
GRANT SELECT ON Cita TO db_Recepcionista;
GO

-- Permisos para db_Dentista (tratamientos y consultas relacionadas con pacientes)
GRANT EXECUTE ON OBJECT::[sp_ConsultarTratamientosPorPaciente] TO [db_Dentista];
GRANT EXECUTE ON OBJECT::[sp_ActualizarEstadoTratamiento] TO [db_Dentista];
GRANT EXECUTE ON OBJECT::[InsertTratamiento] TO [db_Dentista];
GRANT EXECUTE ON OBJECT::[UpdateTratamiento] TO [db_Dentista];
GRANT EXECUTE ON OBJECT::[DeleteTratamiento] TO [db_Dentista];
GRANT EXECUTE ON OBJECT::[InsertProcedimiento] TO [db_Dentista];
GRANT EXECUTE ON OBJECT::[UpdateProcedimiento] TO [db_Dentista];
GRANT EXECUTE ON OBJECT::[DeleteProcedimiento] TO [db_Dentista];
GRANT EXECUTE ON OBJECT::[InsertHistorialMedico] TO [db_Dentista];
GRANT EXECUTE ON OBJECT::[UpdateHistorialMedico] TO [db_Dentista];
GRANT EXECUTE ON OBJECT::[DeleteHistorialMedico] TO [db_Dentista];
GRANT SELECT ON [vw_HistorialPaciente] TO [db_Dentista];
GRANT SELECT ON [vw_TratamientosPorDentista] TO [db_Dentista];
GRANT SELECT ON [vw_PacientesConTratamientosActivos] TO [db_Dentista];
GRANT SELECT ON [vw_DentistasConMasTratamientos] TO [db_Dentista];
GO

-- Permisos de tablas para db_Dentista
GRANT SELECT ON Paciente TO db_Dentista;
GRANT SELECT, INSERT, UPDATE ON Tratamiento TO db_Dentista;
GRANT SELECT, INSERT, UPDATE ON Procedimiento TO db_Dentista;
GRANT SELECT ON Estado_Tratamiento TO db_Dentista;
GRANT SELECT ON Cita TO db_Dentista;
GRANT SELECT ON Historial_Medico TO db_Dentista;
GRANT SELECT, INSERT, UPDATE ON Historial_Tratamiento TO db_Dentista;
GO

-- Permisos para db_AsistenteDental (soporte en tratamientos y consultas básicas)
GRANT EXECUTE ON OBJECT::[sp_ConsultarCitasPorPaciente] TO [db_AsistenteDental];
GRANT EXECUTE ON OBJECT::[sp_BuscarPacientePorNombre] TO [db_AsistenteDental];
GRANT SELECT ON [vw_HistorialPaciente] TO [db_AsistenteDental];
GRANT SELECT ON [vw_ProximasCitas] TO [db_AsistenteDental];
GO

-- Permisos para db_Admin (todos los permisos en todos los procedimientos y tablas)
GRANT EXECUTE ON OBJECT::[sp_BuscarPacientePorNombre] TO [db_Admin];
GRANT EXECUTE ON OBJECT::[sp_AgendarCita] TO [db_Admin];
GRANT EXECUTE ON OBJECT::[sp_ReprogramarCita] TO [db_Admin];
GRANT EXECUTE ON OBJECT::[sp_ConsultarCitasPorFecha] TO [db_Admin];
GRANT EXECUTE ON OBJECT::[sp_ActualizarEstadoTratamiento] TO [db_Admin];
GRANT EXECUTE ON OBJECT::[sp_GenerarFactura] TO [db_Admin];
GRANT EXECUTE ON OBJECT::[sp_RegistrarPago] TO [db_Admin];
GRANT EXECUTE ON OBJECT::[sp_ActualizarSaldoCuenta] TO [db_Admin];
GRANT EXECUTE ON OBJECT::[sp_ConsultarCitasPorPaciente] TO [db_Admin];
GRANT EXECUTE ON OBJECT::[sp_GenerarReporteFacturacion] TO [db_Admin];

-- Permisos para procedimientos CRUD generales
GRANT EXECUTE ON OBJECT::[InsertPaciente] TO [db_Admin];
GRANT EXECUTE ON OBJECT::[UpdatePaciente] TO [db_Admin];
GRANT EXECUTE ON OBJECT::[DeletePaciente] TO [db_Admin];
GRANT EXECUTE ON OBJECT::[InsertTratamiento] TO [db_Admin];
GRANT EXECUTE ON OBJECT::[UpdateTratamiento] TO [db_Admin];
GRANT EXECUTE ON OBJECT::[DeleteTratamiento] TO [db_Admin];
GRANT EXECUTE ON OBJECT::[InsertProcedimiento] TO [db_Admin];
GRANT EXECUTE ON OBJECT::[UpdateProcedimiento] TO [db_Admin];
GRANT EXECUTE ON OBJECT::[DeleteProcedimiento] TO [db_Admin];
GRANT EXECUTE ON OBJECT::[InsertFactura] TO [db_Admin];
GRANT EXECUTE ON OBJECT::[UpdateFactura] TO [db_Admin];
GRANT EXECUTE ON OBJECT::[DeleteFactura] TO [db_Admin];
GRANT EXECUTE ON OBJECT::[InsertTipoTratamiento] TO [db_Admin];
GRANT EXECUTE ON OBJECT::[UpdateTipoTratamiento] TO [db_Admin];
GRANT EXECUTE ON OBJECT::[DeleteTipoTratamiento] TO [db_Admin];
GRANT EXECUTE ON OBJECT::[InsertTipo_Pago] TO [db_Admin];
GRANT EXECUTE ON OBJECT::[UpdateTipo_Pago] TO [db_Admin];
GRANT EXECUTE ON OBJECT::[DeleteTipo_Pago] TO [db_Admin];
GRANT EXECUTE ON OBJECT::[InsertPago] TO [db_Admin];
GRANT EXECUTE ON OBJECT::[UpdatePago] TO [db_Admin];
GRANT EXECUTE ON OBJECT::[DeletePago] TO [db_Admin];
GRANT EXECUTE ON OBJECT::[InsertEstado_Pago] TO [db_Admin];
GRANT EXECUTE ON OBJECT::[UpdateEstado_Pago] TO [db_Admin];
GRANT EXECUTE ON OBJECT::[DeleteEstado_Pago] TO [db_Admin];
GRANT EXECUTE ON OBJECT::[InsertHistorialMedico] TO [db_Admin];
GRANT EXECUTE ON OBJECT::[UpdateHistorialMedico] TO [db_Admin];
GRANT EXECUTE ON OBJECT::[DeleteHistorialMedico] TO [db_Admin];
GRANT EXECUTE ON OBJECT::[InsertEstadoCitas] TO [db_Admin];
GRANT EXECUTE ON OBJECT::[UpdateEstadoCitas] TO [db_Admin];
GRANT EXECUTE ON OBJECT::[DeleteEstadoCitas] TO [db_Admin];
GRANT EXECUTE ON OBJECT::[InsertDentista_Especialidad] TO [db_Admin];
GRANT EXECUTE ON OBJECT::[UpdateDentista_Especialidad] TO [db_Admin];
GRANT EXECUTE ON OBJECT::[DeleteDentista_Especialidad] TO [db_Admin];

-- Permisos de tablas para db_Admin (todos los permisos en todas las tablas)
GRANT SELECT, INSERT, UPDATE, DELETE ON Estado_Pago TO db_Admin;
GRANT SELECT, INSERT, UPDATE, DELETE ON Tipo_Tratamiento TO db_Admin;
GRANT SELECT, INSERT, UPDATE, DELETE ON Estado_Tratamiento TO db_Admin;
GRANT SELECT, INSERT, UPDATE, DELETE ON Tratamiento TO db_Admin;
GRANT SELECT, INSERT, UPDATE, DELETE ON Procedimiento TO db_Admin;
GRANT SELECT, INSERT, UPDATE, DELETE ON Estado_Cuenta TO db_Admin;
GRANT SELECT, INSERT, UPDATE, DELETE ON Factura TO db_Admin;
GRANT SELECT, INSERT, UPDATE, DELETE ON Tipo_Pago TO db_Admin;
GRANT SELECT, INSERT, UPDATE, DELETE ON Pago TO db_Admin;
GRANT SELECT, INSERT, UPDATE, DELETE ON Paciente TO db_Admin;
GRANT SELECT, INSERT, UPDATE, DELETE ON Historial_Medico TO db_Admin;
GRANT SELECT, INSERT, UPDATE, DELETE ON Cuenta TO db_Admin;
GRANT SELECT, INSERT, UPDATE, DELETE ON Factura_Procedimiento TO db_Admin;
GRANT SELECT, INSERT, UPDATE, DELETE ON Factura_Tratamiento TO db_Admin;
GRANT SELECT, INSERT, UPDATE, DELETE ON Auditoria TO db_Admin;
GRANT SELECT, INSERT, UPDATE, DELETE ON Funcionario TO db_Admin;
GRANT SELECT, INSERT, UPDATE, DELETE ON Usuarios TO db_Admin;
GRANT SELECT, INSERT, UPDATE, DELETE ON DB_User TO db_Admin;
GRANT SELECT, INSERT, UPDATE, DELETE ON Dentista TO db_Admin;
GRANT SELECT, INSERT, UPDATE, DELETE ON Especialidad TO db_Admin;
GRANT SELECT, INSERT, UPDATE, DELETE ON Dentista_Especialidad TO db_Admin;
GRANT SELECT, INSERT, UPDATE, DELETE ON Estado_Citas TO db_Admin;
GRANT SELECT, INSERT, UPDATE, DELETE ON Cita TO db_Admin;
GRANT SELECT, INSERT, UPDATE, DELETE ON Historial_Tratamiento TO db_Admin;
GRANT SELECT, INSERT, UPDATE, DELETE ON Roles TO db_Admin;
GRANT SELECT, INSERT, UPDATE, DELETE ON Permisos TO db_Admin;
GRANT SELECT, INSERT, UPDATE, DELETE ON Roles_Permisos TO db_Admin;
GRANT SELECT, INSERT, UPDATE, DELETE ON Usuario_Roles TO db_Admin;
GO





-- Asignar roles de base de datos a los usuarios
EXEC sp_addrolemember 'db_Admin', 'usrAdmin';
EXEC sp_addrolemember 'db_Dentista', 'usrDentista';
EXEC sp_addrolemember 'db_Recepcionista', 'usrRecepcionista';
EXEC sp_addrolemember 'db_AsistenteDental', 'usrAsistenteDental';
GO
