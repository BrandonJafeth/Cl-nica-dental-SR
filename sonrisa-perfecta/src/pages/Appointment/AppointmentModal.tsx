import React from "react";

function AppointmentModal({ appointment, onClose }) {
    if (!appointment) return null;

    return (
        <div className="fixed inset-0 bg-black bg-opacity-50 flex justify-center items-center z-50">
            <div className="bg-white rounded-lg w-full max-w-md p-6 shadow-lg">
                <h2 className="text-2xl font-bold text-center text-blue-700 mb-4">Detalles de la Cita</h2>
                
                <div className="space-y-3 text-gray-700">
                    <p><strong>Fecha:</strong> {appointment.Fecha_Cita}</p>
                    <p><strong>Motivo:</strong> {appointment.Motivo}</p>
                    <p><strong>Hora de Inicio:</strong> {appointment.Hora_Inicio}</p>
                    <p><strong>Hora de Fin:</strong> {appointment.Hora_Fin}</p>
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
