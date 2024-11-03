import Navbar from "../../components/Navbar";

function AppointmentsPage() {
    return (
        <div className="min-h-screen bg-gray-100 text-gray-800">
            <Navbar />
            <div className="container mx-auto py-10 px-4">
                <h1 className="text-3xl font-semibold text-center text-blue-700 mb-6">Gestión de Citas</h1>
                
                {/* Botón para agregar nueva cita */}
                <div className="flex justify-end mb-4">
                    <button className="bg-blue-600 text-white px-4 py-2 rounded-lg hover:bg-blue-700 transition duration-300">
                        + Nueva Cita
                    </button>
                </div>
                
                {/* Tabla de citas */}
                <div className="bg-white shadow-lg rounded-lg overflow-hidden">
                    <table className="min-w-full leading-normal">
                        <thead className="bg-blue-700 text-white">
                            <tr>
                                <th className="px-5 py-3 border-b-2 border-gray-200 text-left text-sm font-semibold">Paciente</th>
                                <th className="px-5 py-3 border-b-2 border-gray-200 text-left text-sm font-semibold">Fecha</th>
                                <th className="px-5 py-3 border-b-2 border-gray-200 text-left text-sm font-semibold">Hora</th>
                                <th className="px-5 py-3 border-b-2 border-gray-200 text-left text-sm font-semibold">Doctor</th>
                                <th className="px-5 py-3 border-b-2 border-gray-200 text-center text-sm font-semibold">Acciones</th>
                            </tr>
                        </thead>
                        <tbody>
                            {/* Fila de datos quemados */}
                            {mockData.map((appointment) => (
                                <tr key={appointment.id} className="hover:bg-gray-100 transition">
                                    <td className="px-5 py-4 border-b border-gray-200 text-sm">
                                        {appointment.patient}
                                    </td>
                                    <td className="px-5 py-4 border-b border-gray-200 text-sm">
                                        {appointment.date}
                                    </td>
                                    <td className="px-5 py-4 border-b border-gray-200 text-sm">
                                        {appointment.time}
                                    </td>
                                    <td className="px-5 py-4 border-b border-gray-200 text-sm">
                                        {appointment.doctor}
                                    </td>
                                    <td className="px-5 py-4 border-b border-gray-200 text-sm text-center">
                                        <button className="bg-green-500 text-white px-3 py-1 rounded hover:bg-green-600 transition mx-1">
                                            Ver
                                        </button>
                                        <button className="bg-yellow-500 text-white px-3 py-1 rounded hover:bg-yellow-600 transition mx-1">
                                            Editar
                                        </button>
                                        <button className="bg-red-500 text-white px-3 py-1 rounded hover:bg-red-600 transition mx-1">
                                            Eliminar
                                        </button>
                                    </td>
                                </tr>
                            ))}
                        </tbody>
                    </table>
                </div>
            </div>
        </div>
    );
}

// Datos de ejemplo quemados
const mockData = [
    { id: 1, patient: "Juan Pérez", date: "2024-11-07", time: "09:00", doctor: "Dr. Sánchez" },
    { id: 2, patient: "María López", date: "2024-11-07", time: "10:00", doctor: "Dr. Gómez" },
    { id: 3, patient: "Carlos Martínez", date: "2024-11-08", time: "14:00", doctor: "Dr. Torres" },
    { id: 4, patient: "Ana Ramírez", date: "2024-11-08", time: "15:30", doctor: "Dr. Sánchez" },
];

export default AppointmentsPage;
