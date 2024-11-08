
import { HistorialTratamiento } from '../../types/type';
import ApiService from '../ApiService/ApiService';

class HistorialTratamientoService extends ApiService<HistorialTratamiento> {
  constructor() {
    super();
  }

  public getAllHistorialTratamientos() {
    return this.getAll('/HistorialTratamiento');
  }

  public getHistorialTratamientoById(id: string) {
    return this.getOne('/HistorialTratamiento', id);
  }

  public createHistorialTratamiento(data: HistorialTratamiento) {
    return this.create('/HistorialTratamiento', data);
  }

  public updateHistorialTratamiento(id: string, data: Partial<HistorialTratamiento>) {
    return this.patch('/HistorialTratamiento', id, data);
  }

  public deleteHistorialTratamiento(id: string) {
    return this.delete('/HistorialTratamiento', id);
  }
}

export const historialTratamientoService = new HistorialTratamientoService();
export default historialTratamientoService;
