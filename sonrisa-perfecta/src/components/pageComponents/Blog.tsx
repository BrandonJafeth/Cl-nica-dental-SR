
const blogPosts = [
  {
    title: 'Importancia de una Buena Higiene Bucal',
    summary: 'Descubre cómo una higiene bucal adecuada puede prevenir enfermedades y mejorar tu salud general.',
    link: '#',
  },
  {
    title: '¿Qué es la Ortodoncia Invisible?',
    summary: 'Conoce cómo funciona la ortodoncia invisible y si es la opción adecuada para ti.',
    link: '#',
  },
  {
    title: 'Beneficios del Blanqueamiento Dental',
    summary: 'Aprende sobre los diferentes métodos de blanqueamiento dental y cómo mejorar tu sonrisa.',
    link: '#',
  },
];

const Blog = () => {
  return (
    <section className="bg-white py-16 px-6">
      <div className="max-w-6xl mx-auto text-center">
        <h2 className="text-4xl font-bold text-blue-900 mb-8">Artículos del Blog</h2>
        <div className="grid gap-8 md:grid-cols-2 lg:grid-cols-3">
          {blogPosts.map((post, index) => (
            <div key={index} className="bg-blue-50 p-6 rounded-lg shadow-lg">
              <h3 className="text-2xl font-bold text-blue-900 mb-4">{post.title}</h3>
              <p className="text-gray-700 mb-4">{post.summary}</p>
              <a href={post.link} className="text-blue-500 font-semibold hover:underline">
                Leer más →
              </a>
            </div>
          ))}
        </div>
      </div>
    </section>
  );
};

export default Blog;
