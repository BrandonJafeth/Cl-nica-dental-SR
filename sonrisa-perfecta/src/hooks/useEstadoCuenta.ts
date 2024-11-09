// src/hooks/useEstadoCuenta.ts
import { useState, useEffect } from 'react';
import EstadoCuentaService from '../services/TablesServices/EstadoCuentaService';
import { EstadoCuenta } from '../types/type';

export function useEstadoCuenta() {
  const [estadosCuenta, setEstadosCuenta] = useState<EstadoCuenta[]>([]);
  const [loading, setLoading] = useState<boolean>(false);
  const [error, setError] = useState<string | null>(null);

  const fetchEstadosCuenta = async () => {
    setLoading(true);
    setError(null);
    try {
      const data = await EstadoCuentaService.getAll();
      setEstadosCuenta(data);
    } catch (err) {
      setError('Error al cargar los estados de cuenta');
    } finally {
      setLoading(false);
    }
  };

  const addEstadoCuenta = async (data: EstadoCuenta) => {
    try {
      const newEstadoCuenta = await EstadoCuentaService.create(data);
      setEstadosCuenta((prev) => [...prev, newEstadoCuenta]);
    } catch (err) {
      setError('Error al agregar el estado de cuenta');
    }
  };

  const updateEstadoCuenta = async (id: string, data: Partial<EstadoCuenta>) => {
    try {
      const updatedEstadoCuenta = await EstadoCuentaService.update(id, data);
      setEstadosCuenta((prev) =>
        prev.map((estadoCuenta) =>
          estadoCuenta.ID_Estado_Cuenta === id ? updatedEstadoCuenta : estadoCuenta
        )
      );
    } catch (err) {
      setError('Error al actualizar el estado de cuenta');
    }
  };

  const deleteEstadoCuenta = async (id: string) => {
    try {
      await EstadoCuentaService.delete(id);
      setEstadosCuenta((prev) =>
        prev.filter((estadoCuenta) => estadoCuenta.ID_Estado_Cuenta !== id)
      );
    } catch (err) {
      setError('Error al eliminar el estado de cuenta');
    }
  };

  useEffect(() => {
    fetchEstadosCuenta();
  }, []);

  return {
    estadosCuenta,
    loading,
    error,
    addEstadoCuenta,
    updateEstadoCuenta,
    deleteEstadoCuenta,
    fetchEstadosCuenta,
  };
}
