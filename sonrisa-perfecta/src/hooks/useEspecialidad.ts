// src/hooks/useEspecialidad.ts
import { useState, useEffect } from 'react';
import EspecialidadService from '../services/TablesServices/EspecialidadService';
import { Especialidad } from '../types/type';

export function useEspecialidad() {
  const [especialidades, setEspecialidades] = useState<Especialidad[]>([]);
  const [loading, setLoading] = useState<boolean>(false);
  const [error, setError] = useState<string | null>(null);

  const fetchEspecialidades = async () => {
    setLoading(true);
    setError(null);
    try {
      const data = await EspecialidadService.getAll();
      setEspecialidades(data);
    } catch (err) {
      setError('Error al cargar las especialidades');
    } finally {
      setLoading(false);
    }
  };

  const addEspecialidad = async (data: Especialidad) => {
    try {
      const newEspecialidad = await EspecialidadService.create(data);
      setEspecialidades((prev) => [...prev, newEspecialidad]);
    } catch (err) {
      setError('Error al agregar la especialidad');
    }
  };

  const updateEspecialidad = async (id: string, data: Partial<Especialidad>) => {
    try {
      const updatedEspecialidad = await EspecialidadService.update(id, data);
      setEspecialidades((prev) =>
        prev.map((especialidad) =>
          especialidad.ID_Especialidad === id ? updatedEspecialidad : especialidad
        )
      );
    } catch (err) {
      setError('Error al actualizar la especialidad');
    }
  };

  const deleteEspecialidad = async (id: string) => {
    try {
      await EspecialidadService.delete(id);
      setEspecialidades((prev) =>
        prev.filter((especialidad) => especialidad.ID_Especialidad !== id)
      );
    } catch (err) {
      setError('Error al eliminar la especialidad');
    }
  };

  useEffect(() => {
    fetchEspecialidades();
  }, []);

  return {
    especialidades,
    loading,
    error,
    addEspecialidad,
    updateEspecialidad,
    deleteEspecialidad,
    fetchEspecialidades,
  };
}
