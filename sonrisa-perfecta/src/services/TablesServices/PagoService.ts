// src/services/TablesServices/PagoService.ts
import axios from 'axios';
import { Pago } from '../../types/type';

const API_URL = 'https://localhost:7232/api/Pago';

export const PagoService = {
  async getAll(): Promise<Pago[]> {
    const response = await axios.get<Pago[]>(API_URL);
    return response.data;
  },

  async getById(id: string): Promise<Pago> {
    const response = await axios.get<Pago>(`${API_URL}/${id}`);
    return response.data;
  },

  async create(data: Pago): Promise<Pago> {
    const response = await axios.post<Pago>(API_URL, data);
    return response.data;
  },

  async update(id: string, data: Partial<Pago>): Promise<Pago> {
    const response = await axios.put<Pago>(`${API_URL}/${id}`, data);
    return response.data;
  },

  async delete(id: string): Promise<void> {
    await axios.delete(`${API_URL}/${id}`);
  },
};

export default PagoService;