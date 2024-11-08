
import { Funcionario } from '../../types/type';
import ApiService from '../ApiService/ApiService';

class FuncionarioService extends ApiService<Funcionario> {
  constructor() {
    super();
  }

  public getAllFuncionarios() {
    return this.getAll('/Funcionario');
  }

  public getFuncionarioById(id: string) {
    return this.getOne('/Funcionario', id);
  }

  public createFuncionario(data: Funcionario) {
    return this.create('/Funcionario', data);
  }

  public updateFuncionario(id: string, data: Partial<Funcionario>) {
    return this.update('/Funcionario', id, data);
  }

  public deleteFuncionario(id: string) {
    return this.delete('/Funcionario', id);
  }
}

export const funcionarioService = new FuncionarioService();
export default funcionarioService;
