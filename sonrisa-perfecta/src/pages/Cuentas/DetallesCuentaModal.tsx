// src/components/DetallesCuentaModal.tsx
import React from 'react';
import { Cuenta } from '../../types/type';

interface DetallesCuentaModalProps {
  cuenta: Cuenta;
  onClose: () => void;
}

function DetallesCuentaModal({ cuenta, onClose }: DetallesCuentaModalProps) {
  return (
    <div className="fixed inset-0 bg-black bg-opacity-50 flex justify-center items-center z-50">
      <div className="bg-white rounded-lg w-full max-w-md p-6 shadow-lg">
        <h2 className="text-2xl font-bold text-center text-blue-700 mb-4">Detalles de la Cuenta</h2>
        
        <div className="space-y-3 text-gray-700">
          <p><strong>ID Cuenta:</strong> {cuenta.iD_Cuenta}</p>
          <p><strong>Saldo Total:</strong> {cuenta.saldo_Total}</p>
          <p><strong>Fecha de Apertura:</strong> {cuenta.fecha_Apertura}</p>
          <p><strong>Fecha de Cierre:</strong> {cuenta.fecha_Cierre === '0001-01-01' ? 'N/A' : cuenta.fecha_Cierre}</p>
          <p><strong>Última Actualización:</strong> {cuenta.fecha_Ultima_Actualizacion}</p>
          <p><strong>Observaciones:</strong> {cuenta.observaciones}</p>
        </div>

        <div className="flex justify-end mt-6">
          <button
            onClick={onClose}
            className="bg-blue-600 text-white px-4 py-2 rounded-lg hover:bg-blue-700 transition duration-300"
          >
            Cerrar
          </button>
        </div>
      </div>
    </div>
  );
}

export default DetallesCuentaModal;
