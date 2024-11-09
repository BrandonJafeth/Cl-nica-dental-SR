// src/services/DentistaEspecialidadService.ts
import axios from 'axios';
import { DentistaEspecialidad } from '../../types/type';

const API_URL = 'https://localhost:7232/api/Dentista_Especialidad';

export const DentistaEspecialidadService = {
  async getAll(): Promise<DentistaEspecialidad[]> {
    const response = await axios.get<DentistaEspecialidad[]>(API_URL);
    return response.data;
  },

  async getById(id: string): Promise<DentistaEspecialidad> {
    const response = await axios.get<DentistaEspecialidad>(`${API_URL}/${id}`);
    return response.data;
  },

  async create(data: DentistaEspecialidad): Promise<DentistaEspecialidad> {
    const response = await axios.post<DentistaEspecialidad>(API_URL, data);
    return response.data;
  },

  async update(id: string, data: Partial<DentistaEspecialidad>): Promise<DentistaEspecialidad> {
    const response = await axios.put<DentistaEspecialidad>(`${API_URL}/${id}`, data);
    return response.data;
  },

  async delete(id: string): Promise<void> {
    await axios.delete(`${API_URL}/${id}`);
  },
};

export default DentistaEspecialidadService;
