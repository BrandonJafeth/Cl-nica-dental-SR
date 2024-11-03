import Navbar from "../../components/Navbar";

function BillingPage() {
    return (
        <div className="min-h-screen bg-gray-100 text-gray-800">
            <Navbar />
            <div className="container mx-auto py-10 px-4">
                <h1 className="text-3xl font-bold text-center text-blue-700 mb-8">Gestión de Facturas</h1>

                {/* Botón para agregar una nueva factura */}
                <div className="flex justify-end mb-6">
                    <button className="bg-blue-700 text-white px-6 py-2 rounded-lg hover:bg-blue-800 transition duration-300 shadow-md">
                        + Nueva Factura
                    </button>
                </div>

                {/* Tabla de facturas */}
                <div className="bg-white shadow-md rounded-lg overflow-hidden">
                    <table className="min-w-full leading-normal">
                        <thead className="bg-blue-700 text-white">
                            <tr>
                                <th className="px-5 py-3 border-b border-gray-200 text-left text-sm font-semibold">Cliente</th>
                                <th className="px-5 py-3 border-b border-gray-200 text-left text-sm font-semibold">Fecha</th>
                                <th className="px-5 py-3 border-b border-gray-200 text-left text-sm font-semibold">Monto</th>
                                <th className="px-5 py-3 border-b border-gray-200 text-left text-sm font-semibold">Estado</th>
                                <th className="px-5 py-3 border-b border-gray-200 text-center text-sm font-semibold">Acciones</th>
                            </tr>
                        </thead>
                        <tbody>
                            {mockBills.map((bill) => (
                                <tr key={bill.id} className="hover:bg-gray-50">
                                    <td className="px-5 py-4 border-b border-gray-200 text-sm">{bill.client}</td>
                                    <td className="px-5 py-4 border-b border-gray-200 text-sm">{bill.date}</td>
                                    <td className="px-5 py-4 border-b border-gray-200 text-sm">${bill.amount.toFixed(2)}</td>
                                    <td className="px-5 py-4 border-b border-gray-200 text-sm">
                                        <span className={`px-2 py-1 rounded-full text-white text-xs ${bill.status === 'Pagada' ? 'bg-green-500' : 'bg-red-500'}`}>
                                            {bill.status}
                                        </span>
                                    </td>
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

// Datos de ejemplo de facturas
const mockBills = [
    { id: 1, client: "Laura García", date: "2024-01-15", amount: 250.75, status: "Pagada" },
    { id: 2, client: "Carlos Ramírez", date: "2024-02-20", amount: 150.50, status: "Pendiente" },
    { id: 3, client: "María Fernández", date: "2024-03-10", amount: 300.00, status: "Pagada" },
    { id: 4, client: "Luis Moreno", date: "2024-04-05", amount: 120.20, status: "Pendiente" },
];

export default BillingPage;
