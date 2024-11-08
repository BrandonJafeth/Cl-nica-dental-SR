// services/TratamientoService.ts
import { Tratamiento } from "../../types/type";
import ApiService from "../ApiService/ApiService";

class TratamientoService extends ApiService<Tratamiento> {
  constructor() {
    super();
  }

  public getAllTratamientos() {
    return this.getAll('/Tratamiento');
  }

  public getTratamientoById(id: string) {
    return this.getOne('/Tratamiento', id);
  }

  public createTratamiento(data: Tratamiento) {
    return this.create('/Tratamiento', data);
  }

  public updateTratamiento(id: string, data: Partial<Tratamiento>) {
    return this.patch('/Tratamiento', id, data);
  }

  public deleteTratamiento(id: string) {
    return this.delete('/Tratamiento', id);
  }
}

export const tratamientoService = new TratamientoService();
export default tratamientoService;
