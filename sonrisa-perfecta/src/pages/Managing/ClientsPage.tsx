import Navbar from "../../components/Navbar";

function ClientsPage() {
    return (
        <div className="min-h-screen bg-gray-100 text-gray-800">
            <Navbar />
            <div className="container mx-auto py-10 px-4">
                <h1 className="text-3xl font-bold text-center text-blue-700 mb-8">Gestión de Clientes</h1>

                {/* Botón para agregar un nuevo cliente */}
                <div className="flex justify-end mb-6">
                    <button className="bg-blue-700 text-white px-6 py-2 rounded-lg hover:bg-blue-800 transition duration-300 shadow-md">
                        + Nuevo Cliente
                    </button>
                </div>

                {/* Tabla de clientes */}
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
                            {mockClients.map((client) => (
                                <tr key={client.id} className="hover:bg-gray-50">
                                    <td className="px-5 py-4 border-b border-gray-200 text-sm">{client.name}</td>
                                    <td className="px-5 py-4 border-b border-gray-200 text-sm">{client.email}</td>
                                    <td className="px-5 py-4 border-b border-gray-200 text-sm">{client.phone}</td>
                                    <td className="px-5 py-4 border-b border-gray-200 text-sm">{client.registrationDate}</td>
                                    <td className="px-5 py-4 border-b border-gray-200 text-sm text-center">
                                        <button className="bg-green-500 text-white px-3 py-1 rounded-md hover:bg-green-600 transition mx-1 shadow">
                                            Ver
                                        </button>
                                        <button className="bg-yellow-500 text-white px-3 py-1 rounded-md hover:bg-yellow-600 transition mx-1 shadow">
                                            Editar
                                        </button>
                                        <button className="bg-red-500 text-white px-3 py-1 rounded-md hover:bg-red-600 transition mx-1 shadow">
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

// Datos de ejemplo de clientes
const mockClients = [
    { id: 1, name: "Laura García", email: "laura.garcia@email.com", phone: "123-456-7890", registrationDate: "2024-01-15" },
    { id: 2, name: "Carlos Ramírez", email: "carlos.ramirez@email.com", phone: "987-654-3210", registrationDate: "2024-02-20" },
    { id: 3, name: "María Fernández", email: "maria.fernandez@email.com", phone: "456-789-0123", registrationDate: "2024-03-10" },
    { id: 4, name: "Luis Moreno", email: "luis.moreno@email.com", phone: "321-654-0987", registrationDate: "2024-04-05" },
];

export default ClientsPage;
