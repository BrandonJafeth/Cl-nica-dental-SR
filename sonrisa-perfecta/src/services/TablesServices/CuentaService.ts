// services/CuentaService.ts
import { Cuenta } from "../../types/type";
import ApiService from "../ApiService/ApiService";
class CuentaService extends ApiService<Cuenta> {
  constructor() {
    super();
  }

  public getAllCuentas() {
    return this.getAll('/Cuenta');
  }

  public getCuentaById(id: string) {
    return this.getOne('/Cuenta', id);
  }

  public createCuenta(data: Cuenta) {
    return this.create('/Cuenta', data);
  }

  public updateCuenta(id: string, data: Partial<Cuenta>) {
    return this.patch('/Cuenta', id, data);
  }

  public deleteCuenta(id: string) {
    return this.delete('/Cuenta', id);
  }
}

export const cuentaService = new CuentaService();
export default cuentaService;
