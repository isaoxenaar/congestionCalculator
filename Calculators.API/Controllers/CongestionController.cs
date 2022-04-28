using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Calculators.API;
using Calculators.API.Data;

namespace Calculators.API.Controllers;

[ApiController]
[Route("[controller]")]
public class CongestionController : ControllerBase
{

    private readonly ICongestionRepository _repo;
    public CongestionController(ICongestionRepository repo)
    {
        _repo = repo;
    }

    [HttpGet("{id}")]
    public ActionResult<Vehicle> GetVehicle(string id)
    {
        Vehicle car = _repo.GetOne(id);
        if (car == null)
            return NotFound();
        return Ok(car);
    }

    [HttpPost]
    public ActionResult<Vehicle> PostVehicle(string city, string vehicleType, DateTime[] dates)
    {
        GothenburgTaxCalculator calc = new GothenburgTaxCalculator();
        var car = _repo.Create(city, vehicleType, dates);
        var tax = calc.GetTax(car, car.Dates);
        _repo.Update(car.Id, tax);
        return CreatedAtAction(nameof(GetVehicle), new { id = car.Id }, car);
    }

    [HttpPut("/ChangeTax/{id}")]
    public ActionResult<Vehicle> NewDates(string id, DateTime[] dates)
    {
        GothenburgTaxCalculator calc = new GothenburgTaxCalculator();
        var car = _repo.GetOne(id);
        car.SetDates(dates);
        var tax = calc.GetTax(car, car.Dates);
        var updatedCar = _repo.Update(car.Id, tax);
        return Ok(updatedCar);
    }
}
