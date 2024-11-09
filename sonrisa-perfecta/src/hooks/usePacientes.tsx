// src/hooks/usePacientes.ts
import { useState, useEffect } from 'react';
import pacienteService from '../services/TablesServices/PacienteService';
import { Paciente } from '../types/type';

export const usePacientes = () => {
  const [pacientes, setPacientes] = useState<Paciente[]>([]);
  const [loading, setLoading] = useState<boolean>(false);
  const [error, setError] = useState<string | null>(null);

  const fetchPacientes = async () => {
    setLoading(true);
    try {
      const data = await pacienteService.getAllPacientes();
      setPacientes(data);
    } catch (err) {
      setError('Error al cargar los pacientes');
    } finally {
      setLoading(false);
    }
  };

  useEffect(() => {
    fetchPacientes();
  }, []);

  return {
    pacientes,
    loading,
    error,
    refetch: fetchPacientes,
  };
};
