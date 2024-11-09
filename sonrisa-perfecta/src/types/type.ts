//jose tontio 
// Types para IDs específicos
type IdPaciente = `PAC${number}${number}${number}${number}`;
type IdEstadoPago = `EP${number}${number}${number}${number}`;
type IdTipoTratamiento = `TT${number}${number}${number}${number}`;
type IdTratamiento = `TRA${number}${number}${number}${number}`;
type IdProcedimiento = `PRO${number}${number}${number}${number}`;
type IdFactura = `FAC${number}${number}${number}${number}`;
type IdTipoPago = `TP${number}${number}${number}${number}`;
type IdEstadoCuenta = `EC${number}${number}${number}${number}`;
type IdHistorialMedico = `HM${number}${number}${number}${number}`;
type IdCuenta = `CUE${number}${number}${number}${number}`;
type IdFacturaProcedimiento = `FP${number}${number}${number}${number}`;
type IdFacturaTratamiento = `FT${number}${number}${number}${number}`;
type UniqueIdentifier = string; // For UNIQUEIDENTIFIER (UUID)
type IdFuncionario = `FUN${number}${number}${number}${number}`;
type IdUsuario = `USR${number}${number}${number}${number}`;
type IdDentista = `DEN${number}${number}${number}${number}`;
type IdEspecialidad = `ESP${number}${number}${number}${number}`;
type IdDentistaEspecialidad = `DE${number}${number}${number}${number}`;
type IdEstadoCita = `ECI${number}${number}${number}${number}`;
type IdCita = `CIT${number}${number}${number}${number}`;
type IdHistorialTratamiento = `HT${number}${number}${number}${number}`;
type IdRoles = `ROL${number}${number}${number}${number}`;
type IdPermisos = `PER${number}${number}${number}${number}`;
type IdRolesPermisos = `RP${number}${number}${number}${number}`;
type IdUsuarioRoles = `UR${number}${number}${number}${number}`;

// Interfaces de las tablas

export interface EstadoPago {
  ID_EstadoPago: IdEstadoPago;
  Nombre_EP: string;
  Descripcion_EP: string;
}

export interface TipoTratamiento {
  ID_TipoTratamiento: IdTipoTratamiento;
  Nombre_Tipo_Tratamiento: string;
  Descripcion_Tipo_Tratamiento: string;
}

export interface Tratamiento {
  ID_Tratamiento: IdTratamiento;
  Nombre_Tra: string;
  Descripcion_Tra: string;
  ID_TipoTratamiento: IdTipoTratamiento;
}

export interface Procedimiento {
  ID_Procedimiento: IdProcedimiento;
  Fecha_Proc: string; // ISO format date
  Detalles_Proc: string;
  Hora_Inicio_Proc: string; // ISO format time
  Hora_Fin_Proc: string; // ISO format time
  ID_Tratamiento: IdTratamiento;
}

export interface Factura {
  ID_Factura: IdFactura;
  MontoTotal_Fa: number;
  FechaEmision_Fa: string; // ISO format date
  ID_EstadoPago: IdEstadoPago;
}

export interface TipoPago {
  ID_Tipo_Pago: IdTipoPago;
  Nombre_TP: string;
  Descripcion_TP: string;
}

export interface Pago {
  ID_Pago: UniqueIdentifier;
  Monto_Pago: number;
  Fecha_Pago: string; // ISO format date
  ID_Factura: IdFactura;
  ID_Tipo_Pago: IdTipoPago;
}

export interface EstadoCuenta {
  ID_Estado_Cuenta: IdEstadoCuenta;
  Nombre_EC: string;
  Descripcion_EC: string;
}

export interface Paciente {
  iD_Paciente: string;
  nombre_Pac: string;
  apellido1_Pac: string;
  apellido2_Pac: string;
  fecha_Nacimiento_Pac: string; // ISO format date
  telefono_Pac: string;
  correo_Pac: string;
  direccion_Pac: string;
  iD_HistorialMedico: IdHistorialMedico;
}



