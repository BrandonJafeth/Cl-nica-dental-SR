import { useState } from "react";
import Navbar from "../../components/Navbar";
import PatientForm from "../../pages/Patient/PatientForm";
import PatientList from "../../pages/Patient/PatientList";
import PatientModal from "../../pages/Patient/PatientModal";

function ClientsPage() {
    const [showForm, setShowForm] = useState(false);
    const [selectedPatient, setSelectedPatient] = useState(null);
    const [showModal, setShowModal] = useState(false); // Estado para controlar el modal de detalles
    const [patients, setPatients] = useState([
        { id: 1, name: "Laura García", email: "laura.garcia@email.com", phone: "123-456-7890", registrationDate: "2024-01-15" },
        { id: 2, name: "Carlos Ramírez", email: "carlos.ramirez@email.com", phone: "987-654-3210", registrationDate: "2024-02-20" },
        { id: 3, name: "María Fernández", email: "maria.fernandez@email.com", phone: "456-789-0123", registrationDate: "2024-03-10" },
        { id: 4, name: "Luis Moreno", email: "luis.moreno@email.com", phone: "321-654-0987", registrationDate: "2024-04-05" },
    ]);

    // Función para mostrar el formulario en modo de agregar
    const handleAddPatient = () => {
        setSelectedPatient(null);
        setShowForm(true);
    };

    // Función para mostrar el formulario en modo de edición
    const handleEditPatient = (patient) => {
        setSelectedPatient(patient);
        setShowForm(true);
    };

    // Función para cerrar el formulario
    const handleCloseForm = () => {
        setShowForm(false);
        setSelectedPatient(null);
    };

    // Función para ver detalles del paciente en el modal
    const handleViewPatient = (patient) => {
        setSelectedPatient(patient);
        setShowModal(true); // Mostrar el modal
    };

    // Función para cerrar el modal de detalles
    const handleCloseModal = () => {
        setShowModal(false);
        setSelectedPatient(null);
    };

    // Función para eliminar un paciente de la lista
    const handleDeletePatient = (patientId) => {
        const confirmDelete = window.confirm("¿Estás seguro de que deseas eliminar este paciente?");
        if (confirmDelete) {
            setPatients(patients.filter(patient => patient.id !== patientId));
        }
    };

    return (
        <div className="min-h-screen bg-gray-100 text-gray-800">
            <Navbar />
            <div className="container mx-auto py-10 px-4">
                <h1 className="text-3xl font-bold text-center text-blue-700 mb-8">Gestión de Pacientes</h1>

                {showForm ? (
                    <PatientForm
                        onClose={handleCloseForm}
                        patient={selectedPatient}
                    />
                ) : (
                    <>
                        <div className="flex justify-end mb-6">
                            <button
                                onClick={handleAddPatient}
                                className="bg-blue-700 text-white px-6 py-2 rounded-lg hover:bg-blue-800 transition duration-300 shadow-md"
                            >
                                + Nuevo Paciente
                            </button>
                        </div>

                        <PatientList
                            patients={patients}
                            onEditPatient={handleEditPatient}
                            onViewPatient={handleViewPatient}
                            onDeletePatient={handleDeletePatient}
                        />
                    </>
                )}

                {/* Modal para ver los detalles del paciente */}
                {showModal && (
                    <PatientModal
                        patient={selectedPatient}
                        onClose={handleCloseModal}
                    />
                )}
            </div>
        </div>
    );
}

export default ClientsPage;
