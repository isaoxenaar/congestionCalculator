
namespace Calculators.API
{
    public class City
    {
        public string Name { get; set; }
        public List<Interval>? Rates { get; set; }
        public string[]? TollfreeDates { get; set; }
        public string[]? TollFreeVehicles { get; set; }
        public string[]? TollFreeDays { get; set; }
    }

    public class Interval
    {
        public string Start { get; set; }
        public string End { get; set; }
        public int? Rate { get; set; }
    }
}