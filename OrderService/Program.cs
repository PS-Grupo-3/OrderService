using Application.Interfaces.Adapter;
using Application.Interfaces.Command;
using Application.Interfaces.ITicket;
using Application.Interfaces.ITicketSeat;
using Application.Interfaces.ITicketSector;
using Application.Interfaces.ITicketStatus;
using Application.Interfaces.Query;
using Infrastructure.Adapter;
using Infrastructure.Commands;
using Infrastructure.Persistence;
using Infrastructure.Queries;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using OrderService.BackgroundServices;
using OrderService.Middlewares;
using System.Reflection;
using System.Security.Claims;
using System.Text;


var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    var jwt = builder.Configuration.GetSection("Jwt");
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidIssuer = jwt["Issuer"],
        ValidateAudience = false,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt["Key"])),
        RoleClaimType = "userRole",
    };
}); 



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

builder.Services.AddScoped<IOrderStatusQuery, OrderStatusQuery>();

builder.Services.AddScoped<IPaymentTypeQuery, PaymentTypeQuery>();



builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.Load("Application")));

builder.Services.AddHostedService<OrderExpiration>();

builder.Services.AddSingleton<IAuthorizationMiddlewareResultHandler, AuthExceptionMiddleware>();

builder.Services.AddScoped<ITicketCommand, TicketCommand>();
builder.Services.AddScoped<ITicketSeatCommand, TicketSeatCommand>();
builder.Services.AddScoped<ITicketSectorCommand, TicketSectorCommand>();
builder.Services.AddScoped<ITicketQuery, TicketQuery>();
builder.Services.AddScoped<ITicketSeatQuery, TicketSeatQuery>();
builder.Services.AddScoped<ITicketSectorQuery, TicketSectorQuery>();
builder.Services.AddScoped<ITicketStatusQuery, TicketStatusQuery>();


builder.Services.AddHttpClient<IEventServiceClient, EventServiceClient>(client =>
{
    client.BaseAddress = new Uri(configuration["Services:EventServiceBaseUrl"] ?? throw new ArgumentNullException("La Url de EventService no esta configurada"));
});

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

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
