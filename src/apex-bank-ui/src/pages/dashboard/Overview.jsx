import React from 'react';
import { motion } from 'framer-motion';
import { TrendingUp, ArrowUpRight, ArrowDownLeft, Wallet, CreditCard } from 'lucide-react';

const Overview = () => {
  const stats = [
    { label: 'Total Balance', value: '$45,230.00', icon: <Wallet color="#ca8a04" />, change: '+2.5%' },
    { label: 'Monthly Income', value: '$8,400.00', icon: <ArrowDownLeft color="#10b981" />, change: '+12%' },
    { label: 'Monthly Expenses', value: '$3,120.00', icon: <ArrowUpRight color="#ef4444" />, change: '-5%' },
  ];

  const recentTransactions = [
    { id: 1, name: 'Amazon.com', date: 'Oct 12, 2026', amount: '-$120.50', status: 'Completed', type: 'Expense' },
    { id: 2, name: 'Salary Credit', date: 'Oct 10, 2026', amount: '+$5,000.00', status: 'Completed', type: 'Income' },
    { id: 3, name: 'Starbucks Coffee', date: 'Oct 09, 2026', amount: '-$15.20', status: 'Pending', type: 'Expense' },
    { id: 4, name: 'Rent Payment', date: 'Oct 05, 2026', amount: '-$1,200.00', status: 'Completed', type: 'Expense' },
  ];

  return (
    <div style={{ display: 'flex', flexDirection: 'column', gap: '32px' }}>
      <header>
        <h2 style={{ fontSize: '28px', marginBottom: '8px' }}>Dashboard Overview</h2>
        <p style={{ color: '#94a3b8' }}>Welcome back, John! Here's what's happening with your accounts.</p>
      </header>

      {/* Stats Grid */}
      <div style={{ display: 'grid', gridTemplateColumns: 'repeat(auto-fit, minmax(300px, 1fr))', gap: '24px' }}>
        {stats.map((stat, i) => (
          <motion.div 
            key={i}
            initial={{ opacity: 0, y: 20 }}
            animate={{ opacity: 1, y: 0 }}
            transition={{ delay: i * 0.1 }}
            className="glass"
            style={{ padding: '24px', display: 'flex', justifyContent: 'space-between', alignItems: 'center' }}
          >
            <div>
              <p style={{ fontSize: '14px', color: '#94a3b8', marginBottom: '8px' }}>{stat.label}</p>
              <h3 style={{ fontSize: '24px', fontWeight: '800' }}>{stat.value}</h3>
              <span style={{ fontSize: '12px', color: stat.change.startsWith('+') ? '#10b981' : '#ef4444' }}>
                {stat.change} <span style={{ color: '#94a3b8' }}>from last month</span>
              </span>
            </div>
            <div style={{ padding: '12px', background: 'rgba(255,255,255,0.05)', borderRadius: '12px' }}>
              {stat.icon}
            </div>
          </motion.div>
        ))}
      </div>

      <div style={{ display: 'grid', gridTemplateColumns: '2fr 1fr', gap: '32px' }}>
        {/* Recent Transactions */}
        <div className="glass" style={{ padding: '24px' }}>
          <div style={{ display: 'flex', justifyContent: 'space-between', alignItems: 'center', marginBottom: '24px' }}>
            <h3 style={{ fontSize: '20px' }}>Recent Transactions</h3>
            <button style={{ color: 'var(--accent)', background: 'none', border: 'none', cursor: 'pointer', fontWeight: '600' }}>View All</button>
          </div>
          <div style={{ display: 'flex', flexDirection: 'column', gap: '16px' }}>
            {recentTransactions.map((tx) => (
              <div key={tx.id} style={{ display: 'flex', justifyContent: 'space-between', alignItems: 'center', padding: '12px 0', borderBottom: '1px solid rgba(255,255,255,0.05)' }}>
                <div style={{ display: 'flex', alignItems: 'center', gap: '16px' }}>
                  <div style={{ width: '40px', height: '40px', borderRadius: '10px', background: 'rgba(255,255,255,0.05)', display: 'flex', alignItems: 'center', justifyContent: 'center' }}>
                    {tx.type === 'Income' ? <ArrowDownLeft color="#10b981" /> : <ArrowUpRight color="#ef4444" />}
                  </div>
                  <div>
                    <div style={{ fontWeight: '600' }}>{tx.name}</div>
                    <div style={{ fontSize: '12px', color: '#94a3b8' }}>{tx.date}</div>
                  </div>
                </div>
                <div style={{ textAlign: 'right' }}>
                  <div style={{ fontWeight: '700', color: tx.amount.startsWith('+') ? '#10b981' : '#fff' }}>{tx.amount}</div>
                  <div style={{ fontSize: '11px', color: tx.status === 'Completed' ? '#10b981' : '#ca8a04' }}>{tx.status}</div>
                </div>
              </div>
            ))}
          </div>
        </div>

        {/* My Cards */}
        <div className="glass" style={{ padding: '24px' }}>
          <h3 style={{ fontSize: '20px', marginBottom: '24px' }}>My Cards</h3>
          <div style={{ 
            background: 'linear-gradient(135deg, #0f172a 0%, #334155 100%)',
            padding: '24px',
            borderRadius: '20px',
            position: 'relative',
            overflow: 'hidden',
            aspectRatio: '1.6',
            display: 'flex',
            flexDirection: 'column',
            justifyContent: 'space-between'
          }}>
            <div style={{ display: 'flex', justifyContent: 'space-between', alignItems: 'flex-start' }}>
              <CreditCard size={32} color="var(--accent)" />
              <div style={{ fontWeight: '800', fontStyle: 'italic' }}>VISA</div>
            </div>
            <div>
              <div style={{ fontSize: '18px', letterSpacing: '2px', marginBottom: '8px' }}>**** **** **** 4582</div>
              <div style={{ display: 'flex', gap: '24px', fontSize: '12px', color: '#cbd5e1' }}>
                <div>
                  <div style={{ fontSize: '10px', color: '#94a3b8' }}>EXPIRY</div>
                  12/28
                </div>
                <div>
                  <div style={{ fontSize: '10px', color: '#94a3b8' }}>CVV</div>
                  ***
                </div>
              </div>
            </div>
          </div>
          <button className="btn btn-outline" style={{ width: '100%', marginTop: '24px', justifyContent: 'center' }}>+ Add New Card</button>
        </div>
      </div>
    </div>
  );
};

export default Overview;
