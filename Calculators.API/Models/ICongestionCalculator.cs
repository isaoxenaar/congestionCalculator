using System.Collections.Generic;
using Calculators.API.Controllers;

namespace Calculators.API
{
    public interface ICongestionCalculator
    {
        int GetTax(Vehicle vehicle, DateTime[] dates);
        bool IsTollFreeVehicle(Vehicle vehicle);
        int GetTollFee(DateTime date, Vehicle vehicle);
        bool IsTollFreeDate(DateTime date);
    }
}