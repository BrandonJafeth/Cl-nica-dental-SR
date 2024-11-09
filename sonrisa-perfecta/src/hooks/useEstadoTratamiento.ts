// src/hooks/useEstadoTratamiento.ts
import { useState, useEffect } from 'react';
import EstadoTratamientoService from '../services/TablesServices/EstadoTratamientoService';
import { EstadoTratamiento } from '../types/type';

export function useEstadoTratamiento() {
  const [estadosTratamiento, setEstadosTratamiento] = useState<EstadoTratamiento[]>([]);
  const [loading, setLoading] = useState<boolean>(false);
  const [error, setError] = useState<string | null>(null);

  const fetchEstadosTratamiento = async () => {
    setLoading(true);
    setError(null);
    try {
      const data = await EstadoTratamientoService.getAll();
      setEstadosTratamiento(data);
    } catch (err) {
      setError('Error al cargar los estados de tratamiento');
    } finally {
      setLoading(false);
    }
  };

  const addEstadoTratamiento = async (data: EstadoTratamiento) => {
    try {
      const newEstadoTratamiento = await EstadoTratamientoService.create(data);
      setEstadosTratamiento((prev) => [...prev, newEstadoTratamiento]);
    } catch (err) {
      setError('Error al agregar el estado de tratamiento');
    }
  };

  const updateEstadoTratamiento = async (id: string, data: Partial<EstadoTratamiento>) => {
    try {
      const updatedEstadoTratamiento = await EstadoTratamientoService.update(id, data);
      setEstadosTratamiento((prev) =>
        prev.map((estadoTratamiento) =>
          estadoTratamiento.ID_EstadoTratamiento === id ? updatedEstadoTratamiento : estadoTratamiento
        )
      );
    } catch (err) {
      setError('Error al actualizar el estado de tratamiento');
    }
  };

  const deleteEstadoTratamiento = async (id: string) => {
    try {
      await EstadoTratamientoService.delete(id);
      setEstadosTratamiento((prev) =>
        prev.filter((estadoTratamiento) => estadoTratamiento.ID_EstadoTratamiento !== id)
      );
    } catch (err) {
      setError('Error al eliminar el estado de tratamiento');
    }
  };

  useEffect(() => {
    fetchEstadosTratamiento();
  }, []);

  return {
    estadosTratamiento,
    loading,
    error,
    addEstadoTratamiento,
    updateEstadoTratamiento,
    deleteEstadoTratamiento,
    fetchEstadosTratamiento,
  };
}
