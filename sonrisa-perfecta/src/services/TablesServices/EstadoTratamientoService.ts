// src/services/EstadoTratamientoService.ts
import axios from 'axios';
import { EstadoTratamiento } from '../../types/type'

const API_URL = 'https://localhost:7232/api/Estado_Tratamiento';

export const EstadoTratamientoService = {
  async getAll(): Promise<EstadoTratamiento[]> {
    const response = await axios.get<EstadoTratamiento[]>(API_URL);
    return response.data;
  },

  async getById(id: string): Promise<EstadoTratamiento> {
    const response = await axios.get<EstadoTratamiento>(`${API_URL}/${id}`);
    return response.data;
  },

  async create(data: EstadoTratamiento): Promise<EstadoTratamiento> {
    const response = await axios.post<EstadoTratamiento>(API_URL, data);
    return response.data;
  },

  async update(id: string, data: Partial<EstadoTratamiento>): Promise<EstadoTratamiento> {
    const response = await axios.put<EstadoTratamiento>(`${API_URL}/${id}`, data);
    return response.data;
  },

  async delete(id: string): Promise<void> {
    await axios.delete(`${API_URL}/${id}`);
  },
};

export default EstadoTratamientoService;
