import { useState, useEffect } from "react";
import { Cita } from "../../types/type"; // Asegúrate de que el tipo `Cita` esté correctamente definido
import citaService from "../../services/TablesServices/CitaService";

interface AppointmentFormProps {
    appointment?: Cita;
    onClose: () => void;
    onSave: () => void;
}

function AppointmentForm({ appointment, onClose, onSave }: AppointmentFormProps) {
    const [formData, setFormData] = useState<Cita>({
        iD_Cita: "",
        fecha_Cita: "",
        motivo: "",
        hora_Inicio: "",
        hora_Fin: "",
        iD_Paciente: "",
        iD_Dentista: "",
        iD_Funcionario: "",
        iD_EstadoCita: ""
    });

    useEffect(() => {
        if (appointment) {
            setFormData({
                iD_Cita: appointment.iD_Cita || "",
                fecha_Cita: appointment.fecha_Cita || "",
                motivo: appointment.motivo || "",
                hora_Inicio: appointment.hora_Inicio || "",
                hora_Fin: appointment.hora_Fin || "",
                iD_Paciente: appointment.iD_Paciente || "",
                iD_Dentista: appointment.iD_Dentista || "",
                iD_Funcionario: appointment.iD_Funcionario || "",
                iD_EstadoCita: appointment.iD_EstadoCita || ""
            });
        }
    }, [appointment]);

    const handleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
        const { name, value } = e.target;
        setFormData((prevData) => ({ ...prevData, [name]: value }));
    };

    const handleSubmit = async (e: React.FormEvent) => {
        e.preventDefault();
        try {
            await citaService.createCita(formData); // Llama a la API para crear la cita
            onSave(); // Actualiza la lista de citas
            onClose();
        } catch (error) {
            console.error("Error al crear la cita:", error);
        }
    };

    return (
        <div className="bg-white p-6 shadow-lg rounded-lg">
            <h2 className="text-2xl font-semibold mb-4">{appointment ? "Editar Cita" : "Nueva Cita"}</h2>
            <form onSubmit={handleSubmit} className="space-y-4">
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
                        placeholder="Motivo de la cita"
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
                        placeholder="ID del Paciente"
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
                        placeholder="ID del Dentista"
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
                        placeholder="ID del Funcionario"
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
                        placeholder="Estado de la Cita"
                        className="w-full p-2 border border-gray-300 rounded"
                    />
                </div>
                <div className="flex justify-end space-x-4">
                    <button type="submit" className="bg-blue-700 text-white px-4 py-2 rounded hover:bg-blue-800">
                        Guardar
                    </button>
                    <button
                        type="button"
                        onClick={onClose}
                        className="text-gray-500 hover:text-gray-700"
                    >
                        Cancelar
                    </button>
                </div>
            </form>
        </div>
    );
}

export default AppointmentForm;
