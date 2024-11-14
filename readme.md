# Employee Management System

This project implements an **Employee Management System** that allows users to create, update, and delete employee records. The system also includes functionality to calculate the number of working days between two dates, considering weekends and public holidays.

## Features
- **Employee Management:**
  - Add, update, and delete employee records.
  - Each employee record includes:
    - Id
    - Name
    - Email
    - Job Position
- **Working Day Calculator:**
  - Calculate the number of working days between two dates.
  - The starting date must be a weekday (Monday to Friday).
  - The system will exclude weekends (Saturday and Sunday) and public holidays when calculating working days.
- **Public Holidays:**
  - A table to store public holidays, which are excluded from the working days calculation.

## Architecture
This application uses the following technologies:

- **Backend:**
  - **C#** with **ASP.NET MVC 4**
  - **Entity Framework** (LINQ)
  - **ADO.NET Database First** approach
  - **Service & Repository Layers**:
    - The **Service Layer** handles the business logic.
    - The **Repository Layer** interacts with the database.
  - **Caching**:
    - **CachedLong**: Cache a result indefinitely until it is manually cleared.
    - **Cached**: Cache a result for up to 5 minutes.

- **Frontend:**
  - **Bootstrap** (for responsive design)
  - **Custom CSS** for styling
  - **jQuery** for frontend scripting and interactions

## Project Setup

1. Clone the repository:
   ```bash
   git clone https://github.com/yourusername/employee-management.git



Install the NuGet Packages:

dotnet add package Microsoft.EntityFrameworkCore
dotnet add package Microsoft.EntityFrameworkCore.SqlServer  # For SQL Server
dotnet add package Microsoft.EntityFrameworkCore.Tools    # For migrations and tools



To create a migration file in Entity Framework Core

dotnet add package Microsoft.EntityFrameworkCore.Tools
dotnet ef migrations add InitialCreate
dotnet ef database update



