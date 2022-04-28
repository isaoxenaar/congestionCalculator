using System.Collections.Generic;
using Calculators.API.Controllers;

namespace Calculators.API.Data
{
    public interface ICongestionRepository
    {
        bool SaveChanges();
        Vehicle GetOne(string id);
        Vehicle Create(string type, string city, DateTime[] dates);
        Vehicle Update(string id, int tax);
    }
}
