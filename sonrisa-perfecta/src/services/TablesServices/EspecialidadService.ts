// services/EspecialidadService.ts
import { Especialidad } from "../../types/type";
import ApiService from "../ApiService/ApiService";

class EspecialidadService extends ApiService<Especialidad> {
  constructor() {
    super();
  }

  public getAllEspecialidades() {
    return this.getAll('/Especialidad');
  }

  public getEspecialidadById(id: string) {
    return this.getOne('/Especialidad', id);
  }

  public createEspecialidad(data: Especialidad) {
    return this.create('/Especialidad', data);
  }

  public updateEspecialidad(id: string, data: Partial<Especialidad>) {
    return this.patch('/Especialidad', id, data);
  }

  public deleteEspecialidad(id: string) {
    return this.delete('/Especialidad', id);
  }
}

export const especialidadService = new EspecialidadService();
export default especialidadService;
