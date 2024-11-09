// src/hooks/useTratamiento.ts
import { useState, useEffect } from 'react';
import tratamientoService  from '../services/TablesServices/TratamientoService';
import { Tratamiento } from '../types/type';

export function useTratamiento() {
  const [tratamientos, setTratamientos] = useState<Tratamiento[]>([]);
  const [loading, setLoading] = useState<boolean>(false);
  const [error, setError] = useState<string | null>(null);

  const fetchTratamientos = async () => {
    setLoading(true);
    setError(null);
    try {
      const data = await tratamientoService.getAllTratamientos();
      console.log("Tratamientos cargados:", data); // Verificar que los datos se están cargando
      setTratamientos(data);
    } catch (err) {
      setError('Error al cargar los tratamientos');
    } finally {
      setLoading(false);
    }
  };
  const addTratamiento = async (data: Tratamiento) => {
    try {
      const newTratamiento = await tratamientoService.createTratamiento(data);
      setTratamientos((prev) => [...prev, newTratamiento]);
    } catch (err) {
      setError('Error al agregar el tratamiento');
    }
  };

  const updateTratamiento = async (id: string, data: Partial<Tratamiento>) => {
    try {
      const updatedTratamiento = await tratamientoService.updateTratamiento(id, data);
      setTratamientos((prev) =>
        prev.map((tratamiento) => (tratamiento.ID_Tratamiento === id ? updatedTratamiento : tratamiento))
      );
    } catch (err) {
      setError('Error al actualizar el tratamiento');
    }
  };

  const deleteTratamiento = async (id: string) => {
    try {
      await tratamientoService.deleteTratamiento(id);
      setTratamientos((prev) => prev.filter((tratamiento) => tratamiento.ID_Tratamiento !== id));
    } catch (err) {
      setError('Error al eliminar el tratamiento');
    }
  };

  useEffect(() => {
    fetchTratamientos();
  }, []);

  return {
    tratamientos,
    loading,
    error,
    addTratamiento,
    updateTratamiento,
    deleteTratamiento,
    fetchTratamientos,
  };
}
