// src/hooks/useFacturaTratamiento.ts
import { useState, useEffect } from 'react';
import FacturaTratamientoService from '../services/TablesServices/FacturaTratamientoService';
import { FacturaTratamiento } from '../types/type';

export function useFacturaTratamiento() {
  const [facturaTratamientos, setFacturaTratamientos] = useState<FacturaTratamiento[]>([]);
  const [loading, setLoading] = useState<boolean>(false);
  const [error, setError] = useState<string | null>(null);

  const fetchFacturaTratamientos = async () => {
    setLoading(true);
    setError(null);
    try {
      const data = await FacturaTratamientoService.getAll();
      setFacturaTratamientos(data);
    } catch (err) {
      setError('Error al cargar los registros de Factura_Tratamiento');
    } finally {
      setLoading(false);
    }
  };

  const addFacturaTratamiento = async (data: FacturaTratamiento) => {
    try {
      const newFacturaTratamiento = await FacturaTratamientoService.create(data);
      setFacturaTratamientos((prev) => [...prev, newFacturaTratamiento]);
    } catch (err) {
      setError('Error al agregar el registro de Factura_Tratamiento');
    }
  };

  const updateFacturaTratamiento = async (id: string, data: Partial<FacturaTratamiento>) => {
    try {
      const updatedFacturaTratamiento = await FacturaTratamientoService.update(id, data);
      setFacturaTratamientos((prev) =>
        prev.map((facturaTratamiento) => 
          facturaTratamiento.ID_Factura_Tratamiento === id ? updatedFacturaTratamiento : facturaTratamiento
        )
      );
    } catch (err) {
      setError('Error al actualizar el registro de Factura_Tratamiento');
    }
  };

  const deleteFacturaTratamiento = async (id: string) => {
    try {
      await FacturaTratamientoService.delete(id);
      setFacturaTratamientos((prev) => prev.filter((facturaTratamiento) => facturaTratamiento.ID_Factura_Tratamiento !== id));
    } catch (err) {
      setError('Error al eliminar el registro de Factura_Tratamiento');
    }
  };

  useEffect(() => {
    fetchFacturaTratamientos();
  }, []);

  return {
    facturaTratamientos,
    loading,
    error,
    addFacturaTratamiento,
    updateFacturaTratamiento,
    deleteFacturaTratamiento,
    fetchFacturaTratamientos,
  };
}
