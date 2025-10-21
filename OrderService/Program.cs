using Infrastructure.Persistence;
using Application.Interfaces.Payment;
using Infrastructure.Queries.Payment;
using Infrastructure.Commands.Payment;
using Application.Interfaces.Order;
using Infrastructure.Commands.Order;
using Infrastructure.Queries.Order;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using System.Reflection;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(options => {
    options.UseSqlServer(connectionString);
});

//Custom

builder.Services.AddScoped<IPaymentQuery, PaymentQuery>();
builder.Services.AddScoped<IPaymentCommand, PaymentCommand>();

builder.Services.AddScoped<IOrderQuery, OrderQuery>();
builder.Services.AddScoped<IOrderCommand, OrderCommand>();


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
