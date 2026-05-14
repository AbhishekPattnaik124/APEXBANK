# 📄 Product Requirements Document (PRD)
## ApexBank Management System — v2.0

| | |
|---|---|
| **Document Type** | Product Requirements Document |
| **Product** | ApexBank Management System |
| **Version** | 2.0.0 — God-Tier Enterprise Release |
| **Author** | Abhishek Pattnaik |
| **Date** | May 2026 |
| **Status** | ✅ Approved for Development |

---

## 1. Executive Summary

ApexBank is a **production-grade, full-stack digital banking management platform** designed for modern Indian fintech needs. It provides a comprehensive suite of banking operations — from core account management and real-time fund transfers, to enterprise-grade loan processing, card management, and AI-assisted fraud detection — all wrapped in a luxury "Black & Gold" premium user experience.

Version 2.0 elevates the platform to **enterprise tier** with complete MySQL database integration, BCrypt security hardening, comprehensive audit trails, and full compliance-ready KYC workflows.

---

## 2. Product Vision

> **"To be the most secure, visually stunning, and developer-friendly open-source banking platform — setting the gold standard for fintech products built in India."**

### 2.1 Strategic Goals
| Goal | Metric | Target (v2.0) |
|---|---|---|
| Security | Zero plain-text credentials | ✅ BCrypt + Account Lockout |
| Performance | API response time | < 200ms P95 |
| Reliability | System uptime | 99.9% SLA |
| User Experience | CSAT Score | > 4.5/5.0 |
| Compliance | KYC Coverage | 100% of active users |

---

## 3. Target Users & Personas

### 👤 Persona 1 — Priya (The Digital-First Customer)
- **Age**: 26, Software Engineer, Bhubaneswar
- **Goals**: View balances instantly, transfer money, apply for personal loans online, track expenses
- **Pain Points**: Slow bank apps, confusing UIs, hidden charges, paper-based loan applications
- **Devices**: Mobile (primary), Desktop (secondary)

### 👤 Persona 2 — Rajesh (The Branch Employee)
- **Age**: 34, Banking Operations Officer
- **Goals**: Process loan approvals, assist customers, run reports, flag suspicious transactions
- **Pain Points**: Legacy bank software, slow approval workflows, no real-time data
- **Devices**: Desktop

### 👤 Persona 3 — Arjun (The System Administrator)
- **Age**: 40, IT Head / CTO
- **Goals**: Monitor platform health, manage users, view audit trails, ensure compliance
- **Pain Points**: No unified admin panel, no audit logs, manual KYC verification
- **Devices**: Desktop, Dashboard TV

---

## 4. Feature Epics & User Stories

### 🔐 Epic 1 — Authentication & Security

| Story ID | User Story | Priority | Status |
|---|---|---|---|
| AUTH-01 | As a user, I want to register with my name, email, and phone so that I get a unique savings account automatically | P0 | ✅ Done |
| AUTH-02 | As a user, I want to log in with email + password so that I can access my dashboard | P0 | ✅ Done |
| AUTH-03 | As a system, I want to lock accounts after 5 failed logins for 15 minutes to prevent brute-force attacks | P0 | ✅ Done |
| AUTH-04 | As a user, I want my session to expire after 7 days and be forced to re-login | P1 | ✅ Done |
| AUTH-05 | As an admin, I want to manually unlock a locked user account | P1 | ✅ Done |

### 💰 Epic 2 — Account Management

| Story ID | User Story | Priority | Status |
|---|---|---|---|
| ACC-01 | As a customer, I want to view my account balance and account number | P0 | ✅ Done |
| ACC-02 | As a customer, I want to see Savings, Current, or Business account types | P1 | ✅ Done |
| ACC-03 | As an admin, I want to freeze/unfreeze any account | P0 | ✅ Done |
| ACC-04 | As a customer, I want to see my IFSC code and branch details | P2 | ✅ Done |

### 💸 Epic 3 — Transactions

| Story ID | User Story | Priority | Status |
|---|---|---|---|
| TXN-01 | As a customer, I want to transfer funds between accounts atomically | P0 | ✅ Done |
| TXN-02 | As a customer, I want to deposit and withdraw cash | P0 | ✅ Done |
| TXN-03 | As a customer, I want to see my last 50 transactions with reference numbers | P0 | ✅ Done |
| TXN-04 | As a system, I want to record the IP address, channel, and fee for every transaction | P1 | ✅ Done |
| TXN-05 | As a customer, I want to receive a real-time notification for every transaction via SignalR | P1 | ✅ Done |

### 🏦 Epic 4 — Loan Management

| Story ID | User Story | Priority | Status |
|---|---|---|---|
| LOAN-01 | As a customer, I want to apply for Personal, Home, Auto, or Education loans online | P0 | ✅ Done |
| LOAN-02 | As a customer, I want to use an EMI calculator before applying | P1 | ✅ Done |
| LOAN-03 | As an employee, I want to review and approve/reject loan applications with a reason | P0 | ✅ Done |
| LOAN-04 | As a customer, I want to track my loan outstanding balance and payment history | P1 | ✅ Done |
| LOAN-05 | As the system, I want to auto-calculate EMI using the standard formula | P0 | ✅ Done |

