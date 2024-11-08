// services/EstadoPagoService.ts

import { EstadoPago } from "../../types/type";
import ApiService from "../ApiService/ApiService";

class EstadoPagoService extends ApiService<EstadoPago> {
  constructor() {
    super();
  }

  public getAllEstadoPago() {
    return this.getAll('/EstadoPago');
  }

  public getEstadoPagoById(id: string) {
    return this.getOne('/EstadoPago', id);
  }

  public createEstadoPago(data: EstadoPago) {
    return this.create('/EstadoPago', data);
  }

  public updateEstadoPago(id: string, data: Partial<EstadoPago>) {
    return this.update('/EstadoPago', id, data);
  }

  public deleteEstadoPago(id: string) {
    return this.delete('/EstadoPago', id);
  }
}

export const estadoPagoService = new EstadoPagoService();
export default estadoPagoService;
