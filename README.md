## Video
https://github.com/user-attachments/assets/df6bfa3f-c538-4301-bb8a-04e32e1577fe

<p align="center">
  <img src="https://img.shields.io/badge/.NET-10.0-512BD4?style=for-the-badge&logo=dotnet&logoColor=white" />
  <img src="https://img.shields.io/badge/EF%20Core-10.0.7-512BD4?style=for-the-badge" />
  <img src="https://img.shields.io/badge/AutoMapper-16.1.1-orange?style=for-the-badge" />
  <img src="https://img.shields.io/badge/Bootstrap-5.3.3-7952B3?style=for-the-badge&logo=bootstrap&logoColor=white" />
  <img src="https://img.shields.io/badge/Repository%20Pattern-✓-success?style=for-the-badge" />
</p>

A complete **ASP.NET Core MVC** web application for managing car wash bookings with intelligent time conflict detection, service management, and a fully responsive Arabic RTL interface.

---
## 💡 Business Problem

Service-based businesses often face booking conflicts and scheduling issues, leading to customer dissatisfaction.

## 🚀 Solution

This system prevents overlapping bookings and manages time slots efficiently using ASP.NET MVC.

## 🎯 Value

- Eliminates double bookings
- Improves customer experience
- Saves manual scheduling effort
---

## ✨ Features

| Feature | Description |
|---------|-------------|
| 📅 **Booking Management** | Create bookings with automatic time conflict validation |
| 🔧 **Service Management** | CRUD operations for services with pricing & duration |
| 📱 **Responsive Design** | Fully responsive across all devices |
| 🌍 **RTL Support** | Complete Arabic language support |
| 🗄️ **Clean Architecture** | Repository Pattern + Unit of Work + Service Layer |

---

## 🏗️ Architecture
```plaintext
CarWashBooking/
|-- ServiceBooking.sln
|-- ServiceBooking.Application
  |-- ServiceBooking.Application.csproj
  |-- Services
    |-- BookingValidator.cs
  |-- ViewModels
    |-- ServiceRequest.cs
    |-- ServiceResponse.cs
    |-- Booking
      |-- BookingRequest.cs
      |-- BookingResponse.cs
|-- ServiceBooking.Infrastructure
  |-- ServiceBooking.Infrastructure.csproj
  |-- DataAccess
    |-- Repository.cs
    |-- UnitOfWork.cs
  |-- Mapping
    |-- MappingProfile.cs
  |-- Migrations
    |-- 20260422084300_InitialMig.Designer.cs
    |-- 20260422084300_InitialMig.cs
    |-- DatabaseContextModelSnapshot.cs
  |-- Data
    |-- DatabaseContext.cs
|-- ServiceBooking.Core
  |-- ServiceBooking.Core.csproj
  |-- Entities
    |-- Booking.cs
    |-- Service.cs
  |-- Interfaces
    |-- IBookingValidator.cs
    |-- IRepository.cs
    |-- IUnitOfWork.cs
|-- ServiceBooking.MVC
  |-- Program.cs
  |-- ServiceBooking.MVC.csproj
  |-- appsettings.Development.json
  |-- appsettings.json
  |-- Extensions
    |-- CurrencyHelper.cs
    |-- ServicesConfiguration.cs
  |-- Controllers
    |-- BookingsController.cs
    |-- ServicesController.cs
  |-- Properties
    |-- launchSettings.json
  |-- Views
    |-- _ViewImports.cshtml
    |-- _ViewStart.cshtml
    |-- Bookings
      |-- Create.cshtml
      |-- Delete.cshtml
      |-- Details.cshtml
      |-- Edit.cshtml
      |-- Index.cshtml
    |-- Services
      |-- Create.cshtml
      |-- Delete.cshtml
      |-- Details.cshtml
      |-- Edit.cshtml
      |-- Index.cshtml
    |-- Shared
      |-- _Alerts.cshtml
      |-- _Layout.cshtml
      |-- _Layout.cshtml.css
      |-- _ValidationScriptsPartial.cshtml
  |-- wwwroot
    |-- css/site.css
    |-- js/site.js
```
---

## 🛠️ Tech Stack

### Backend
| Technology | Purpose |
|------------|---------|
| **.NET 10** | Core Framework |
| **Entity Framework Core** | ORM & Database Management |
| **AutoMapper** | Object-to-Object Mapping |
| **Repository Pattern** | Abstracted Data Access |
| **Unit of Work** | Transaction Management |

### Frontend
| Technology | Purpose |
|------------|---------|
| **Razor Views** | Server-Side Rendering |
| **Bootstrap 5 RTL** | Responsive Arabic UI |
| **Bootstrap Icons** | Consistent Iconography |
| **Custom CSS/JS** | Enhanced UX & Interactions |

---

## 🚀 Getting Started

### Prerequisites
- [.NET 10 SDK](https://dotnet.microsoft.com/download)
- SQL Server / SQL Server Express / LocalDB / PostgreSQL

### Installation

```bash
# 1. Clone the repository
git clone https://github.com/MARYAM-memo/CarWashBookingSystem.git
cd CarWashBookingSystem

# 2. Restore dependencies
dotnet restore

# 3. Apply database migrations
dotnet ef database update

# 4. Run the application
dotnet run

```

| Page                | Preview                               |
| ------------------- | ------------------------------------- |
| **Services List**   | <img width="1425" height="594" alt="Services" src="https://github.com/user-attachments/assets/320bb9e2-4271-4fce-9240-84ad9bf8487f" />|
| **Service Details** | <img width="1425" height="860" alt="Services_Details" src="https://github.com/user-attachments/assets/458ef687-4ae6-413d-b56b-4034f636c3b2" />|
| **Create Service**  | <img width="1425" height="781" alt="Services_Create" src="https://github.com/user-attachments/assets/3ab04c70-2ac2-40b7-a84a-817040a8d3db" />|
| **Delete Service**  | <img width="1425" height="676" alt="Services_Delete" src="https://github.com/user-attachments/assets/4cdfd5c0-50fa-4815-9b8f-b572e1884605" />|
| **Bookings List**   | <img width="1425" height="621" alt="Bookings" src="https://github.com/user-attachments/assets/c91dc85c-60bc-4992-a344-bba87021eba8" />|
| **Booking Details** | <img width="1425" height="996" alt="Bookings_Details" src="https://github.com/user-attachments/assets/2b6b2ed9-167a-41ca-870e-8b585172c55e" />|
| **Create Booking**  | <img width="1425" height="857" alt="Bookings_Create" src="https://github.com/user-attachments/assets/d5c64c1f-674d-490a-a226-50b5364f1d7f" />|
| **Delete Booking**  | <img width="1425" height="734" alt="Bookings_Delete" src="https://github.com/user-attachments/assets/7ba2d7da-ba41-4b40-aa7c-2819d0a9832e" />|

---

## 📩 Need a similar system?

I can build a custom version tailored to your business needs.
Feel free to reach out via LinkedIn or email.

### 👩‍💻 Developer
## [Marim Mohamed] — .NET Backend Developer
+ https://linkedin.com/in/marim-m-03055a196
+ https://github.com/MARYAM-memo
+ mailto:marimeltaweel26@gmail.com


<p align="center">
  ⭐ If you found this project helpful, don't forget to give it a star!
</p>
