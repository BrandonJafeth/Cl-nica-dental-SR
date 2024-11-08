
import { Cita } from '../../types/type';
import ApiService from '../ApiService/ApiService';

class CitaService extends ApiService<Cita> {
  constructor() {
    super();
  }

  public getAllCitas() {
    return this.getAll('/Cita');
  }

  public getCitaById(id: string) {
    return this.getOne('/Cita', id);
  }

  public createCita(data: Cita) {
    return this.create('/Cita', data);
  }

  public updateCita(id: string, data: Partial<Cita>) {
    return this.patch('/Cita', id, data);
  }

  public deleteCita(id: string) {
    return this.delete('/Cita', id);
  }
}

export const citaService = new CitaService();
export default citaService;
