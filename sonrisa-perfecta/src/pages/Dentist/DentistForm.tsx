// src/components/DentistForm.tsx

import { useState, useEffect } from "react";

interface Dentist {
    ID_Dentista?: string;
    nombre_Den: string;
    apellido1_Den: string;
    apellido2_Den: string;
    direccion_Den: string;
    fechaNacimiento_Den: string;
    telefono_Den: string;
    correo_Den: string;
    ID_Funcionario: string;
}

interface DentistFormProps {
    dentist?: Dentist; // Se pasa solo cuando se está editando
    onClose: () => void;
    onSave: (data: Dentist) => void;
}

function DentistForm({ dentist, onClose, onSave }: DentistFormProps) {
    const [formData, setFormData] = useState<Dentist>({
        ID_Dentista: dentist?.ID_Dentista || "",
        nombre_Den: dentist?.nombre_Den || "",
        apellido1_Den: dentist?.apellido1_Den || "",
        apellido2_Den: dentist?.apellido2_Den || "",
        direccion_Den: dentist?.direccion_Den || "",
        fechaNacimiento_Den: dentist?.fechaNacimiento_Den || "",
        telefono_Den: dentist?.telefono_Den || "",
        correo_Den: dentist?.correo_Den || "",
        ID_Funcionario: dentist?.ID_Funcionario || ""
    });

    useEffect(() => {
        if (dentist) {
            setFormData(dentist);
        }
    }, [dentist]);

    const handleSubmit = (e: React.FormEvent) => {
        e.preventDefault();
        onSave(formData); // Llama a onSave con los datos del formulario
    };

    const handleChange = (e: React.ChangeEvent<HTMLInputElement | HTMLTextAreaElement>) => {
        const { name, value } = e.target;
        setFormData((prev) => ({ ...prev, [name]: value }));
    };

    return (
        <div className="bg-white p-6 shadow-lg rounded-lg max-w-md mx-auto">
            <h2 className="text-2xl font-semibold mb-4">{dentist ? "Editar Dentista" : "Nuevo Dentista"}</h2>
            <form onSubmit={handleSubmit} className="space-y-4">
                <div>
                    <label className="block text-gray-700 font-semibold mb-2">ID Dentista de DENT0003 en adelante</label>
                    <input
                        type="text"
                        name="ID_Dentista"
                        value={formData.ID_Dentista}
                        onChange={handleChange}
                        className="w-full p-2 border border-gray-300 rounded"
                        required
                    />
                </div>
                <div>
                    <label className="block text-gray-700 font-semibold mb-2">Nombre</label>
                    <input
                        type="text"
                        name="nombre_Den"
                        value={formData.nombre_Den}
                        onChange={handleChange}
                        className="w-full p-2 border border-gray-300 rounded"
                        required
                    />
                </div>
                <div>
                    <label className="block text-gray-700 font-semibold mb-2">Primer Apellido</label>
                    <input
                        type="text"
                        name="apellido1_Den"
                        value={formData.apellido1_Den}
                        onChange={handleChange}
                        className="w-full p-2 border border-gray-300 rounded"
                        required
                    />
                </div>
                <div>
                    <label className="block text-gray-700 font-semibold mb-2">Segundo Apellido</label>
                    <input
                        type="text"
                        name="apellido2_Den"
                        value={formData.apellido2_Den}
                        onChange={handleChange}
                        className="w-full p-2 border border-gray-300 rounded"
                        required
                    />
                </div>
                <div>
                    <label className="block text-gray-700 font-semibold mb-2">Dirección</label>
                    <textarea
                        name="direccion_Den"
                        value={formData.direccion_Den}
                        onChange={handleChange}
                        className="w-full p-2 border border-gray-300 rounded"
                        required
                    />
                </div>
                <div>
                    <label className="block text-gray-700 font-semibold mb-2">Fecha de Nacimiento</label>
                    <input
                        type="date"
                        name="fechaNacimiento_Den"
                        value={formData.fechaNacimiento_Den}
                        onChange={handleChange}
                        className="w-full p-2 border border-gray-300 rounded"
                        required
                    />
                </div>
                <div>
                    <label className="block text-gray-700 font-semibold mb-2">Teléfono</label>
                    <input
                        type="tel"
                        name="telefono_Den"
                        value={formData.telefono_Den}
                        onChange={handleChange}
                        className="w-full p-2 border border-gray-300 rounded"
                        required
                    />
                </div>
                <div>
                    <label className="block text-gray-700 font-semibold mb-2">Correo Electrónico</label>
                    <input
                        type="email"
                        name="correo_Den"
                        value={formData.correo_Den}
                        onChange={handleChange}
                        className="w-full p-2 border border-gray-300 rounded"
                        required
                    />
                </div>
                <div>
                    <label className="block text-gray-700 font-semibold mb-2">ID del Funcionario</label>
                    <input
                        type="text"
                        name="ID_Funcionario"
                        value={formData.ID_Funcionario}
                        onChange={handleChange}
                        className="w-full p-2 border border-gray-300 rounded"
                        required
                    />
                </div>

                <div className="flex justify-end space-x-4 mt-6">
                    <button type="submit" className="bg-blue-700 text-white px-4 py-2 rounded hover:bg-blue-800 transition">
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

export default DentistForm;
