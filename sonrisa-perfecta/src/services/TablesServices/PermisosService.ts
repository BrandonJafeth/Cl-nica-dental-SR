// services/PermisosService.ts
import { Permisos } from "../../types/type";
import ApiService from "../ApiService/ApiService";  

class PermisosService extends ApiService<Permisos> {
  constructor() {
    super();
  }

  public getAllPermisos() {
    return this.getAll('/Permisos');
  }

  public getPermisoById(id: string) {
    return this.getOne('/Permisos', id);
  }

  public createPermiso(data: Permisos) {
    return this.create('/Permisos', data);
  }

  public updatePermiso(id: string, data: Partial<Permisos>) {
    return this.update('/Permisos', id, data);
  }

  public deletePermiso(id: string) {
    return this.delete('/Permisos', id);
  }
}

export const permisosService = new PermisosService();
export default permisosService;
