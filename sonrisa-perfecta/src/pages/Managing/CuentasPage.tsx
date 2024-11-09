// src/pages/CuentasPage.tsx
import { useState, useEffect } from 'react';
import { usePacientes } from '../../hooks/usePacientes';
import { useCuenta } from '../../hooks/useCuenta';
import Navbar from '../../components/Navbar';
import { Cuenta } from '../../types/type';
import ClientesList from '../Cuentas/ClientesList';
import DetallesCuentaModal from '../Cuentas/DetallesCuentaModal';

function CuentasPage() {
  const { pacientes, loading: loadingPacientes, error: errorPacientes } = usePacientes();
  const { cuentas, loading: loadingCuentas, error: errorCuentas } = useCuenta();
  const [selectedCuenta, setSelectedCuenta] = useState<Cuenta | null>(null);
  const [showCuentaModal, setShowCuentaModal] = useState(false);

  useEffect(() => {
    console.log("Pacientes cargados:", pacientes); // Verificar los datos de pacientes
    console.log("Cuentas cargadas:", cuentas); // Verificar los datos de cuentas
  }, [pacientes, cuentas]);

  const handleViewCuenta = (pacienteId: string) => {
    console.log("Buscando cuenta para paciente con ID:", pacienteId);
    console.log("Cuentas disponibles:", cuentas);

    const cuenta = cuentas.find((c) => {
      // Corrige el nombre de la propiedad a `iD_Paciente`
      console.log("Comparando:", c.iD_Paciente?.trim().toUpperCase(), "con", pacienteId.trim().toUpperCase());
      return c.iD_Paciente && c.iD_Paciente.trim().toUpperCase() === pacienteId.trim().toUpperCase();
    });

    if (cuenta) {
      console.log("Cuenta encontrada:", cuenta);
      setSelectedCuenta(cuenta);
      setShowCuentaModal(true);
    } else {
      console.warn("No se encontró la cuenta para el paciente con ID:", pacienteId);
      alert("No se encontró la cuenta para este paciente.");
    }
  };

  const closeCuentaModal = () => {
    setSelectedCuenta(null);
    setShowCuentaModal(false);
  };

  return (
    <div className="min-h-screen bg-gray-100 text-gray-800">
      <Navbar />
      <div className="container mx-auto py-10 px-4">
        <h1 className="text-3xl font-bold text-center text-blue-700 mb-8">Cuentas de Pacientes</h1>

        {loadingPacientes || loadingCuentas ? (
          <p>Cargando información...</p>
        ) : errorPacientes || errorCuentas ? (
          <p className="text-red-600">Error al cargar datos: {errorPacientes || errorCuentas}</p>
        ) : (
          <ClientesList pacientes={pacientes} onViewCuenta={handleViewCuenta} />
        )}

        {showCuentaModal && selectedCuenta && (
          <DetallesCuentaModal cuenta={selectedCuenta} onClose={closeCuentaModal} />
        )}
      </div>
    </div>
  );
}

export default CuentasPage;
