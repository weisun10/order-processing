## EF Core
- dotnet tool install --global dotnet-ef
- add references to "Microsoft.EntityFrameworkCore.Design", "Microsoft.EntityFrameworkCore.Relational", "Microsoft.EntityFrameworkCore.SqlServer"
- dotnet ef migrations add InitialCreate --context OrderDbContext -p Infrastructure -s Presentation/WebAPIs
- dotnet ef migrations add InitialCreate --context OrderEventDbContext -p Infrastructure -s Presentation/OrderEventService
- dotnet ef migrations list --context OrderDbContext -p Infrastructure -s Presentation/WebAPIs
- dotnet ef migrations remove --context OrderDbContext -p Infrastructure -s Presentation/WebAPIs
- dotnet ef migrations script --idempotent --context OrderDbContext -p Infrastructure -s Presentation/WebAPIs
- dotnet ef database update --context OrderDbContext -p Infrastructure -s Presentation/WebAPIs
- dotnet ef database update --context OrderEventDbContext -p Infrastructure -s Presentation/OrderEventService
