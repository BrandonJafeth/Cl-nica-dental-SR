// services/PacienteService.ts

import { Paciente } from "../../types/type";
import ApiService from "../ApiService/ApiService";


class PacienteService extends ApiService<Paciente> {
  constructor() {
    super();
  }

  public getAllPacientes() {
    return this.getAll('/Paciente');
  }

  public getPacienteById(id: string) {
    return this.getOne('/Paciente', id);
  }

  public createPaciente(data: Paciente) {
    return this.create('/Paciente', data);
  }

  public updatePaciente(id: string, data: Partial<Paciente>) {
    return this.update('/Paciente', id, data);
  }

  public deletePaciente(id: string) {
    return this.delete('/Paciente', id);
  }
}

export const pacienteService = new PacienteService();
export default pacienteService;
