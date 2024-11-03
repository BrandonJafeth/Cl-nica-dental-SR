
import { Link } from 'react-router-dom';


function Navbar() {
  return (
    <nav className="bg-blue-500 p-4">
      <div className="text-white text-lg font-bold">
        <Link to="/">Logo</Link>
      </div>
      <ul className="flex space-x-4 mt-2">
        <li>
          <Link to="/" className="text-white hover:text-gray-300">Home</Link>
        </li>
        <li>
          <Link to="/clients" className="text-white hover:text-gray-300">Client manager</Link>
        </li>
        <li>
          <Link to="/appointments" className="text-white hover:text-gray-300">Appointments</Link>
        </li>
        <li>
          <Link to="/bills" className="text-white hover:text-gray-300">Billing service</Link>
        </li>
        <li>
          <Link to="/Treatments-Procedures" className="text-white hover:text-gray-300">Treatments & Procedures</Link>
        </li>
        <li>
          <Link to="/contact" className="text-white hover:text-gray-300">Contact</Link>
        </li>
      </ul>
    </nav>
  );
}

export default Navbar;
