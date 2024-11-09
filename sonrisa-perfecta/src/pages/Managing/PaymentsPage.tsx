// src/pages/Managing/PaymentsPage.tsx
import Navbar from "../../components/Navbar";
import { usePago } from "../../hooks/usePago";
import { Pago } from "../../types/type";
import { useState, useEffect } from "react";

function PaymentsPage() {
  const { pagos, loading, error, createPago, updatePago, deletePago, fetchPagos } = usePago();
  const [showViewModal, setShowViewModal] = useState(false);
  const [showEditModal, setShowEditModal] = useState(false);
  const [showDeleteModal, setShowDeleteModal] = useState(false);
  const [showAddModal, setShowAddModal] = useState(false);
  const [selectedPago, setSelectedPago] = useState<Pago | null>(null);
  const [newPago, setNewPago] = useState<Omit<Pago, 'iD_Pago'>>({
    monto_Pago: 0,
    fecha_Pago: '',
    iD_Factura: '',
    iD_Tipo_Pago: '',
  });

  useEffect(() => {
    fetchPagos();
  }, []);

  if (loading) {
    return (
      <div className="min-h-screen bg-gray-100 text-gray-800 flex items-center justify-center">
        <p>Cargando pagos...</p>
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

  const handleView = (pago: Pago) => {
    setSelectedPago(pago);
    setShowViewModal(true);
  };

  const handleEdit = (pago: Pago) => {
    setSelectedPago(pago);
    setShowEditModal(true);
  };

  const handleDelete = (pago: Pago) => {
    setSelectedPago(pago);
    setShowDeleteModal(true);
  };

  const handleUpdatePago = async () => {
    if (selectedPago) {
      await updatePago(selectedPago.iD_Pago, selectedPago);
      setShowEditModal(false);
    }
  };

  const handleDeletePago = async () => {
    if (selectedPago) {
      await deletePago(selectedPago.iD_Pago);
      setShowDeleteModal(false);
    }
  };

  const handleAddPago = async () => {
    await createPago(newPago as Pago);
    setShowAddModal(false);
    setNewPago({
      monto_Pago: 0,
      fecha_Pago: '',
      iD_Factura: '',
      iD_Tipo_Pago: '',
    });
  };

  return (
    <div className="min-h-screen bg-gray-100 text-gray-800">
      <Navbar />
      <div className="container mx-auto py-10 px-4">
        <h1 className="text-3xl font-bold text-center text-blue-700 mb-8">
          Gestión de Pagos
        </h1>

        {/* Botón para agregar un nuevo pago */}
        <div className="flex justify-end mb-6">
          <button
            onClick={() => setShowAddModal(true)}
            className="bg-blue-700 text-white px-6 py-2 rounded-lg hover:bg-blue-800 transition duration-300 shadow-md"
          >
            + Nuevo Pago
          </button>
        </div>

        {/* Tabla de pagos */}
        <div className="bg-white shadow-md rounded-lg overflow-hidden">
          <table className="min-w-full leading-normal">
            <thead className="bg-blue-700 text-white">
              <tr>
                <th className="px-5 py-3 border-b border-gray-200 text-left text-sm font-semibold">
                  ID Pago
                </th>
                <th className="px-5 py-3 border-b border-gray-200 text-left text-sm font-semibold">
                  Monto Pago
                </th>
                <th className="px-5 py-3 border-b border-gray-200 text-left text-sm font-semibold">
                  Fecha Pago
                </th>
                <th className="px-5 py-3 border-b border-gray-200 text-left text-sm font-semibold">
                  ID Factura
                </th>
                <th className="px-5 py-3 border-b border-gray-200 text-left text-sm font-semibold">
                  ID Tipo Pago
                </th>
                <th className="px-5 py-3 border-b border-gray-200 text-center text-sm font-semibold">
                  Acciones
                </th>
              </tr>
            </thead>
            <tbody>
              {pagos.map((pago) => (
                <tr key={pago.iD_Pago} className="hover:bg-gray-50">
                  <td className="px-5 py-4 border-b border-gray-200 text-sm">
                    {pago.iD_Pago}
                  </td>
                  <td className="px-5 py-4 border-b border-gray-200 text-sm">
                    ${pago.monto_Pago.toFixed(2)}
                  </td>
                  <td className="px-5 py-4 border-b border-gray-200 text-sm">
                    {new Date(pago.fecha_Pago).toLocaleDateString()}
                  </td>
                  <td className="px-5 py-4 border-b border-gray-200 text-sm">
                    {pago.iD_Factura}
                  </td>
                  <td className="px-5 py-4 border-b border-gray-200 text-sm">
                    {pago.iD_Tipo_Pago}
                  </td>
                  <td className="px-5 py-4 border-b border-gray-200 text-sm text-center">
                    <button
                      onClick={() => handleView(pago)}
                      className="bg-green-500 text-white px-3 py-1 rounded-md hover:bg-green-600 transition mx-1 shadow"
                    >
                      Ver
                    </button>
                    <button
                      onClick={() => handleEdit(pago)}
                      className="bg-yellow-500 text-white px-3 py-1 rounded-md hover:bg-yellow-600 transition mx-1 shadow"
                    >
                      Editar
                    </button>
                    <button
                      onClick={() => handleDelete(pago)}
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
              <h2 className="text-xl font-bold mb-4">Agregar Nuevo Pago</h2>
              <form
                onSubmit={(e) => {
                  e.preventDefault();
                  handleAddPago();
                }}
              >
                <div className="mb-3">
                  <label className="block text-sm font-medium">Monto Pago</label>
                  <input
                    type="number"
                    step="0.01"
                    value={newPago.monto_Pago}
                    onChange={(e) =>
                      setNewPago({
                        ...newPago,
                        monto_Pago: parseFloat(e.target.value) || 0,
                      })
                    }
                    className="mt-1 block w-full border border-gray-300 rounded-md p-2"
                    required
                  />
                </div>
                <div className="mb-3">
                  <label className="block text-sm font-medium">Fecha Pago</label>
                  <input
                    type="date"
                    value={newPago.fecha_Pago}
                    onChange={(e) =>
                      setNewPago({ ...newPago, fecha_Pago: e.target.value })
                    }
                    className="mt-1 block w-full border border-gray-300 rounded-md p-2"
                    required
                  />
                </div>
                <div className="mb-3">
                  <label className="block text-sm font-medium">ID Factura</label>
                  <input
                    type="text"
                    value={newPago.iD_Factura}
                    onChange={(e) =>
                      setNewPago({ ...newPago, iD_Factura: e.target.value })
                    }
                    className="mt-1 block w-full border border-gray-300 rounded-md p-2"
                    required
                  />
                </div>
                <div className="mb-3">
                  <label className="block text-sm font-medium">ID Tipo Pago</label>
                  <input
                    type="text"
                    value={newPago.iD_Tipo_Pago}
                    onChange={(e) =>
                      setNewPago({ ...newPago, iD_Tipo_Pago: e.target.value })
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
        {showEditModal && selectedPago && (
          <div className="fixed inset-0 flex items-center justify-center bg-black bg-opacity-50">
            <div className="bg-white p-6 rounded-lg shadow-lg w-96">
              <h2 className="text-xl font-bold mb-4">Editar Pago</h2>
              <form
                onSubmit={(e) => {
                  e.preventDefault();
                  handleUpdatePago();
                }}
              >
                <div className="mb-3">
                  <label className="block text-sm font-medium">ID Pago</label>
                  <input
                    type="text"
                    value={selectedPago.iD_Pago}
                    readOnly
                    className="mt-1 block w-full border border-gray-300 rounded-md p-2 bg-gray-100"
                  />
                </div>
                <div className="mb-3">
                  <label className="block text-sm font-medium">Monto Pago</label>
                  <input
                    type="number"
                    step="0.01"
                    value={selectedPago.monto_Pago}
                    onChange={(e) =>
                      setSelectedPago({
                        ...selectedPago!,
                        monto_Pago: parseFloat(e.target.value) || 0,
                      })
                    }
                    className="mt-1 block w-full border border-gray-300 rounded-md p-2"
                  />
                </div>
                <div className="mb-3">
                  <label className="block text-sm font-medium">Fecha Pago</label>
                  <input
                    type="date"
                    value={selectedPago.fecha_Pago}
                    onChange={(e) =>
                      setSelectedPago({
                        ...selectedPago!,
                        fecha_Pago: e.target.value,
                      })
                    }
                    className="mt-1 block w-full border border-gray-300 rounded-md p-2"
                  />
                </div>
                <div className="mb-3">
                  <label className="block text-sm font-medium">ID Factura</label>
                  <input
                    type="text"
                    value={selectedPago.iD_Factura}
                    onChange={(e) =>
                      setSelectedPago({
                        ...selectedPago!,
                        iD_Factura: e.target.value,
                      })
                    }
                    className="mt-1 block w-full border border-gray-300 rounded-md p-2"
                  />
                </div>
                <div className="mb-3">
                  <label className="block text-sm font-medium">ID Tipo Pago</label>
                  <input
                    type="text"
                    value={selectedPago.iD_Tipo_Pago}
                    onChange={(e) =>
                      setSelectedPago({
                        ...selectedPago!,
                        iD_Tipo_Pago: e.target.value,
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
        {showDeleteModal && selectedPago && (
          <div className="fixed inset-0 flex items-center justify-center bg-black bg-opacity-50">
            <div className="bg-white p-6 rounded-lg shadow-lg">
              <h2 className="text-xl font-bold mb-4">Eliminar Pago</h2>
              <p>
                ¿Está seguro que desea eliminar el pago con ID{' '}
                {selectedPago.iD_Pago}?
              </p>
              <div className="flex justify-end mt-4">
                <button
                  onClick={() => setShowDeleteModal(false)}
                  className="bg-gray-500 text-white px-4 py-2 rounded mr-2"
                >
                  Cancelar
                </button>
                <button
                  onClick={handleDeletePago}
                  className="bg-red-500 text-white px-4 py-2 rounded"
                >
                  Eliminar
                </button>
              </div>
            </div>
          </div>
        )}

        {/* Modal Ver */}
        {showViewModal && selectedPago && (
          <div className="fixed inset-0 flex items-center justify-center bg-black bg-opacity-50">
            <div className="bg-white p-6 rounded-lg shadow-lg">
              <h2 className="text-xl font-bold mb-4">Detalles del Pago</h2>
              <p>
                <strong>ID:</strong> {selectedPago.iD_Pago}
              </p>
              <p>
                <strong>Monto Pago:</strong> ${selectedPago.monto_Pago.toFixed(2)}
              </p>
              <p>
                <strong>Fecha Pago:</strong> {new Date(selectedPago.fecha_Pago).toLocaleDateString()}
              </p>
              <p>
                <strong>ID Factura:</strong> {selectedPago.iD_Factura}
              </p>
              <p>
                <strong>ID Tipo Pago:</strong> {selectedPago.iD_Tipo_Pago}
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

export default PaymentsPage;