

function DentistModal({ dentist, onClose }) {
    if (!dentist) return null; // Si no hay dentista, no muestra nada

    return (
        <div className="fixed inset-0 bg-black bg-opacity-50 flex justify-center items-center z-50">
            <div className="bg-white rounded-lg w-full max-w-md p-6 shadow-lg">
                <h2 className="text-2xl font-bold text-center text-blue-700 mb-4">Detalles del Dentista</h2>
                
                <div className="space-y-3 text-gray-700">
                    <p><strong>Nombre:</strong> {dentist.nombre_Den}</p>
                    <p><strong>Apellidos:</strong> {dentist.apellido1_Den} {dentist.apellido2_Den}</p>
                    <p><strong>Dirección:</strong> {dentist.direccion_Den}</p>
                    <p><strong>Fecha de Nacimiento:</strong> {dentist.fechaNacimiento_Den}</p>
                    <p><strong>Teléfono:</strong> {dentist.telefono_Den}</p>
                    <p><strong>Correo:</strong> {dentist.correo_Den}</p>
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

export default DentistModal;
