// src/services/EstadoCitaService.ts
import axios from 'axios';
import { EstadoCita } from '../../types/type';

const API_URL = 'https://localhost:7232/api/Estado_Cita';

export const EstadoCitaService = {
  async getAll(): Promise<EstadoCita[]> {
    const response = await axios.get<EstadoCita[]>(API_URL);
    return response.data;
  },

  async getById(id: string): Promise<EstadoCita> {
    const response = await axios.get<EstadoCita>(`${API_URL}/${id}`);
    return response.data;
  },

  async create(data: EstadoCita): Promise<EstadoCita> {
    const response = await axios.post<EstadoCita>(API_URL, data);
    return response.data;
  },

  async update(id: string, data: Partial<EstadoCita>): Promise<EstadoCita> {
    const response = await axios.put<EstadoCita>(`${API_URL}/${id}`, data);
    return response.data;
  },

  async delete(id: string): Promise<void> {
    await axios.delete(`${API_URL}/${id}`);
  },
};

export default EstadoCitaService;
