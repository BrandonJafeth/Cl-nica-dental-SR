import { useState, useEffect } from "react";

function PatientForm({ patient, onClose }) {
    const [formData, setFormData] = useState({
        ID_Paciente: "",
        Nombre_Pac: "",
        Apellido1_Pac: "",
        Apellido2_Pac: "",
        Fecha_Nacimiento_Pac: "",
        Telefono_Pac: "",
        Correo_Pac: "",
        Direccion_Pac: "",
        ID_HistorialMedico: ""
    });

    useEffect(() => {
        if (patient) {
            setFormData({
                ID_Paciente: patient.ID_Paciente,
                Nombre_Pac: patient.Nombre_Pac,
                Apellido1_Pac: patient.Apellido1_Pac,
                Apellido2_Pac: patient.Apellido2_Pac,
                Fecha_Nacimiento_Pac: patient.Fecha_Nacimiento_Pac,
                Telefono_Pac: patient.Telefono_Pac,
                Correo_Pac: patient.Correo_Pac,
                Direccion_Pac: patient.Direccion_Pac,
                ID_HistorialMedico: patient.ID_HistorialMedico
            });
        }
    }, [patient]);

    const handleSubmit = (e) => {
        e.preventDefault();
        console.log("Datos del formulario:", formData);
        onClose();
    };

    const handleChange = (e) => {
        const { name, value } = e.target;
        setFormData((prev) => ({ ...prev, [name]: value }));
    };

    return (
        <div className="bg-white p-6 shadow-lg rounded-lg">
            <h2 className="text-2xl font-semibold mb-4">{patient ? "Editar Paciente" : "Nuevo Paciente"}</h2>
            <form onSubmit={handleSubmit} className="space-y-4">
                <div>
                    <label className="block text-gray-700 font-semibold mb-2">Nombre</label>
                    <input
                        type="text"
                        name="Nombre_Pac"
                        value={formData.Nombre_Pac}
                        onChange={handleChange}
                        placeholder="Nombre"
                        className="w-full p-2 border border-gray-300 rounded"
                    />
                </div>
                <div>
                    <label className="block text-gray-700 font-semibold mb-2">Primer Apellido</label>
                    <input
                        type="text"
                        name="Apellido1_Pac"
                        value={formData.Apellido1_Pac}
                        onChange={handleChange}
                        placeholder="Primer Apellido"
                        className="w-full p-2 border border-gray-300 rounded"
                    />
                </div>
                <div>
                    <label className="block text-gray-700 font-semibold mb-2">Segundo Apellido</label>
                    <input
                        type="text"
                        name="Apellido2_Pac"
                        value={formData.Apellido2_Pac}
                        onChange={handleChange}
                        placeholder="Segundo Apellido"
                        className="w-full p-2 border border-gray-300 rounded"
                    />
                </div>
                <div>
                    <label className="block text-gray-700 font-semibold mb-2">Fecha de Nacimiento</label>
                    <input
                        type="date"
                        name="Fecha_Nacimiento_Pac"
                        value={formData.Fecha_Nacimiento_Pac}
                        onChange={handleChange}
                        className="w-full p-2 border border-gray-300 rounded"
                    />
                </div>
                <div>
                    <label className="block text-gray-700 font-semibold mb-2">Teléfono</label>
                    <input
                        type="text"
                        name="Telefono_Pac"
                        value={formData.Telefono_Pac}
                        onChange={handleChange}
                        placeholder="Teléfono"
                        className="w-full p-2 border border-gray-300 rounded"
                    />
                </div>
                <div>
                    <label className="block text-gray-700 font-semibold mb-2">Correo Electrónico</label>
                    <input
                        type="email"
                        name="Correo_Pac"
                        value={formData.Correo_Pac}
                        onChange={handleChange}
                        placeholder="Correo"
                        className="w-full p-2 border border-gray-300 rounded"
                    />
                </div>
                <div>
                    <label className="block text-gray-700 font-semibold mb-2">Dirección</label>
                    <textarea
                        name="Direccion_Pac"
                        value={formData.Direccion_Pac}
                        onChange={handleChange}
                        placeholder="Dirección"
                        className="w-full p-2 border border-gray-300 rounded"
                    ></textarea>
                </div>
                <div>
                    <label className="block text-gray-700 font-semibold mb-2">ID del Historial Médico</label>
                    <input
                        type="text"
                        name="ID_HistorialMedico"
                        value={formData.ID_HistorialMedico}
                        onChange={handleChange}
                        placeholder="ID Historial Médico"
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

export default PatientForm;
