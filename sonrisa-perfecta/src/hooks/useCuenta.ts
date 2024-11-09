// src/hooks/useCuenta.ts
import { useState, useEffect } from 'react';
import CuentaService from '../services/TablesServices/CuentaService';
import { Cuenta } from '../types/type';

export function useCuenta() {
  const [cuentas, setCuentas] = useState<Cuenta[]>([]);
  const [loading, setLoading] = useState<boolean>(false);
  const [error, setError] = useState<string | null>(null);

  const fetchCuentas = async () => {
    setLoading(true);
    setError(null);
    try {
      const data = await CuentaService.getAll();
      setCuentas(data);
    } catch (err) {
      setError('Error al cargar las cuentas');
    } finally {
      setLoading(false);
    }
  };

  const addCuenta = async (data: Cuenta) => {
    try {
      const newCuenta = await CuentaService.create(data);
      setCuentas((prev) => [...prev, newCuenta]);
    } catch (err) {
      setError('Error al agregar la cuenta');
    }
  };

  const updateCuenta = async (id: string, data: Partial<Cuenta>) => {
    try {
      const updatedCuenta = await CuentaService.update(id, data);
      setCuentas((prev) =>
        prev.map((cuenta) => (cuenta.ID_Cuenta === id ? updatedCuenta : cuenta))
      );
    } catch (err) {
      setError('Error al actualizar la cuenta');
    }
  };

  const deleteCuenta = async (id: string) => {
    try {
      await CuentaService.delete(id);
      setCuentas((prev) => prev.filter((cuenta) => cuenta.ID_Cuenta !== id));
    } catch (err) {
      setError('Error al eliminar la cuenta');
    }
  };

  useEffect(() => {
    fetchCuentas();
  }, []);

  return {
    cuentas,
    loading,
    error,
    addCuenta,
    updateCuenta,
    deleteCuenta,
    fetchCuentas,
  };
}
