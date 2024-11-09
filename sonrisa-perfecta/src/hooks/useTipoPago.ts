// src/hooks/useTipoPago.ts
import { useState, useEffect } from 'react';
import TipoPagoService from '../services/TablesServices/TipoPagoService';
import { TipoPago } from '../types/type';

export function useTipoPago() {
  const [tipoPagos, setTipoPagos] = useState<TipoPago[]>([]);
  const [loading, setLoading] = useState<boolean>(false);
  const [error, setError] = useState<string | null>(null);

  const fetchTipoPagos = async () => {
    setLoading(true);
    setError(null);
    try {
      const data = await TipoPagoService.getAll();
      setTipoPagos(data);
    } catch (err) {
      setError('Error al cargar los tipos de pago');
    } finally {
      setLoading(false);
    }
  };

  const addTipoPago = async (data: TipoPago) => {
    try {
      const newTipoPago = await TipoPagoService.create(data);
      setTipoPagos((prev) => [...prev, newTipoPago]);
    } catch (err) {
      setError('Error al agregar el tipo de pago');
    }
  };

  const updateTipoPago = async (id: string, data: Partial<TipoPago>) => {
    try {
      const updatedTipoPago = await TipoPagoService.update(id, data);
      setTipoPagos((prev) =>
        prev.map((tipo) => (tipo.ID_Tipo_Pago === id ? updatedTipoPago : tipo))
      );
    } catch (err) {
      setError('Error al actualizar el tipo de pago');
    }
  };

  const deleteTipoPago = async (id: string) => {
    try {
      await TipoPagoService.delete(id);
      setTipoPagos((prev) => prev.filter((tipo) => tipo.ID_Tipo_Pago !== id));
    } catch (err) {
      setError('Error al eliminar el tipo de pago');
    }
  };

  useEffect(() => {
    fetchTipoPagos();
  }, []);

  return {
    tipoPagos,
    loading,
    error,
    addTipoPago,
    updateTipoPago,
    deleteTipoPago,
    fetchTipoPagos,
  };
}
