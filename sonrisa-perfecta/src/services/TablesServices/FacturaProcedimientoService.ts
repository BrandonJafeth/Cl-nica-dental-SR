// src/services/FacturaProcedimientoService.ts
import axios from 'axios';
import { FacturaProcedimiento } from '../../types/type';

const API_URL = 'https://localhost:7232/api/Factura_Procedimiento';

export const FacturaProcedimientoService = {
  async getAll(): Promise<FacturaProcedimiento[]> {
    const response = await axios.get<FacturaProcedimiento[]>(API_URL);
    return response.data;
  },

  async getById(id: string): Promise<FacturaProcedimiento> {
    const response = await axios.get<FacturaProcedimiento>(`${API_URL}/${id}`);
    return response.data;
  },

  async create(data: FacturaProcedimiento): Promise<FacturaProcedimiento> {
    const response = await axios.post<FacturaProcedimiento>(API_URL, data);
    return response.data;
  },

  async update(id: string, data: Partial<FacturaProcedimiento>): Promise<FacturaProcedimiento> {
    const response = await axios.put<FacturaProcedimiento>(`${API_URL}/${id}`, data);
    return response.data;
  },

  async delete(id: string): Promise<void> {
    await axios.delete(`${API_URL}/${id}`);
  },
};

export default FacturaProcedimientoService;
