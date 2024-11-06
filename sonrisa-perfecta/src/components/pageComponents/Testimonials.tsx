
import { FaStar, FaStarHalfAlt, FaRegStar } from 'react-icons/fa';

const testimonials = [
  {
    id: 1,
    name: 'Robert Fox',
    image: 'https://randomuser.me/api/portraits/men/32.jpg',
    comment: 'Lorem ipsum is placeholder text commonly used in the graphic, print, and publishing industries for previewing layouts.',
    rating: 4.5,
  },
  {
    id: 2,
    name: 'Albert Flores',
    image: 'https://randomuser.me/api/portraits/men/33.jpg',
    comment: 'Lorem ipsum is placeholder text commonly used in the graphic, print, and publishing industries for previewing layouts.',
    rating: 4,
  },
  {
    id: 3,
    name: 'Bessie Cooper',
    image: 'https://randomuser.me/api/portraits/women/34.jpg',
    comment: 'Lorem ipsum is placeholder text commonly used in the graphic, print, and publishing industries for previewing layouts.',
    rating: 4.5,
  },
];

type StarRatingProps = {
    rating: number;
  };
  
  const StarRating: React.FC<StarRatingProps> = ({ rating }) => {
    const fullStars = Math.floor(rating);
    const halfStar = rating % 1 !== 0;
    const emptyStars = 5 - fullStars - (halfStar ? 1 : 0);
  
    return (
      <div className="flex items-center space-x-1 text-yellow-400">
        {[...Array(fullStars)].map((_, i) => (
          <FaStar key={i} />
        ))}
        {halfStar && <FaStarHalfAlt />}
        {[...Array(emptyStars)].map((_, i) => (
          <FaRegStar key={i} />
        ))}
      </div>
    );
  };

const Testimonials = () => {
  return (
    <section className="py-20 bg-blue-50">
      <div className="px-4 mx-auto max-w-7xl sm:px-6 lg:px-8 text-center">
        <h2 className="text-xl font-semibold text-blue-600">TESTIMONIOS</h2>
        <h3 className="mt-2 text-3xl font-bold text-gray-800 sm:text-4xl">Lo que nuestros clientes opinan sobre nosotros</h3>
        <p className="max-w-2xl mx-auto mt-4 text-gray-600">
          Lorem ipsum is placeholder text commonly used in the graphic, print, and publishing industries for previewing layouts.
        </p>
      </div>

      <div className="grid gap-8 mt-12 sm:grid-cols-2 lg:grid-cols-3 max-w-6xl mx-auto px-6 lg:px-0">
        {testimonials.map((testimonial) => (
          <div key={testimonial.id} className="bg-white rounded-lg shadow-lg p-6 text-center">
            <div className="relative w-24 h-24 mx-auto mb-4">
              <img
                className="w-full h-full rounded-full border-4 border-white shadow-lg"
                src={testimonial.image}
                alt={testimonial.name}
              />
            </div>
            <h4 className="text-lg font-semibold text-gray-800">{testimonial.name}</h4>
            <p className="mt-2 text-gray-600">{testimonial.comment}</p>
            <div className="mt-4">
              <StarRating rating={testimonial.rating} />
            </div>
          </div>
        ))}
      </div>
    </section>
  );
};

export default Testimonials;
