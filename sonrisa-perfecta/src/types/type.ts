// Interfaces de las tablas

export interface EstadoPago {
  ID_EstadoPago: string;
  Nombre_EP: string;
  Descripcion_EP: string;
}

export interface TipoTratamiento {
  ID_TipoTratamiento: string;
  Nombre_Tipo_Tratamiento: string;
  Descripcion_Tipo_Tratamiento: string;
}

export interface Tratamiento {
  ID_Tratamiento: string;
  Nombre_Tra: string;
  Descripcion_Tra: string;
  ID_TipoTratamiento: string;
  ID_EstadoTratamiento: string;
}

export interface Procedimiento {
  ID_Procedimiento: string;
  Fecha_Proc: string; // ISO format date
  Detalles_Proc: string;
  Hora_Inicio_Proc: string; // ISO format time
  Hora_Fin_Proc: string; // ISO format time
  ID_Tratamiento: string;
}

// src/types/type.ts

export interface Factura {
  iD_Factura: string;
  montoTotal_Fa: number;
  fechaEmision_Fa: string; // ISO format date
  iD_EstadoPago: string;
}

export interface TipoPago {
  ID_Tipo_Pago: string;
  Nombre_TP: string;
  Descripcion_TP: string;
}

export interface Pago {
  iD_Pago: string;
  monto_Pago: number;
  fecha_Pago: string; // ISO format date
  iD_Factura: string;
  iD_Tipo_Pago: string;
}

export interface EstadoCuenta {
  ID_Estado_Cuenta: string;
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
  ID_HistorialMedico: string;
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

export interface FacturaProcedimiento {
  ID_Factura_Procedimiento: string;
  ID_Factura: string;
  ID_Procedimiento: string;
}

export interface FacturaTratamiento {
  ID_Factura_Tratamiento: string;
  ID_Factura: string;
  ID_Tratamiento: string;
}

export interface Auditoria {
  ID_Auditoria: string;
  Fecha_Hora_Accion: string; // ISO format datetime
  Accion: string;
  DispositivoQueRealizo: string;
  Usuario: string;
}

export interface Funcionario {
  ID_Funcionario: string;
  Nombre: string;
  Apellido1: string;
  Apellido2: string;
  Email: string;
  Contraseña: string;
}

export interface Usuarios {
  ID_Usuario: string;
  Nombre: string;
  Apellido1: string;
  Apellido2: string;
  Email: string;
  Contraseña: string;
  Token: string;
  ID_Funcionario: string;
}

export interface Dentista {
  ID_Dentista: string;
  Nombre_Den: string;
  Apellido1_Den: string;
  Apellido2_Den: string;
  Direccion_Den: string;
  FechaNacimiento_Den: string; // ISO format date
  Telefono_Den: string;
  Correo_Den: string;
  ID_Funcionario: string;
}

export interface Especialidad {
  ID_Especialidad: string;
  Nombre_Esp: string;
  Descripcion_Esp: string;
}

export interface DentistaEspecialidad {
  ID_Dentista_Especialidad: string;
  ID_Dentista: string;
  ID_Especialidad: string;
}

export interface EstadoCita {
  ID_EstadoCita: string;
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
  ID_Historial_Tratamiento: string;
  ID_HistorialMedico: string;
  ID_Tratamiento: string;
  Fecha_Tratamiento: string; // ISO format date
}

export interface Roles {
  ID_Roles: string;
  Nombre: string;
  Descripcion: string;
}

export interface Permisos {
  ID_Permisos: string;
  Nombre: string;
  Descripcion: string;
}

export interface RolesPermisos {
  ID_Roles_Permisos: string;
  ID_Roles: string;
  ID_Permisos: string;
}

export interface UsuarioRoles {
  ID_Usuario_Roles: string;
  ID_Usuario: string;
  ID_Roles: string;
}

// Tabla: Estado_Tratamiento
export type EstadoTratamiento = {
  ID_EstadoTratamiento: string;
  Nombre_Estado: string;
  Descripcion_Estado: string;
};


