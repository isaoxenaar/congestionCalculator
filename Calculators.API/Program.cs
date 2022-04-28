using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.InMemory;
using Calculators.API;
using Calculators.API.Controllers;
using Calculators.API.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
options.UseInMemoryDatabase("InMemoryDb")
);
builder.Services.AddControllers();
builder.Services.AddScoped<ICongestionRepository, CongestionRepository>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

Main();

static void Main()
{
    Console.WriteLine("What vehicle Type are you?");
    Console.WriteLine("Car, Truck, Limousine, Motorcycle, Bus, Emergency, Diplomat, Foreign or Military ?");
    var vehicleType = Console.ReadLine().ToLower();

    DateTime[] testDates = new DateTime[]{
        new DateTime(2013,1,14,21,00,00),
        new DateTime(2013,1,15,21,00,00),
        new DateTime(2013,02,07,06,23,27),
        new DateTime(2013,02,07,15,27,00),
        new DateTime(2013,02,08,06,27,00),
        new DateTime(2013,02,08,06,00,27),
        new DateTime(2013,02,08,14,35,00),
        new DateTime(2013,02,08,15,29,00),
        new DateTime(2013,02,08,15,47,00),
        new DateTime(2013,02,08,16,01,00),
        new DateTime(2013,02,08,16,48,00),
        new DateTime(2013,02,08,17,49,00),
        new DateTime(2013,02,08,18,29,00),
        new DateTime(2013,02,08,18,15,00),
        new DateTime(2013,03,26,14,25,00),
        new DateTime(2013,03,28,14,07,27),
    };

    GothenburgTaxCalculator calc = new GothenburgTaxCalculator();
    Vehicle car = new Vehicle();
    car.SetVehicleType(vehicleType);
    car.SetDates(testDates);
    var tax = calc.GetTax(car, car.Dates);
    System.Console.WriteLine($"You have to pay {tax} KR in taxes");
}

app.Run();

