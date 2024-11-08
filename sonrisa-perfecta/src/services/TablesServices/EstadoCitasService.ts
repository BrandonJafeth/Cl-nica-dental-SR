// services/EstadoCitasService.ts
import { EstadoCitas } from "../../types/type";
import ApiService from "../ApiService/ApiService";

class EstadoCitasService extends ApiService<EstadoCitas> {
  constructor() {
    super();
  }

  public getAllEstadoCitas() {
    return this.getAll('/EstadoCitas');
  }

  public getEstadoCitaById(id: string) {
    return this.getOne('/EstadoCitas', id);
  }

  public createEstadoCita(data: EstadoCitas) {
    return this.create('/EstadoCitas', data);
  }

  public updateEstadoCita(id: string, data: Partial<EstadoCitas>) {
    return this.patch('/EstadoCitas', id, data);
  }

  public deleteEstadoCita(id: string) {
    return this.delete('/EstadoCitas', id);
  }
}

export const estadoCitasService = new EstadoCitasService();
export default estadoCitasService;
