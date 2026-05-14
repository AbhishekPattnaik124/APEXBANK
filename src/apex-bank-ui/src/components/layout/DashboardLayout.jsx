import { useState } from 'react';
import { Link, useLocation, useNavigate } from 'react-router-dom';

import { 
  LayoutDashboard, 
  Send, 
  History, 
  User, 
  Settings, 
  LogOut, 
  Bell, 
  Search,
  Menu,
  X
} from 'lucide-react';
import NotificationToast from '../NotificationToast';

const DashboardLayout = ({ children, role = 'Customer' }) => {
  const [isSidebarOpen, setSidebarOpen] = useState(true);
  const location = useLocation();
  const navigate = useNavigate();


  const commonLinks = [
    { name: 'Profile', icon: <User size={20} />, path: '/dashboard/profile' },
    { name: 'Settings', icon: <Settings size={20} />, path: '/dashboard/settings' },
  ];

  const roleLinks = {
    Customer: [
      { name: 'Overview', icon: <LayoutDashboard size={20} />, path: '/dashboard' },
      { name: 'Transfer', icon: <Send size={20} />, path: '/dashboard/transfer' },
      { name: 'History', icon: <History size={20} />, path: '/dashboard/history' },
    ],
    Employee: [
      { name: 'Management', icon: <LayoutDashboard size={20} />, path: '/employee' },
      { name: 'Approvals', icon: <Send size={20} />, path: '/employee/approvals' },
      { name: 'Support', icon: <History size={20} />, path: '/employee/support' },
    ],
    Admin: [
      { name: 'Analytics', icon: <LayoutDashboard size={20} />, path: '/admin' },
      { name: 'Users', icon: <User size={20} />, path: '/admin/users' },
      { name: 'Fraud Logs', icon: <History size={20} />, path: '/admin/fraud' },
    ]
  };

  const menuItems = [...(roleLinks[role] || []), ...commonLinks];


  return (
    <div style={{ display: 'flex', minHeight: '100vh', background: '#020617', color: '#fff' }}>
      {/* Sidebar */}
      <aside style={{ 
        width: isSidebarOpen ? '260px' : '0', 
        transition: 'all 0.3s ease',
        background: 'rgba(15, 23, 42, 0.9)',
        borderRight: '1px solid rgba(255,255,255,0.1)',
        overflow: 'hidden',
        display: 'flex',
        flexDirection: 'column',
        zIndex: 50
      }}>
        <div style={{ padding: '30px 24px' }}>
          <Link to="/" style={{ textDecoration: 'none' }}>
            <h1 style={{ fontSize: '24px', fontWeight: '800', color: 'var(--accent)' }}>APEX<span style={{ color: '#fff' }}>BANK</span></h1>
          </Link>
        </div>


        <nav style={{ flex: 1, padding: '0 12px' }}>
          {menuItems.map((item) => (
            <Link 
              key={item.name} 
              to={item.path}
              style={{ 
                display: 'flex', 
                alignItems: 'center', 
                gap: '12px', 
                padding: '12px 16px',
                marginBottom: '4px',
                borderRadius: '8px',
                textDecoration: 'none',
                color: location.pathname === item.path ? 'var(--accent)' : '#94a3b8',
                background: location.pathname === item.path ? 'rgba(202, 138, 4, 0.1)' : 'transparent',
                transition: 'all 0.2s'
              }}
            >
              {item.icon}
              <span style={{ fontWeight: '500' }}>{item.name}</span>
            </Link>
          ))}
        </nav>

        <div style={{ padding: '24px', borderTop: '1px solid rgba(255,255,255,0.1)' }}>
          <button 
            onClick={() => navigate('/')}
            style={{ 
              display: 'flex', 
              alignItems: 'center', 
              gap: '12px', 
              color: '#ef4444', 
              background: 'none', 
              border: 'none', 
              cursor: 'pointer',
              fontWeight: '600'
            }}
          >
            <LogOut size={20} />
            Logout
          </button>
        </div>

      </aside>

      {/* Main Content */}
      <main style={{ flex: 1, display: 'flex', flexDirection: 'column' }}>
        {/* Topbar */}
        <header style={{ 
          height: '70px', 
          background: 'rgba(2, 6, 23, 0.8)', 
          backdropFilter: 'blur(10px)',
          borderBottom: '1px solid rgba(255,255,255,0.1)',
          display: 'flex',
          alignItems: 'center',
          justifyContent: 'space-between',
          padding: '0 30px',
          position: 'sticky',
          top: 0,
          zIndex: 40
        }}>
          <button onClick={() => setSidebarOpen(!isSidebarOpen)} style={{ background: 'none', border: 'none', color: '#fff', cursor: 'pointer' }}>
            {isSidebarOpen ? <X size={24} /> : <Menu size={24} />}
          </button>

          <div style={{ display: 'flex', alignItems: 'center', gap: '24px' }}>
            <div style={{ position: 'relative' }}>
              <Search size={18} style={{ position: 'absolute', left: '12px', top: '50%', transform: 'translateY(-50%)', color: '#94a3b8' }} />
              <input 
                type="text" 
                placeholder="Search transactions..." 
                style={{ background: 'rgba(255,255,255,0.05)', border: '1px solid rgba(255,255,255,0.1)', borderRadius: '20px', padding: '8px 16px 8px 40px', color: '#fff', outline: 'none', width: '250px' }}
              />
            </div>
            <Bell size={20} style={{ color: '#94a3b8', cursor: 'pointer' }} />
            <div style={{ display: 'flex', alignItems: 'center', gap: '10px', cursor: 'pointer' }}>
              <div style={{ width: '36px', height: '36px', borderRadius: '50%', background: 'var(--accent)', display: 'flex', alignItems: 'center', justifyContent: 'center', fontWeight: 'bold' }}>JD</div>
              <span style={{ fontSize: '14px', fontWeight: '500' }}>John Doe</span>
            </div>
          </div>
        </header>

        {/* Page Content */}
        <div style={{ padding: '40px', flex: 1 }}>
          {children}
        </div>
        <NotificationToast />
      </main>
    </div>

  );
};

export default DashboardLayout;
