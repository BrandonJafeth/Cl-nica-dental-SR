import React from 'react';
import { BrowserRouter as Router, Route, Routes } from 'react-router-dom';
import HomePage from '../pages/Informative/HomePage';
import ClientsPage from '../pages/Managing/ClientsPage';
import AppointmentsPage from '../pages/Managing/AppointmentsPage';
import BillingPage from '../pages/Managing/BillingPage';
import Login from '../pages/Auth/Login';
import Register from '../pages/Auth/Register';
import CuentasPage from '../pages/Managing/CuentasPage';
import DentistPage from '../pages/Managing/DentistPage';
import TratamientoPage from '../pages/Managing/TratamientoPage';
import PaymentsPage from '../pages/Managing/PaymentsPage';
import PandTPage from '../pages/Managing/P&TPage';

const AppRouter: React.FC = () => {
    return (
        <Router>
            <Routes>
                <Route path="/" element={<HomePage />} />
                <Route path="/login" element={<Login />} />
                <Route path="/register" element={<Register />} />
                <Route path="/clients" element={<ClientsPage />} />
                <Route path="/appointments" element={<AppointmentsPage />} />
                <Route path="/bills" element={<BillingPage />} />
                <Route path="/Treatments-Procedures" element={<PandTPage />} />
                <Route path="/cuentas" element={<CuentasPage />} />
                <Route path="/payments" element={<PaymentsPage />} />
                <Route path="/treatments" element={<TratamientoPage />} />
                <Route path="/dentist" element={<DentistPage />} />
            </Routes>
        </Router>
    );
};

export default AppRouter;

