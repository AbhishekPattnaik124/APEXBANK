import React from 'react';
import { Link } from 'react-router-dom';


const Hero = () => {


  return (
    <section className="container" style={{ paddingTop: '80px', paddingBottom: '100px', textAlign: 'center' }}>
      <div className="glass" style={{ padding: '80px 40px', position: 'relative', overflow: 'hidden' }}>
        <div style={{ position: 'absolute', top: '-50%', left: '-20%', width: '100%', height: '200%', background: 'radial-gradient(circle, rgba(202, 138, 4, 0.1) 0%, transparent 70%)', zIndex: 0 }}></div>
        
        <h1 style={{ fontSize: '64px', marginBottom: '24px', lineHeight: '1.1', position: 'relative', zIndex: 1 }}>
          The Future of <span style={{ color: 'var(--accent)' }}>Digital Banking</span> is Here
        </h1>
        <p style={{ fontSize: '20px', color: 'var(--text-muted)', maxWidth: '700px', margin: '0 auto 40px', lineHeight: '1.6', position: 'relative', zIndex: 1 }}>
          Experience enterprise-grade security, seamless international transfers, and real-time financial insights with ApexBank. Built for the modern world.
        </p>
        <div style={{ display: 'flex', gap: '20px', justifyContent: 'center', position: 'relative', zIndex: 1 }}>
          <Link to="/register" className="btn btn-primary" style={{ padding: '15px 40px', fontSize: '18px', textDecoration: 'none' }}>Get Started Now</Link>
          <Link to="/dashboard" className="btn btn-outline" style={{ padding: '15px 40px', fontSize: '18px', textDecoration: 'none' }}>Explore Demo</Link>
        </div>


        <div style={{ marginTop: '60px', display: 'flex', justifyContent: 'center', gap: '40px', opacity: 0.7 }}>
          <div style={{ textAlign: 'left' }}>
            <div style={{ fontSize: '24px', fontWeight: '800' }}>2M+</div>
            <div style={{ fontSize: '12px', color: 'var(--text-muted)' }}>Active Users</div>
          </div>
          <div style={{ textAlign: 'left' }}>
            <div style={{ fontSize: '24px', fontWeight: '800' }}>$40B+</div>
            <div style={{ fontSize: '12px', color: 'var(--text-muted)' }}>Processed Annually</div>
          </div>
          <div style={{ textAlign: 'left' }}>
            <div style={{ fontSize: '24px', fontWeight: '800' }}>150+</div>
            <div style={{ fontSize: '12px', color: 'var(--text-muted)' }}>Countries Supported</div>
          </div>
        </div>
      </div>
    </section>
  );
};

export default Hero;
