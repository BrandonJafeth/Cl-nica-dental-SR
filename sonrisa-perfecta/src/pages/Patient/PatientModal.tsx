function PatientModal({ patient, onClose }) {
    if (!patient) return null; // Si no hay paciente, no muestra nada

    return (
        <div className="fixed inset-0 bg-black bg-opacity-50 flex justify-center items-center z-50">
            <div className="bg-white rounded-lg w-full max-w-md p-6 shadow-lg">
                <h2 className="text-2xl font-bold text-center text-blue-700 mb-4">Detalles del Paciente</h2>
                
                <div className="space-y-3 text-gray-700">
                    <p><strong>Nombre:</strong> {patient.name}</p>
                    <p><strong>Email:</strong> {patient.email}</p>
                    <p><strong>Teléfono:</strong> {patient.phone}</p>
                    <p><strong>Fecha de Registro:</strong> {patient.registrationDate}</p>
                    {/* Puedes añadir más detalles aquí */}
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

export default PatientModal;
