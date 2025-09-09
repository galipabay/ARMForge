# ARMForge ERP

![ARMForge ERP Banner](ARMForge(nonlongv).png)

> **Note:** This project is under active development. Features and documentation will be updated as the system evolves.

## Overview

**ARMForge ERP** is a modular, scalable, and modern ERP solution built with ASP.NET Core (.NET 8) and Razor Pages. It aims to streamline business processes such as inventory, sales, purchasing, shipment, HR, and more. The architecture is layered for maintainability and extensibility.

---

## Features

- **User & Role Management:** Secure authentication, authorization, and granular role-based access.
- **Product & Inventory:** CRUD operations, stock tracking, and category management.
- **Order Management:** Customer orders, order items, status tracking.
- **Supplier & Purchase Orders:** Supplier database, purchase order lifecycle.
- **Shipment & Logistics:** Shipment creation, driver/vehicle assignment, tracking.
- **Customer Management:** Company/contact details, order history.
- **HR Module:** Employee records, department, salary, and reporting structure.
- **Invoice Management:** Automated invoice generation and payment tracking.
- **Extensible Architecture:** Easily add new modules (e.g., CRM, Finance).

---

## Technologies

- **Backend:** ASP.NET Core (.NET 8), Razor Pages, Entity Framework Core (PostgreSQL)
- **Frontend:** Razor Pages (API-first, ready for SPA/Blazor integration)
- **Authentication:** ASP.NET Identity, JWT (planned)
- **Testing:** xUnit, Moq (planned)
- **CI/CD:** GitHub Actions (planned)
- **Documentation:** XML comments, Swagger/OpenAPI (planned)
- **API Client Testing Relations:** Bruno Software
---

## Getting Started

### Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [PostgreSQL](https://www.postgresql.org/download/)
- Visual Studio 2022