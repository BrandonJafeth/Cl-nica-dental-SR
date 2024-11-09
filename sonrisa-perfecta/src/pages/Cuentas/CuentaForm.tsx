// src/components/CuentaForm.tsx

import { useState } from "react";
import { Cuenta } from "../../types/type";

interface CuentaFormProps {
  cuenta?: Cuenta;
  onClose: () => void;
  onSave: (data: Cuenta) => void;
}

function CuentaForm({ cuenta, onClose, onSave }: CuentaFormProps) {
  const [formData, setFormData] = useState<Cuenta>({
    ID_Cuenta: cuenta?.ID_Cuenta || "",
    Saldo_Total: cuenta?.Saldo_Total || 0,
    Fecha_Apertura: cuenta?.Fecha_Apertura || "",
    Fecha_Cierre: cuenta?.Fecha_Cierre || "",
    Fecha_Ultima_Actualizacion: cuenta?.Fecha_Ultima_Actualizacion || "",
    Observaciones: cuenta?.Observaciones || "",
    ID_Estado_Cuenta: cuenta?.ID_Estado_Cuenta || "",
    ID_Factura: cuenta?.ID_Factura || "",
    ID_Paciente: cuenta?.ID_Paciente || ""
  });

  const handleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    const { name, value } = e.target;
    setFormData({ ...formData, [name]: value });
  };

  const handleSubmit = (e: React.FormEvent) => {
    e.preventDefault();
    onSave(formData);
    onClose();
  };

  return (
    <div className="bg-white p-6 shadow-lg rounded-lg">
      <h2 className="text-2xl font-semibold mb-4">{cuenta ? "Editar Cuenta" : "Nueva Cuenta"}</h2>
      <form onSubmit={handleSubmit} className="space-y-4">
        {/* Agregar inputs para cada campo */}
        {/* Input para ID Cuenta, Saldo Total, Fecha Apertura, etc. */}
        {/* Ejemplo: */}
        <input
          type="text"
          name="ID_Cuenta"
          value={formData.ID_Cuenta}
          onChange={handleChange}
          placeholder="ID Cuenta"
          className="w-full p-2 border border-gray-300 rounded"
        />
        {/* Repite este patrón para los demás campos */}
        <button type="submit" className="bg-blue-700 text-white px-4 py-2 rounded">
          Guardar
        </button>
      </form>
    </div>
  );
}

export default CuentaForm;
