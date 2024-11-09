// src/services/DentistaService.ts
import axios from 'axios';
import { Dentista } from '../../types/type';

const API_URL = 'https://localhost:7232/api/Dentista';

export const DentistaService = {
  async getAll(): Promise<Dentista[]> {
    const response = await axios.get<Dentista[]>(API_URL);
    return response.data;
  },

  async getById(id: string): Promise<Dentista> {
    const response = await axios.get<Dentista>(`${API_URL}/${id}`);
    return response.data;
  },

  async create(data: Dentista): Promise<Dentista> {
    const response = await axios.post<Dentista>(API_URL, data);
    return response.data;
  },

  async update(id: string, data: Partial<Dentista>): Promise<Dentista> {
    const response = await axios.put<Dentista>(`${API_URL}/${id}`, data);
    return response.data;
  },

  async delete(id: string): Promise<void> {
    await axios.delete(`${API_URL}/${id}`);
  },
};

export default DentistaService;
