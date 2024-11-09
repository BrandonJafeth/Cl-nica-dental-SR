import React from "react";
import { Cita } from "../../types/type";

interface AppointmentModalProps {
  appointment: Cita;
  onClose: () => void;
}

function AppointmentModal({ appointment, onClose }: AppointmentModalProps) {
  if (!appointment) return null;

  return (
    <div className="fixed inset-0 bg-black bg-opacity-50 flex justify-center items-center z-50">
      <div className="bg-white rounded-lg w-full max-w-md p-6 shadow-lg">
        <h2 className="text-2xl font-bold text-center text-blue-700 mb-4">Detalles de la Cita</h2>
        
        <div className="space-y-3 text-gray-700">
          <p><strong>ID de la Cita:</strong> {appointment.iD_Cita}</p>
          <p><strong>Fecha:</strong> {appointment.fecha_Cita}</p>
          <p><strong>Motivo:</strong> {appointment.motivo}</p>
          <p><strong>Hora de Inicio:</strong> {appointment.hora_Inicio}</p>
          <p><strong>Hora de Fin:</strong> {appointment.hora_Fin}</p>
          <p><strong>Paciente ID:</strong> {appointment.iD_Paciente}</p>
          <p><strong>Dentista ID:</strong> {appointment.iD_Dentista}</p>
          <p><strong>Funcionario ID:</strong> {appointment.iD_Funcionario}</p>
          <p><strong>Estado de la Cita:</strong> {appointment.iD_EstadoCita}</p>
        </div>

        <div className="flex justify-end mt-6">
          <button
            onClick={onClose}
            className="bg-blue-600 text-white px-4 py-2 rounded-lg hover:bg-blue-700 transition duration-300"
          >
            Cerrar
          </button>
        </div>
      </div>
    </div>
  );
}

export default AppointmentModal;
