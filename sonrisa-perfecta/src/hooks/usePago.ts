// src/hooks/usePago.ts
import { useState } from 'react';
import PagoService from '../services/TablesServices/PagoService';
import { Pago } from '../types/type';

export function usePago() {
  const [pagos, setPagos] = useState<Pago[]>([]);
  const [loading, setLoading] = useState<boolean>(false);
  const [error, setError] = useState<string | null>(null);

  const fetchPagos = async () => {
    setLoading(true);
    setError(null);
    try {
      const data = await PagoService.getAll();
      setPagos(data);
    } catch (err) {
      setError('Error al cargar los pagos');
    } finally {
      setLoading(false);
    }
  };

  const createPago = async (data: Pago) => {
    setLoading(true);
    setError(null);
    try {
      const newPago = await PagoService.create(data);
      setPagos((prev) => [...prev, newPago]);
    } catch (err) {
      setError('Error al crear el pago');
      throw err;
    } finally {
      setLoading(false);
    }
  };

  const updatePago = async (id: string, data: Partial<Pago>) => {
    setLoading(true);
    setError(null);
    try {
      const updatedPago = await PagoService.update(id, data);
      setPagos((prev) =>
        prev.map((pago) => (pago.iD_Pago === id ? updatedPago : pago))
      );
    } catch (err) {
      setError('Error al actualizar el pago');
      throw err;
    } finally {
      setLoading(false);
    }
  };

  const deletePago = async (id: string) => {
    setLoading(true);
    setError(null);
    try {
      await PagoService.delete(id);
      setPagos((prev) => prev.filter((pago) => pago.iD_Pago !== id));
    } catch (err) {
      setError('Error al eliminar el pago');
      throw err;
    } finally {
      setLoading(false);
    }
  };

  return {
    pagos,
    loading,
    error,
    fetchPagos,
    createPago,
    updatePago,
    deletePago,
  };
}