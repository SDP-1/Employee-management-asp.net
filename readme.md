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


## Sample Screenshots

![Screenshot 2024-11-15 003625](https://github.com/user-attachments/assets/c2853d17-abbc-4e05-8faf-1d5093d54f35)
![Screenshot 2024-11-15 003655](https://github.com/user-attachments/assets/bc96f429-b925-4c5f-a42a-76d412cd8e18)
![Screenshot 2024-11-15 003712](https://github.com/user-attachments/assets/5992e288-eadb-4812-9122-15ac42668215)
![Screenshot 2024-11-15 003734](https://github.com/user-attachments/assets/508e0938-7a2f-4197-99ef-bfd6bf413917)
![Screenshot 2024-11-15 003800](https://github.com/user-attachments/assets/49ed6f47-eaab-450f-91f3-b7746543b97a)
![Screenshot 2024-11-15 003826](https://github.com/user-attachments/assets/b79bf7e5-4558-4fed-8b24-effbac0c42e3)
![Screenshot 2024-11-15 003900](https://github.com/user-attachments/assets/eda27946-8acd-4d5e-bac1-5439f3442fdd)






