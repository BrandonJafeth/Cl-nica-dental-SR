import React from "react";

function AppointmentList({ appointments, onEditAppointment, onViewAppointment, onDeleteAppointment }) {
    return (
        <div className="bg-white shadow-md rounded-lg overflow-hidden">
            <table className="min-w-full leading-normal">
                <thead className="bg-blue-700 text-white">
                    <tr>
                        <th className="px-5 py-3 border-b border-gray-200 text-left text-sm font-semibold">Fecha</th>
                        <th className="px-5 py-3 border-b border-gray-200 text-left text-sm font-semibold">Motivo</th>
                        <th className="px-5 py-3 border-b border-gray-200 text-left text-sm font-semibold">Hora de Inicio</th>
                        <th className="px-5 py-3 border-b border-gray-200 text-left text-sm font-semibold">Hora de Fin</th>
                        <th className="px-5 py-3 border-b border-gray-200 text-center text-sm font-semibold">Acciones</th>
                    </tr>
                </thead>
                <tbody>
                    {appointments.map((appointment) => (
                        <tr key={appointment.id} className="hover:bg-gray-50">
                            <td className="px-5 py-4 border-b border-gray-200 text-sm">{appointment.Fecha_Cita}</td>
                            <td className="px-5 py-4 border-b border-gray-200 text-sm">{appointment.Motivo}</td>
                            <td className="px-5 py-4 border-b border-gray-200 text-sm">{appointment.Hora_Inicio}</td>
                            <td className="px-5 py-4 border-b border-gray-200 text-sm">{appointment.Hora_Fin}</td>
                            <td className="px-5 py-4 border-b border-gray-200 text-sm text-center space-x-2">
                                <button
                                    onClick={() => onViewAppointment(appointment)}
                                    className="bg-blue-500 text-white px-3 py-1 rounded-md hover:bg-blue-600 transition shadow"
                                >
                                    Ver
                                </button>
                                <button
                                    onClick={() => onEditAppointment(appointment)}
                                    className="bg-yellow-500 text-white px-3 py-1 rounded-md hover:bg-yellow-600 transition shadow"
                                >
                                    Editar
                                </button>
                                <button
                                    onClick={() => onDeleteAppointment(appointment.id)}
                                    className="bg-red-500 text-white px-3 py-1 rounded-md hover:bg-red-600 transition shadow"
                                >
                                    Eliminar
                                </button>
                            </td>
                        </tr>
                    ))}
                </tbody>
            </table>
        </div>
    );
}

export default AppointmentList;
