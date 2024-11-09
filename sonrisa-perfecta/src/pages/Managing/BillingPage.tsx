// BillingPage.tsx
import Navbar from "../../components/Navbar";
import { useFactura } from "../../hooks/useFactura";
import { Factura } from "../../types/type";
import { useState } from "react";

function BillingPage() {
  const { facturas, loading, error, addFactura, updateFactura, deleteFactura } = useFactura();
  const [showViewModal, setShowViewModal] = useState(false);
  const [showEditModal, setShowEditModal] = useState(false);
  const [showDeleteModal, setShowDeleteModal] = useState(false);
  const [showAddModal, setShowAddModal] = useState(false);
  const [selectedFactura, setSelectedFactura] = useState<Factura | null>(null);
  const [newFactura, setNewFactura] = useState<Factura>({
    iD_Factura: '',
    montoTotal_Fa: 0,
    fechaEmision_Fa: '',
    iD_EstadoPago: '',
  });

  if (loading) {
    return (
      <div className="min-h-screen bg-gray-100 text-gray-800 flex items-center justify-center">
        <p>Cargando facturas...</p>
      </div>
    );
  }

  if (error) {
    return (
      <div className="min-h-screen bg-gray-100 text-gray-800 flex items-center justify-center">
        <p>{error}</p>
      </div>
    );
  }

  const handleView = (factura: Factura) => {
    setSelectedFactura(factura);
    setShowViewModal(true);
  };

  const handleEdit = (factura: Factura) => {
    setSelectedFactura(factura);
    setShowEditModal(true);
  };

  const handleDelete = (factura: Factura) => {
    setSelectedFactura(factura);
    setShowDeleteModal(true);
  };

  const handleUpdateFactura = async () => {
    if (selectedFactura) {
      await updateFactura(selectedFactura.iD_Factura, selectedFactura);
      setShowEditModal(false);
    }
  };

  const handleDeleteFactura = async () => {
    if (selectedFactura) {
      await deleteFactura(selectedFactura.iD_Factura);
      setShowDeleteModal(false);
    }
  };

  const handleAddFactura = async () => {
    await addFactura(newFactura);
    setShowAddModal(false);
    setNewFactura({
      iD_Factura: '',
      montoTotal_Fa: 0,
      fechaEmision_Fa: '',
      iD_EstadoPago: '',
    });
  };

  return (
    <div className="min-h-screen bg-gray-100 text-gray-800">
      <Navbar />
      <div className="container mx-auto py-10 px-4">
        <h1 className="text-3xl font-bold text-center text-blue-700 mb-8">
          Gestión de Facturas
        </h1>

        {/* Botón para agregar una nueva factura */}
        <div className="flex justify-end mb-6">
          <button
            onClick={() => setShowAddModal(true)}
            className="bg-blue-700 text-white px-6 py-2 rounded-lg hover:bg-blue-800 transition duration-300 shadow-md"
          >
            + Nueva Factura
          </button>
        </div>

        {/* Tabla de facturas */}
        <div className="bg-white shadow-md rounded-lg overflow-hidden">
          <table className="min-w-full leading-normal">
            <thead className="bg-blue-700 text-white">
              <tr>
                <th className="px-5 py-3 border-b border-gray-200 text-left text-sm font-semibold">
                  ID Factura
                </th>
                <th className="px-5 py-3 border-b border-gray-200 text-left text-sm font-semibold">
                  Fecha Emisión
                </th>
                <th className="px-5 py-3 border-b border-gray-200 text-left text-sm font-semibold">
                  Monto Total
                </th>
                <th className="px-5 py-3 border-b border-gray-200 text-left text-sm font-semibold">
                  Estado Pago
                </th>
                <th className="px-5 py-3 border-b border-gray-200 text-center text-sm font-semibold">
                  Acciones
                </th>
              </tr>
            </thead>
            <tbody>
              {facturas.map((factura) => (
                <tr key={factura.iD_Factura} className="hover:bg-gray-50">
                  <td className="px-5 py-4 border-b border-gray-200 text-sm">
                    {factura.iD_Factura}
                  </td>
                  <td className="px-5 py-4 border-b border-gray-200 text-sm">
                    {factura.fechaEmision_Fa}
                  </td>
                  <td className="px-5 py-4 border-b border-gray-200 text-sm">
                    ${factura.montoTotal_Fa.toFixed(2)}
                  </td>
                  <td className="px-5 py-4 border-b border-gray-200 text-sm">
                    {factura.iD_EstadoPago}
                  </td>
                  <td className="px-5 py-4 border-b border-gray-200 text-sm text-center">
                    <button
                      onClick={() => handleView(factura)}
                      className="bg-green-500 text-white px-3 py-1 rounded-md hover:bg-green-600 transition mx-1 shadow"
                    >
                      Ver
                    </button>
                    <button
                      onClick={() => handleEdit(factura)}
                      className="bg-yellow-500 text-white px-3 py-1 rounded-md hover:bg-yellow-600 transition mx-1 shadow"
                    >
                      Editar
                    </button>
                    <button
                      onClick={() => handleDelete(factura)}
                      className="bg-red-500 text-white px-3 py-1 rounded-md hover:bg-red-600 transition mx-1 shadow"
                    >
                      Eliminar
                    </button>
                  </td>
                </tr>
              ))}
            </tbody>
          </table>
        </div>

              {/* Modal Agregar */}
              {showAddModal && (
                  <div className="fixed inset-0 flex items-center justify-center bg-black bg-opacity-50">
                      <div className="bg-white p-6 rounded-lg shadow-lg w-96">
                          <h2 className="text-xl font-bold mb-4">Agregar Nueva Factura</h2>
                          <form
                              onSubmit={(e) => {
                                  e.preventDefault();
                                  handleAddFactura();
                              }}
                          >
                              <div className="mb-3">
                                  <label className="block text-sm font-medium">ID Factura</label>
                                  <input
                                      type="text"
                                      value={newFactura.iD_Factura}
                                      onChange={(e) =>
                                          setNewFactura({ ...newFactura, iD_Factura: e.target.value })
                                      }
                                      className="mt-1 block w-full border border-gray-300 rounded-md p-2"
                                      required
                                  />
                              </div>
                              <div className="mb-3">
                                  <label className="block text-sm font-medium">Monto Total</label>
                                  <input
                                      type="number"
                                      step="0.01"
                                      value={newFactura.montoTotal_Fa}
                                      onChange={(e) =>
                                          setNewFactura({
                                              ...newFactura,
                                              montoTotal_Fa: parseFloat(e.target.value) || 0,
                                          })
                                      }
                                      className="mt-1 block w-full border border-gray-300 rounded-md p-2"
                                      required
                                  />
                              </div>
                              <div className="mb-3">
                                  <label className="block text-sm font-medium">Fecha Emisión</label>
                                  <input
                                      type="date"
                                      value={newFactura.fechaEmision_Fa}
                                      onChange={(e) =>
                                          setNewFactura({ ...newFactura, fechaEmision_Fa: e.target.value })
                                      }
                                      className="mt-1 block w-full border border-gray-300 rounded-md p-2"
                                      required
                                  />
                              </div>
                              <div className="mb-3">
                                  <label className="block text-sm font-medium">ID Estado Pago</label>
                                  <input
                                      type="text"
                                      value={newFactura.iD_EstadoPago}
                                      onChange={(e) =>
                                          setNewFactura({ ...newFactura, iD_EstadoPago: e.target.value })
                                      }
                                      className="mt-1 block w-full border border-gray-300 rounded-md p-2"
                                      required
                                  />
                              </div>
                              <div className="flex justify-end">
                                  <button
                                      type="button"
                                      onClick={() => setShowAddModal(false)}
                                      className="bg-gray-500 text-white px-4 py-2 rounded mr-2"
                                  >
                                      Cancelar
                                  </button>
                                  <button
                                      type="submit"
                                      className="bg-blue-700 text-white px-4 py-2 rounded"
                                  >
                                      Agregar
                                  </button>
                              </div>
                          </form>
                      </div>
                  </div>
              )}

              {/* Modal Editar */}
              {showEditModal && selectedFactura && (
                  <div className="fixed inset-0 flex items-center justify-center bg-black bg-opacity-50">
                      <div className="bg-white p-6 rounded-lg shadow-lg w-96">
                          <h2 className="text-xl font-bold mb-4">Editar Factura</h2>
                          <form
                              onSubmit={async (e) => {
                                  e.preventDefault();
                                  await handleUpdateFactura();
                              }}
                          >
                              <div className="mb-3">
                                  <label className="block text-sm font-medium">ID Factura</label>
                                  <input
                                      type="text"
                                      value={selectedFactura.iD_Factura}
                                      readOnly
                                      className="mt-1 block w-full border border-gray-300 rounded-md p-2 bg-gray-100"
                                  />
                              </div>
                              <div className="mb-3">
                                  <label className="block text-sm font-medium">Monto Total</label>
                                  <input
                                      type="number"
                                      step="0.01"
                                      value={selectedFactura.montoTotal_Fa}
                                      onChange={(e) =>
                                          setSelectedFactura({
                                              ...selectedFactura!,
                                              montoTotal_Fa: parseFloat(e.target.value) || 0,
                                          })
                                      }
                                      className="mt-1 block w-full border border-gray-300 rounded-md p-2"
                                  />
                              </div>
                              <div className="mb-3">
                                  <label className="block text-sm font-medium">Fecha Emisión</label>
                                  <input
                                      type="date"
                                      value={selectedFactura.fechaEmision_Fa}
                                      onChange={(e) =>
                                          setSelectedFactura({
                                              ...selectedFactura!,
                                              fechaEmision_Fa: e.target.value,
                                          })
                                      }
                                      className="mt-1 block w-full border border-gray-300 rounded-md p-2"
                                  />
                              </div>
                              <div className="mb-3">
                                  <label className="block text-sm font-medium">ID Estado Pago</label>
                                  <input
                                      type="text"
                                      value={selectedFactura.iD_EstadoPago}
                                      onChange={(e) =>
                                          setSelectedFactura({
                                              ...selectedFactura!,
                                              iD_EstadoPago: e.target.value,
                                          })
                                      }
                                      className="mt-1 block w-full border border-gray-300 rounded-md p-2"
                                  />
                              </div>
                              <div className="flex justify-end">
                                  <button
                                      type="button"
                                      onClick={() => setShowEditModal(false)}
                                      className="bg-gray-500 text-white px-4 py-2 rounded mr-2"
                                  >
                                      Cancelar
                                  </button>
                                  <button
                                      type="submit"
                                      className="bg-blue-700 text-white px-4 py-2 rounded"
                                  >
                                      Guardar
                                  </button>
                              </div>
                          </form>
                      </div>
                  </div>
              )}

              {/* Modal Eliminar */}
              {showDeleteModal && selectedFactura && (
                  <div className="fixed inset-0 flex items-center justify-center bg-black bg-opacity-50">
                      <div className="bg-white p-6 rounded-lg shadow-lg">
                          <h2 className="text-xl font-bold mb-4">Eliminar Factura</h2>
                          <p>
                              ¿Está seguro que desea eliminar la factura con ID{' '}
                              {selectedFactura.iD_Factura}?
                          </p>
                          <div className="flex justify-end mt-4">
                              <button
                                  onClick={() => setShowDeleteModal(false)}
                                  className="bg-gray-500 text-white px-4 py-2 rounded mr-2"
                              >
                                  Cancelar
                              </button>
                              <button
                                  onClick={handleDeleteFactura}
                                  className="bg-red-500 text-white px-4 py-2 rounded"
                              >
                                  Eliminar
                              </button>
                          </div>
                      </div>
                  </div>
              )}
              {/* Modal Ver */}
              {showViewModal && selectedFactura && (
                  <div className="fixed inset-0 flex items-center justify-center bg-black bg-opacity-50">
                      <div className="bg-white p-6 rounded-lg shadow-lg">
                          <h2 className="text-xl font-bold mb-4">Detalles de la Factura</h2>
                          <p>
                              <strong>ID:</strong> {selectedFactura.iD_Factura}
                          </p>
                          <p>
                              <strong>Monto Total:</strong> $
                              {selectedFactura.montoTotal_Fa.toFixed(2)}
                          </p>
                          <p>
                              <strong>Fecha Emisión:</strong> {selectedFactura.fechaEmision_Fa}
                          </p>
                          <p>
                              <strong>ID Estado Pago:</strong> {selectedFactura.iD_EstadoPago}
                          </p>
                          <button
                              onClick={() => setShowViewModal(false)}
                              className="mt-4 bg-blue-700 text-white px-4 py-2 rounded"
                          >
                              Cerrar
                          </button>
                      </div>
                  </div>
              )}

      </div>
    </div>
  );
}

export default BillingPage;