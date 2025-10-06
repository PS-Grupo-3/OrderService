using Application.Interface;
using Infraestructure.Command;
using Infraestructure.Queries;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}




//Custom 
//Inyecciones de dependencia.
builder.Services.AddScoped<IPaymentCommand,PaymentCommand>(); //Cuando se pide un IPaymentCommand se crea una clase de paymentcommand
builder.Services.AddScoped<IPaymentQuery,PaymentQueries>();
//Fin de inyecciones de dependencia

//End Custom

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
