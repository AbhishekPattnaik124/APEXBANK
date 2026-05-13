import React, { useState, useEffect } from 'react';
import { motion, AnimatePresence } from 'framer-motion';
import { Bell, X } from 'lucide-react';
import notificationService from '../services/NotificationService';

const NotificationToast = () => {
  const [notifications, setNotifications] = useState([]);

  useEffect(() => {
    notificationService.start();
    notificationService.onNotification((message) => {
      const id = Date.now();
      setNotifications(prev => [...prev, { id, message }]);
      setTimeout(() => {
        setNotifications(prev => prev.filter(n => n.id !== id));
      }, 5000);
    });
  }, []);

  return (
    <div style={{ position: 'fixed', top: '24px', right: '24px', zIndex: 1000, display: 'flex', flexDirection: 'column', gap: '12px' }}>
      <AnimatePresence>
        {notifications.map((n) => (
          <motion.div
            key={n.id}
            initial={{ opacity: 0, x: 50, scale: 0.9 }}
            animate={{ opacity: 1, x: 0, scale: 1 }}
            exit={{ opacity: 0, scale: 0.9, transition: { duration: 0.2 } }}
            className="glass"
            style={{ 
              padding: '16px 20px', 
              minWidth: '300px', 
              display: 'flex', 
              alignItems: 'center', 
              gap: '16px',
              borderLeft: '4px solid var(--accent)',
              background: 'rgba(15, 23, 42, 0.95)',
              boxShadow: '0 10px 30px rgba(0,0,0,0.5)'
            }}
          >
            <div style={{ background: 'rgba(202, 138, 4, 0.1)', padding: '8px', borderRadius: '50%' }}>
              <Bell size={20} color="var(--accent)" />
            </div>
            <div style={{ flex: 1 }}>
              <div style={{ fontWeight: '700', fontSize: '14px', color: '#fff' }}>New Notification</div>
              <div style={{ fontSize: '13px', color: '#94a3b8' }}>{n.message}</div>
            </div>
            <button 
              onClick={() => setNotifications(prev => prev.filter(notif => notif.id !== n.id))}
              style={{ background: 'none', border: 'none', color: '#64748b', cursor: 'pointer' }}
            >
              <X size={16} />
            </button>
          </motion.div>
        ))}
      </AnimatePresence>
    </div>
  );
};

export default NotificationToast;
