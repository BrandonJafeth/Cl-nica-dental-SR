
import axios from 'axios';
import { UsuarioRoles } from '../../types/type';

const API_URL = 'https://localhost:7232/api/UsuarioRoles';

export const UsuarioRolesService = {
  async getAll(): Promise<UsuarioRoles[]> {
    const response = await axios.get<UsuarioRoles[]>(API_URL);
    return response.data;
  },

  async getById(id: string): Promise<UsuarioRoles> {
    const response = await axios.get<UsuarioRoles>(`${API_URL}/${id}`);
    return response.data;
  },

  async create(data: UsuarioRoles): Promise<UsuarioRoles> {
    const response = await axios.post<UsuarioRoles>(API_URL, data);
    return response.data;
  },

  async update(id: string, data: UsuarioRoles): Promise<UsuarioRoles> {
    const response = await axios.put<UsuarioRoles>(`${API_URL}/${id}`, data);
    return response.data;
  },

  async delete(id: string): Promise<void> {
    await axios.delete(`${API_URL}/${id}`);
  },
};
