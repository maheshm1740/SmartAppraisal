
📊 Smart Appraisal

Smart Appraisal is a 5-Tier ASP.NET Core MVC application designed to automate and manage employee performance appraisals. The system follows clean architecture principles with proper separation of concerns using Business, Data, UI, and Test layers.

🏗️ Architecture Overview (5-Tier)

UI Layer – ASP.NET Core MVC with Razor Views

Business Logic Layer (BLL) – Core business rules & validation

Data Access Layer (DAL) – Entity Framework Core & database operations

Domain / Model Layer – Entities & DTOs

Testing Layer – Unit testing for application logic

📂 Project Structure
SmartAppraisal/
│── BLSmartAppraisal/        # Business Logic Layer
│── DLSmartAppraisal/        # Data Access Layer (EF Core)
│── ULSmartAppraisal/        # UI Layer (ASP.NET Core MVC + Razor Views)
│── SmartAppraisal/          # Core / Domain Models
│── UnitTestSmartAppraisal/  # Unit Testing Layer
│── SmartAppraisal.sln
│── README.md

🚀 Key Features

ASP.NET Core MVC architecture

Razor Views for server-side UI rendering

Entity Framework Core with SQL Server

Role-based appraisal workflows

Employee evaluation & feedback management

Secure data handling and validation

Clean separation of concerns (5-tier)

Unit testing support

🛠️ Tech Stack

Framework: ASP.NET Core MVC

Language: C#

UI: Razor Views (.cshtml), HTML, CSS, Bootstrap

ORM: Entity Framework Core

Database: Microsoft SQL Server

IDE: Visual Studio

Version Control: Git & GitHub

⚙️ Setup & Installation
Prerequisites

Visual Studio 2022+

.NET SDK

SQL Server & SSMS

Git

Steps
git clone <repository-url>
cd SmartAppraisal


Open SmartAppraisal.sln in Visual Studio

Update connection string in appsettings.json

Run database migrations (if any)

Start the application using IIS Express

🧪 Testing

Unit tests implemented using UnitTestSmartAppraisal

Focused on business logic validation

🎯 Future Enhancements

Advanced appraisal analytics

Export reports (PDF/Excel)

Email notifications

Admin dashboard improvements

Cloud deployment (Azure)

👤 Author

Mahesh M
ASP.NET Core / Full Stack Developer
GitHub: https://github.com/maheshm1740

LinkedIn: https://linkedin.com/in/mahesh1740

📄 License

This project is developed for learning and portfolio purpose
