// services/UsuarioService.ts
import { Usuarios } from "../../types/type";
import ApiService from "../ApiService/ApiService";

class UsuarioService extends ApiService<Usuarios> {
  constructor() {
    super();
  }

  public getAllUsuarios() {
    return this.getAll('/Usuario');
  }

  public getUsuarioById(id: string) {
    return this.getOne('/Usuario', id);
  }

  public createUsuario(data: Usuarios) {
    return this.create('/Usuario', data);
  }

  public updateUsuario(id: string, data: Partial<Usuarios>) {
    return this.update('/Usuario', id, data);
  }

  public deleteUsuario(id: string) {
    return this.delete('/Usuario', id);
  }
}

export const usuarioService = new UsuarioService();
export default usuarioService;
