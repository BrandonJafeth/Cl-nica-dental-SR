// src/hooks/useDentista.ts
import { useState, useEffect } from 'react';
import DentistaService from '../services/TablesServices/DentistaService';
import { Dentista } from '../types/type';

export function useDentista() {
  const [dentistas, setDentistas] = useState<Dentista[]>([]);
  const [loading, setLoading] = useState<boolean>(false);
  const [error, setError] = useState<string | null>(null);

  const fetchDentistas = async () => {
    setLoading(true);
    setError(null);
    try {
      const data = await DentistaService.getAll();
      setDentistas(data);
    } catch (err) {
      setError('Error al cargar los dentistas');
    } finally {
      setLoading(false);
    }
  };

  const addDentista = async (data: Dentista) => {
    try {
      const newDentista = await DentistaService.create(data);
      setDentistas((prev) => [...prev, newDentista]);
    } catch (err) {
      setError('Error al agregar el dentista');
    }
  };

  const updateDentista = async (id: string, data: Partial<Dentista>) => {
    try {
      const updatedDentista = await DentistaService.update(id, data);
      setDentistas((prev) =>
        prev.map((dentista) => (dentista.ID_Dentista === id ? updatedDentista : dentista))
      );
    } catch (err) {
      setError('Error al actualizar el dentista');
    }
  };

  const deleteDentista = async (id: string) => {
    try {
      await DentistaService.delete(id);
      setDentistas((prev) => prev.filter((dentista) => dentista.ID_Dentista !== id));
    } catch (err) {
      setError('Error al eliminar el dentista');
    }
  };

  useEffect(() => {
    fetchDentistas();
  }, []);

  return {
    dentistas,
    loading,
    error,
    addDentista,
    updateDentista,
    deleteDentista,
    fetchDentistas,
  };
}
