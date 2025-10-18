# 🧩 CustomCADs — Backend

CustomCADs is the backend powering a full-featured platform for discovering, purchasing, and commissioning 3D CAD models.  
It’s built as a **Modular Monolith** following **Clean Architecture**, **DDD**, **CQRS**, and **EDA** principles — combining structure, flexibility, and scalability in one cohesive system.

---

## 🏗️ Architecture & Design

- **Runtime:** Latest .NET (currently 9)
- **Architecture:** Modular Monolith with per-module Clean Architecture and Shared Kernel  
  - Domains adhere to **DDD**
  - Application layers follow **CQRS**
  - Cross-module and async flows rely on **EDA**  
  - **Sagas** orchestrate complex processes (deliveries, payments, rewards, etc.)
- **Cloud:**  
  - **Cloudflare R2** → CADs & images storage  
  - **AWS** → EC2/EB + ECR (backend), RDS (PostgreSQL), ACM + CloudFront (HTTPS/CDN)  
  - **Namecheap** → domain management
- **DevOps:**  
  - **GitHub Actions** → CI/CD (build → push → manual deploy)
  - **Docker** → image packaging and deploys  
  - **Terraform** → full Infrastructure as Code (IaC)
- **Solutions:**  
  - `CustomCADs.sln` (Development)  
  - `CustomCADs.Staging.sln`  
  - `CustomCADs.Production.sln`  
  - Root also includes GitHub workflows, Terraform configs, Dockerfiles, and Bash automation scripts.

---

## 🧰 Core Libraries

| Layer | Technologies |
|-------|---------------|
| **API** | FastEndpoints · Microsoft.OpenAPI · Scalar |
| **Infrastructure** | Polly · Stripe · SignalR · WolverineFx · FluentValidation · FluentEmail · Quartz.NET |
| **Persistence** | PostgreSQL · EF Core · EFCore.BulkExtensions |

### 🧑‍💻 Developer Utilities
- Custom Bash scripts for EF Core CLI automation on Linux  
- Optional source generation for Wolverine message handlers  
- Dedicated EF Core migration project for all modules (instant & parallel) and a separate one for Identity

---

## 🔐 Authentication & Core Features

- **JWT + Refresh Token** auth system with **CSRF** protection  
- **Google SSO** via OpenID Connect  
- **ASP.NET Core Identity** as the underlying foundation  
- **Automatic request idempotency** via custom middleware & attributes  
- **Real-time updates & notifications** using SignalR  
- **Stripe integration** (webhooks + checkout + internal reconciliation)
- **Exchange rates** auto-updated every 24h (cached + cron job)


---

## 🧭 Roles & Capabilities

### 👤 Guests
- Register/Login  
- Explore the Gallery  
- Access current Exchange Rates  

### 💼 Customers
- Manage current Cart & Items (add/remove/customize)
- Purchase Cart (persisted as one of many “Purchased Cart”)
- Request **Custom 3D Models (Customs)**
- Track **Shipments** (for purchased/delivered items or Customs)
- View Notifications  

### 🧱 Contributors
- Upload & manage Products (CRUD)  
- Submit Products for Designer validation  
- Sell full rights to Products to Designers  
- Apply for **Designer** position  

### 🎨 Designers
- Upload Products directly to Gallery (tagged **Professional**)  
- Validate or report Contributors’ Products  
- Manage Custom Requests (**accept → begin → finish**)  
- Purchase rights to Contributors’ Products  

### 🛡️ Admins
- Read all resources (including Users/Roles)  
- Modify most resources  
- Manage **Tags** & **Categories**  
- Handle reports and remove flagged content  

---

## 📂 Directory Structure

<table>
<tr>
<td>

<h4>Root layout:</h4>
<pre><code>CustomCADs/
├── src/
│   ├── Modules/
│   ├── Shared/
│   ├── Tools/
│   └── Presentation/
├── tests/
│   └── UnitTests/
├── .github/
├── .terraform/
├── .platform/
├── Dockerfile
├── Dockerfile.staging
├── Dockerfile.production
└── README.md
</code></pre>

</td>
<td>

<h4>Src layout:</h4>
<pre><code>src/
├── Modules/
├── Shared/
│   ├── Domain/
│   ├── Persistence/
│   ├── Application/
│   ├── Infrastructure/
│   └── API/
├── Tools/
│   ├── Migrations/
│   ├── Identity/
│   └── CodeGen/
├── Presentation/
│   ├── GlobalExceptionHandler.cs
│   ├── ProgramExtensions.cs
│   └── Program.cs
</code></pre>

</td>
<td>

<h4>Modules layout:</h4>
<pre><code>Modules/
├── Accounts/
│   ├── Domain/
│   ├── Persistence/
│   ├── Application/
│   ├── Infrastructure/
│   └── API/
├── Carts/
├── Catalog/
├── Customs/
├── Delivery/
├── Files/
├── Idempotency/
├── Identity/
├── Notifications/
└── Printing/
</code></pre>

</td>
<td>

<h4>Tests layout:</h4>
<pre><code>UnitTests/
├── Accounts/
│   ├── Data/
│   ├── Domain/
│   ├── Application/
├── Carts/
├── Catalog/
├── Customs/
├── Delivery/
├── Files/
├── Idempotency/
├── Identity/
├── Notifications/
└── Printing/
</code></pre>

</td>
</tr>
</table>


## 📞 **Contact**

- Author: Ivan (NinjataWRLD)
- Role: Co-Founder, Architect & Full-Stack/DevOps Engineer of CustomCADs
- Email: [Personal](mailto:ivanangelov414@gmail.com) or [Support](mailto:customcads2023@gmail.com)
- Phone: [+359 885 440 400]
- GitHub: [github.com/NinjataWRLD](https://github.com/NinjataWRLD)