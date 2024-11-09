// src/hooks/useDentistaEspecialidad.ts
import { useState, useEffect } from 'react';
import DentistaEspecialidadService from '../services/TablesServices/DentistaEspecialidadService';
import { DentistaEspecialidad } from '../types/type';

export function useDentistaEspecialidad() {
  const [dentistasEspecialidad, setDentistasEspecialidad] = useState<DentistaEspecialidad[]>([]);
  const [loading, setLoading] = useState<boolean>(false);
  const [error, setError] = useState<string | null>(null);

  const fetchDentistasEspecialidad = async () => {
    setLoading(true);
    setError(null);
    try {
      const data = await DentistaEspecialidadService.getAll();
      setDentistasEspecialidad(data);
    } catch (err) {
      setError('Error al cargar las asociaciones de dentistas y especialidades');
    } finally {
      setLoading(false);
    }
  };

  const addDentistaEspecialidad = async (data: DentistaEspecialidad) => {
    try {
      const newDentistaEspecialidad = await DentistaEspecialidadService.create(data);
      setDentistasEspecialidad((prev) => [...prev, newDentistaEspecialidad]);
    } catch (err) {
      setError('Error al agregar la asociación de dentista y especialidad');
    }
  };

  const updateDentistaEspecialidad = async (id: string, data: Partial<DentistaEspecialidad>) => {
    try {
      const updatedDentistaEspecialidad = await DentistaEspecialidadService.update(id, data);
      setDentistasEspecialidad((prev) =>
        prev.map((dentistaEspecialidad) =>
          dentistaEspecialidad.ID_Dentista_Especialidad === id ? updatedDentistaEspecialidad : dentistaEspecialidad
        )
      );
    } catch (err) {
      setError('Error al actualizar la asociación de dentista y especialidad');
    }
  };

  const deleteDentistaEspecialidad = async (id: string) => {
    try {
      await DentistaEspecialidadService.delete(id);
      setDentistasEspecialidad((prev) =>
        prev.filter((dentistaEspecialidad) => dentistaEspecialidad.ID_Dentista_Especialidad !== id)
      );
    } catch (err) {
      setError('Error al eliminar la asociación de dentista y especialidad');
    }
  };

  useEffect(() => {
    fetchDentistasEspecialidad();
  }, []);

  return {
    dentistasEspecialidad,
    loading,
    error,
    addDentistaEspecialidad,
    updateDentistaEspecialidad,
    deleteDentistaEspecialidad,
    fetchDentistasEspecialidad,
  };
}
