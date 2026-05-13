# 🏦 ApexBank Management System

**ApexBank** is a high-fidelity, full-stack banking management platform designed with a premium "Black & Gold" luxury aesthetic. Built using **.NET 8 Clean Architecture** and **React (Vite)**, it offers a secure, scalable, and visually stunning experience for modern fintech needs.

## 🚀 Live Demo
- **Frontend**: [https://apexbank-ui.onrender.com](https://apexbank-ui.onrender.com)
- **Backend API**: [https://apexbank.onrender.com](https://apexbank.onrender.com)

---

## ✨ Key Features
- **Luxury UI/UX**: Sophisticated dark theme with gold accents, glassmorphism, and fluid animations.
- **Full-Stack Security**: JWT-based authentication, role-based access control (Admin, Employee, Customer), and AES-256 data encryption.
- **Clean Architecture**: Decoupled layers (Domain, Application, Infrastructure, API) for maximum maintainability.
- **Real-time Notifications**: Integrated **SignalR** for instant transaction and system alerts.
- **Industry Pages**: Production-grade content for Personal Banking, Business Solutions, and Security Assurance.
- **Responsive Design**: Optimized for everything from mobile phones to high-resolution desktop displays.

---

## 🛠️ Technology Stack
- **Backend**: .NET 8 (C#), Entity Framework Core, SignalR, JWT.
- **Frontend**: React 18, Vite, Framer Motion, Lucide Icons, Vanilla CSS.
- **Database**: MySQL (configured for Pomelo).
- **Deployment**: Render (Docker for Backend, Static Site for Frontend).

---

## 📂 Project Structure
```bash
├── src/
│   ├── ApexBank.Api/            # Main API Layer (Controllers, Hubs)
│   ├── ApexBank.Application/    # Business Logic & Service Interfaces
│   ├── ApexBank.Domain/         # Core Entities & Domain Logic
│   ├── ApexBank.Infrastructure/ # Persistence & Third-party Integrations
│   └── apex-bank-ui/            # React Frontend Application
├── Dockerfile                   # Containerization for Backend
└── render.yaml                  # Deployment Blueprint
```

---

## 🏁 Getting Started

### Prerequisites
- .NET 8 SDK
- Node.js (v18+)
- MySQL Server

### 1. Backend Setup
```bash
cd src/ApexBank.Api
# Update ConnectionString in appsettings.json
dotnet ef database update
dotnet run
```

### 2. Frontend Setup
```bash
cd src/apex-bank-ui
npm install
npm run dev
```

---

## ☁️ Deployment Steps

### Backend (Render Web Service)
1.  **Connect Repo**: Select your GitHub repository.
2.  **Dockerfile Path**: Set to `render.Dockerfile`.
3.  **Root Directory**: Keep empty.
4.  **Environment Variables**: Add `ConnectionStrings__DefaultConnection` and `Jwt__Key`.

### Frontend (Render Static Site)
1.  **Connect Repo**: Select your GitHub repository.
2.  **Root Directory**: Set to `src/apex-bank-ui`.
3.  **Build Command**: `npm install && npm run build`.
4.  **Publish Directory**: `dist`.
5.  **Redirects**: Add a rewrite rule for `/*` to `/index.html`.

---

## 📄 License
This project is for portfolio purposes and follows standard open-source guidelines.

**Built with ❤️ by [AbhishekPattnaik124](https://github.com/AbhishekPattnaik124)**
