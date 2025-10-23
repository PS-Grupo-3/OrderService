using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using System.Reflection;
using Application.Interfaces.Command;
using Infrastructure.Commands;
using Infrastructure.Queries;
using Application.Interfaces.Query;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(options => {
    options.UseSqlServer(connectionString);
});

//Custom
builder.Services.AddScoped<IOrderQuery, OrderQuery>();
builder.Services.AddScoped<IOrderCommand, OrderCommand>();

builder.Services.AddScoped<IOrderStatusCommand, OrderStatusCommand>();
builder.Services.AddScoped<IOrderStatusQuery, OrderStatusQuery>();

builder.Services.AddScoped<IPaymentStatusQuery, PaymentStatusQuery>();

builder.Services.AddScoped<IPaymentTypeQuery, PaymentTypeQuery>();
builder.Services.AddScoped<IPaymentTypeCommand, PaymentTypeCommand>();




builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.Load("Application")));
//End Custom



var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
