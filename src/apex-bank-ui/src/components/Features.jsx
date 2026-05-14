const Features = () => {


  const features = [
    { title: 'Global Transfers', description: 'Send money to over 150 countries with the lowest market rates and instant processing.', icon: '🌍' },
    { title: 'Smart Analytics', description: 'Get real-time insights into your spending habits with AI-powered financial tracking.', icon: '📊' },
    { title: 'Top-tier Security', description: 'Your data is protected by multi-factor authentication and AES-256 encryption.', icon: '🔒' },
    { title: '24/7 Support', description: 'Our dedicated team is always available to help you with any queries or issues.', icon: '💬' }
  ];

  return (
    <section className="container" style={{ paddingBottom: '100px' }}>
      <h2 style={{ textAlign: 'center', fontSize: '36px', marginBottom: '60px' }}>Why Choose <span style={{ color: 'var(--accent)' }}>ApexBank?</span></h2>
      <div style={{ display: 'grid', gridTemplateColumns: 'repeat(auto-fit, minmax(250px, 1fr))', gap: '30px' }}>
        {features.map((f, i) => (
          <div key={i} className="glass" style={{ padding: '40px', textAlign: 'left', transition: 'transform 0.3s ease' }}>
            <div style={{ fontSize: '40px', marginBottom: '20px' }}>{f.icon}</div>
            <h3 style={{ fontSize: '20px', marginBottom: '15px' }}>{f.title}</h3>
            <p style={{ color: 'var(--text-muted)', lineHeight: '1.6' }}>{f.description}</p>
          </div>
        ))}
      </div>
    </section>
  );
};

export default Features;
