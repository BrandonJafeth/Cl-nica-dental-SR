import { FaTooth} from 'react-icons/fa';
import dentist from '../../assets/images/Dentist2.jpg';

const AboutUs = () => {
  return (
    <section className="min-h-screen flex items-center bg-white shadow-lg">
      <div className="px-4 mx-auto sm:px-6 lg:px-8 max-w-7xl w-full">
        <div className="grid items-center grid-cols-1 md:grid-cols-2 gap-12">
          
          {/* Texto */}
          <div className="text-center md:text-left space-y-6">
            {/* Línea decorativa y título */}
            <div className="flex items-center justify-center md:justify-start space-x-3">
              <FaTooth className="text-blue-500 text-3xl" />
              <h2 className="text-4xl font-bold leading-tight text-gray-800 sm:text-5xl lg:text-6xl">
                Bienvenidos a <br className="block sm:hidden" />Nuestra Clínica Dental
              </h2>
            </div>

            {/* Subtítulo */}
            <p className="max-w-lg text-lg leading-relaxed text-gray-600 dark:text-gray-500">
              Nos dedicamos a brindar servicios dentales de alta calidad, con un equipo experimentado y comprometido en ofrecerte la mejor atención.
            </p>

            {/* Información de contacto */}
            <p className="text-lg text-gray-600 dark:text-gray-500">
              <span className="relative inline-block">
                <span className="relative">¿Tienes alguna pregunta?</span>
              </span>
              <br className="block sm:hidden" />
              Contáctanos en <a href="#" className="transition-all duration-200 text-blue-500 hover:text-blue-700 hover:underline">nuestras redes sociales</a>
            </p>

            {/* Botón de Cita */}
            <div className="mt-6">
              <a href="#cita" className="inline-block px-8 py-3 bg-blue-500 text-white rounded-full font-medium shadow-lg hover:bg-blue-600 transition duration-300">
                Haz una Cita
              </a>
            </div>
          </div>

          {/* Imagen */}
          <div className="relative flex justify-center md:justify-end">
            {/* Blob decorativo */}
            <img
              className="absolute -z-10 inset-x-0 bottom-0 transform translate-y-10 -translate-x-1/2 left-1/2 max-w-sm md:max-w-md lg:max-w-lg opacity-20"
              src="https://cdn.rareblocks.xyz/collection/celebration/images/team/1/blob-shape.svg"
              alt=""
            />
            {/* Imagen principal */}
            <img
              className="relative w-80 md:w-[28rem] lg:w-[30rem] rounded-lg shadow-2xl border-4 border-white transition-transform duration-300 hover:scale-105"
              src={dentist} // Reemplaza con la imagen de tu clínica
              alt="Nuestra Clínica"
            />
          </div>
          
        </div>
      </div>
    </section>
  );
};

export default AboutUs;
