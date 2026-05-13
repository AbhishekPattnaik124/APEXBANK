import React from 'react';
import { User, Mail, Phone, MapPin, Shield, Bell, Moon } from 'lucide-react';

const Profile = () => {
  return (
    <div style={{ maxWidth: '900px', margin: '0 auto', display: 'flex', flexDirection: 'column', gap: '32px' }}>
      <header>
        <h2 style={{ fontSize: '28px', marginBottom: '8px' }}>Account Settings</h2>
        <p style={{ color: '#94a3b8' }}>Manage your personal information and security preferences.</p>
      </header>

      <div style={{ display: 'grid', gridTemplateColumns: '1fr 2fr', gap: '32px' }}>
        {/* Profile Card */}
        <div className="glass" style={{ padding: '32px', textAlign: 'center' }}>
          <div style={{ width: '100px', height: '100px', borderRadius: '50%', background: 'var(--accent)', margin: '0 auto 20px', display: 'flex', alignItems: 'center', justifyContent: 'center', fontSize: '32px', fontWeight: 'bold' }}>JD</div>
          <h3 style={{ fontSize: '20px', marginBottom: '4px' }}>John Doe</h3>
          <p style={{ fontSize: '14px', color: '#94a3b8', marginBottom: '24px' }}>Premium Customer since 2024</p>
          <button className="btn btn-outline" style={{ width: '100%', justifyContent: 'center' }}>Change Photo</button>
        </div>

        {/* Info Grid */}
        <div style={{ display: 'flex', flexDirection: 'column', gap: '24px' }}>
          <div className="glass" style={{ padding: '24px' }}>
            <h3 style={{ fontSize: '18px', marginBottom: '20px', display: 'flex', alignItems: 'center', gap: '10px' }}>
              <User size={20} color="var(--accent)" /> Personal Details
            </h3>
            <div style={{ display: 'grid', gridTemplateColumns: '1fr 1fr', gap: '20px' }}>
              <div>
                <label style={{ display: 'block', fontSize: '12px', color: '#94a3b8', marginBottom: '4px' }}>Full Name</label>
                <input type="text" defaultValue="John Doe" style={{ width: '100%', background: 'rgba(255,255,255,0.05)', border: '1px solid rgba(255,255,255,0.1)', padding: '10px', borderRadius: '8px', color: '#fff' }} />
              </div>
              <div>
                <label style={{ display: 'block', fontSize: '12px', color: '#94a3b8', marginBottom: '4px' }}>Email Address</label>
                <input type="email" defaultValue="john.doe@example.com" style={{ width: '100%', background: 'rgba(255,255,255,0.05)', border: '1px solid rgba(255,255,255,0.1)', padding: '10px', borderRadius: '8px', color: '#fff' }} />
              </div>
            </div>
          </div>

          <div className="glass" style={{ padding: '24px' }}>
            <h3 style={{ fontSize: '18px', marginBottom: '20px', display: 'flex', alignItems: 'center', gap: '10px' }}>
              <Shield size={20} color="var(--accent)" /> Security & Privacy
            </h3>
            <div style={{ display: 'flex', flexDirection: 'column', gap: '16px' }}>
              <div style={{ display: 'flex', justifyContent: 'space-between', alignItems: 'center' }}>
                <div>
                  <div style={{ fontSize: '14px', fontWeight: '600' }}>Two-Factor Authentication</div>
                  <div style={{ fontSize: '12px', color: '#94a3b8' }}>Secure your account with 2FA</div>
                </div>
                <div style={{ width: '40px', height: '20px', background: 'var(--accent)', borderRadius: '10px', position: 'relative' }}>
                  <div style={{ width: '16px', height: '16px', background: '#fff', borderRadius: '50%', position: 'absolute', right: '2px', top: '2px' }}></div>
                </div>
              </div>
              <button className="btn btn-outline" style={{ border: '1px solid #ef4444', color: '#ef4444', padding: '8px 16px', fontSize: '13px' }}>Reset Password</button>
            </div>
          </div>
        </div>
      </div>
    </div>
  );
};

export default Profile;
