import Footer from "../../components/Footer";
import Navbar from "../../components/Navbar";
import AboutUs from "../../components/pageComponents/AboutUs";
import Blog from "../../components/pageComponents/Blog";
import Hero from "../../components/pageComponents/Hero";
import ServicesSection from "../../components/pageComponents/ServicesSection";
import Testimonials from "../../components/pageComponents/Testimonials";


function HomePage() {
  return (
    <>
    <Navbar />
    <Hero/>
    <AboutUs/>
    <ServicesSection/>
    <Testimonials/>
    <Blog/>
    <Footer/>
    </>
  )
}

export default HomePage