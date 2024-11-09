// src/components/EditAppointmentForm.tsx
import { useState, useEffect } from "react";
import { Cita } from "../../types/type";
import citaService from "../../services/TablesServices/CitaService";

interface EditAppointmentFormProps {
  appointment: Cita;
  onClose: () => void;
  onSave: () => void;
}

function EditAppointmentForm({ appointment, onClose, onSave }: EditAppointmentFormProps) {
  const [formData, setFormData] = useState<Cita>({
    iD_Cita: appointment.iD_Cita || "",
    fecha_Cita: appointment.fecha_Cita || "",
    motivo: appointment.motivo || "",
    hora_Inicio: appointment.hora_Inicio || "",
    hora_Fin: appointment.hora_Fin || "",
    iD_Paciente: appointment.iD_Paciente || "",
    iD_Dentista: appointment.iD_Dentista || "",
    iD_Funcionario: appointment.iD_Funcionario || "",
    iD_EstadoCita: appointment.iD_EstadoCita || "",
  });

  useEffect(() => {
    if (appointment) {
      setFormData({
        iD_Cita: appointment.iD_Cita,
        fecha_Cita: appointment.fecha_Cita,
        motivo: appointment.motivo,
        hora_Inicio: appointment.hora_Inicio,
        hora_Fin: appointment.hora_Fin,
        iD_Paciente: appointment.iD_Paciente,
        iD_Dentista: appointment.iD_Dentista,
        iD_Funcionario: appointment.iD_Funcionario,
        iD_EstadoCita: appointment.iD_EstadoCita,
      });
    }
  }, [appointment]);

  const handleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    const { name, value } = e.target;
    setFormData({ ...formData, [name]: value });
  };

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    try {
      await citaService.updateCita(formData.iD_Cita!, formData);
      onSave();
      onClose();
    } catch (error) {
      console.error("Error al actualizar la cita:", error);
    }
  };

  return (
    <form onSubmit={handleSubmit} className="space-y-4">
      <div>
        <label className="block text-gray-700 font-semibold mb-2">ID de la Cita</label>
        <input
          type="text"
          name="iD_Cita"
          value={formData.iD_Cita}
          onChange={handleChange}
          readOnly
          className="w-full p-2 border border-gray-300 rounded bg-gray-100"
        />
      </div>
      <div>
        <label className="block text-gray-700 font-semibold mb-2">Fecha de la Cita</label>
        <input
          type="date"
          name="fecha_Cita"
          value={formData.fecha_Cita}
          onChange={handleChange}
          className="w-full p-2 border border-gray-300 rounded"
        />
      </div>
      <div>
        <label className="block text-gray-700 font-semibold mb-2">Motivo</label>
        <input
          type="text"
          name="motivo"
          value={formData.motivo}
          onChange={handleChange}
          className="w-full p-2 border border-gray-300 rounded"
        />
      </div>
      <div>
        <label className="block text-gray-700 font-semibold mb-2">Hora de Inicio</label>
        <input
          type="time"
          name="hora_Inicio"
          value={formData.hora_Inicio}
          onChange={handleChange}
          className="w-full p-2 border border-gray-300 rounded"
        />
      </div>
      <div>
        <label className="block text-gray-700 font-semibold mb-2">Hora de Fin</label>
        <input
          type="time"
          name="hora_Fin"
          value={formData.hora_Fin}
          onChange={handleChange}
          className="w-full p-2 border border-gray-300 rounded"
        />
      </div>
      <div>
        <label className="block text-gray-700 font-semibold mb-2">Paciente ID</label>
        <input
          type="text"
          name="iD_Paciente"
          value={formData.iD_Paciente}
          onChange={handleChange}
          className="w-full p-2 border border-gray-300 rounded"
        />
      </div>
      <div>
        <label className="block text-gray-700 font-semibold mb-2">Dentista ID</label>
        <input
          type="text"
          name="iD_Dentista"
          value={formData.iD_Dentista}
          onChange={handleChange}
          className="w-full p-2 border border-gray-300 rounded"
        />
      </div>
      <div>
        <label className="block text-gray-700 font-semibold mb-2">Funcionario ID</label>
        <input
          type="text"
          name="iD_Funcionario"
          value={formData.iD_Funcionario}
          onChange={handleChange}
          className="w-full p-2 border border-gray-300 rounded"
        />
      </div>
      <div>
        <label className="block text-gray-700 font-semibold mb-2">Estado de la Cita</label>
        <input
          type="text"
          name="iD_EstadoCita"
          value={formData.iD_EstadoCita}
          onChange={handleChange}
          className="w-full p-2 border border-gray-300 rounded"
        />
      </div>
      <div className="flex justify-end space-x-4">
        <button type="submit" className="bg-blue-700 text-white px-4 py-2 rounded hover:bg-blue-800">
          Guardar Cambios
        </button>
        <button type="button" onClick={onClose} className="text-gray-500 hover:text-gray-700">
          Cancelar
        </button>
      </div>
    </form>
  );
}

export default EditAppointmentForm;
