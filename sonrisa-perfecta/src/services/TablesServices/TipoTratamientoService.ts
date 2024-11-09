// src/services/TipoTratamientoService.ts
import axios from 'axios';
import { TipoTratamiento } from '../../types/type';

const API_URL = 'https://localhost:7232/api/TipoTratamiento';

export const TipoTratamientoService = {
  async getAll(): Promise<TipoTratamiento[]> {
    const response = await axios.get<TipoTratamiento[]>(API_URL);
    return response.data;
  },

  async getById(id: string): Promise<TipoTratamiento> {
    const response = await axios.get<TipoTratamiento>(`${API_URL}/${id}`);
    return response.data;
  },

  async create(data: TipoTratamiento): Promise<TipoTratamiento> {
    const response = await axios.post<TipoTratamiento>(API_URL, data);
    return response.data;
  },

  async update(id: string, data: Partial<TipoTratamiento>): Promise<TipoTratamiento> {
    const response = await axios.put<TipoTratamiento>(`${API_URL}/${id}`, data);
    return response.data;
  },

  async delete(id: string): Promise<void> {
    await axios.delete(`${API_URL}/${id}`);
  },
};

export default TipoTratamientoService;
