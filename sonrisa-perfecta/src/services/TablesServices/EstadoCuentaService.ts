// src/services/EstadoCuentaService.ts
import axios from 'axios';
import { EstadoCuenta } from '../../types/type';

const API_URL = 'https://localhost:7232/api/EstadoCuenta';

export const EstadoCuentaService = {
  async getAll(): Promise<EstadoCuenta[]> {
    const response = await axios.get<EstadoCuenta[]>(API_URL);
    return response.data;
  },

  async getById(id: string): Promise<EstadoCuenta> {
    const response = await axios.get<EstadoCuenta>(`${API_URL}/${id}`);
    return response.data;
  },

  async create(data: EstadoCuenta): Promise<EstadoCuenta> {
    const response = await axios.post<EstadoCuenta>(API_URL, data);
    return response.data;
  },

  async update(id: string, data: Partial<EstadoCuenta>): Promise<EstadoCuenta> {
    const response = await axios.put<EstadoCuenta>(`${API_URL}/${id}`, data);
    return response.data;
  },

  async delete(id: string): Promise<void> {
    await axios.delete(`${API_URL}/${id}`);
  },
};

export default EstadoCuentaService;
