import React from 'react';
import ApexNavbar from '../components/ApexNavbar';




import { motion } from 'framer-motion';

const InfoPage = ({ title, description }) => {
  return (
    <div style={{ minHeight: '100vh', background: '#020617', color: '#fff' }}>
      <ApexNavbar />

      <div className="container" style={{ paddingTop: '100px', textAlign: 'center' }}>
        <motion.div 
          initial={{ opacity: 0, y: 20 }}
          animate={{ opacity: 1, y: 0 }}
          className="glass"
          style={{ padding: '80px 40px' }}
        >
          <h1 style={{ fontSize: '48px', color: 'var(--accent)', marginBottom: '24px' }}>{title}</h1>
          <p style={{ fontSize: '20px', color: '#94a3b8', maxWidth: '700px', margin: '0 auto', lineHeight: '1.6' }}>
            {description}
          </p>
          <div style={{ marginTop: '40px' }}>
            <p style={{ color: 'var(--accent)', fontWeight: '600' }}>This feature is coming soon to the ApexBank platform.</p>
          </div>
        </motion.div>
      </div>
    </div>
  );
};

export default InfoPage;
