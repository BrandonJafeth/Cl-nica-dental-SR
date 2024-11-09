// src/pages/Tratamiento/TratamientoForm.tsx
import React, { useState, useEffect } from "react";
import { Tratamiento } from "../../types/type";

interface TratamientoFormProps {
  tratamiento?: Tratamiento | null;
  onClose: () => void;
  onSave: (tratamiento: Tratamiento) => void;
}

const TratamientoForm: React.FC<TratamientoFormProps> = ({ tratamiento, onClose, onSave }) => {
  const [formData, setFormData] = useState<Tratamiento>({
    ID_Tratamiento: tratamiento?.ID_Tratamiento || "",
    Nombre_Tra: tratamiento?.Nombre_Tra || "",
    Descripcion_Tra: tratamiento?.Descripcion_Tra || "",
    ID_TipoTratamiento: tratamiento?.ID_TipoTratamiento || "",
    ID_EstadoTratamiento: tratamiento?.ID_EstadoTratamiento || "", // Añadir campo ID_EstadoTratamiento
  });

  useEffect(() => {
    if (tratamiento) {
      setFormData(tratamiento);
    }
  }, [tratamiento]);

  const handleSubmit = (e: React.FormEvent) => {
    e.preventDefault();
    onSave(formData);
  };

  const handleChange = (e: React.ChangeEvent<HTMLInputElement | HTMLTextAreaElement>) => {
    const { name, value } = e.target;
    setFormData((prev) => ({ ...prev, [name]: value }));
  };

  return (
    <div className="bg-white p-6 shadow-lg rounded-lg">
      <h2 className="text-2xl font-semibold mb-4">{tratamiento ? "Editar Tratamiento" : "Nuevo Tratamiento"}</h2>
      <form onSubmit={handleSubmit} className="space-y-4">
        <div>
          <label className="block text-gray-700 font-semibold mb-2">ID Tratamiento</label>
          <input
            type="text"
            name="ID_Tratamiento"
            value={formData.ID_Tratamiento}
            onChange={handleChange}
            className="w-full p-2 border border-gray-300 rounded"
            placeholder="Ej. TRA0001"
          />
        </div>
        <div>
          <label className="block text-gray-700 font-semibold mb-2">Nombre</label>
          <input
            type="text"
            name="Nombre_Tra"
            value={formData.Nombre_Tra}
            onChange={handleChange}
            className="w-full p-2 border border-gray-300 rounded"
            placeholder="Nombre del Tratamiento"
          />
        </div>
        <div>
          <label className="block text-gray-700 font-semibold mb-2">Descripción</label>
          <textarea
            name="Descripcion_Tra"
            value={formData.Descripcion_Tra}
            onChange={handleChange}
            className="w-full p-2 border border-gray-300 rounded"
            placeholder="Descripción del Tratamiento"
          />
        </div>
        <div>
          <label className="block text-gray-700 font-semibold mb-2">Tipo de Tratamiento</label>
          <input
            type="text"
            name="ID_TipoTratamiento"
            value={formData.ID_TipoTratamiento}
            onChange={handleChange}
            className="w-full p-2 border border-gray-300 rounded"
            placeholder="Ej. TT0001"
          />
        </div>
        <div>
          <label className="block text-gray-700 font-semibold mb-2">Estado del Tratamiento</label>
          <input
            type="text"
            name="ID_EstadoTratamiento"
            value={formData.ID_EstadoTratamiento}
            onChange={handleChange}
            className="w-full p-2 border border-gray-300 rounded"
            placeholder="Ej. EST0001"
          />
        </div>
        <div className="flex justify-end space-x-4">
          <button type="submit" className="bg-blue-700 text-white px-4 py-2 rounded hover:bg-blue-800">
            Guardar
          </button>
          <button type="button" onClick={onClose} className="text-gray-500 hover:text-gray-700">
            Cancelar
          </button>
        </div>
      </form>
    </div>
  );
};

export default TratamientoForm;