export interface HistorialMedico {
  ID_HistorialMedico: IdHistorialMedico;
  Fecha_Historial: string; // ISO format date
  Diagnostico: string;
  Tratamientos_Medicos: string;
}

export interface Cuenta {
  ID_Cuenta: string;
  Saldo_Total: number;
  Fecha_Apertura: string;
  Fecha_Cierre: string;
  Fecha_Ultima_Actualizacion: string;
  Observaciones: string;
  ID_Estado_Cuenta: string;
  ID_Factura: string;
  ID_Paciente: string;  // Asegúrate de que esto esté definido aquí
}

export type{ IdFacturaProcedimiento, IdProcedimiento }
export interface FacturaProcedimiento {
  ID_Factura_Procedimiento: IdFacturaProcedimiento;
  ID_Factura: IdFactura;
  ID_Procedimiento: IdProcedimiento;
}

export type { IdFacturaTratamiento, IdFactura, IdTratamiento }  

export interface FacturaTratamiento {
  ID_Factura_Tratamiento: IdFacturaTratamiento;
  ID_Factura: IdFactura;
  ID_Tratamiento: IdTratamiento;
}

export interface Auditoria {
  ID_Auditoria: UniqueIdentifier;
  Fecha_Hora_Accion: string; // ISO format datetime
  Accion: string;
  DispositivoQueRealizo: string;
  Usuario: string;
}

export interface Funcionario {
  ID_Funcionario: IdFuncionario;
  Nombre: string;
  Apellido1: string;
  Apellido2: string;
  Email: string;
  Contraseña: string;
}

export interface Usuarios {
  ID_Usuario: IdUsuario;
  Nombre: string;
  Apellido1: string;
  Apellido2: string;
  Email: string;
  Contraseña: string;
  Token: string;
  ID_Funcionario: IdFuncionario;
}

export interface Dentista {
  ID_Dentista: IdDentista;
  Nombre_Den: string;
  Apellido1_Den: string;
  Apellido2_Den: string;
  Direccion_Den: string;
  FechaNacimiento_Den: string; // ISO format date
  Telefono_Den: string;
  Correo_Den: string;
  ID_Funcionario: IdFuncionario;
}

export interface Especialidad {
  ID_Especialidad: IdEspecialidad;
  Nombre_Esp: string;
  Descripcion_Esp: string;
}

export interface DentistaEspecialidad {
  ID_Dentista_Especialidad: IdDentistaEspecialidad;
  ID_Dentista: IdDentista;
  ID_Especialidad: IdEspecialidad;
}


export type { IdEstadoCita }
export interface EstadoCita {
  ID_EstadoCita: IdEstadoCita;
  Nombre_Estado: string;
  Descripcion_Estado: string;
}

export interface Cita {
  iD_Cita?: string;
  fecha_Cita: string;
  motivo: string;
  hora_Inicio: string;
  hora_Fin: string;
  iD_Paciente: string;
  iD_Dentista: string;
  iD_Funcionario: string;
  iD_EstadoCita: string;
}

export interface HistorialTratamiento {
  ID_Historial_Tratamiento: IdHistorialTratamiento;
  ID_HistorialMedico: IdHistorialMedico;
  ID_Tratamiento: IdTratamiento;
  Fecha_Tratamiento: string; // ISO format date
}

export interface Roles {
  ID_Roles: IdRoles;
  Nombre: string;
  Descripcion: string;
}

export interface Permisos {
  ID_Permisos: IdPermisos;
  Nombre: string;
  Descripcion: string;
}

export interface RolesPermisos {
  ID_Roles_Permisos: IdRolesPermisos;
  ID_Roles: IdRoles;
  ID_Permisos: IdPermisos;
}

export interface UsuarioRoles {
  ID_Usuario_Roles: IdUsuarioRoles;
  ID_Usuario: IdUsuario;
  ID_Roles: IdRoles;
}


// Tabla: Estado_Tratamiento
export type EstadoTratamiento = {
  ID_EstadoTratamiento: string;
  Nombre_Estado: string;
  Descripcion_Estado: string;
};