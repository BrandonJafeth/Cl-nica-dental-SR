// src/hooks/useFacturaProcedimiento.ts
import { useState, useEffect } from 'react';
import FacturaProcedimientoService from '../services/TablesServices/FacturaProcedimientoService';
import { FacturaProcedimiento } from '../types/type';

export function useFacturaProcedimiento() {
  const [facturaProcedimientos, setFacturaProcedimientos] = useState<FacturaProcedimiento[]>([]);
  const [loading, setLoading] = useState<boolean>(false);
  const [error, setError] = useState<string | null>(null);

  const fetchFacturaProcedimientos = async () => {
    setLoading(true);
    setError(null);
    try {
      const data = await FacturaProcedimientoService.getAll();
      setFacturaProcedimientos(data);
    } catch (err) {
      setError('Error al cargar los registros de Factura_Procedimiento');
    } finally {
      setLoading(false);
    }
  };

  const addFacturaProcedimiento = async (data: FacturaProcedimiento) => {
    try {
      const newFacturaProcedimiento = await FacturaProcedimientoService.create(data);
      setFacturaProcedimientos((prev) => [...prev, newFacturaProcedimiento]);
    } catch (err) {
      setError('Error al agregar el registro de Factura_Procedimiento');
    }
  };

  const updateFacturaProcedimiento = async (id: string, data: Partial<FacturaProcedimiento>) => {
    try {
      const updatedFacturaProcedimiento = await FacturaProcedimientoService.update(id, data);
      setFacturaProcedimientos((prev) =>
        prev.map((facturaProcedimiento) =>
          facturaProcedimiento.ID_Factura_Procedimiento === id ? updatedFacturaProcedimiento : facturaProcedimiento
        )
      );
    } catch (err) {
      setError('Error al actualizar el registro de Factura_Procedimiento');
    }
  };

  const deleteFacturaProcedimiento = async (id: string) => {
    try {
      await FacturaProcedimientoService.delete(id);
      setFacturaProcedimientos((prev) =>
        prev.filter((facturaProcedimiento) => facturaProcedimiento.ID_Factura_Procedimiento !== id)
      );
    } catch (err) {
      setError('Error al eliminar el registro de Factura_Procedimiento');
    }
  };

  useEffect(() => {
    fetchFacturaProcedimientos();
  }, []);

  return {
    facturaProcedimientos,
    loading,
    error,
    addFacturaProcedimiento,
    updateFacturaProcedimiento,
    deleteFacturaProcedimiento,
    fetchFacturaProcedimientos,
  };
}
