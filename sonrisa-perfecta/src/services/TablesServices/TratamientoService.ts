
import { Tratamiento } from '../../types/type';
import ApiService from '../ApiService/ApiService';

class TratamientoService extends ApiService<Tratamiento> {
  constructor() {
    super();
  }

  public async getAllTratamientos(): Promise<Tratamiento[]> {
    const response = await this.getAll('/Tratamiento');
    return response.data; // Devuelve solo los datos
  }

  public async getTratamientoById(id: string): Promise<Tratamiento> {
    const response = await this.getOne('/Tratamiento', id);
    return response.data; // Devuelve solo los datos
  }

  public async createTratamiento(data: Tratamiento): Promise<Tratamiento> {
    const response = await this.create('/Tratamiento', data);
    return response.data; // Devuelve solo los datos
  }

  public async updateTratamiento(id: string, data: Partial<Tratamiento>): Promise<Tratamiento> {
    const response = await this.update('/Tratamiento', id, data);
    return response.data; // Devuelve solo los datos
  }

  public async deleteTratamiento(id: string): Promise<void> {
    await this.delete('/Tratamiento', id);
  }
}

export const tratamientoService = new TratamientoService();
export default tratamientoService;
