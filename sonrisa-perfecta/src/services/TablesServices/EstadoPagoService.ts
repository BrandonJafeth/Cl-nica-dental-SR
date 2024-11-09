// src/services/EstadoPagoService.ts
import axios from 'axios';
import { EstadoPago } from '../../types/type';

const API_URL = 'https://localhost:7232/api/Estado_Pago';

export const EstadoPagoService = {
  async getAll(): Promise<EstadoPago[]> {
    const response = await axios.get<EstadoPago[]>(API_URL);
    return response.data;
  },

  async getById(id: string): Promise<EstadoPago> {
    const response = await axios.get<EstadoPago>(`${API_URL}/${id}`);
    return response.data;
  },

  async create(data: EstadoPago): Promise<EstadoPago> {
    const response = await axios.post<EstadoPago>(API_URL, data);
    return response.data;
  },

  async update(id: string, data: Partial<EstadoPago>): Promise<EstadoPago> {
    const response = await axios.put<EstadoPago>(`${API_URL}/${id}`, data);
    return response.data;
  },

  async delete(id: string): Promise<void> {
    await axios.delete(`${API_URL}/${id}`);
  },
};

export default EstadoPagoService;
