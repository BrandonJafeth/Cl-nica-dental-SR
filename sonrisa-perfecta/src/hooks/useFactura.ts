// src/hooks/useFactura.ts
import { useState, useEffect } from 'react';
import FacturaService from '../services/TablesServices/FacturaService';
import { Factura } from '../types/type';

export function useFactura() {
  const [facturas, setFacturas] = useState<Factura[]>([]);
  const [loading, setLoading] = useState<boolean>(false);
  const [error, setError] = useState<string | null>(null);

  const fetchFacturas = async () => {
    setLoading(true);
    setError(null);
    try {
      const data = await FacturaService.getAll();
      setFacturas(data);
    } catch (err) {
      setError('Error al cargar las facturas');
    } finally {
      setLoading(false);
    }
  };

  const addFactura = async (data: Factura) => {
    try {
      const newFactura = await FacturaService.create(data);
      setFacturas((prev) => [...prev, newFactura]);
    } catch (err) {
      setError('Error al agregar la factura');
    }
  };

  const updateFactura = async (id: string, data: Partial<Factura>) => {
    try {
      const updatedFactura = await FacturaService.update(id, data);
      setFacturas((prev) =>
        prev.map((factura) =>
          factura.ID_Factura === id ? updatedFactura : factura
        )
      );
    } catch (err) {
      setError('Error al actualizar la factura');
    }
  };

  const deleteFactura = async (id: string) => {
    try {
      await FacturaService.delete(id);
      setFacturas((prev) => prev.filter((factura) => factura.ID_Factura !== id));
    } catch (err) {
      setError('Error al eliminar la factura');
    }
  };

  useEffect(() => {
    fetchFacturas();
  }, []);

  return {
    facturas,
    loading,
    error,
    addFactura,
    updateFactura,
    deleteFactura,
    fetchFacturas,
  };
}
