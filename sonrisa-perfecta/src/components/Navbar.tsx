import Logo from '../assets/images/Sonrisa Perfecta.png';
import { Link } from 'react-router-dom';

function Navbar() {
  return (
    <nav className="bg-blue-600 shadow-lg">
      <div className="container mx-auto flex items-center justify-between py-4 px-6">
        
        {/* Logo */}
        <div className="flex items-center">
          <Link to="/">
            <img src={Logo} alt="Logo" className="h-24 w-auto mr-4" /> {/* Ajusta la altura según lo necesites */}
          </Link>
        </div>

        {/* Enlaces de navegación */}
        <ul className="flex items-center space-x-6">
          <li>
            <Link to="/" className="text-white hover:text-[#93f7ea] text-lg transition-colors duration-200">Inicio</Link>
          </li>
          <li>
            <Link to="/clients" className="text-white hover:text-[#93f7ea] text-lg transition-colors duration-200">Pacientes</Link>
          </li>
          <li>
            <Link to="/appointments" className="text-white hover:text-[#93f7ea] text-lg transition-colors duration-200">Citas</Link>
          </li>
          <li>
            <Link to="/bills" className="text-white hover:text-[#93f7ea] text-lg transition-colors duration-200">Facturación</Link>
          </li>
          <li>
            <Link to="/treatments" className="text-white hover:text-[#93f7ea] text-lg transition-colors duration-200">Tratamientos</Link>
          </li>
          <li>
            <Link to="/dentist" className="text-white hover:text-[#93f7ea] text-lg transition-colors duration-200">Dentistas</Link>
          </li>
          <li>
            <Link to="/payments" className="text-white hover:text-[#93f7ea] text-lg transition-colors duration-200">Pagos</Link>
          </li>
          <li>
            <Link to="/cuentas" className="text-white hover:text-[#93f7ea] text-lg transition-colors duration-200">Cuentas</Link>
          </li>
        </ul>

      
      </div>
    </nav>
  );
}

export default Navbar;
