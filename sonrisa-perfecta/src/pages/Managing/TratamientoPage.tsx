// src/pages/TratamientoPage.tsx

import { useState } from "react";
import TratamientoList from "../..//pages/Tratamiento/TratamientoList";
import TratamientoForm from "../../pages/Tratamiento/TratamientoForm";
import { useTratamiento } from "../../hooks/useTratamiento";
import { Tratamiento } from "../../types/type";
import Navbar from "../../components/Navbar";

function TratamientoPage() {
  const [showForm, setShowForm] = useState(false);
  const [selectedTratamiento, setSelectedTratamiento] = useState<Tratamiento | null>(null);
  const { tratamientos, addTratamiento, updateTratamiento, deleteTratamiento } = useTratamiento();

  const handleAddTratamiento = () => {
    setSelectedTratamiento(null);
    setShowForm(true);
  };

  const handleEditTratamiento = (tratamiento: Tratamiento) => {
    setSelectedTratamiento(tratamiento);
    setShowForm(true);
  };

  const handleCloseForm = () => {
    setShowForm(false);
    setSelectedTratamiento(null);
  };

  const handleSaveTratamiento = (tratamiento: Tratamiento) => {
    if (selectedTratamiento) {
      // Editando tratamiento existente
      updateTratamiento(selectedTratamiento.ID_Tratamiento, tratamiento);
    } else {
      // Agregando nuevo tratamiento
      addTratamiento(tratamiento);
    }
    handleCloseForm();
  };

  return (
    <>
    <Navbar />
    <div className="min-h-screen bg-gray-100 text-gray-800">
      <div className="container mx-auto py-10 px-4">
        <h1 className="text-3xl font-bold text-center text-blue-700 mb-8">Gestión de Tratamientos</h1>

        {showForm ? (
          <TratamientoForm
            onClose={handleCloseForm}
            onSave={handleSaveTratamiento}
            tratamiento={selectedTratamiento}
          />
        ) : (
          <>
          
            <div className="flex justify-end mb-6">
              <button
                onClick={handleAddTratamiento}
                className="bg-blue-700 text-white px-6 py-2 rounded-lg hover:bg-blue-800 transition shadow-md"
              >
                + Nuevo Tratamiento
              </button>
            </div>
            <TratamientoList
              tratamientos={tratamientos}
              onEditTratamiento={handleEditTratamiento}
              onDeleteTratamiento={deleteTratamiento}
            />
          </>
        )}
      </div>
    </div>
    </>
  );
}

export default TratamientoPage;
