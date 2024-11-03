import Navbar from "../../components/Navbar";

function PandTPage() {
    return (
        <div className="min-h-screen bg-gray-100 text-gray-800">
            <Navbar />
            <div className="container mx-auto py-10 px-4">
                <h1 className="text-3xl font-bold text-center text-blue-700 mb-8">Procedimientos y Tratamientos</h1>

                {/* Botón para agregar un nuevo procedimiento o tratamiento */}
                <div className="flex justify-end mb-6">
                    <button className="bg-blue-700 text-white px-6 py-2 rounded-lg hover:bg-blue-800 transition duration-300 shadow-md">
                        + Nuevo Tratamiento
                    </button>
                </div>

                {/* Lista de tratamientos y procedimientos */}
                <div className="grid grid-cols-1 sm:grid-cols-2 md:grid-cols-3 gap-6">
                    {mockTreatments.map((treatment) => (
                        <div key={treatment.id} className="bg-white shadow-md rounded-lg p-6">
                            <h2 className="text-xl font-bold text-blue-700 mb-2">{treatment.name}</h2>
                            <p className="text-gray-600 mb-4">{treatment.description}</p>
                            <p className="text-gray-600 mb-1"><strong>Duración:</strong> {treatment.duration} mins</p>
                            <p className="text-gray-600 mb-4"><strong>Costo:</strong> ${treatment.cost}</p>
                            
                            <div className="flex justify-between">
                                <button className="bg-green-500 text-white px-3 py-1 rounded-md hover:bg-green-600 transition shadow">
                                    Ver
                                </button>
                                <button className="bg-yellow-500 text-white px-3 py-1 rounded-md hover:bg-yellow-600 transition shadow">
                                    Editar
                                </button>
                                <button className="bg-red-500 text-white px-3 py-1 rounded-md hover:bg-red-600 transition shadow">
                                    Eliminar
                                </button>
                            </div>
                        </div>
                    ))}
                </div>
            </div>
        </div>
    );
}

// Datos de ejemplo de tratamientos y procedimientos
const mockTreatments = [
    {
        id: 1,
        name: "Limpieza Dental",
        description: "Una limpieza profunda para eliminar la placa y el sarro.",
        duration: 30,
        cost: 50,
    },
    {
        id: 2,
        name: "Extracción de Muelas",
        description: "Procedimiento para extraer muelas del juicio.",
        duration: 60,
        cost: 150,
    },
    {
        id: 3,
        name: "Blanqueamiento Dental",
        description: "Tratamiento para blanquear los dientes.",
        duration: 45,
        cost: 100,
    },
    {
        id: 4,
        name: "Implante Dental",
        description: "Procedimiento para colocar implantes dentales.",
        duration: 90,
        cost: 1200,
    },
    {
        id: 5,
        name: "Ortodoncia",
        description: "Tratamiento de ortodoncia para alinear los dientes.",
        duration: 120,
        cost: 500,
    },
    {
        id: 6,
        name: "Endodoncia",
        description: "Tratamiento de conducto para salvar el diente.",
        duration: 75,
        cost: 300,
    },
];

export default PandTPage;
