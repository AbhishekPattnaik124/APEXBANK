# 📐 Technical Requirements Document (TRD)
## ApexBank Management System — v2.0

| | |
|---|---|
| **Document Type** | Technical Requirements Document |
| **Product** | ApexBank Management System |
| **Version** | 2.0.0 |
| **Author** | Abhishek Pattnaik |
| **Date** | May 2026 |
| **Status** | ✅ Implemented |

---

## 1. System Architecture

### 1.1 Architecture Pattern
ApexBank follows the **Clean Architecture** pattern (also known as Onion Architecture), enforcing strict dependency inversion and separation of concerns across 4 layers:

```
┌─────────────────────────────────────────────────────┐
│                   Presentation Layer                │
│         ApexBank.Api  (Controllers, Hubs)           │
│         apex-bank-ui  (React + Vite SPA)            │
├─────────────────────────────────────────────────────┤
│                 Application Layer                   │
│    ApexBank.Application  (Services, DTOs, Interfaces│
├─────────────────────────────────────────────────────┤
│                   Domain Layer                      │
│        ApexBank.Domain  (Entities, Value Objects)   │
├─────────────────────────────────────────────────────┤
│               Infrastructure Layer                  │
│  ApexBank.Infrastructure  (EF Core, MySQL, JWT,     │
│                            BCrypt, SignalR)          │
└─────────────────────────────────────────────────────┘
```

**Dependency Rule**: Each layer can only depend on layers beneath it. Domain knows nothing about Infrastructure.

### 1.2 High-Level Data Flow

```
User Browser (React)
     │ HTTPS / WebSocket
     ▼
ApexBank.Api (ASP.NET Core 8)
     │ Rate Limiting → Auth → CORS
     ▼
JWT Middleware (validates token)
     │
     ▼
Controller (validates input)
     │
     ▼
Application Service (business logic)
     │ via IApplicationDbContext
     ▼
ApplicationDbContext (EF Core)
     │ Pomelo MySQL Driver
     ▼
MySQL 8.0 Database (ApexBankDb)
```

### 1.3 Real-Time Communication (SignalR)

```
API Server (NotificationHub)
     │ WebSocket / Long Polling fallback
     ▼
React Client (@microsoft/signalr)
     │ JWT passed as query param ?access_token=
     ▼
NotificationToast component
```

---

## 2. Technology Stack

### 2.1 Backend
| Component | Technology | Version |
|---|---|---|
| Framework | ASP.NET Core | 8.0 |
| Language | C# | 12 |
| ORM | Entity Framework Core | 8.0 |
| MySQL Driver | Pomelo.EntityFrameworkCore.MySql | 8.0 |
| Authentication | JWT Bearer (Microsoft.AspNetCore.Authentication.JwtBearer) | 8.0 |
| Password Hashing | BCrypt.Net-Next | 4.0.3 |
| Real-time | ASP.NET Core SignalR | 8.0 |
| API Docs | Swashbuckle (Swagger) | 6.6.2 |
| Rate Limiting | Built-in (System.Threading.RateLimiting) | 8.0 |

### 2.2 Frontend
| Component | Technology | Version |
|---|---|---|
| Framework | React | 18.x |
| Build Tool | Vite | 5.x |
| Routing | React Router DOM | 6.x |
| Animation | Framer Motion | 11.x |
| Icons | Lucide React | Latest |
| HTTP Client | Axios | 1.x |
| Real-time | @microsoft/signalr | 8.x |
| Styling | Vanilla CSS (Custom Design System) | — |

### 2.3 Database
| Component | Details |
|---|---|
| Engine | MySQL 8.0 |
| Charset | utf8mb4 (full Unicode + emoji) |
| Collation | utf8mb4_unicode_ci |
| Port | 3306 |
| Schema | ApexBankDb |
| Migrations | EF Core Code-First Migrations |

### 2.4 Deployment
| Component | Platform |
|---|---|
| Backend | Render Web Service (Docker) |
| Frontend | Render Static Site |
| Database | Render MySQL / Aiven / PlanetScale |
| Container | Docker (render.Dockerfile) |

---

## 3. Database Schema (ERD)

### 3.1 Tables & Relationships

```
Users (1) ─────────────── (N) Accounts
   │                              │
   │                              │
   (1)                           (N)
   │                              │
   (N) Loans              Transactions (N)
   │                              │
   (N) Notifications     Cards (N)
   
AuditLogs (standalone audit trail)
```

### 3.2 Table Specifications

