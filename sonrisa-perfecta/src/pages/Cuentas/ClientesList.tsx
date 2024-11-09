// src/components/ClientesList.tsx
import React from 'react';
import { Paciente } from '../../types/type';


interface ClientesListProps {
  pacientes: Paciente[];
  onViewCuenta: (id: string) => void;
}

const ClientesList: React.FC<ClientesListProps> = ({ pacientes, onViewCuenta }) => {
  return (
    <div className="bg-white shadow-md rounded-lg overflow-hidden">
      <table className="min-w-full leading-normal">
        <thead className="bg-blue-700 text-white">
          <tr>
            <th className="px-5 py-3 border-b border-gray-200 text-left text-sm font-semibold">Nombre</th>
            <th className="px-5 py-3 border-b border-gray-200 text-left text-sm font-semibold">Correo</th>
            <th className="px-5 py-3 border-b border-gray-200 text-left text-sm font-semibold">Teléfono</th>
            <th className="px-5 py-3 border-b border-gray-200 text-center text-sm font-semibold">Acciones</th>
          </tr>
        </thead>
        <tbody>
          {pacientes.map((paciente) => (
            <tr key={paciente.iD_Paciente} className="hover:bg-gray-50">
              <td className="px-5 py-4 border-b border-gray-200 text-sm">{`${paciente.nombre_Pac} ${paciente.apellido1_Pac}`}</td>
              <td className="px-5 py-4 border-b border-gray-200 text-sm">{paciente.correo_Pac}</td>
              <td className="px-5 py-4 border-b border-gray-200 text-sm">{paciente.telefono_Pac}</td>
              <td className="px-5 py-4 border-b border-gray-200 text-sm text-center space-x-2">
                <button
                  onClick={() => onViewCuenta(paciente.iD_Paciente)}
                  className="bg-blue-500 text-white px-3 py-1 rounded-md hover:bg-blue-600 transition shadow"
                >
                  Ver Cuenta
                </button>
              </td>
            </tr>
          ))}
        </tbody>
      </table>
    </div>
  );
};

export default ClientesList;
