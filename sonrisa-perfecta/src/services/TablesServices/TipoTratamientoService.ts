// services/TipoTratamientoService.ts
import { TipoTratamiento } from "../../types/type";
import ApiService from "../ApiService/ApiService";

class TipoTratamientoService extends ApiService<TipoTratamiento> {
  constructor() {
    super();
  }

  public getAllTipoTratamiento() {
    return this.getAll('/TipoTratamiento');
  }

  public getTipoTratamientoById(id: string) {
    return this.getOne('/TipoTratamiento', id);
  }

  public createTipoTratamiento(data: TipoTratamiento) {
    return this.create('/TipoTratamiento', data);
  }

  public updateTipoTratamiento(id: string, data: Partial<TipoTratamiento>) {
    return this.patch('/TipoTratamiento', id, data);
  }

  public deleteTipoTratamiento(id: string) {
    return this.delete('/TipoTratamiento', id);
  }
}

export const tipoTratamientoService = new TipoTratamientoService();
export default tipoTratamientoService;