#### `Users`
| Column | Type | Constraints |
|---|---|---|
| Id | CHAR(36) | PK, NOT NULL |
| FirstName | VARCHAR(100) | NOT NULL |
| LastName | VARCHAR(100) | NOT NULL |
| Email | VARCHAR(256) | UNIQUE, NOT NULL |
| PasswordHash | TEXT | NOT NULL (BCrypt $2a$11$...) |
| PhoneNumber | VARCHAR(15) | |
| Role | VARCHAR(20) | DEFAULT 'Customer' |
| IsActive | TINYINT(1) | DEFAULT 1 |
| KycStatus | VARCHAR(20) | DEFAULT 'Pending' |
| FailedLoginAttempts | INT | DEFAULT 0 |
| IsLockedOut | TINYINT(1) | DEFAULT 0 |
| LockoutEndAt | DATETIME | NULLABLE |
| LastLoginAt | DATETIME | NULLABLE |
| CreatedAt | DATETIME | NOT NULL |

#### `Accounts`
| Column | Type | Constraints |
|---|---|---|
| Id | CHAR(36) | PK, NOT NULL |
| AccountNumber | VARCHAR(20) | UNIQUE, NOT NULL |
| Balance | DECIMAL(18,2) | NOT NULL, DEFAULT 0 |
| AccountType | VARCHAR(20) | DEFAULT 'Savings' |
| Currency | VARCHAR(5) | DEFAULT 'INR' |
| Status | VARCHAR(20) | DEFAULT 'Active' |
| IsFrozen | TINYINT(1) | DEFAULT 0 |
| CreditLimit | DECIMAL(18,2) | DEFAULT 0 |
| InterestRate | DECIMAL(5,2) | DEFAULT 3.5 |
| IfscCode | VARCHAR(20) | |
| BranchCode | VARCHAR(20) | |
| UserId | CHAR(36) | FK → Users.Id |
| CreatedAt | DATETIME | NOT NULL |

#### `Transactions`
| Column | Type | Constraints |
|---|---|---|
| Id | CHAR(36) | PK, NOT NULL |
| SourceAccountId | CHAR(36) | FK → Accounts.Id |
| DestinationAccountId | CHAR(36) | FK → Accounts.Id, NULLABLE |
| Amount | DECIMAL(18,2) | NOT NULL |
| Fee | DECIMAL(18,2) | DEFAULT 0 |
| BalanceAfter | DECIMAL(18,2) | |
| TransactionType | VARCHAR(20) | Transfer/Deposit/Withdrawal |
| Status | VARCHAR(20) | DEFAULT 'Completed' |
| ReferenceNumber | VARCHAR(30) | UNIQUE |
| Channel | VARCHAR(20) | DEFAULT 'Web' |
| IpAddress | VARCHAR(50) | |
| Currency | VARCHAR(5) | DEFAULT 'INR' |
| ExchangeRate | DECIMAL(10,4) | DEFAULT 1 |
| Description | TEXT | |
| CreatedAt | DATETIME | NOT NULL |

#### `Loans`
| Column | Type | Constraints |
|---|---|---|
| Id | CHAR(36) | PK |
| LoanNumber | VARCHAR(20) | UNIQUE |
| LoanType | VARCHAR(30) | Personal/Home/Auto/Education |
| Principal | DECIMAL(18,2) | |
| InterestRate | DECIMAL(5,2) | Annual % |
| TermMonths | INT | |
| MonthlyEmi | DECIMAL(18,2) | |
| TotalPayable | DECIMAL(18,2) | |
| OutstandingBalance | DECIMAL(18,2) | |
| Status | VARCHAR(20) | Pending/Active/Closed/Rejected |
| UserId | CHAR(36) | FK → Users.Id |
| ApprovedAt | DATETIME | NULLABLE |
| CreatedAt | DATETIME | |

#### `Cards`
| Column | Type | Constraints |
|---|---|---|
| Id | CHAR(36) | PK |
| AccountId | CHAR(36) | FK → Accounts.Id (CASCADE DELETE) |
| CardType | VARCHAR(20) | Debit/Credit/Prepaid |
| CardNetwork | VARCHAR(20) | Visa/Mastercard/RuPay |
| MaskedNumber | VARCHAR(25) | **** **** **** 4242 |
| CardHolderName | VARCHAR(100) | |
| ExpiryMonth | VARCHAR(2) | |
| ExpiryYear | VARCHAR(4) | |
| DailyLimit | DECIMAL(18,2) | DEFAULT 50000 |
| MonthlyLimit | DECIMAL(18,2) | DEFAULT 200000 |
| IsActive | TINYINT(1) | DEFAULT 1 |
| IsFrozen | TINYINT(1) | DEFAULT 0 |
| IsVirtual | TINYINT(1) | DEFAULT 0 |

