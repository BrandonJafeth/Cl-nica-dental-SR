// src/services/EspecialidadService.ts
import axios from 'axios';
import { Especialidad } from '../../types/type';

const API_URL = 'https://localhost:7232/api/Especialidad';

export const EspecialidadService = {
  async getAll(): Promise<Especialidad[]> {
    const response = await axios.get<Especialidad[]>(API_URL);
    return response.data;
  },

  async getById(id: string): Promise<Especialidad> {
    const response = await axios.get<Especialidad>(`${API_URL}/${id}`);
    return response.data;
  },

  async create(data: Especialidad): Promise<Especialidad> {
    const response = await axios.post<Especialidad>(API_URL, data);
    return response.data;
  },

  async update(id: string, data: Partial<Especialidad>): Promise<Especialidad> {
    const response = await axios.put<Especialidad>(`${API_URL}/${id}`, data);
    return response.data;
  },

  async delete(id: string): Promise<void> {
    await axios.delete(`${API_URL}/${id}`);
  },
};

export default EspecialidadService;
