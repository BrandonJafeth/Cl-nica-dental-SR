import React from "react";

function PatientList({ patients, onEditPatient, onViewPatient, onDeletePatient }) {
    return (
        <div className="bg-white shadow-md rounded-lg overflow-hidden">
            <table className="min-w-full leading-normal">
                <thead className="bg-blue-700 text-white">
                    <tr>
                        <th className="px-5 py-3 border-b border-gray-200 text-left text-sm font-semibold">Nombre</th>
                        <th className="px-5 py-3 border-b border-gray-200 text-left text-sm font-semibold">Email</th>
                        <th className="px-5 py-3 border-b border-gray-200 text-left text-sm font-semibold">Teléfono</th>
                        <th className="px-5 py-3 border-b border-gray-200 text-left text-sm font-semibold">Fecha de Registro</th>
                        <th className="px-5 py-3 border-b border-gray-200 text-center text-sm font-semibold">Acciones</th>
                    </tr>
                </thead>
                <tbody>
                    {patients.map((patient) => (
                        <tr key={patient.id} className="hover:bg-gray-50">
                            <td className="px-5 py-4 border-b border-gray-200 text-sm">{patient.name}</td>
                            <td className="px-5 py-4 border-b border-gray-200 text-sm">{patient.email}</td>
                            <td className="px-5 py-4 border-b border-gray-200 text-sm">{patient.phone}</td>
                            <td className="px-5 py-4 border-b border-gray-200 text-sm">{patient.registrationDate}</td>
                            <td className="px-5 py-4 border-b border-gray-200 text-sm text-center space-x-2">
                                <button
                                    onClick={() => onViewPatient(patient)}
                                    className="bg-blue-500 text-white px-3 py-1 rounded-md hover:bg-blue-600 transition shadow"
                                >
                                    Ver
                                </button>
                                <button
                                    onClick={() => onEditPatient(patient)}
                                    className="bg-yellow-500 text-white px-3 py-1 rounded-md hover:bg-yellow-600 transition shadow"
                                >
                                    Editar
                                </button>
                                <button
                                    onClick={() => onDeletePatient(patient.id)}
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

export default PatientList;
