using System;
using application.Services;
using Microsoft.EntityFrameworkCore;
using persistence;
using persistence.Repositories;
using infrastructure;
using System.Reflection;
using MediatR;
using application.Contract.Repo;
using Microsoft.Extensions.DependencyInjection;
using galaxy_pay.infrastructure.Hubs;
using Hangfire;
using Hangfire.MemoryStorage;
using galaxy_pay.infrastructure.Jobs;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Cấu hình Serilog
Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .WriteTo.Seq("http://seq:80")
    .CreateLogger();

builder.Host.UseSerilog();

builder.WebHost.UseUrls("http://0.0.0.0:5111");

// Add services
builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHangfire(config =>
{
    config.UseMemoryStorage();
});

builder.Services.AddHangfireServer();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy
            .WithOrigins("http://127.0.0.1:5500", "http://localhost:5500") // cho chắc ăn
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials();
    });
});

builder.Services.AddSignalR();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<ProductService>();

builder.Services.AddControllers();
builder.Services.AddInfrastructure();
builder.Services.AddPersistence(builder.Configuration);

builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(typeof(ProductService).Assembly);
});

var app = builder.Build();

// Middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.MapOpenApi();
}

// app.UseHttpsRedirection(); // Uncomment nếu cần HTTPS

app.UseRouting(); // cần thiết trước MapHub
app.UseCors("AllowFrontend");
app.UseAuthorization();
app.UseHangfireDashboard();
JobScheduler.ScheduleJobs(builder.Configuration);

app.MapControllers();
app.MapHub<NotificationHub>("/hubs/notifications");

app.Run();
