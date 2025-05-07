using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using OrderProcessing.Infrastructure.Persistence;
using Serilog;
using OrderProcessing.Infrastructure.Messaging;
using OrderProcessing.Presentation.OrderEventService.Handlers;
using Rebus.Config;
using Microsoft.Extensions.DependencyInjection;

var host = Host.CreateDefaultBuilder(args)
    .ConfigureAppConfiguration((hostingContext, config) =>
    {
        config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
    })
    .UseSerilog((hostingContext, services, loggerConfiguration) =>
    {
        loggerConfiguration
            .ReadFrom.Configuration(hostingContext.Configuration);
    })
    .ConfigureServices((context, services) =>
    {
        // Register EF Core context for OrderEvents.
        services.AddDbContext<OrderEventDbContext>(options =>
            options.UseSqlServer(context.Configuration.GetConnectionString("DefaultConnection")));

        // Configure Rebus with your chosen transport.
        services.AddRebusWithRabbitMq(context.Configuration);

        // Auto-register message handlers.
        services.AutoRegisterHandlersFromAssemblyOf<OrderCreatedEventHandler>();
    })
    .Build();

// using (var scope = host.Services.CreateScope())
// {
//     var dbContext = scope.ServiceProvider.GetRequiredService<OrderEventDbContext>();
//     await dbContext.Database.MigrateAsync();
// }

Log.Information("[OrderEventService] Listening for OrderCreatedEvent ...");
await host.RunAsync();
