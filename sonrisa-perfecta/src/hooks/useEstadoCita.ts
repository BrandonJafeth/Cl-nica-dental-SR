// src/hooks/useEstadoCita.ts
import { useState, useEffect } from 'react';
import EstadoCitaService from '../services/TablesServices/EstadoCitasService';
import { EstadoCita } from '../types/type';

export function useEstadoCita() {
  const [estadosCita, setEstadosCita] = useState<EstadoCita[]>([]);
  const [loading, setLoading] = useState<boolean>(false);
  const [error, setError] = useState<string | null>(null);

  const fetchEstadosCita = async () => {
    setLoading(true);
    setError(null);
    try {
      const data = await EstadoCitaService.getAll();
      setEstadosCita(data);
    } catch (err) {
      setError('Error al cargar los estados de cita');
    } finally {
      setLoading(false);
    }
  };

  const addEstadoCita = async (data: EstadoCita) => {
    try {
      const newEstadoCita = await EstadoCitaService.create(data);
      setEstadosCita((prev) => [...prev, newEstadoCita]);
    } catch (err) {
      setError('Error al agregar el estado de cita');
    }
  };

  const updateEstadoCita = async (id: string, data: Partial<EstadoCita>) => {
    try {
      const updatedEstadoCita = await EstadoCitaService.update(id, data);
      setEstadosCita((prev) =>
        prev.map((estadoCita) =>
          estadoCita.ID_EstadoCita === id ? updatedEstadoCita : estadoCita
        )
      );
    } catch (err) {
      setError('Error al actualizar el estado de cita');
    }
  };

  const deleteEstadoCita = async (id: string) => {
    try {
      await EstadoCitaService.delete(id);
      setEstadosCita((prev) =>
        prev.filter((estadoCita) => estadoCita.ID_EstadoCita !== id)
      );
    } catch (err) {
      setError('Error al eliminar el estado de cita');
    }
  };

  useEffect(() => {
    fetchEstadosCita();
  }, []);

  return {
    estadosCita,
    loading,
    error,
    addEstadoCita,
    updateEstadoCita,
    deleteEstadoCita,
    fetchEstadosCita,
  };
}
