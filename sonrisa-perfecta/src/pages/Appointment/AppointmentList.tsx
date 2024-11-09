// src/components/AppointmentList.tsx
import { useState } from "react";
import { useCita } from "../../hooks/useCita";
import AppointmentModal from "./AppointmentModal";
import EditAppointmentForm from "./EditAppointmentForm";
import { Cita } from "../../types/type";

function AppointmentList() {
  const { citas, loading, error, deleteCita } = useCita();
  const [selectedAppointment, setSelectedAppointment] = useState<Cita | null>(null);
  const [isModalOpen, setIsModalOpen] = useState(false);
  const [isEditMode, setIsEditMode] = useState(false);

  const handleViewClick = (appointment: Cita) => {
    setSelectedAppointment(appointment);
    setIsModalOpen(true);
  };

  const handleEditClick = (appointment: Cita) => {
    setSelectedAppointment(appointment);
    setIsEditMode(true);
  };

  const handleDeleteClick = async (id: string) => {
    if (window.confirm("¿Estás seguro de que deseas eliminar esta cita?")) {
      await deleteCita(id);
      fetchCitas(); // Refresca la lista de citas
    }
  };

  const closeModal = () => {
    setIsModalOpen(false);
    setSelectedAppointment(null);
  };

  const closeEditForm = () => {
    setIsEditMode(false);
    setSelectedAppointment(null);
  };

  if (loading) return <p>Cargando citas...</p>;
  if (error) return <p>{error}</p>;

  return (
    <div className="bg-white shadow-md rounded-lg overflow-hidden">
      <table className="min-w-full leading-normal">
        <thead className="bg-blue-700 text-white">
          <tr>
            <th className="px-5 py-3 border-b border-gray-200 text-left text-sm font-semibold">Fecha</th>
            <th className="px-5 py-3 border-b border-gray-200 text-left text-sm font-semibold">Motivo</th>
            <th className="px-5 py-3 border-b border-gray-200 text-left text-sm font-semibold">Hora de Inicio</th>
            <th className="px-5 py-3 border-b border-gray-200 text-left text-sm font-semibold">Hora de Fin</th>
            <th className="px-5 py-3 border-b border-gray-200 text-center text-sm font-semibold">Acciones</th>
          </tr>
        </thead>
        <tbody>
          {citas.map((appointment) => (
            <tr key={appointment.iD_Cita} className="hover:bg-gray-50">
              <td className="px-5 py-4 border-b border-gray-200 text-sm">{appointment.fecha_Cita}</td>
              <td className="px-5 py-4 border-b border-gray-200 text-sm">{appointment.motivo}</td>
              <td className="px-5 py-4 border-b border-gray-200 text-sm">{appointment.hora_Inicio}</td>
              <td className="px-5 py-4 border-b border-gray-200 text-sm">{appointment.hora_Fin}</td>
              <td className="px-5 py-4 border-b border-gray-200 text-sm text-center space-x-2">
                <button
                  onClick={() => handleViewClick(appointment)}
                  className="bg-blue-500 text-white px-3 py-1 rounded-md hover:bg-blue-600 transition shadow"
                >
                  Ver
                </button>
                <button
                  onClick={() => handleEditClick(appointment)}
                  className="bg-yellow-500 text-white px-3 py-1 rounded-md hover:bg-yellow-600 transition shadow"
                >
                  Editar
                </button>
                <button
                  onClick={() => appointment.iD_Cita && handleDeleteClick(appointment.iD_Cita)}
                  className="bg-red-500 text-white px-3 py-1 rounded-md hover:bg-red-600 transition shadow"
                >
                  Eliminar
                </button>
              </td>
            </tr>
          ))}
        </tbody>
      </table>

      {/* Modal para ver detalles */}
      {isModalOpen && selectedAppointment && (
        <AppointmentModal appointment={selectedAppointment} onClose={closeModal} />
      )}

      {/* Formulario para editar cita */}
      {isEditMode && selectedAppointment && (
        <div className="fixed inset-0 flex items-center justify-center bg-black bg-opacity-50 z-50">
          <div className="bg-white p-6 rounded-lg shadow-lg w-full max-w-lg">
            <EditAppointmentForm
              appointment={selectedAppointment}
              onClose={closeEditForm}
              onSave={() => {
                fetchCitas(); // Refresca la lista de citas al guardar
                closeEditForm();
              }}
            />
          </div>
        </div>
      )}
    </div>
  );
}

export default AppointmentList;
