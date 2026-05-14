import ApexNavbar from '../components/ApexNavbar';










import { motion } from 'framer-motion';
import { Mail, Phone, MapPin } from 'lucide-react';



const ContactSupport = () => {
  return (
    <div style={{ minHeight: '100vh', background: '#000', color: '#fff' }}>
      <ApexNavbar />

      
      <section className="container" style={{ paddingTop: '100px', textAlign: 'center' }}>
        <motion.h1 initial={{ opacity: 0 }} animate={{ opacity: 1 }} style={{ fontSize: '56px', marginBottom: '20px' }}>
          We're Here to <span style={{ color: 'var(--accent)' }}>Help</span>
        </motion.h1>
        <p style={{ color: '#94a3b8', fontSize: '20px', maxWidth: '800px', margin: '0 auto 60px' }}>
          Connect with our global support team 24/7. We usually respond in under 5 minutes.
        </p>

        <div style={{ display: 'grid', gridTemplateColumns: '1fr 1fr', gap: '60px', textAlign: 'left' }}>
          {/* Contact Form */}
          <div className="glass" style={{ padding: '40px' }}>
            <h3 style={{ fontSize: '24px', marginBottom: '32px' }}>Send us a Message</h3>
            <form style={{ display: 'flex', flexDirection: 'column', gap: '20px' }}>
              <div style={{ display: 'grid', gridTemplateColumns: '1fr 1fr', gap: '20px' }}>
                <input placeholder="First Name" style={{ background: 'rgba(255,255,255,0.05)', border: '1px solid rgba(255,255,255,0.1)', padding: '15px', borderRadius: '8px', color: '#fff' }} />
                <input placeholder="Last Name" style={{ background: 'rgba(255,255,255,0.05)', border: '1px solid rgba(255,255,255,0.1)', padding: '15px', borderRadius: '8px', color: '#fff' }} />
              </div>
              <input placeholder="Work Email" style={{ background: 'rgba(255,255,255,0.05)', border: '1px solid rgba(255,255,255,0.1)', padding: '15px', borderRadius: '8px', color: '#fff' }} />
              <textarea placeholder="How can we help you?" rows="5" style={{ background: 'rgba(255,255,255,0.05)', border: '1px solid rgba(255,255,255,0.1)', padding: '15px', borderRadius: '8px', color: '#fff' }}></textarea>
              <button className="btn btn-primary" style={{ justifyContent: 'center', padding: '15px' }}>Submit Request</button>
            </form>
          </div>

          {/* Contact Info */}
          <div style={{ display: 'flex', flexDirection: 'column', gap: '40px' }}>
            <div className="glass" style={{ padding: '30px', display: 'flex', gap: '20px' }}>
              <div style={{ background: 'rgba(212, 175, 55, 0.1)', padding: '12px', borderRadius: '12px', height: 'fit-content' }}>
                <Mail color="var(--accent)" />
              </div>
              <div>
                <h4 style={{ fontSize: '18px', marginBottom: '8px' }}>Email Support</h4>
                <p style={{ color: '#94a3b8' }}>support@apexbank.com</p>
              </div>
            </div>

            <div className="glass" style={{ padding: '30px', display: 'flex', gap: '20px' }}>
              <div style={{ background: 'rgba(212, 175, 55, 0.1)', padding: '12px', borderRadius: '12px', height: 'fit-content' }}>
                <Phone color="var(--accent)" />
              </div>
              <div>
                <h4 style={{ fontSize: '18px', marginBottom: '8px' }}>Phone Support</h4>
                <p style={{ color: '#94a3b8' }}>+1 (800) APEX-BANK</p>
              </div>
            </div>

            <div className="glass" style={{ padding: '30px', display: 'flex', gap: '20px' }}>
              <div style={{ background: 'rgba(212, 175, 55, 0.1)', padding: '12px', borderRadius: '12px', height: 'fit-content' }}>
                <MapPin color="var(--accent)" />
              </div>
              <div>
                <h4 style={{ fontSize: '18px', marginBottom: '8px' }}>Global Headquarters</h4>
                <p style={{ color: '#94a3b8' }}>71 Wall Street, New York, NY 10005</p>
              </div>
            </div>
          </div>
        </div>
      </section>
    </div>
  );
};

export default ContactSupport;
