using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculators.API
{
    public interface IVehicle
    {
        void SetCity(string city);
        void SetVehicleType(string type);
        void SetDates(DateTime[] dates);
    }
}