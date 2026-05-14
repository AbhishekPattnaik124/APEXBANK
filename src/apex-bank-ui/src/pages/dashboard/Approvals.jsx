import { Check, X, FileText, User } from 'lucide-react';

const Approvals = () => {
  const pendingRequests = [
    { id: 1, name: 'David Miller', type: 'Savings Account', date: '2 hours ago', status: 'Pending KYC' },
    { id: 2, name: 'Sarah Wilson', type: 'Business Account', date: '5 hours ago', status: 'Documents Uploaded' },
    { id: 3, name: 'James Knight', type: 'Loan Application', date: '1 day ago', status: 'Pending Review' },
  ];

  return (
    <div style={{ display: 'flex', flexDirection: 'column', gap: '32px' }}>
      <header>
        <h2 style={{ fontSize: '28px', marginBottom: '8px' }}>Pending Approvals</h2>
        <p style={{ color: '#94a3b8' }}>Review and process customer account applications and KYC documents.</p>
      </header>

      <div style={{ display: 'grid', gridTemplateColumns: 'repeat(auto-fit, minmax(400px, 1fr))', gap: '24px' }}>
        {pendingRequests.map((req) => (
          <div key={req.id} className="glass" style={{ padding: '24px' }}>
            <div style={{ display: 'flex', justifyContent: 'space-between', alignItems: 'flex-start', marginBottom: '20px' }}>
              <div style={{ display: 'flex', gap: '16px', alignItems: 'center' }}>
                <div style={{ width: '48px', height: '48px', borderRadius: '12px', background: 'rgba(255,255,255,0.05)', display: 'flex', alignItems: 'center', justifyContent: 'center' }}>
                  <User color="var(--accent)" />
                </div>
                <div>
                  <div style={{ fontWeight: '700', fontSize: '18px' }}>{req.name}</div>
                  <div style={{ fontSize: '14px', color: '#94a3b8' }}>{req.type}</div>
                </div>
              </div>
              <span style={{ fontSize: '12px', color: 'var(--accent)', background: 'rgba(202,138,4,0.1)', padding: '4px 12px', borderRadius: '20px' }}>{req.status}</span>
            </div>

            <div style={{ padding: '16px', background: 'rgba(255,255,255,0.02)', borderRadius: '12px', marginBottom: '24px', display: 'flex', alignItems: 'center', gap: '12px' }}>
              <FileText size={20} color="#94a3b8" />
              <div style={{ flex: 1, fontSize: '13px' }}>
                <div style={{ color: '#cbd5e1' }}>KYC_Document_v1.pdf</div>
                <div style={{ color: '#64748b' }}>Uploaded {req.date}</div>
              </div>
              <button style={{ color: 'var(--accent)', background: 'none', border: 'none', fontSize: '13px', fontWeight: '600', cursor: 'pointer' }}>View</button>
            </div>

            <div style={{ display: 'flex', gap: '12px' }}>
              <button className="btn btn-primary" style={{ flex: 1, justifyContent: 'center', background: '#10b981' }}>
                <Check size={18} /> Approve
              </button>
              <button className="btn btn-outline" style={{ flex: 1, justifyContent: 'center', color: '#ef4444', border: '1px solid #ef4444' }}>
                <X size={18} /> Reject
              </button>
            </div>
          </div>
        ))}
      </div>
    </div>
  );
};

export default Approvals;
