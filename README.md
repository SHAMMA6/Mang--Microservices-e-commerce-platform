# Microservices E-Commerce Platform

## 🚀 Overview
This is a fully functional **Microservices-based E-Commerce Platform** built with **.NET**. The project follows best practices for **scalability, security, and maintainability**.

<img width="989" alt="Microservices Application Flow" src="https://github.com/user-attachments/assets/bcb51922-62e5-4fab-917d-319ff59843f1" />

## 🔥 Features
- **RESTful APIs** for seamless communication
- **MVC Web Application** for a user-friendly experience
- **.NET Identity** for **Authentication & Authorization** with **JWT & Cookies**
- **API Gateway with Ocelot** to manage microservices
- **Secure Payments** with **Stripe Integration**
- **Email Service** for order confirmations & user notifications
- **Message Queue Integration** using **Azure Service Bus** & **RabbitMQ**
- **Event-Driven Architecture** for efficient messaging
- **10 Well-Structured Projects** ensuring **Separation of Concerns**

## ⚠️ Important Notes
1. **Two Branches Available:**
   - `main` branch uses **Azure Service Bus**
   - `RabbitMQ Implementation` branch uses **RabbitMQ**
    
2. **Do Not Update Packages**: Avoid updating packages like `RabbitMQ`, `Identity`, etc., as the application may not work properly.
3. **Ocelot API Gateway Activation**:
   - Change every project's port to `:7777` to activate the API Gateway.
     
4. **Ocelot Configuration (`ocelot.json`) in the Gateway Project**:
   ```json
   "CouponAPI": "https://localhost:7001",
   "AuthAPI": "https://localhost:7002",
   "ProductAPI": "https://localhost:7003",
   "ShoppingCartAPI": "https://localhost:7004",
   "OrderAPI": "https://localhost:7005"
   ```
   
5. **Using the Azure Service Bus Branch?**
   - You must **fill in the secret keys** from your Azure account.
   - **Keys & emails are empty by default**, so make sure to set them up correctly.
     
6. **Database Setup:**
   - Each API has its own **EF Core Migrations**.
   - Run `dotnet ef database update` in each service to apply migrations.
   - Connection strings should be configured in **appsettings.json** for each API.

## 📂 Project Structure
```
📦 ECommercePlatform
├── 📂 FrontEnd
│   ├── 📂 WebApp (MVC UI)
│
├── 📂 Gateway
│   ├── 📂 GatewayAPI (Ocelot)
│
├── 📂 Integration
│   ├── 📂 MessageQueue (Azure Service Bus / RabbitMQ)
│
├── 📂 Services
│   ├── 📂 AuthAPI
│   ├── 📂 EmailAPI
│   ├── 📂 OrderAPI
│   ├── 📂 ProductAPI
│   ├── 📂 RewardAPI
│   ├── 📂 ShoppingCartAPI
│   ├── 📂 CouponAPI
```

## 🚀 Deployment
- Can be deployed on **Azure**, **Docker**, or **Kubernetes**.
- Update **appsettings.json** for production configurations.
- Use **Docker Compose** to run all microservices together.

## 📧 Contact & Contributions
Feel free to contribute or reach out for any questions! 🚀

---
