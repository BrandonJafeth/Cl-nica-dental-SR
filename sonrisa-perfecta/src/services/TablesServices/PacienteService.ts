// src/services/TablesServices/PacienteService.ts
import axios from 'axios';
import { Paciente } from '../../types/type';

const API_URL = 'https://localhost:7232/api/Paciente';

const pacienteService = {
  async getAllPacientes(): Promise<Paciente[]> {
    const response = await axios.get<Paciente[]>(API_URL);
    return response.data;
  },
  async getPacienteById(id: string): Promise<Paciente> {
    const response = await axios.get<Paciente>(`${API_URL}/${id}`);
    return response.data;
  },
  async createPaciente(data: Paciente): Promise<Paciente> {
    const response = await axios.post<Paciente>(API_URL, data);
    return response.data;
  },
  async updatePaciente(id: string, data: Partial<Paciente>): Promise<Paciente> {
    const response = await axios.put<Paciente>(`${API_URL}/${id}`, data);
    return response.data;
  },
  async deletePaciente(id: string): Promise<void> {
    await axios.delete(`${API_URL}/${id}`);
  },
};

export default pacienteService;
