import { useState } from "react";
import Navbar from "../../components/Navbar";
import AppointmentList from "../../pages/Appointment/AppointmentList";
import AppointmentForm from "../../pages/Appointment/AppointmentForm";
import AppointmentModal from "../../pages/Appointment/AppointmentModal";

function AppointmentPage() {
    const [appointments, setAppointments] = useState([
        { id: 1, Fecha_Cita: "2024-03-20", Motivo: "Consulta", Hora_Inicio: "10:00", Hora_Fin: "11:00" },
        { id: 2, Fecha_Cita: "2024-03-21", Motivo: "Limpieza dental", Hora_Inicio: "14:00", Hora_Fin: "15:00" },
    ]);
    const [showForm, setShowForm] = useState(false);
    const [showModal, setShowModal] = useState(false);
    const [selectedAppointment, setSelectedAppointment] = useState(null);

    // Función para abrir el formulario en modo de agregar nueva cita
    const handleAddAppointment = () => {
        setSelectedAppointment(null);
        setShowForm(true);
    };

    // Función para abrir el formulario en modo de edición
    const handleEditAppointment = (appointment) => {
        setSelectedAppointment(appointment);
        setShowForm(true);
    };

    // Función para ver detalles de una cita en el modal
    const handleViewAppointment = (appointment) => {
        setSelectedAppointment(appointment);
        setShowModal(true);
    };

    // Función para eliminar una cita de la lista
    const handleDeleteAppointment = (appointmentId) => {
        const confirmDelete = window.confirm("¿Estás seguro de que deseas eliminar esta cita?");
        if (confirmDelete) {
            setAppointments((prev) => prev.filter((appointment) => appointment.id !== appointmentId));
        }
    };

    // Función para cerrar el formulario
    const handleCloseForm = () => {
        setShowForm(false);
        setSelectedAppointment(null);
    };

    // Función para cerrar el modal
    const handleCloseModal = () => {
        setShowModal(false);
        setSelectedAppointment(null);
    };

    return (
        <div className="min-h-screen bg-gray-100 text-gray-800">
            <Navbar />
            <div className="container mx-auto py-10 px-4">
                <h1 className="text-3xl font-bold text-center text-blue-700 mb-8">Gestión de Citas</h1>

                {/* Mostrar el formulario o la lista de citas */}
                {showForm ? (
                    <AppointmentForm
                        onClose={handleCloseForm}
                        appointment={selectedAppointment}
                    />
                ) : (
                    <>
                        <div className="flex justify-end mb-6">
                            <button
                                onClick={handleAddAppointment}
                                className="bg-blue-700 text-white px-6 py-2 rounded-lg hover:bg-blue-800 transition duration-300 shadow-md"
                            >
                                + Nueva Cita
                            </button>
                        </div>

                        <AppointmentList
                            appointments={appointments}
                            onEditAppointment={handleEditAppointment}
                            onViewAppointment={handleViewAppointment}
                            onDeleteAppointment={handleDeleteAppointment}
                        />
                    </>
                )}

                {/* Modal para ver los detalles de la cita */}
                {showModal && (
                    <AppointmentModal
                        appointment={selectedAppointment}
                        onClose={handleCloseModal}
                    />
                )}
            </div>
        </div>
    );
}

export default AppointmentPage;
