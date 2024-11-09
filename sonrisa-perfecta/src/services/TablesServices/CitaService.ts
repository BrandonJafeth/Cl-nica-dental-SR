// src/services/CitaService.ts
import axios from 'axios';
import { Cita } from '../../types/type';

const API_URL = 'https://localhost:7232/api/Cita';

class CitaService {
  // Obtiene todas las citas
  public async getAllCitas(): Promise<Cita[]> {
    const response = await axios.get<Cita[]>(API_URL);
    return response.data;
  }

  // Obtiene una cita por ID
  public async getCitaById(id: string): Promise<Cita> {
    const response = await axios.get<Cita>(`${API_URL}/${id}`);
    return response.data;
  }

  // Crea una nueva cita
  public async createCita(data: Cita): Promise<Cita> {
    const response = await axios.post<Cita>(API_URL, data);
    return response.data;
  }

  // Actualiza una cita existente
  public async updateCita(id: string, data: Cita): Promise<Cita> {
    const response = await axios.put<Cita>(`${API_URL}/${id}`, data, {
        headers: { 'Content-Type': 'application/json' }
    });
    return response.data;
}

  // Elimina una cita
  public async deleteCita(id: string): Promise<void> {
    await axios.delete(`${API_URL}/${id}`);
  }
}

export const citaService = new CitaService();
export default citaService;
