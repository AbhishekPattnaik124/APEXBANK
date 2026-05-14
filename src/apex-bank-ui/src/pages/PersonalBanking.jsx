import ApexNavbar from '../components/ApexNavbar';










import { motion } from 'framer-motion';
import { CreditCard, Smartphone, Zap, Globe, ShieldCheck, Heart } from 'lucide-react';

const PersonalBanking = () => {
  const features = [
    { icon: <CreditCard size={24} />, title: "Smart Cards", desc: "Dynamic CVV and instant freezing capabilities." },
    { icon: <Zap size={24} />, title: "Instant Transfers", desc: "Send money globally in under 30 seconds." },
    { icon: <Globe size={24} />, title: "Zero FX Fees", desc: "Spend abroad with interbank exchange rates." },
    { icon: <Smartphone size={24} />, title: "Mobile First", desc: "Complete control of your finances from your pocket." },
    { icon: <ShieldCheck size={24} />, title: "Insured Deposits", desc: "Your funds are protected by national reserves." },
    { icon: <Heart size={24} />, title: "Family Sharing", desc: "Joint accounts for your loved ones with ease." },
  ];

  return (
    <div style={{ minHeight: '100vh', background: '#000', color: '#fff' }}>
      <ApexNavbar />

      
      <section className="container" style={{ paddingTop: '100px', textAlign: 'center' }}>
        <motion.h1 initial={{ opacity: 0 }} animate={{ opacity: 1 }} style={{ fontSize: '56px', marginBottom: '20px' }}>
          Banking That Moves <span style={{ color: 'var(--accent)' }}>With You</span>
        </motion.h1>
        <p style={{ color: '#94a3b8', fontSize: '20px', maxWidth: '800px', margin: '0 auto 60px' }}>
          Designed for the digital nomad, the daily commuter, and everyone in between. Personal banking, evolved.
        </p>

        <div style={{ display: 'grid', gridTemplateColumns: 'repeat(auto-fit, minmax(300px, 1fr))', gap: '30px' }}>
          {features.map((f, i) => (
            <motion.div 
              key={i}
              whileHover={{ y: -10 }}
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

      <section style={{ padding: '100px 0', background: 'rgba(212, 175, 55, 0.03)', marginTop: '80px' }}>
        <div className="container" style={{ display: 'grid', gridTemplateColumns: '1fr 1fr', gap: '60px', alignItems: 'center' }}>
          <div>
            <h2 style={{ fontSize: '40px', marginBottom: '24px' }}>Savings Redefined</h2>
            <p style={{ color: '#94a3b8', fontSize: '18px', marginBottom: '32px' }}>
              Earn up to 4.5% APY on your savings with our Smart Vaults. No lock-in periods, no hidden fees. Just pure growth.
            </p>
            <button className="btn btn-primary">Start Saving Today</button>
          </div>
          <div className="glass" style={{ height: '300px', display: 'flex', alignItems: 'center', justifyContent: 'center' }}>
            <div style={{ fontSize: '80px', fontWeight: '800', color: 'var(--accent)' }}>4.5%</div>
          </div>
        </div>
      </section>
    </div>
  );
};

export default PersonalBanking;
