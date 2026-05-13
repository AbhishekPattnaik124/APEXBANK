import React, { useState } from 'react';
import { motion } from 'framer-motion';
import { Send, User, Search, Info } from 'lucide-react';

const Transfer = () => {
  const [amount, setAmount] = useState('');
  const [recipient, setRecipient] = useState('');

  const beneficiaries = [
    { id: 1, name: 'Alice Smith', acc: '**** 8821', avatar: 'AS' },
    { id: 2, name: 'Bob Johnson', acc: '**** 1102', avatar: 'BJ' },
    { id: 3, name: 'Charlie Davis', acc: '**** 4452', avatar: 'CD' },
  ];

  return (
    <div style={{ maxWidth: '800px', margin: '0 auto' }}>
      <header style={{ marginBottom: '40px' }}>
        <h2 style={{ fontSize: '28px', marginBottom: '8px' }}>Send Money</h2>
        <p style={{ color: '#94a3b8' }}>Transfer funds instantly to any ApexBank account or saved beneficiary.</p>
      </header>

      <div style={{ display: 'grid', gridTemplateColumns: '1fr 1fr', gap: '32px' }}>
        {/* Transfer Form */}
        <div className="glass" style={{ padding: '32px' }}>
          <form style={{ display: 'flex', flexDirection: 'column', gap: '24px' }}>
            <div>
              <label style={{ display: 'block', marginBottom: '12px', fontSize: '14px', color: '#94a3b8' }}>Select Beneficiary</label>
              <div style={{ position: 'relative' }}>
                <Search size={18} style={{ position: 'absolute', left: '12px', top: '50%', transform: 'translateY(-50%)', color: '#94a3b8' }} />
                <input 
                  type="text" 
                  placeholder="Enter name or account number" 
                  value={recipient}
                  onChange={(e) => setRecipient(e.target.value)}
                  style={{ width: '100%', padding: '12px 12px 12px 40px', background: 'rgba(255,255,255,0.05)', border: '1px solid rgba(255,255,255,0.1)', borderRadius: '12px', color: '#fff', outline: 'none' }}
                />
              </div>
            </div>

            <div>
              <label style={{ display: 'block', marginBottom: '12px', fontSize: '14px', color: '#94a3b8' }}>Amount to Transfer</label>
              <div style={{ position: 'relative' }}>
                <span style={{ position: 'absolute', left: '16px', top: '50%', transform: 'translateY(-50%)', fontSize: '20px', fontWeight: 'bold' }}>$</span>
                <input 
                  type="number" 
                  placeholder="0.00" 
                  value={amount}
                  onChange={(e) => setAmount(e.target.value)}
                  style={{ width: '100%', padding: '16px 16px 16px 40px', fontSize: '24px', fontWeight: '700', background: 'rgba(255,255,255,0.05)', border: '1px solid rgba(255,255,255,0.1)', borderRadius: '12px', color: 'var(--accent)', outline: 'none' }}
                />
              </div>
            </div>

            <div style={{ padding: '16px', background: 'rgba(202, 138, 4, 0.05)', borderRadius: '12px', display: 'flex', gap: '12px', alignItems: 'flex-start' }}>
              <Info size={20} color="var(--accent)" style={{ marginTop: '2px' }} />
              <p style={{ fontSize: '12px', color: '#94a3b8', lineHeight: '1.5' }}>
                Transfer fee is 0.00%. Funds are typically available immediately. Please verify the recipient before confirming.
              </p>
            </div>

            <button className="btn btn-primary" style={{ padding: '16px', justifyContent: 'center', fontSize: '18px' }}>
              Confirm Transfer <Send size={20} />
            </button>
          </form>
        </div>

        {/* Quick Select */}
        <div>
          <h3 style={{ fontSize: '18px', marginBottom: '20px' }}>Quick Select</h3>
          <div style={{ display: 'flex', flexDirection: 'column', gap: '16px' }}>
            {beneficiaries.map((b) => (
              <div 
                key={b.id} 
                onClick={() => setRecipient(b.name)}
                style={{ 
                  padding: '16px', 
                  borderRadius: '16px', 
                  background: recipient === b.name ? 'rgba(202, 138, 4, 0.1)' : 'rgba(255,255,255,0.03)',
                  border: recipient === b.name ? '1px solid var(--accent)' : '1px solid rgba(255,255,255,0.05)',
                  display: 'flex',
                  alignItems: 'center',
                  gap: '16px',
                  cursor: 'pointer',
                  transition: 'all 0.2s'
                }}
              >
                <div style={{ width: '44px', height: '44px', borderRadius: '50%', background: 'rgba(255,255,255,0.1)', display: 'flex', alignItems: 'center', justifyContent: 'center', fontWeight: 'bold', color: 'var(--accent)' }}>
                  {b.avatar}
                </div>
                <div>
                  <div style={{ fontWeight: '600' }}>{b.name}</div>
                  <div style={{ fontSize: '12px', color: '#94a3b8' }}>{b.acc}</div>
                </div>
              </div>
            ))}
          </div>
        </div>
      </div>
    </div>
  );
};

export default Transfer;
