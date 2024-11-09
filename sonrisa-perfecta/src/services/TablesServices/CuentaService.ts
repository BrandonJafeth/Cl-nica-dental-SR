// src/services/CuentaService.ts
import axios from 'axios';
import { Cuenta } from '../../types/type';

const API_URL = 'https://localhost:7232/api/Cuenta';

export const CuentaService = {
  async getAll(): Promise<Cuenta[]> {
    const response = await axios.get<Cuenta[]>(API_URL);
    return response.data;
  },

  async getById(id: string): Promise<Cuenta> {
    const response = await axios.get<Cuenta>(`${API_URL}/${id}`);
    return response.data;
  },

  async create(data: Cuenta): Promise<Cuenta> {
    const response = await axios.post<Cuenta>(API_URL, data);
    return response.data;
  },

  async update(id: string, data: Partial<Cuenta>): Promise<Cuenta> {
    const response = await axios.put<Cuenta>(`${API_URL}/${id}`, data);
    return response.data;
  },

  async delete(id: string): Promise<void> {
    await axios.delete(`${API_URL}/${id}`);
  },
};

export default CuentaService;
