using Application.Interfaces.Command;
using Application.Interfaces.Query;
using Infrastructure.Commands;
using Infrastructure.Persistence;
using Infrastructure.Queries;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using OrderService.BackgroundServices;
using System.Reflection;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Order Service API",
        Description = "API for managing Orders and OrderDetails using Clean Architecture and CQRS.",
        Contact = new OpenApiContact
        {
            Name = "Ticketech Team",
            Email = "support@TicketechTeam.local"
        }
    });
});

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(options => {
    options.UseSqlServer(connectionString);
});

//Custom
builder.Services.AddScoped<IOrderQuery, OrderQuery>();
builder.Services.AddScoped<IOrderCommand, OrderCommand>();

builder.Services.AddScoped<IPaymentStatusQuery, PaymentStatusQuery>();

builder.Services.AddScoped<IPaymentTypeQuery, PaymentTypeQuery>();
builder.Services.AddScoped<IPaymentTypeCommand, PaymentTypeCommand>();



builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.Load("Application")));

builder.Services.AddHostedService<OrderExpiration>();
//End Custom

var app = builder.Build();

app.UseMiddleware<OrderService.Middlewares.ExceptionMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Order Service API v1");
        c.RoutePrefix = "swagger";
    });
}

//CORS
app.UseCors(policy => policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
