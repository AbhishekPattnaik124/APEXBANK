import ApexNavbar from '../components/ApexNavbar';










import { motion } from 'framer-motion';
import { Briefcase, BarChart3, Users, Code, Building, Layers } from 'lucide-react';

const BusinessSolutions = () => {
  const businessFeatures = [
    { icon: <Briefcase size={24} />, title: "Corporate Accounts", desc: "Manage multi-entity businesses from a single dashboard." },
    { icon: <BarChart3 size={24} />, title: "Real-time Expense Tracking", desc: "Automate your accounting with direct ERP integrations." },
    { icon: <Users size={24} />, title: "Team Management", desc: "Issue physical and virtual cards to your employees with custom limits." },
    { icon: <Code size={24} />, title: "Developer APIs", desc: "Build custom financial workflows with our robust REST APIs." },
    { icon: <Building size={24} />, title: "Merchant Services", desc: "Accept payments globally with industry-low transaction fees." },
    { icon: <Layers size={24} />, title: "Liquidity Management", desc: "Optimize your cash flow with automated treasury tools." },
  ];

  return (
    <div style={{ minHeight: '100vh', background: '#000', color: '#fff' }}>
      <ApexNavbar />

      
      <section className="container" style={{ paddingTop: '100px', textAlign: 'center' }}>
        <motion.h1 initial={{ opacity: 0 }} animate={{ opacity: 1 }} style={{ fontSize: '56px', marginBottom: '20px' }}>
          Scale Your <span style={{ color: 'var(--accent)' }}>Enterprise</span>
        </motion.h1>
        <p style={{ color: '#94a3b8', fontSize: '20px', maxWidth: '800px', margin: '0 auto 60px' }}>
          The operating system for modern businesses. From startups to conglomerates, we provide the tools to lead.
        </p>

        <div style={{ display: 'grid', gridTemplateColumns: 'repeat(auto-fit, minmax(300px, 1fr))', gap: '30px' }}>
          {businessFeatures.map((f, i) => (
            <motion.div 
              key={i}
              whileHover={{ scale: 1.02 }}
              className="glass" 
              style={{ padding: '40px', textAlign: 'left', border: '1px solid rgba(212, 175, 55, 0.1)' }}
            >
              <div style={{ color: 'var(--accent)', marginBottom: '20px' }}>{f.icon}</div>
              <h3 style={{ fontSize: '20px', marginBottom: '12px' }}>{f.title}</h3>
              <p style={{ color: '#94a3b8', fontSize: '15px' }}>{f.desc}</p>
            </motion.div>
          ))}
        </div>
      </section>

      <section style={{ padding: '100px 0', marginTop: '80px' }}>
        <div className="container glass" style={{ padding: '60px', display: 'flex', justifyContent: 'space-between', alignItems: 'center' }}>
          <div>
            <h2 style={{ fontSize: '32px', marginBottom: '16px' }}>Ready to transform your business?</h2>
            <p style={{ color: '#94a3b8' }}>Speak with our relationship managers for a custom solution.</p>
          </div>
          <button className="btn btn-primary" style={{ padding: '15px 40px' }}>Contact Sales</button>
        </div>
      </section>
    </div>
  );
};

export default BusinessSolutions;
