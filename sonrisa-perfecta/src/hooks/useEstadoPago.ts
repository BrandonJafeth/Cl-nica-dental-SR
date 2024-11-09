// src/hooks/useEstadoPago.ts
import { useState, useEffect } from 'react';
import EstadoPagoService from '../services/TablesServices/EstadoPagoService';
import { EstadoPago } from '../types/type';

export function useEstadoPago() {
  const [estadosPago, setEstadosPago] = useState<EstadoPago[]>([]);
  const [loading, setLoading] = useState<boolean>(false);
  const [error, setError] = useState<string | null>(null);

  const fetchEstadosPago = async () => {
    setLoading(true);
    setError(null);
    try {
      const data = await EstadoPagoService.getAll();
      setEstadosPago(data);
    } catch (err) {
      setError('Error al cargar los estados de pago');
    } finally {
      setLoading(false);
    }
  };

  const addEstadoPago = async (data: EstadoPago) => {
    try {
      const newEstadoPago = await EstadoPagoService.create(data);
      setEstadosPago((prev) => [...prev, newEstadoPago]);
    } catch (err) {
      setError('Error al agregar el estado de pago');
    }
  };

  const updateEstadoPago = async (id: string, data: Partial<EstadoPago>) => {
    try {
      const updatedEstadoPago = await EstadoPagoService.update(id, data);
      setEstadosPago((prev) =>
        prev.map((estadoPago) =>
          estadoPago.ID_EstadoPago === id ? updatedEstadoPago : estadoPago
        )
      );
    } catch (err) {
      setError('Error al actualizar el estado de pago');
    }
  };

  const deleteEstadoPago = async (id: string) => {
    try {
      await EstadoPagoService.delete(id);
      setEstadosPago((prev) =>
        prev.filter((estadoPago) => estadoPago.ID_EstadoPago !== id)
      );
    } catch (err) {
      setError('Error al eliminar el estado de pago');
    }
  };

  useEffect(() => {
    fetchEstadosPago();
  }, []);

  return {
    estadosPago,
    loading,
    error,
    addEstadoPago,
    updateEstadoPago,
    deleteEstadoPago,
    fetchEstadosPago,
  };
}
