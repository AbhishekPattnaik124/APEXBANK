import { Users, AlertTriangle, Activity, ShieldAlert } from 'lucide-react';

const AdminAnalytics = () => {
  const systemStats = [
    { label: 'Total Users', value: '12,840', icon: <Users />, color: '#3b82f6' },
    { label: 'Active Sessions', value: '452', icon: <Activity />, color: '#10b981' },
    { label: 'System Health', value: '99.9%', icon: <ShieldAlert />, color: '#8b5cf6' },
    { label: 'Security Alerts', value: '3', icon: <AlertTriangle />, color: '#ef4444' },
  ];

  const fraudAlerts = [
    { id: 1, user: 'Unknown Device', location: 'Kiev, UA', severity: 'High', action: 'Account Locked' },
    { id: 2, user: 'Multiple Failed Logins', location: 'London, UK', severity: 'Medium', action: 'OTP Required' },
    { id: 3, user: 'Large Transfer Alert', location: 'Mumbai, IN', severity: 'Low', action: 'Flagged' },
  ];

  return (
    <div style={{ display: 'flex', flexDirection: 'column', gap: '32px' }}>
      <header>
        <h2 style={{ fontSize: '28px', marginBottom: '8px' }}>System Administration</h2>
        <p style={{ color: '#94a3b8' }}>Monitor bank performance, user activity, and security infrastructure.</p>
      </header>

      {/* Admin Stats */}
      <div style={{ display: 'grid', gridTemplateColumns: 'repeat(auto-fit, minmax(240px, 1fr))', gap: '24px' }}>
        {systemStats.map((stat, i) => (
          <div key={i} className="glass" style={{ padding: '24px', borderLeft: `4px solid ${stat.color}` }}>
            <div style={{ display: 'flex', justifyContent: 'space-between', marginBottom: '16px' }}>
              <span style={{ color: '#94a3b8', fontSize: '14px', fontWeight: '600' }}>{stat.label}</span>
              <div style={{ color: stat.color }}>{stat.icon}</div>
            </div>
            <div style={{ fontSize: '28px', fontWeight: '800' }}>{stat.value}</div>
          </div>
        ))}
      </div>

      <div style={{ display: 'grid', gridTemplateColumns: '1fr 1fr', gap: '32px' }}>
        {/* Fraud Monitoring */}
        <div className="glass" style={{ padding: '24px' }}>
          <div style={{ display: 'flex', alignItems: 'center', gap: '12px', marginBottom: '24px' }}>
            <ShieldAlert color="#ef4444" />
            <h3 style={{ fontSize: '20px' }}>Fraud Detection Center</h3>
          </div>
          <div style={{ display: 'flex', flexDirection: 'column', gap: '12px' }}>
            {fraudAlerts.map((alert) => (
              <div key={alert.id} style={{ padding: '16px', background: 'rgba(239, 68, 68, 0.05)', borderRadius: '12px', border: '1px solid rgba(239, 68, 68, 0.1)', display: 'flex', justifyContent: 'space-between', alignItems: 'center' }}>
                <div>
                  <div style={{ fontWeight: '700', color: '#f8fafc' }}>{alert.user}</div>
                  <div style={{ fontSize: '12px', color: '#94a3b8' }}>Location: {alert.location}</div>
                </div>
                <div style={{ display: 'flex', flexDirection: 'column', alignItems: 'flex-end', gap: '8px' }}>
                  <span style={{ fontSize: '11px', fontWeight: '800', color: '#ef4444', textTransform: 'uppercase' }}>{alert.severity} Risk</span>
                  <div style={{ display: 'flex', gap: '8px' }}>
                    <button style={{ padding: '4px 8px', fontSize: '11px', background: '#ef4444', border: 'none', borderRadius: '4px', color: '#fff', cursor: 'pointer' }}>Block</button>
                    <button style={{ padding: '4px 8px', fontSize: '11px', background: 'rgba(255,255,255,0.05)', border: '1px solid rgba(255,255,255,0.1)', borderRadius: '4px', color: '#fff', cursor: 'pointer' }}>Dismiss</button>
                  </div>
                </div>
              </div>
            ))}
          </div>

        </div>

        {/* System Logs */}
        <div className="glass" style={{ padding: '24px' }}>
          <h3 style={{ fontSize: '20px', marginBottom: '24px' }}>Real-time Audit Logs</h3>
          <div style={{ display: 'flex', flexDirection: 'column', gap: '12px', fontFamily: 'monospace', fontSize: '12px', color: '#94a3b8' }}>
            <div style={{ padding: '8px', borderLeft: '2px solid #10b981' }}>[21:14:02] INFO: User login successful - ID: 4421</div>
            <div style={{ padding: '8px', borderLeft: '2px solid #3b82f6' }}>[21:13:58] DB: Transaction commit - TRX_99281</div>
            <div style={{ padding: '8px', borderLeft: '2px solid #ca8a04' }}>[21:13:45] WARN: Rate limit reached - IP: 192.168.1.1</div>
            <div style={{ padding: '8px', borderLeft: '2px solid #10b981' }}>[21:13:20] INFO: New account application - sarah_w</div>
          </div>
        </div>
      </div>
    </div>
  );
};

export default AdminAnalytics;
