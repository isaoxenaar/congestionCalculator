using Xunit;
using System;
using Calculators.API;
using Calculators.API.Controllers;

namespace Calculator.Tests;

public class UnitTest
{
    DateTime[] withinDay = new DateTime[]{
        new DateTime(2013,1,14,21,00,00),
        new DateTime(2013,1,15,21,00,00),
    };

    DateTime[] within60 = new DateTime[]{
        new DateTime(2013, 02, 08, 13, 00, 00),
        new DateTime(2013, 02, 08, 15, 45, 00),
        new DateTime(2013, 02, 08, 16, 43, 00),
    };

    DateTime[] max60 = new DateTime[]{
        new DateTime(2013,02,08,06,00,00),
        new DateTime(2013,02,08,07,15,00),
        new DateTime(2013,02,08,08,25,00),
        new DateTime(2013,02,08,15,31,00),
        new DateTime(2013,02,08,17,10,00),
        new DateTime(2013,02,08,18,20,00)
    };

    DateTime[] holiday = new DateTime[]{
        new DateTime(2013,12,31,14,25,00),
        new DateTime(2013,01,01,14,07,27)
    };

    DateTime[] weekend = new DateTime[]{
        new DateTime(2013,03,23,14,25,00),
        new DateTime(2013,03,24,14,30,27)
    };

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

    [Fact]
    public void ShouldReturn89()
    {
        GothenburgTaxCalculator testCalc = new GothenburgTaxCalculator();
        Vehicle car = new Vehicle();
        car.SetVehicleType("Car");
        car.SetDates(testDates);
        var rate = testCalc.GetTax(car, car.Dates);
        Assert.Equal(89, rate);
    }

    [Fact]
    public void TollFreeVehicle()
    {
        GothenburgTaxCalculator testCalc = new GothenburgTaxCalculator();
        Vehicle car = new Vehicle();
        car.SetVehicleType("Foreign");
        car.SetDates(holiday);
        var rate = testCalc.GetTax(car, car.Dates);
        Assert.Equal(0, rate);
    }

    [Fact]
    public void OnlyOnceIn60min()
    {
        GothenburgTaxCalculator testCalc = new GothenburgTaxCalculator();
        Vehicle car = new Vehicle();
        car.SetVehicleType("Truck");
        car.SetDates(within60);
        var rate = testCalc.GetTax(car, car.Dates);
        Assert.Equal(26, rate);
    }

    [Fact]
    public void Max60Kroner()
    {
        GothenburgTaxCalculator testCalc = new GothenburgTaxCalculator();
        Vehicle car = new Vehicle();
        car.SetVehicleType("Limousine");
        car.SetDates(max60);
        var rate = testCalc.GetTax(car, car.Dates);
        Assert.Equal(60, rate);
    }

    [Fact]
    public void FreeOnHoliday()
    {
        GothenburgTaxCalculator testCalc = new GothenburgTaxCalculator();
        Vehicle car = new Vehicle();
        car.SetVehicleType("Scooter");
        car.SetDates(holiday);
        var rate = testCalc.GetTax(car, car.Dates);
        Assert.Equal(0, rate);
    }

    [Fact]
    public void FreeOnWeekend()
    {
        GothenburgTaxCalculator testCalc = new GothenburgTaxCalculator();
        Vehicle car = new Vehicle();
        car.SetVehicleType("Car");
        car.SetDates(weekend);
        var rate = testCalc.GetTax(car, car.Dates);
        Assert.Equal(0, rate);
    }
}
