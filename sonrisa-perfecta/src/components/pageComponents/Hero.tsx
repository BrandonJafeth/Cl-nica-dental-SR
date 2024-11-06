import { FaPhoneAlt } from 'react-icons/fa';
import { motion } from 'framer-motion';
import girl3 from '../../assets/images/girlSmiling3.png';

const Hero = () => {
  return (
    <section className="relative min-h-screen bg-blue-50 flex flex-col justify-between px-6 overflow-hidden">
      {/* Fondo con formas y animaciones */}
      <div className="absolute inset-0 overflow-hidden">
        <motion.div
          className="w-[450px] h-[450px] bg-indigo-200 rounded-full absolute top-20 right-0 md:right-20 opacity-70"
          animate={{ scale: [1, 1.1, 1] }}
          transition={{ duration: 4, repeat: Infinity }}
        />
        <motion.div
          className="w-[300px] h-[300px] bg-purple-200 rounded-full absolute bottom-0 left-0 opacity-50"
          animate={{ scale: [1, 1.1, 1] }}
          transition={{ duration: 5, repeat: Infinity }}
        />
      </div>

      {/* Contenido principal */}
      <div className="relative z-10 max-w-7xl mx-auto flex flex-col md:flex-row items-center justify-between w-full px-4">
        {/* Texto */}
        <div className="text-center md:text-left md:w-1/2 space-y-6">
          <h1 className="text-4xl md:text-5xl font-bold text-gray-800 leading-tight">
            "Cuidamos tu sonrisa, realzamos tu confianza"
          </h1>
          <p className="text-gray-600 max-w-md mx-auto md:mx-0">
            Lorem ipsum is placeholder text commonly used in the graphic, print, 
            and publishing industries for previewing layouts and visual mockups.
          </p>

          {/* Botón de llamada a la acción */}
          <div className="flex items-center justify-center md:justify-start space-x-4">
            <button className="bg-indigo-600 text-white px-6 py-3 rounded-lg shadow-md hover:bg-indigo-700 transition duration-300">
              Book an appointment
            </button>
            <div className="flex items-center space-x-2 text-indigo-700">
              <FaPhoneAlt className="text-2xl" />
              <div>
                <p className="text-xs font-semibold">DENTAL 24H EMERGENCY</p>
                <p className="text-lg font-bold">03 482 394 123</p>
              </div>
            </div>
          </div>
        </div>

        {/* Imagen de la chica */}
        <div className="md:w-1/2 mt-10 md:mt-0 relative flex justify-center md:justify-end">
          <img
            src={girl3}
            alt="Smiling woman"
            className="w-auto max-w-xs md:max-w-sm lg:max-w-lg translate-y-10"
          />
        </div>
      </div>

    
    </section>
  );
};

export default Hero;