### 💳 Epic 5 — Card Management

| Story ID | User Story | Priority | Status |
|---|---|---|---|
| CARD-01 | As a customer, I want to issue a Debit or Credit card for my account | P0 | ✅ Done |
| CARD-02 | As a customer, I want to freeze my card temporarily if I suspect misuse | P0 | ✅ Done |
| CARD-03 | As a customer, I want to set my daily and monthly spending limits | P1 | ✅ Done |
| CARD-04 | As a customer, I want to permanently block a lost card | P0 | ✅ Done |
| CARD-05 | As a customer, I want to view a virtual card display with masked number on the dashboard | P2 | ✅ Done |

### 🔔 Epic 6 — Notifications

| Story ID | User Story | Priority | Status |
|---|---|---|---|
| NOTIF-01 | As a customer, I want real-time transaction alerts via SignalR | P0 | ✅ Done |
| NOTIF-02 | As a customer, I want to see my notification history and mark them as read | P1 | ✅ Done |
| NOTIF-03 | As a customer, I want to see an unread badge count on my dashboard | P1 | ✅ Done |

### 🛡️ Epic 7 — KYC & Compliance

| Story ID | User Story | Priority | Status |
|---|---|---|---|
| KYC-01 | As a customer, I want to submit my Aadhar/Passport ID for verification | P1 | 🔄 Planned v2.5 |
| KYC-02 | As an admin, I want to approve or reject KYC submissions | P0 | ✅ Done |
| KYC-03 | As the system, I want to restrict features until KYC is "Verified" | P1 | 🔄 Planned v2.5 |
| AUDIT-01 | As an admin, I want to see every create/update/delete operation with old and new values | P0 | ✅ Done |

### 📊 Epic 8 — Admin & Analytics

| Story ID | User Story | Priority | Status |
|---|---|---|---|
| ADMIN-01 | As an admin, I want a dashboard showing total users, deposits, loans, and transactions | P0 | ✅ Done |
| ADMIN-02 | As an admin, I want to activate/deactivate any user account | P0 | ✅ Done |
| ADMIN-03 | As an admin, I want to see all audit logs with pagination | P1 | ✅ Done |
| ADMIN-04 | As an admin, I want to see KYC pending count and take action | P1 | ✅ Done |

---

## 5. Non-Functional Requirements

### 5.1 Performance
| Requirement | Target |
|---|---|
| API Response P95 (reads) | < 200ms |
| API Response P95 (writes) | < 500ms |
| Dashboard FCP | < 2 seconds |
| Database Query | < 50ms (indexed) |
| Concurrent Users | 500 simultaneous |

### 5.2 Security
| Requirement | Implementation |
|---|---|
| Password Storage | BCrypt work factor 11 |
| Authentication | JWT HS256, 7-day expiry, ClockSkew = 0 |
| Brute Force Protection | 5-attempt lockout, 15-min cooldown |
| API Rate Limiting | 60 req/min general, 10/5min auth |
| CORS | Whitelist-only origins |
| Data in Transit | HTTPS only in production |
| SQL Injection | EF Core parameterized queries |

### 5.3 Reliability
| Requirement | Target |
|---|---|
| Uptime SLA | 99.9% (< 8.7 hours/year downtime) |
| Database Backups | Daily automated |
| Transaction Atomicity | All fund movements use DB transactions |
| Error Handling | Global exception middleware + structured logs |

---

## 6. Product Roadmap

```
v1.0 — Complete          v2.0 — Current           v2.5 — Next             v3.0 — Future
────────────────         ──────────────────────   ───────────────────     ──────────────────
✅ JWT Auth              ✅ BCrypt Hardening       🔄 KYC Doc Upload       📋 AI Fraud ML
✅ Basic Accounts        ✅ Loan Management        🔄 UPI Integration      📋 React Native App
✅ Transactions          ✅ Card Management        🔄 Statement PDF        📋 Multi-currency
✅ SignalR               ✅ Admin Dashboard        🔄 Recurring Payments   📋 Open Banking API
✅ Luxury UI             ✅ Audit Logs             🔄 2FA / OTP            📋 NBFC License Prep
✅ Render Deploy         ✅ MySQL Fully Embedded   🔄 Credit Score Widget  📋 Blockchain Receipts
```

---

## 7. KPIs & Success Metrics

| KPI | Measurement Method | Target |
|---|---|---|
| API Error Rate | % of 5xx responses | < 0.1% |
| Login Success Rate | Successful / total attempts | > 95% |
| Loan Approval Time | Application → decision | < 24 hours |
| Notification Delivery | SignalR delivery rate | > 99% |
| Performance Score | Lighthouse score | > 90 |

---

*ApexBank PRD v2.0 — Confidential. Built with ❤️ by Abhishek Pattnaik.*
