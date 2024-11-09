// src/components/DentistList.tsx

import { Dentista } from "../../types/type";

interface DentistListProps {
    dentists: Dentista[]; // Lista de dentistas
    onEditDentist: (dentist: Dentista) => void; // Función de edición
    onViewDentist: (dentist: Dentista) => void; // Función para ver detalles
    onDeleteDentist: (id: string) => void; // Función de eliminación
}

function DentistList({ dentists, onEditDentist, onViewDentist, onDeleteDentist }: DentistListProps) {
    return (
        <div className="bg-white shadow-md rounded-lg overflow-hidden">
            <table className="min-w-full leading-normal">
                <thead className="bg-blue-700 text-white">
                    <tr>
                        <th className="px-5 py-3 border-b border-gray-200 text-left text-sm font-semibold">Nombre</th>
                        <th className="px-5 py-3 border-b border-gray-200 text-left text-sm font-semibold">Teléfono</th>
                        <th className="px-5 py-3 border-b border-gray-200 text-left text-sm font-semibold">Correo</th>
                        <th className="px-5 py-3 border-b border-gray-200 text-center text-sm font-semibold">Acciones</th>
                    </tr>
                </thead>
                <tbody>
                    {dentists.map((dentist) => (
                        <tr key={dentist.ID_Dentista} className="hover:bg-gray-50">
                            <td className="px-5 py-4 border-b border-gray-200 text-sm">
                                {dentist.Nombre_Den} {dentist.apellido1_Den} {dentist.apellido2_Den}
                            </td>
                            <td className="px-5 py-4 border-b border-gray-200 text-sm">{dentist.telefono_Den}</td>
                            <td className="px-5 py-4 border-b border-gray-200 text-sm">{dentist.correo_Den}</td>
                            <td className="px-5 py-4 border-b border-gray-200 text-sm text-center space-x-2">
                                <button
                                    onClick={() => onViewDentist(dentist)}
                                    className="bg-blue-500 text-white px-3 py-1 rounded-md hover:bg-blue-600 transition shadow"
                                >
                                    Ver
                                </button>
                                <button
                                    onClick={() => onEditDentist(dentist)}
                                    className="bg-yellow-500 text-white px-3 py-1 rounded-md hover:bg-yellow-600 transition shadow"
                                >
                                    Editar
                                </button>
                                <button
                                    onClick={() => onDeleteDentist(dentist.ID_Dentista)}
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

export default DentistList;
