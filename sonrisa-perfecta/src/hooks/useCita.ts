// src/hooks/useCita.ts
import { useState, useEffect } from 'react';
import { Cita } from '../types/type';
import citaService from '../services/TablesServices/CitaService';


export function useCita() {
  const [citas, setCitas] = useState<Cita[]>([]);
  const [loading, setLoading] = useState<boolean>(false);
  const [error, setError] = useState<string | null>(null);

  // Cargar todas las citas
  const fetchCitas = async () => {
    setLoading(true);
    setError(null);
    try {
      const data = await citaService.getAllCitas();
      setCitas(data);
    } catch (err) {
      setError('Error al cargar las citas');
    } finally {
      setLoading(false);
    }
  };

  const deleteCita = async (id: string) => {
    try {
      await citaService.deleteCita(id);
      setCitas((prev) => prev.filter((cita) => cita.iD_Cita !== id));
    } catch (err) {
      setError('Error al eliminar la cita');
    }
  };

  useEffect(() => {
    fetchCitas();
  }, []);

  return {
    citas,
    loading,
    error,
    deleteCita,
  };
}
