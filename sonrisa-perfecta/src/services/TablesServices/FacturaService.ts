import { Factura } from "../../types/type";
import ApiService from "../ApiService/ApiService";

class FacturaService extends ApiService<Factura> {
  constructor() {
    super();
  }

  public getAllFacturas() {
    return this.getAll('/Factura');
  }

  public getFacturaById(id: string) {
    return this.getOne('/Factura', id);
  }

  public createFactura(data: Factura) {
    return this.create('/Factura', data);
  }

  public updateFactura(id: string, data: Partial<Factura>) {
    return this.patch('/Factura', id, data);
  }

  public deleteFactura(id: string) {
    return this.delete('/Factura', id);
  }
}

export const facturaService = new FacturaService();
export default facturaService;
