import { Search, Filter, Download, ArrowUpRight, ArrowDownLeft } from 'lucide-react';
import jsPDF from 'jspdf';
import 'jspdf-autotable';

const History = () => {

  const transactions = [
    { id: 'TX99281', name: 'Amazon.com', date: 'Oct 12, 2026', amount: '-$120.50', status: 'Completed', type: 'Expense', method: 'Debit Card' },
    { id: 'TX99282', name: 'Salary Credit', date: 'Oct 10, 2026', amount: '+$5,000.00', status: 'Completed', type: 'Income', method: 'Direct Deposit' },
    { id: 'TX99283', name: 'Starbucks Coffee', date: 'Oct 09, 2026', amount: '-$15.20', status: 'Pending', type: 'Expense', method: 'Debit Card' },
    { id: 'TX99284', name: 'Rent Payment', date: 'Oct 05, 2026', amount: '-$1,200.00', status: 'Completed', type: 'Expense', method: 'Bank Transfer' },
    { id: 'TX99285', name: 'Alice Smith', date: 'Oct 02, 2026', amount: '-$450.00', status: 'Completed', type: 'Transfer', method: 'Internal' },
    { id: 'TX99286', name: 'Apple Store', date: 'Sep 28, 2026', amount: '-$999.00', status: 'Completed', type: 'Expense', method: 'Credit Card' },
  ];

  const exportToPDF = () => {
    const doc = new jsPDF();
    doc.setFontSize(20);
    doc.text('ApexBank Transaction Statement', 14, 22);
    doc.setFontSize(11);
    doc.setTextColor(100);
    doc.text(`Generated on: ${new Date().toLocaleString()}`, 14, 30);

    const tableColumn = ["ID", "Transaction", "Date", "Method", "Status", "Amount"];
    const tableRows = [];

    transactions.forEach(tx => {
      const txData = [
        tx.id,
        tx.name,
        tx.date,
        tx.method,
        tx.status,
        tx.amount
      ];
      tableRows.push(txData);
    });

    doc.autoTable(tableColumn, tableRows, { startY: 40 });
    doc.save(`ApexBank_Statement_${new Date().getTime()}.pdf`);
  };


  return (
    <div style={{ display: 'flex', flexDirection: 'column', gap: '32px' }}>
      <header style={{ display: 'flex', justifyContent: 'space-between', alignItems: 'flex-end' }}>
        <div>
          <h2 style={{ fontSize: '28px', marginBottom: '8px' }}>Transaction History</h2>
          <p style={{ color: '#94a3b8' }}>View and export your complete financial history.</p>
        </div>
        <button className="btn btn-outline" style={{ gap: '8px' }} onClick={exportToPDF}>
          <Download size={18} /> Export PDF
        </button>
      </header>


      {/* Filters Bar */}
      <div className="glass" style={{ padding: '16px 24px', display: 'flex', justifyContent: 'space-between', alignItems: 'center', gap: '20px' }}>
        <div style={{ position: 'relative', flex: 1 }}>
          <Search size={18} style={{ position: 'absolute', left: '12px', top: '50%', transform: 'translateY(-50%)', color: '#94a3b8' }} />
          <input 
            type="text" 
            placeholder="Search by name, ID or amount..." 
            style={{ width: '100%', background: 'transparent', border: 'none', padding: '8px 12px 8px 40px', color: '#fff', outline: 'none' }}
          />
        </div>
        <div style={{ display: 'flex', gap: '12px' }}>
          <button className="btn btn-outline" style={{ padding: '8px 16px', fontSize: '14px', border: '1px solid rgba(255,255,255,0.1)', color: '#fff' }}>
            <Filter size={16} /> Filter
          </button>
          <select style={{ background: 'rgba(255,255,255,0.05)', border: '1px solid rgba(255,255,255,0.1)', borderRadius: '8px', color: '#fff', padding: '0 12px', outline: 'none' }}>
            <option>Last 30 Days</option>
            <option>Last 90 Days</option>
            <option>2026</option>
          </select>
        </div>
      </div>

      {/* Transactions Table */}
      <div className="glass" style={{ overflow: 'hidden' }}>
        <table style={{ width: '100%', borderCollapse: 'collapse', textAlign: 'left' }}>
          <thead>
            <tr style={{ background: 'rgba(255,255,255,0.02)', borderBottom: '1px solid rgba(255,255,255,0.05)' }}>
              <th style={{ padding: '20px 24px', color: '#94a3b8', fontWeight: '500', fontSize: '14px' }}>TRANSACTION</th>
              <th style={{ padding: '20px 24px', color: '#94a3b8', fontWeight: '500', fontSize: '14px' }}>DATE</th>
              <th style={{ padding: '20px 24px', color: '#94a3b8', fontWeight: '500', fontSize: '14px' }}>METHOD</th>
              <th style={{ padding: '20px 24px', color: '#94a3b8', fontWeight: '500', fontSize: '14px' }}>STATUS</th>
              <th style={{ padding: '20px 24px', color: '#94a3b8', fontWeight: '500', fontSize: '14px', textAlign: 'right' }}>AMOUNT</th>
            </tr>
          </thead>
          <tbody>
            {transactions.map((tx) => (
              <tr key={tx.id} style={{ borderBottom: '1px solid rgba(255,255,255,0.05)', transition: 'background 0.2s' }}>
                <td style={{ padding: '20px 24px' }}>
                  <div style={{ display: 'flex', alignItems: 'center', gap: '12px' }}>
                    <div style={{ width: '32px', height: '32px', borderRadius: '8px', background: 'rgba(255,255,255,0.05)', display: 'flex', alignItems: 'center', justifyContent: 'center' }}>
                      {tx.type === 'Income' ? <ArrowDownLeft size={16} color="#10b981" /> : <ArrowUpRight size={16} color="#ef4444" />}
                    </div>
                    <div>
                      <div style={{ fontWeight: '600', fontSize: '15px' }}>{tx.name}</div>
                      <div style={{ fontSize: '11px', color: '#94a3b8' }}>ID: {tx.id}</div>
                    </div>
                  </div>
                </td>
                <td style={{ padding: '20px 24px', fontSize: '14px', color: '#cbd5e1' }}>{tx.date}</td>
                <td style={{ padding: '20px 24px', fontSize: '14px', color: '#cbd5e1' }}>{tx.method}</td>
                <td style={{ padding: '20px 24px' }}>
                  <span style={{ 
                    padding: '4px 12px', 
                    borderRadius: '20px', 
                    fontSize: '12px', 
                    fontWeight: '600',
                    background: tx.status === 'Completed' ? 'rgba(16, 185, 129, 0.1)' : 'rgba(202, 138, 4, 0.1)',
                    color: tx.status === 'Completed' ? '#10b981' : '#ca8a04'
                  }}>
                    {tx.status}
                  </span>
                </td>
                <td style={{ padding: '20px 24px', textAlign: 'right', fontWeight: '700', color: tx.amount.startsWith('+') ? '#10b981' : '#fff' }}>
                  {tx.amount}
                </td>
              </tr>
            ))}
          </tbody>
        </table>
        <div style={{ padding: '20px 24px', display: 'flex', justifyContent: 'center', gap: '8px' }}>
          <button className="btn btn-outline" style={{ padding: '8px 16px', fontSize: '13px' }}>Previous</button>
          <button className="btn btn-outline" style={{ padding: '8px 16px', fontSize: '13px', background: 'rgba(255,255,255,0.05)' }}>1</button>
          <button className="btn btn-outline" style={{ padding: '8px 16px', fontSize: '13px' }}>2</button>
          <button className="btn btn-outline" style={{ padding: '8px 16px', fontSize: '13px' }}>Next</button>
        </div>
      </div>
    </div>
  );
};

export default History;
