import ApexNavbar from '../components/ApexNavbar';










import { motion } from 'framer-motion';
import { Shield, Fingerprint, Eye, Lock, Server, CheckCircle2 } from 'lucide-react';

const SecurityAssurance = () => {
  const securityPillars = [
    { icon: <Fingerprint size={24} />, title: "Biometric Auth", desc: "FaceID, Fingerprint, and hardware-key support for all transactions." },
    { icon: <Shield size={24} />, title: "AES-256 Encryption", desc: "Military-grade encryption for all data at rest and in transit." },
    { icon: <Eye size={24} />, title: "24/7 Monitoring", desc: "AI-driven fraud detection systems monitoring for suspicious patterns." },
    { icon: <Lock size={24} />, title: "PCI-DSS Level 1", desc: "The highest global standard for payment card security compliance." },
    { icon: <Server size={24} />, title: "Distributed Infrastructure", desc: "No single point of failure with global multi-region redundancy." },
    { icon: <CheckCircle2 size={24} />, title: "Zero Liability", desc: "Full protection against unauthorized transactions on your account." },
  ];

  return (
    <div style={{ minHeight: '100vh', background: '#000', color: '#fff' }}>
      <ApexNavbar />

      
      <section className="container" style={{ paddingTop: '100px', textAlign: 'center' }}>
        <motion.h1 initial={{ opacity: 0 }} animate={{ opacity: 1 }} style={{ fontSize: '56px', marginBottom: '20px' }}>
          Your Security, Our <span style={{ color: 'var(--accent)' }}>Priority</span>
        </motion.h1>
        <p style={{ color: '#94a3b8', fontSize: '20px', maxWidth: '800px', margin: '0 auto 60px' }}>
          ApexBank employs multi-layered defense systems to ensure your wealth and data remain impenetrable.
        </p>

        <div style={{ display: 'grid', gridTemplateColumns: 'repeat(auto-fit, minmax(300px, 1fr))', gap: '30px' }}>
          {securityPillars.map((p, i) => (
            <motion.div 
              key={i}
              whileHover={{ scale: 1.05 }}
              className="glass" 
              style={{ padding: '40px', textAlign: 'left', border: '1px solid rgba(212, 175, 55, 0.1)' }}
            >
              <div style={{ color: 'var(--accent)', marginBottom: '20px' }}>{p.icon}</div>
              <h3 style={{ fontSize: '20px', marginBottom: '12px' }}>{p.title}</h3>
              <p style={{ color: '#94a3b8', fontSize: '15px' }}>{p.desc}</p>
            </motion.div>
          ))}
        </div>
      </section>

      <section className="container" style={{ padding: '100px 0', textAlign: 'center' }}>
        <div className="glass" style={{ padding: '80px', background: 'linear-gradient(rgba(212, 175, 55, 0.05), transparent)' }}>
          <h2 style={{ fontSize: '36px', marginBottom: '32px' }}>Certified by the Best</h2>
          <div style={{ display: 'flex', justifyContent: 'center', gap: '60px', flexWrap: 'wrap', opacity: 0.5 }}>
            <div style={{ fontWeight: '800', fontSize: '24px' }}>ISO 27001</div>
            <div style={{ fontWeight: '800', fontSize: '24px' }}>SOC 2 TYPE II</div>
            <div style={{ fontWeight: '800', fontSize: '24px' }}>GDPR COMPLIANT</div>
            <div style={{ fontWeight: '800', fontSize: '24px' }}>PSD2 READY</div>
          </div>
        </div>
      </section>
    </div>
  );
};

export default SecurityAssurance;
