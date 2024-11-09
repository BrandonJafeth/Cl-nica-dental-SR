// src/pages/DentistPage.tsx

import { useState } from "react";
import Navbar from "../../components/Navbar";
import DentistForm from "../../pages/Dentist/DentistForm";
import DentistList from "../../pages/Dentist/DentistList";
import DentistModal from "../../pages/Dentist/DentistModal";
import { useDentista } from "../../hooks/useDentista"; // Importa el hook que se comunica con la base de datos

function DentistPage() {
    const [showForm, setShowForm] = useState(false);
    const [selectedDentist, setSelectedDentist] = useState(null);
    const [showModal, setShowModal] = useState(false);

    // Cargar dentistas desde el hook personalizado `useDentista`
    const { dentistas, loading, error, addDentista, updateDentista, deleteDentista } = useDentista();

    const handleAddDentist = () => {
        setSelectedDentist(null);
        setShowForm(true);
    };

    const handleEditDentist = (dentist) => {
        setSelectedDentist(dentist);
        setShowForm(true);
    };

    const handleCloseForm = () => {
        setShowForm(false);
        setSelectedDentist(null);
    };

    const handleViewDentist = (dentist) => {
        setSelectedDentist(dentist);
        setShowModal(true);
    };

    const handleCloseModal = () => {
        setShowModal(false);
        setSelectedDentist(null);
    };

    const handleSaveDentist = async (dentistData) => {
        if (selectedDentist) {
            // Actualizamos el dentista existente
            await updateDentista(selectedDentist.ID_Dentista, dentistData);
        } else {
            // Agregamos un nuevo dentista
            await addDentista(dentistData);
        }
        setShowForm(false);
        setSelectedDentist(null);
    };

    return (
        <div className="min-h-screen bg-gray-100 text-gray-800">
            <Navbar />
            <div className="container mx-auto py-10 px-4">
                <h1 className="text-3xl font-bold text-center text-blue-700 mb-8">Gestión de Dentistas</h1>

                {showForm ? (
                    <DentistForm
                        onClose={handleCloseForm}
                        dentist={selectedDentist}
                        onSave={handleSaveDentist}
                    />
                ) : (
                    <>
                        <div className="flex justify-end mb-6">
                            <button
                                onClick={handleAddDentist}
                                className="bg-blue-700 text-white px-6 py-2 rounded-lg hover:bg-blue-800 transition duration-300 shadow-md"
                            >
                                + Nuevo Dentista
                            </button>
                        </div>

                        {/* Mensajes de carga y error */}
                        {loading && <p>Cargando dentistas...</p>}
                        {error && <p className="text-red-500">{error}</p>}

                        {/* Componente de la lista de dentistas */}
                        <DentistList
                            dentists={dentistas} // Pasamos la lista de dentistas como `props`
                            onEditDentist={handleEditDentist}
                            onViewDentist={handleViewDentist}
                            onDeleteDentist={deleteDentista} // Llamamos directamente desde el hook
                        />
                    </>
                )}

                {/* Modal para ver detalles del dentista */}
                {showModal && (
                    <DentistModal
                        dentist={selectedDentist}
                        onClose={handleCloseModal}
                    />
                )}
            </div>
        </div>
    );
}

export default DentistPage;
