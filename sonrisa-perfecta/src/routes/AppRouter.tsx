import React from 'react';
import { BrowserRouter as Router, Route, Routes } from 'react-router-dom';
import HomePage from '../pages/HomePage';
import ClientsPage from '../pages/Managing/ClientsPage';
import AppointmentsPage from '../pages/Managing/AppointmentsPage';
import BillingPage from '../pages/Managing/BillingPage';
import PandTPage from '../pages/Managing/P&TPage';
import ContactPage from '../pages/ContactPage';


const AppRouter: React.FC = () => {
    return (
        <Router>
            <Routes>
                <Route path="/" element={<HomePage />} />
                <Route path="/clients" element={<ClientsPage />} />
                <Route path="/appointments" element={<AppointmentsPage />} />
                <Route path="/bills" element={<BillingPage / >} />
                <Route path="/Treatments-Procedures" element={<PandTPage/>} />
                <Route path="/contact" element={<ContactPage/>} />
            </Routes>
        </Router>
    );
};

export default AppRouter;