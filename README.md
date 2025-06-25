# 🧼 dotnet-clean-arch

A simple .NET 9 Web API project following the Clean Architecture pattern.

## 💪 Tech Stack

* ASP.NET Core Web API
* Entity Framework Core (Code First)
* PostgreSQL
* MediatR
* Clean Architecture

## 📂 Folder Structure

```text
clean/
│
├── clean.api/             # Startup project - entry point
│   └── Controllers/       # Define your API endpoints (Controllers)
│
├── clean.application/     # Application layer (CQRS + business logic)
│   ├── Features/
│   │   └── [Entity]/
│   │       ├── Commands/
│   │       ├── Queries/
│   │       └── Handlers/
│   └── Contracts/         # Interfaces (e.g. IRepository)
│
├── clean.domain/          # Domain layer (Entities)
│   └── Entities/
│
├── clean.persistence/     # Infrastructure layer (EF + DB config)
│   ├── AppDbContext.cs
│   ├── Configurations/
│   └── Repositories/
```

## ▶️ How to Run

1. **Run the API**:

   ```bash
   cd clean.api
   dotnet run
   ```

2. **Access Swagger UI**:
   Open your browser and go to:
   [http://localhost:5111/swagger/index.html](http://localhost:5111/swagger/index.html)

3. **Add a new Controller**:

   * Create a new file in `clean.api/Controllers/`.

4. **Add a new Feature**:

   * Define `Command`, `Query`, and `Handler` inside `clean.application/Features/{EntityName}/`.

5. **Define Contract Interface**:

   * Create `I{Entity}Repository` inside `clean.application/Contracts/`.

6. **Implement Repository**:

   * Implement it in `clean.persistence/Repositories/`.

7. **Define Entity**:

   * Define model in `clean.domain/Entities/`.

8. **Configure Entity Mapping**:

   * Configure DB mapping in `clean.persistence/Configurations/`.

## 🗃️ EF Core Migrations

To create migrations:

```bash
dotnet ef migrations add AddUserAndRoleTables \
  --project ./clean.persistence \
  --startup-project ./clean.api
```

To apply migrations to DB:

```bash
dotnet ef database update \
  --project ./clean.persistence \
  --startup-project ./clean.api
```

## 📌 Notes

* This project uses **EF Core Code First**.
* Make sure to install the `Microsoft.EntityFrameworkCore.Design` package in the `clean.persistence` project:

  ```bash
  dotnet add clean.persistence package Microsoft.EntityFrameworkCore.Design
  ```

## 📜 License

MIT