#### `AuditLogs`
| Column | Type | Constraints |
|---|---|---|
| Id | CHAR(36) | PK |
| EntityName | VARCHAR(100) | User/Account/Transaction |
| EntityId | VARCHAR(36) | Indexed |
| Action | VARCHAR(50) | Created/Updated/Deleted/Login |
| OldValues | LONGTEXT | JSON NULLABLE |
| NewValues | LONGTEXT | JSON NULLABLE |
| UserId | VARCHAR(36) | |
| UserEmail | VARCHAR(256) | |
| IpAddress | VARCHAR(50) | |
| IsSuccess | TINYINT(1) | DEFAULT 1 |
| CreatedAt | DATETIME | Indexed |

#### `Notifications`
| Column | Type | Constraints |
|---|---|---|
| Id | CHAR(36) | PK |
| UserId | CHAR(36) | FK → Users.Id (CASCADE DELETE) |
| Title | VARCHAR(200) | |
| Message | TEXT | |
| Type | VARCHAR(20) | Info/Success/Warning/Alert |
| IsRead | TINYINT(1) | DEFAULT 0 |
| ReadAt | DATETIME | NULLABLE |
| ReferenceId | VARCHAR(36) | |
| ReferenceType | VARCHAR(50) | Transaction/Loan/Card |

---

## 4. API Contracts

### 4.1 Authentication Endpoints
```
POST   /api/auth/register      → { token, email, role, message }
POST   /api/auth/login         → { token, email, role, message }
```

### 4.2 Account Endpoints (JWT Required)
```
GET    /api/accounts/my        → AccountDto[]
GET    /api/accounts/{id}      → AccountDto
POST   /api/accounts/create    → AccountDto
POST   /api/accounts/{id}/freeze
POST   /api/accounts/{id}/unfreeze
```

### 4.3 Transaction Endpoints (JWT Required)
```
POST   /api/transactions/transfer
POST   /api/transactions/deposit
POST   /api/transactions/withdraw
GET    /api/transactions/account/{accountId}  → TransactionDto[]
```

### 4.4 Loan Endpoints (JWT Required)
```
POST   /api/loans/apply                   → LoanResponseDto
GET    /api/loans/my                      → LoanResponseDto[]
GET    /api/loans/{id}                    → LoanResponseDto
GET    /api/loans/calculate-emi           → EmiCalculationDto (public)
GET    /api/loans/all                     [Admin/Employee] → LoanResponseDto[]
POST   /api/loans/{id}/approve            [Admin/Employee]
POST   /api/loans/{id}/reject             [Admin/Employee]
```

### 4.5 Card Endpoints (JWT Required)
```
GET    /api/cards/account/{accountId}     → CardResponseDto[]
POST   /api/cards/issue                   → CardResponseDto
POST   /api/cards/{id}/freeze
POST   /api/cards/{id}/unfreeze
PUT    /api/cards/{id}/limits
POST   /api/cards/{id}/block
```

### 4.6 Dashboard Endpoints (JWT Required)
```
GET    /api/dashboard/customer            → CustomerDashboardDto
GET    /api/dashboard/admin               [Admin] → AdminDashboardDto
```

### 4.7 Notification Endpoints (JWT Required)
```
GET    /api/notifications                 → NotificationDto[]
GET    /api/notifications/unread-count    → { count }
POST   /api/notifications/{id}/read
POST   /api/notifications/read-all
```

### 4.8 Admin Endpoints (Admin JWT Required)
```
GET    /api/admin/users                   → paginated user list
POST   /api/admin/users/{id}/toggle-active
POST   /api/admin/users/{id}/approve-kyc
POST   /api/admin/users/{id}/unlock
GET    /api/admin/audit-logs              → paginated AuditLog[]
```

### 4.9 Health Check
```
GET    /health                            → { status: "Healthy" }
```

---

## 5. Security Architecture

### 5.1 JWT Token Structure
```json
{
  "sub": "<user-guid>",
  "email": "user@apexbank.in",
  "role": "Customer",
  "jti": "<unique-token-id>",
  "iss": "ApexBank",
  "aud": "ApexBankUsers",
  "exp": <unix-timestamp-7-days>
}
```

### 5.2 Password Hashing
- Algorithm: BCrypt (work factor 11)
- Example hash: `$2a$11$...` (60 characters)
- Never stored in plain text; verified via `BCrypt.Verify()`

### 5.3 Rate Limiting Policy
| Policy | Window | Limit | Applies To |
|---|---|---|---|
| ApiPolicy | 1 minute | 60 requests | All API endpoints |
| AuthPolicy | 5 minutes | 10 requests | `/api/auth/*` |

### 5.4 CORS Whitelist
```
http://localhost:5173      (Vite Dev)
http://localhost:3000      (Alt Dev)
https://apexbank-ui.onrender.com  (Production)
```

