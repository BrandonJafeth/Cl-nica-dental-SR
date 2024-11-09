// src/services/TratamientoService.ts
import axios from 'axios';
import { Tratamiento } from '../../types/type';

const API_URL = 'https://localhost:7232/api/Tratamiento';

const TratamientoService = {
  /**
   * Obtener todos los tratamientos
   */
  async getAllTratamientos(): Promise<Tratamiento[]> {
    const response = await axios.get<Tratamiento[]>(API_URL);
    return response.data;
  },

  /**
   * Obtener un tratamiento por su ID
   * @param id - ID del tratamiento
   */
  async getTratamientoById(id: string): Promise<Tratamiento> {
    const response = await axios.get<Tratamiento>(`${API_URL}/${id}`);
    return response.data;
  },

  /**
   * Crear un nuevo tratamiento
   * @param data - Datos del tratamiento a crear
   */
  async createTratamiento(data: Tratamiento): Promise<Tratamiento> {
    const response = await axios.post<Tratamiento>(API_URL, data);
    return response.data;
  },

  /**
   * Actualizar un tratamiento existente
   * @param id - ID del tratamiento a actualizar
   * @param data - Datos actualizados del tratamiento
   */
  async updateTratamiento(id: string, data: Partial<Tratamiento>): Promise<Tratamiento> {
    const response = await axios.put<Tratamiento>(`${API_URL}/${id}`, data);
    return response.data;
  },

  /**
   * Eliminar un tratamiento
   * @param id - ID del tratamiento a eliminar
   */
  async deleteTratamiento(id: string): Promise<void> {
    await axios.delete(`${API_URL}/${id}`);
  },
};

export default TratamientoService;
