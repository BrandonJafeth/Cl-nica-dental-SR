import { useState, useEffect } from "react";

function AppointmentForm({ appointment, onClose }) {
    const [formData, setFormData] = useState({
        Fecha_Cita: "",
        Motivo: "",
        Hora_Inicio: "",
        Hora_Fin: ""
    });

    useEffect(() => {
        if (appointment) {
            setFormData({
                Fecha_Cita: appointment.Fecha_Cita,
                Motivo: appointment.Motivo,
                Hora_Inicio: appointment.Hora_Inicio,
                Hora_Fin: appointment.Hora_Fin
            });
        }
    }, [appointment]);

    const handleChange = (e:any) => {
        const { name, value } = e.target;
        setFormData({ ...formData, [name]: value });
    };

    const handleSubmit = (e:any) => {
        e.preventDefault();
        console.log("Datos de la cita:", formData);
        onClose();
    };

    return (
        <div className="bg-white p-6 shadow-lg rounded-lg">
            <h2 className="text-2xl font-semibold mb-4">{appointment ? "Editar Cita" : "Nueva Cita"}</h2>
            <form onSubmit={handleSubmit} className="space-y-4">
                <div>
                    <label className="block text-gray-700 font-semibold mb-2">Fecha de la Cita</label>
                    <input
                        type="date"
                        name="Fecha_Cita"
                        value={formData.Fecha_Cita}
                        onChange={handleChange}
                        className="w-full p-2 border border-gray-300 rounded"
                    />
                </div>
                <div>
                    <label className="block text-gray-700 font-semibold mb-2">Motivo</label>
                    <input
                        type="text"
                        name="Motivo"
                        value={formData.Motivo}
                        onChange={handleChange}
                        placeholder="Motivo de la cita"
                        className="w-full p-2 border border-gray-300 rounded"
                    />
                </div>
                <div>
                    <label className="block text-gray-700 font-semibold mb-2">Hora de Inicio</label>
                    <input
                        type="time"
                        name="Hora_Inicio"
                        value={formData.Hora_Inicio}
                        onChange={handleChange}
                        className="w-full p-2 border border-gray-300 rounded"
                    />
                </div>
                <div>
                    <label className="block text-gray-700 font-semibold mb-2">Hora de Fin</label>
                    <input
                        type="time"
                        name="Hora_Fin"
                        value={formData.Hora_Fin}
                        onChange={handleChange}
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
