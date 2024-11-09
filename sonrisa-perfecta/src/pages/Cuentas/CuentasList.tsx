// src/components/CuentaList.tsx

import { Cuenta } from "../../types/type";



interface CuentaListProps {
  cuentas: Cuenta[];
  onViewCuenta: (cuenta: Cuenta) => void;
  onEditCuenta: (cuenta: Cuenta) => void;
  onDeleteCuenta: (id: string) => void;
}

function CuentaList({ cuentas, onViewCuenta, onEditCuenta, onDeleteCuenta }: CuentaListProps) {
  return (
    <div className="bg-white shadow-md rounded-lg overflow-hidden">
      <table className="min-w-full leading-normal">
        <thead className="bg-blue-700 text-white">
          <tr>
            <th className="px-5 py-3 border-b border-gray-200 text-left text-sm font-semibold">ID Cuenta</th>
            <th className="px-5 py-3 border-b border-gray-200 text-left text-sm font-semibold">Saldo Total</th>
            <th className="px-5 py-3 border-b border-gray-200 text-left text-sm font-semibold">Fecha Apertura</th>
            <th className="px-5 py-3 border-b border-gray-200 text-left text-sm font-semibold">Fecha Cierre</th>
            <th className="px-5 py-3 border-b border-gray-200 text-center text-sm font-semibold">Acciones</th>
          </tr>
        </thead>
        <tbody>
          {cuentas.map((cuenta) => (
            <tr key={cuenta.ID_Cuenta} className="hover:bg-gray-50">
              <td className="px-5 py-4 border-b border-gray-200 text-sm">{cuenta.ID_Cuenta}</td>
              <td className="px-5 py-4 border-b border-gray-200 text-sm">${cuenta.Saldo_Total}</td>
              <td className="px-5 py-4 border-b border-gray-200 text-sm">{cuenta.Fecha_Apertura}</td>
              <td className="px-5 py-4 border-b border-gray-200 text-sm">{cuenta.Fecha_Cierre}</td>
              <td className="px-5 py-4 border-b border-gray-200 text-sm text-center space-x-2">
                <button
                  onClick={() => onViewCuenta(cuenta)}
                  className="bg-blue-500 text-white px-3 py-1 rounded-md hover:bg-blue-600 transition shadow"
                >
                  Ver
                </button>
                <button
                  onClick={() => onEditCuenta(cuenta)}
                  className="bg-yellow-500 text-white px-3 py-1 rounded-md hover:bg-yellow-600 transition shadow"
                >
                  Editar
                </button>
                <button
                  onClick={() => onDeleteCuenta(cuenta.ID_Cuenta)}
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
}

export default CuentaList;
