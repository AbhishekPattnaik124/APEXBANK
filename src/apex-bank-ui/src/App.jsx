import React from 'react';
import ApexNavbar from './components/ApexNavbar';



import Hero from './components/Hero';
import Features from './components/Features';




function App() {
  return (
    <div className="app">
      <ApexNavbar />

      <main>
        <Hero />
        <Features />
      </main>
      <footer style={{ padding: '40px 0', textAlign: 'center', color: 'var(--text-muted)' }}>
        <p>&copy; 2026 ApexBank. All rights reserved.</p>
      </footer>
    </div>
  );
}

export default App;