---

## 6. EMI Calculation Formula

**Standard Reducing Balance EMI Formula:**

```
EMI = P × r × (1+r)^n / ((1+r)^n - 1)

Where:
  P = Principal amount
  r = Monthly interest rate = (Annual Rate / 100) / 12
  n = Loan term in months
```

**Example:** ₹2,00,000 loan at 10.5% p.a. for 24 months
- r = 10.5/100/12 = 0.00875
- EMI = 200000 × 0.00875 × (1.00875)^24 / ((1.00875)^24 - 1)
- EMI = **₹9,261.40/month**
- Total Payable = ₹2,22,273.60
- Total Interest = ₹22,273.60

---

## 7. Deployment Architecture

```
                         ┌─────────────────────────────┐
                         │       Render Cloud           │
                         │                             │
User → HTTPS → CDN ──── │ ┌─────────────────────────┐ │
                         │ │   Static Site (React)   │ │
                         │ │   apexbank-ui.render.com│ │
                         │ └────────────┬────────────┘ │
                         │              │ HTTPS API     │
                         │ ┌────────────▼────────────┐ │
                         │ │  Web Service (Docker)   │ │
                         │ │  apexbank.render.com    │ │
                         │ │  Port 8080 (auto)       │ │
                         │ └────────────┬────────────┘ │
                         │              │               │
                         │ ┌────────────▼────────────┐ │
                         │ │     MySQL Database      │ │
                         │ │  (Aiven / PlanetScale)  │ │
                         │ │  Port 3306, TLS         │ │
                         │ └─────────────────────────┘ │
                         └─────────────────────────────┘
```

### 7.1 Docker Configuration
- Base image: `mcr.microsoft.com/dotnet/aspnet:8.0`
- Build stage: `mcr.microsoft.com/dotnet/sdk:8.0`
- Port: `8080` (Render default)
- Environment: `ASPNETCORE_ENVIRONMENT=Production`

### 7.2 Required Environment Variables (Render)
| Variable | Description |
|---|---|
| `ConnectionStrings__DefaultConnection` | Full MySQL connection string |
| `Jwt__Key` | JWT signing secret (min 32 chars) |
| `Jwt__Issuer` | `ApexBank` |
| `Jwt__Audience` | `ApexBankUsers` |
| `ASPNETCORE_URLS` | `http://+:8080` |

---

## 8. Performance SLAs

| Metric | SLA | Monitoring Method |
|---|---|---|
| API Response P95 | < 200ms | Application Insights / Seq |
| Dashboard Load | < 2s FCP | Lighthouse CI |
| DB Query | < 50ms avg | EF Core logging |
| SignalR Latency | < 100ms | Browser DevTools |
| Uptime | 99.9% | Render health check |
| Error Rate | < 0.1% | 5xx response monitoring |

---

## 9. Scalability Considerations

| Concern | Current Solution | Future Path |
|---|---|---|
| Horizontal Scaling | Stateless JWT (can run N instances) | Kubernetes / Render Autoscale |
| Database | Single MySQL (read/write) | Read Replica + Connection Pooling |
| Real-time (SignalR) | In-memory backplane | Redis backplane (Azure/Render Redis) |
| File Storage | Not implemented | AWS S3 / Azure Blob (for KYC docs) |
| Caching | Not implemented | Redis cache for dashboard KPIs |
| Queues | Not implemented | RabbitMQ / Azure Service Bus for notifications |

---

## 10. Local Development Quick-Start

```bash
# 1. Clone the repository
git clone https://github.com/AbhishekPattnaik124/APEXBANK.git
cd APEXBANK

# 2. Set your MySQL password in appsettings.json
#    Server=localhost;Port=3306;Database=ApexBankDb;User=root;Password=YOUR_PASSWORD

# 3. Create the database in MySQL Workbench
CREATE DATABASE IF NOT EXISTS ApexBankDb CHARACTER SET utf8mb4;

# 4. Apply migrations (auto-seeds admin + demo user)
dotnet ef database update --project src/ApexBank.Infrastructure --startup-project src/ApexBank.Api

# 5. Start the API
cd src/ApexBank.Api
dotnet run
# → API:     http://localhost:5000
# → Swagger: http://localhost:5000/swagger

# 6. Start the frontend
cd src/apex-bank-ui
npm install && npm run dev
# → UI: http://localhost:5173

# Demo Credentials:
# Admin:    admin@apexbank.in  /  Admin@12345
# Customer: demo@apexbank.in   /  Demo@12345
```

---

*ApexBank TRD v2.0 — Confidential. Built with ❤️ by Abhishek Pattnaik.*
