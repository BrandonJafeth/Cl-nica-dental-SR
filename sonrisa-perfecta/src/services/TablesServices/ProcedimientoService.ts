// services/ProcedimientoService.ts
import { Procedimiento } from "../../types/type";
import ApiService from "../ApiService/ApiService";
class ProcedimientoService extends ApiService<Procedimiento> {
  constructor() {
    super();
  }

  public getAllProcedimientos() {
    return this.getAll('/Procedimiento');
  }

  public getProcedimientoById(id: string) {
    return this.getOne('/Procedimiento', id);
  }

  public createProcedimiento(data: Procedimiento) {
    return this.create('/Procedimiento', data);
  }

  public updateProcedimiento(id: string, data: Partial<Procedimiento>) {
    return this.patch('/Procedimiento', id, data);
  }

  public deleteProcedimiento(id: string) {
    return this.delete('/Procedimiento', id);
  }
}

export const procedimientoService = new ProcedimientoService();
export default procedimientoService;
