// src/pages/Tratamiento/TratamientoList.tsx
import React from "react";
import { Tratamiento } from "../../types/type";

interface TratamientoListProps {
  tratamientos: Tratamiento[];
  onEditTratamiento: (tratamiento: Tratamiento) => void;
  onDeleteTratamiento: (id: string) => Promise<void>;
}

const TratamientoList: React.FC<TratamientoListProps> = ({
  tratamientos,
  onEditTratamiento,
  onDeleteTratamiento,
}) => {
  console.log("Tratamientos recibidos:", tratamientos); // Verifica que se están recibiendo los datos

  return (
    <div className="bg-white shadow-md rounded-lg overflow-hidden">
      <table className="min-w-full leading-normal">
        <thead className="bg-blue-700 text-white">
          <tr>
            <th className="px-5 py-3 border-b border-gray-200 text-left text-sm font-semibold">Nombre</th>
            <th className="px-5 py-3 border-b border-gray-200 text-left text-sm font-semibold">Descripción</th>
            <th className="px-5 py-3 border-b border-gray-200 text-left text-sm font-semibold">Tipo Tratamiento</th>
            <th className="px-5 py-3 border-b border-gray-200 text-center text-sm font-semibold">Acciones</th>
          </tr>
        </thead>
        <tbody>
          {tratamientos.map((tratamiento) => (
            <tr key={tratamiento.iD_Tratamiento} className="hover:bg-gray-50">
              <td className="px-5 py-4 border-b border-gray-200 text-sm">{tratamiento.nombre_Tra}</td>
              <td className="px-5 py-4 border-b border-gray-200 text-sm">{tratamiento.descripcion_Tra}</td>
              <td className="px-5 py-4 border-b border-gray-200 text-sm">{tratamiento.iD_TipoTratamiento}</td>
              <td className="px-5 py-4 border-b border-gray-200 text-sm text-center space-x-2">
                <button
                  onClick={() => onEditTratamiento(tratamiento)}
                  className="bg-yellow-500 text-white px-3 py-1 rounded-md hover:bg-yellow-600 transition shadow"
                >
                  Editar
                </button>
                <button
                  onClick={() => onDeleteTratamiento(tratamiento.iD_Tratamiento)}
                  className="bg-red-500 text-white px-3 py-1 rounded-md hover:bg-red-600 transition shadow"
                >
                  Eliminar
                </button>
              </td>
            </tr>
          ))}
        </tbody>
      </table>
    </div>
  );
};

export default TratamientoList;
