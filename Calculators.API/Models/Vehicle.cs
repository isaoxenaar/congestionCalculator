using System;
using System.Collections.Generic;
//sing System.Text;
//using System.Threading.Tasks;
//using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace Calculators.API
{
    public class Vehicle : IVehicle
    {
        public string Id { get; set; }
        public int? Tax { get; set; }
        public string Type { get; set; }
        public string City { get; set; }
        [NotMapped]
        public DateTime[] Dates { get; set; }
        public void SetCity(string city)
        {
            City = city;
        }
        public void SetVehicleType(string type)
        {
            Type = type;
        }
        public void SetDates(DateTime[] dates)
        {
            Dates = dates;
        }
    }
}