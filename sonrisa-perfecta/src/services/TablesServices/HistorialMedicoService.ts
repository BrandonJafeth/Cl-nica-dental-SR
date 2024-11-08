// services/HistorialMedicoService.ts
import { HistorialMedico } from "../../types/type";
import ApiService from "../ApiService/ApiService";
class HistorialMedicoService extends ApiService<HistorialMedico> {
  constructor() {
    super();
  }

  public getAllHistorialMedico() {
    return this.getAll('/HistorialMedico');
  }

  public getHistorialMedicoById(id: string) {
    return this.getOne('/HistorialMedico', id);
  }

  public createHistorialMedico(data: HistorialMedico) {
    return this.create('/HistorialMedico', data);
  }

  public updateHistorialMedico(id: string, data: Partial<HistorialMedico>) {
    return this.patch('/HistorialMedico', id, data);
  }

  public deleteHistorialMedico(id: string) {
    return this.delete('/HistorialMedico', id);
  }
}

export const historialMedicoService = new HistorialMedicoService();
export default historialMedicoService;
