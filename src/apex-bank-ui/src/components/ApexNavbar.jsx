import React from 'react';
import { Link } from 'react-router-dom';


const ApexNavbar = () => {



  return (
    <nav className="glass" style={{ margin: '20px', padding: '15px 30px', display: 'flex', justifyContent: 'space-between', alignItems: 'center', position: 'sticky', top: '20px', zIndex: 100 }}>
      <div className="logo" style={{ fontSize: '24px', fontWeight: '800', color: 'var(--accent)' }}>
        APEX<span style={{ color: '#fff' }}>BANK</span>
      </div>
      <div className="links" style={{ display: 'flex', gap: '30px', fontWeight: '500' }}>
        <Link to="/personal" style={{ color: 'inherit', textDecoration: 'none' }}>Personal</Link>
        <Link to="/business" style={{ color: 'inherit', textDecoration: 'none' }}>Business</Link>
        <Link to="/security" style={{ color: 'inherit', textDecoration: 'none' }}>Security</Link>
        <Link to="/contact" style={{ color: 'inherit', textDecoration: 'none' }}>Contact</Link>
      </div>


      <div className="auth" style={{ display: 'flex', gap: '15px' }}>
        <Link to="/login" className="btn btn-outline" style={{ textDecoration: 'none' }}>Login</Link>
        <Link to="/register" className="btn btn-primary" style={{ textDecoration: 'none' }}>Open Account</Link>
      </div>

    </nav>
  );
};

export default ApexNavbar;




