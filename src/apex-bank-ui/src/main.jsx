import { StrictMode } from 'react';
import ReactDOM from 'react-dom/client';
import { BrowserRouter, Routes, Route } from 'react-router-dom';
import App from './App';
import Login from './pages/Login';
import Register from './pages/Register';
import DashboardLayout from './components/layout/DashboardLayout';
import Overview from './pages/dashboard/Overview';
import Transfer from './pages/dashboard/Transfer';
import History from './pages/dashboard/History';
import Approvals from './pages/dashboard/Approvals';
import AdminAnalytics from './pages/dashboard/AdminAnalytics';
import Profile from './pages/dashboard/Profile';
import PersonalBanking from './pages/PersonalBanking';
import BusinessSolutions from './pages/BusinessSolutions';
import SecurityAssurance from './pages/SecurityAssurance';
import ContactSupport from './pages/ContactSupport';
import './index.css';


ReactDOM.createRoot(document.getElementById('root')).render(
  <StrictMode>
    <BrowserRouter>
      <Routes>
        <Route path="/" element={<App />} />
        <Route path="/login" element={<Login />} />
        <Route path="/register" element={<Register />} />
        
        {/* Info Pages */}
        <Route path="/personal" element={<PersonalBanking />} />
        <Route path="/business" element={<BusinessSolutions />} />
        <Route path="/security" element={<SecurityAssurance />} />
        <Route path="/contact" element={<ContactSupport />} />


        
        {/* Dashboard Routes */}
        <Route path="/dashboard" element={<DashboardLayout role="Customer"><Overview /></DashboardLayout>} />
        <Route path="/dashboard/transfer" element={<DashboardLayout role="Customer"><Transfer /></DashboardLayout>} />
        <Route path="/dashboard/history" element={<DashboardLayout role="Customer"><History /></DashboardLayout>} />
        <Route path="/dashboard/profile" element={<DashboardLayout role="Customer"><Profile /></DashboardLayout>} />

        
        {/* Employee Routes */}
        <Route path="/employee" element={<DashboardLayout role="Employee"><Approvals /></DashboardLayout>} />
        <Route path="/employee/approvals" element={<DashboardLayout role="Employee"><Approvals /></DashboardLayout>} />
        
        {/* Admin Routes */}
        <Route path="/admin" element={<DashboardLayout role="Admin"><AdminAnalytics /></DashboardLayout>} />



      </Routes>
    </BrowserRouter>
  </StrictMode>
);

