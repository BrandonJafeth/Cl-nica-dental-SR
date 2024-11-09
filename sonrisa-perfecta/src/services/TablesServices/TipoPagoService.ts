// src/services/TipoPagoService.ts
import axios from 'axios';
import { TipoPago } from '../../types/type';

const API_URL = 'https://localhost:7232/api/TipoPago';

export const TipoPagoService = {
  async getAll(): Promise<TipoPago[]> {
    const response = await axios.get<TipoPago[]>(API_URL);
    return response.data;
  },

  async getById(id: string): Promise<TipoPago> {
    const response = await axios.get<TipoPago>(`${API_URL}/${id}`);
    return response.data;
  },

  async create(data: TipoPago): Promise<TipoPago> {
    const response = await axios.post<TipoPago>(API_URL, data);
    return response.data;
  },

  async update(id: string, data: Partial<TipoPago>): Promise<TipoPago> {
    const response = await axios.put<TipoPago>(`${API_URL}/${id}`, data);
    return response.data;
  },

  async delete(id: string): Promise<void> {
    await axios.delete(`${API_URL}/${id}`);
  },
};

export default TipoPagoService;
