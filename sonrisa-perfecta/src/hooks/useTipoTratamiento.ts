// src/hooks/useTipoTratamiento.ts
import { useState, useEffect } from 'react';
import TipoTratamientoService from '../services/TablesServices/TipoTratamientoService';
import { TipoTratamiento } from '../types/type';

export function useTipoTratamiento() {
  const [tipoTratamientos, setTipoTratamientos] = useState<TipoTratamiento[]>([]);
  const [loading, setLoading] = useState<boolean>(false);
  const [error, setError] = useState<string | null>(null);

  const fetchTipoTratamientos = async () => {
    setLoading(true);
    setError(null);
    try {
      const data = await TipoTratamientoService.getAll();
      setTipoTratamientos(data);
    } catch (err) {
      setError('Error al cargar los tipos de tratamiento');
    } finally {
      setLoading(false);
    }
  };

  const addTipoTratamiento = async (data: TipoTratamiento) => {
    try {
      const newTipoTratamiento = await TipoTratamientoService.create(data);
      setTipoTratamientos((prev) => [...prev, newTipoTratamiento]);
    } catch (err) {
      setError('Error al agregar el tipo de tratamiento');
    }
  };

  const updateTipoTratamiento = async (id: string, data: Partial<TipoTratamiento>) => {
    try {
      const updatedTipoTratamiento = await TipoTratamientoService.update(id, data);
      setTipoTratamientos((prev) =>
        prev.map((tipo) => (tipo.ID_TipoTratamiento === id ? updatedTipoTratamiento : tipo))
      );
    } catch (err) {
      setError('Error al actualizar el tipo de tratamiento');
    }
  };

  const deleteTipoTratamiento = async (id: string) => {
    try {
      await TipoTratamientoService.delete(id);
      setTipoTratamientos((prev) => prev.filter((tipo) => tipo.ID_TipoTratamiento !== id));
    } catch (err) {
      setError('Error al eliminar el tipo de tratamiento');
    }
  };

  useEffect(() => {
    fetchTipoTratamientos();
  }, []);

  return {
    tipoTratamientos,
    loading,
    error,
    addTipoTratamiento,
    updateTipoTratamiento,
    deleteTipoTratamiento,
    fetchTipoTratamientos,
  };
}
