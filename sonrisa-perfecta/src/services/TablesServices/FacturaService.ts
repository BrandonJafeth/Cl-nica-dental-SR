// src/services/FacturaService.ts
import axios from 'axios';
import { Factura } from '../../types/type';

const API_URL = 'https://localhost:7232/api/Factura';

export const FacturaService = {
  async getAll(): Promise<Factura[]> {
    const response = await axios.get<Factura[]>(API_URL);
    return response.data;
  },

  async getById(id: string): Promise<Factura> {
    const response = await axios.get<Factura>(`${API_URL}/${id}`);
    return response.data;
  },

  async create(data: Factura): Promise<Factura> {
    const response = await axios.post<Factura>(API_URL, data);
    return response.data;
  },

  async update(id: string, data: Partial<Factura>): Promise<Factura> {
    const response = await axios.put<Factura>(`${API_URL}/${id}`, data);
    return response.data;
  },

  async delete(id: string): Promise<void> {
    await axios.delete(`${API_URL}/${id}`);
  },
};

export default FacturaService;
