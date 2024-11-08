// services/AuditoriaService.ts
import { Auditoria } from "../../types/type";
import ApiService from "../ApiService/ApiService";

class AuditoriaService extends ApiService<Auditoria> {
  constructor() {
    super();
  }

  public getAllAuditorias() {
    return this.getAll('/Auditoria');
  }

  public getAuditoriaById(id: string) {
    return this.getOne('/Auditoria', id);
  }

  public createAuditoria(data: Auditoria) {
    return this.create('/Auditoria', data);
  }

  public updateAuditoria(id: string, data: Partial<Auditoria>) {
    return this.update('/Auditoria', id, data);
  }

  public deleteAuditoria(id: string) {
    return this.delete('/Auditoria', id);
  }
}

export const auditoriaService = new AuditoriaService();
export default auditoriaService;
