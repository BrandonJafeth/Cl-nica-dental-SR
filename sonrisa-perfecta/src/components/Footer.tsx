import { FaFacebook, FaTwitter, FaInstagram } from 'react-icons/fa';

const Footer = () => {
  return (
    <footer className="bg-blue-600 text-gray-100 py-8">
      <div className="max-w-6xl mx-auto px-4 md:flex md:justify-between">
        {/* Logo y Descripción */}
        <div className="mb-6 md:mb-0">
          <h2 className="text-2xl font-bold text-white flex items-center">
            <span className="text-indigo-500 mr-2">🦷</span> Sonrisa Perfecta.
          </h2>
          <p className="mt-2 max-w-xs">
          "Sonrisa Perfecta: Donde tu sonrisa se convierte en arte."

          </p>
          {/* Redes Sociales */}
          <div className="flex mt-4 space-x-4">
            <a href="#" aria-label="Facebook" className="text-gray-100 hover:text-gray-400">
              <FaFacebook size={20} />
            </a>
            <a href="#" aria-label="Twitter" className="text-gray-100 hover:text-gray-400">
              <FaTwitter size={20} />
            </a>
            <a href="#" aria-label="Instagram" className="text-gray-100 hover:text-gray-400">
              <FaInstagram size={20} />
            </a>
          </div>
        </div>

        {/* Enlaces rápidos */}
        <div className="mb-6 md:mb-0">
          <h3 className="text-lg font-semibold text-white mb-4">Enlaces Rápidos</h3>
          <ul className="space-y-2">
            <li><a href="#" className=" hover:text-gray-400">Acerca de Nosotros</a></li>
            <li><a href="#" className="hover:text-gray-400">Servicios Dentales</a></li>
            <li><a href="#" className="hover:text-gray-400">Dentistas</a></li>
            <li><a href="#" className="hover:text-gray-400">Blogs</a></li>
            <li><a href="#" className="hover:text-gray-400">FAQs</a></li>
          </ul>
        </div>

        {/* Información de Contacto */}
        <div>
          <h3 className="text-lg font-semibold text-gray-100 mb-4">Contacto e Información</h3>
          <ul className="space-y-2">
            <li>
              <div className="flex items-center space-x-2">
                <a href= "#" className="text-indigo-500">📞</a>
                <a href= "#" className="hover:text-gray-400">+088 123 654 987 </a>
              </div>
            </li>
            <li>
              <div className="flex items-center space-x-2">
                <a href= "#" className="text-indigo-500">🕒</a>
                <a href= "#"className="hover:text-gray-400">09:00 AM - 18:00 PM</a>
              </div>
            </li>
            <li>
              <div className="flex items-center space-x-2">
                <a href= "#" className="text-indigo-500">📍</a>
                <a href= "#" className="hover:text-gray-400">35 West Dental Street, California 1004</a>
              </div>
            </li>
          </ul>
        </div>
      </div>

      {/* Derechos reservados y enlaces legales */}
      <div className="mt-8 text-center border-t border-white pt-4">
        <p className="text-gray-100">&copy; Dentalist. Todos los derechos reservados.</p>
        <div className="flex justify-center space-x-4 mt-2">
          <a href="#" className="text-gray-100 hover:text-gray-400">Términos de Uso</a>
          <a href="#" className="text-gray-100 hover:text-gray-400">Política de Privacidad</a>
        </div>
      </div>
    </footer>
  );
};

export default Footer;
