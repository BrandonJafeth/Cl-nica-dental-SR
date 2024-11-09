// src/hooks/useCuenta.ts
import { useState, useEffect } from 'react';
import CuentaService from '../services/TablesServices/CuentaService';
import { Cuenta } from '../types/type';

export function useCuenta() {
  const [cuentas, setCuentas] = useState<Cuenta[]>([]);
  const [loading, setLoading] = useState<boolean>(false);
  const [error, setError] = useState<string | null>(null);

  // Función para cargar las cuentas
  const fetchCuentas = async () => {
    setLoading(true);
    setError(null);
    try {
      const data = await CuentaService.getAll();
      console.log("Cuentas cargadas:", data); // Verifica que `ID_Paciente` esté presente en cada cuenta
      setCuentas(data);
    } catch (err) {
      console.error("Error al cargar las cuentas:", err);
      setError('Error al cargar las cuentas');
    } finally {
      setLoading(false);
    }
  };

  // Función para agregar una nueva cuenta
  const addCuenta = async (data: Cuenta) => {
    try {
      const newCuenta = await CuentaService.create(data);
      setCuentas((prev) => [...prev, newCuenta]);
    } catch (err) {
      console.error("Error al agregar la cuenta:", err);
      setError('Error al agregar la cuenta');
    }
  };

  // Función para actualizar una cuenta existente
  const updateCuenta = async (id: string, data: Partial<Cuenta>) => {
    try {
      const updatedCuenta = await CuentaService.update(id, data);
      setCuentas((prev) =>
        prev.map((cuenta) => (cuenta.ID_Cuenta === id ? updatedCuenta : cuenta))
      );
    } catch (err) {
      console.error("Error al actualizar la cuenta:", err);
      setError('Error al actualizar la cuenta');
    }
  };

  // Función para eliminar una cuenta
  const deleteCuenta = async (id: string) => {
    try {
      await CuentaService.delete(id);
      setCuentas((prev) => prev.filter((cuenta) => cuenta.ID_Cuenta !== id));
    } catch (err) {
      console.error("Error al eliminar la cuenta:", err);
      setError('Error al eliminar la cuenta');
    }
  };

  // Ejecuta la carga de cuentas al montar el componente
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
