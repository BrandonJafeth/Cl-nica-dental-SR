

import { Dentista } from '../../types/type';
import ApiService from '../ApiService/ApiService';

class DentistaService extends ApiService<Dentista> {
  constructor() {
    super();
  }

  public getAllDentistas() {
    return this.getAll('/Dentista');
  }

  public getDentistaById(id: string) {
    return this.getOne('/Dentista', id);
  }

  public createDentista(data: Dentista) {
    return this.create('/Dentista', data);
  }

  public updateDentista(id: string, data: Partial<Dentista>) {
    return this.update('/Dentista', id, data);
  }

  public deleteDentista(id: string) {
    return this.delete('/Dentista', id);
  }
}

export const dentistaService = new DentistaService();
export default dentistaService;
