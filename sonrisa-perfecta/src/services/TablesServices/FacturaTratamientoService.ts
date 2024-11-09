// src/services/FacturaTratamientoService.ts
import axios from 'axios';
import { FacturaTratamiento } from '../../types/type';

const API_URL = 'https://localhost:7232/api/Factura_Tratamiento';

export const FacturaTratamientoService = {
  async getAll(): Promise<FacturaTratamiento[]> {
    const response = await axios.get<FacturaTratamiento[]>(API_URL);
    return response.data;
  },

  async getById(id: string): Promise<FacturaTratamiento> {
    const response = await axios.get<FacturaTratamiento>(`${API_URL}/${id}`);
    return response.data;
  },

  async create(data: FacturaTratamiento): Promise<FacturaTratamiento> {
    const response = await axios.post<FacturaTratamiento>(API_URL, data);
    return response.data;
  },

  async update(id: string, data: Partial<FacturaTratamiento>): Promise<FacturaTratamiento> {
    const response = await axios.put<FacturaTratamiento>(`${API_URL}/${id}`, data);
    return response.data;
  },

  async delete(id: string): Promise<void> {
    await axios.delete(`${API_URL}/${id}`);
  },
};

export default FacturaTratamientoService;
