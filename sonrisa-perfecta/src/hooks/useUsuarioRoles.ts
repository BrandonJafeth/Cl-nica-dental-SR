import { useState, useEffect } from 'react';
import { UsuarioRolesService } from '../services/TablesServices/UsuarioRolesService';
import { UsuarioRoles } from '../types/type';

export function useUsuarioRoles() {
  const [usuarioRoles, setUsuarioRoles] = useState<UsuarioRoles[]>([]);
  const [loading, setLoading] = useState<boolean>(false);
  const [error, setError] = useState<string | null>(null);

  const fetchUsuarioRoles = async () => {
    setLoading(true);
    setError(null);
    try {
      const data = await UsuarioRolesService.getAll();
      setUsuarioRoles(data);
    } catch (err) {
      setError('Error al cargar los roles de usuario');
    } finally {
      setLoading(false);
    }
  };

  const addUsuarioRole = async (data: UsuarioRoles) => {
    try {
      const newRole = await UsuarioRolesService.create(data);
      setUsuarioRoles((prev) => [...prev, newRole]);
    } catch (err) {
      setError('Error al agregar un rol de usuario');
    }
  };

  const updateUsuarioRole = async (id: string, data: UsuarioRoles) => {
    try {
      const updatedRole = await UsuarioRolesService.update(id, data);
      setUsuarioRoles((prev) =>
        prev.map((role) => (role.ID_Usuario_Roles === id ? updatedRole : role))
      );
    } catch (err) {
      setError('Error al actualizar el rol de usuario');
    }
  };

  const deleteUsuarioRole = async (id: string) => {
    try {
      await UsuarioRolesService.delete(id);
      setUsuarioRoles((prev) => prev.filter((role) => role.ID_Usuario_Roles !== id));
    } catch (err) {
      setError('Error al eliminar el rol de usuario');
    }
  };

  useEffect(() => {
    fetchUsuarioRoles();
  }, []);

  return {
    usuarioRoles,
    loading,
    error,
    addUsuarioRole,
    updateUsuarioRole,
    deleteUsuarioRole,
    fetchUsuarioRoles,
  };
}
