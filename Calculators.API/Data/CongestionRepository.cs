using System;
using System.Collections.Generic;
using System.Linq;
using Calculators.API.Controllers;

namespace Calculators.API.Data
{
    public class CongestionRepository : ICongestionRepository
    {
        private readonly AppDbContext _context;

        public CongestionRepository(AppDbContext context)
        {
            _context = context;
        }

        public bool SaveChanges()
        {
            return (_context.SaveChanges() >= 0);
        }

        public Vehicle Create(string city, string type, DateTime[] dates)
        {
            var vehicle = new Vehicle
            {
                Id = Guid.NewGuid().ToString(),
            };

            vehicle.SetDates(dates);
            vehicle.SetVehicleType(type.ToLower());
            vehicle.SetCity(city);

            _context.Vehicles.Add(vehicle);
            _context.SaveChanges();
            return vehicle;
        }

        public Vehicle GetOne(string id)
        {
            return _context.Vehicles.Where(c => c.Id == id)
              .SingleOrDefault();
        }

        public Vehicle Update(string id, int tax)
        {
            var vehicle = GetOne(id);
            vehicle.Tax = tax;
            _context.Vehicles.Update(vehicle);
            _context.SaveChanges();
            return vehicle;
        }

    }
}