import  { useEffect } from 'react';
import { FaTooth, FaXRay, FaTeethOpen, FaSmile } from 'react-icons/fa';
import AOS from 'aos';
import 'aos/dist/aos.css';

const ServicesSection = () => {
  useEffect(() => {
    AOS.init({ duration: 1000, once: true });
  }, []);

  const services = [
    {
      icon: FaTooth,
      title: 'Limpieza Dental',
      description: 'Realizamos limpiezas dentales profesionales para mantener tus dientes libres de placa y sarro.',
      bgColor: 'bg-blue-500',
      shadowColor: 'shadow-blue-500/40',
    },
    {
      icon: FaXRay,
      title: 'Radiografía Dental',
      description: 'Contamos con tecnología avanzada para realizar radiografías y evaluar la salud bucal.',
      bgColor: 'bg-indigo-500',
      shadowColor: 'shadow-indigo-500/40',
    },
    {
      icon: FaTeethOpen,
      title: 'Ortodoncia',
      description: 'Ofrecemos tratamientos de ortodoncia para alinear los dientes y mejorar la sonrisa.',
      bgColor: 'bg-purple-500',
      shadowColor: 'shadow-purple-500/40',
    },
    {
      icon: FaSmile,
      title: 'Blanqueamiento Dental',
      description: 'Nuestros servicios de blanqueamiento te ayudarán a lograr una sonrisa más brillante.',
      bgColor: 'bg-teal-500',
      shadowColor: 'shadow-teal-500/40',
    },
  ];

  return (
    <div className="h-full min-h-screen w-full bg-gray-50 pt-12 p-4">
      {/* Título de la sección */}
      <div className="text-center mb-16">
        <h2 className="text-4xl font-bold text-blue-950 mb-4 mt-12">Nuestros Servicios Dentales</h2>
        <p className="text-gray-500 text-lg max-w-2xl mx-auto">
          Ofrecemos una variedad de servicios odontológicos de calidad para cuidar y mejorar tu salud bucal.
        </p>
      </div>
      
      {/* Tarjetas de servicios */}
      <div className="grid gap-14 md:grid-cols-2 lg:grid-cols-3 xl:grid-cols-4 md:gap-5">
        {services.map((service, index) => (
          <div
            key={index}
            data-aos="fade-up"
            className="rounded-xl bg-white p-6 text-center shadow-xl transform transition-transform hover:scale-105 hover:shadow-2xl hover:border hover:border-blue-500 duration-300"
          >
            <div
              className={`mx-auto flex h-16 w-16 -translate-y-12 transform items-center justify-center rounded-full ${service.bgColor} ${service.shadowColor} shadow-lg`}>
              <service.icon className="h-8 w-8 text-white" />
            </div>
            <h1 className="text-darken mb-3 text-xl font-semibold lg:px-4 text-blue-900">{service.title}</h1>
            <p className="px-4 text-gray-500">{service.description}</p>
          </div>
        ))}
      </div>
    </div>
  );
};

export default ServicesSection;
