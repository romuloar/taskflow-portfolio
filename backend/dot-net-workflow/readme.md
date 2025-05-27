
# Workflow Solution Documentation

### Overview
This solution is a modular workflow/task management system built on .NET 9, 
using ASP.NET Core for the API, Entity Framework Core (SQLite) for persistence, 
and a layered architecture (Domain, Application, Infra, Presentation).

---

### 1. Notification Rules
- Trigger: Notifications are sent when a task is deleted.
- Validation: The notification domain object must be valid (required fields filled). If invalid or null, the process returns an error ("Invalid notification domain").
- Provider: The notification is handled by a provider implementing INotifyTaskDeletedProvider, which is called asynchronously.
- Result: On success, the provider returns a success result; otherwise, an error is returned.
Example Flow:
- Application receives a request to delete a task.
- The notification use case (NotifyTaskDeletedApplication) is executed.
- If the domain is valid, the provider's NotifyAsync method is called.
- The result is returned to the caller.
---
### 2. Application Flow
- API Layer: Receives HTTP requests (e.g., GET /api/task/{id}).
-	Application Layer: Handles business logic (e.g., GetTaskByIdApplication, NotifyTaskDeletedApplication).
- Domain Layer: Contains core entities and business rules.
- Infra Layer: Handles data persistence and external integrations (e.g., notifications, database).
- Database: Uses Entity Framework Core with SQLite (workflow.db).
Typical Request Flow:
- Client calls API endpoint.
- Controller invokes the corresponding Application service.
- Application service validates and processes the request.
- Infra layer interacts with the database or external services.
- Result is returned to the client.
---
### 3. Local Database (dblocal)
- Technology: SQLite via Entity Framework Core.
- Context: WorkflowDbContext manages entities.
- Entities: Example: TaskDomain (Id, Description, Status).
- Schema: Tasks are stored in the tsk.Task table.
- Migrations: On application startup, pending migrations are applied automatically.
- Testing: In-memory database is used for tests to isolate data.

**Sample Entity:**

```csharp
public class TaskDomain
{
    public Guid Id { get; set; }
    public string Description { get; set; }
    public EnumTaskStatus Status { get; set; }
}
```

**Connection String Example:**
```csharp
optionsBuilder.UseSqlite("Data Source=workflow.db");
```

### 4. Additional Information
- .NET Version: All projects target .NET 9.
- API Documentation: Swagger/OpenAPI is enabled in development.
- Dependency Injection: All services are registered via DI.
- Testing: xUnit is used for unit tests, with in-memory EF Core for isolation.
- Project Structure:
- Workflow.Presentation.Api: API project.
- Workflow.Application: Application/business logic.
- Workflow.Domain: Domain entities and interfaces.
- Workflow.Infra.Adapter.Data.EntityFrameworkCore: Data access.
- Workflow.Infra.Adapter.Hosting: Service registration and hosting.
- Workflow.Infra.Adapter.Notification: Notification providers.
---
### 5. How to Run
- Ensure you have the .NET 9 SDK installed.
- Run database migrations (handled automatically on API startup).
- Start the API project (Workflow.Presentation.Api).
- Access Swagger UI at /swagger for API exploration.
---
### 6. Dependencies
- ASP.NET Core 9
- Entity Framework Core 9 (SQLite)
- Swashbuckle (Swagger)
- xUnit (Testing)
- Moq (Testing)

## License

MIT

## Author

Romulo Ribeiro
