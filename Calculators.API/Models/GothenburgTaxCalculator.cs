using System;
namespace Calculators.API;
public class GothenburgTaxCalculator
{
    /**
         * Calculate the total toll fee for one day
         *
         * @param vehicle - the vehicle
         * @param dates   - date and time of all passes on one day
         * @return - the total congestion tax for that day
         */

    public int GetTax(Vehicle vehicle, DateTime[] dates)
    {
        Array.Sort(dates);
        int totalFee = 0;
        int dayFee = 0;
        DateTime startHour = dates[0];
        DateTime startDay = dates[0];
        DateTime prevDate = dates[0];

        for (var i = 0; i < dates.Length; i++)
        {
            if (60 + startHour.Minute + dates[i].Minute > 60 + (startHour.Minute * 2))
            {
                startHour = dates[i];
            }
            if (startDay.Day < dates[i].Day || startDay.Month < dates[i].Month)
            {
                startDay = dates[i];
                if (dayFee > 60) dayFee = 60;
                totalFee += dayFee;
                dayFee = 0;
            }

            if (i > 1) prevDate = dates[i - 1];
            int prevFee = GetTollFee(prevDate, vehicle);
            int nextFee = GetTollFee(dates[i], vehicle);

            var dtDiff = startHour.Day - dates[i].Day;
            var hrDiff = (24 - startHour.Hour) - (24 - dates[i].Hour);
            var mnDiff = (60 - dates[i].Minute) - (60 - startHour.Minute);

            if (startHour != dates[i] && i > 0 && Math.Abs(dtDiff) <= 1 && Math.Abs(hrDiff) <= 1 && Math.Abs(mnDiff) < 60)
            {
                var add = 0;
                if (nextFee > prevFee) add = nextFee - prevFee;
                dayFee += add;
            }
            else
            {
                dayFee += nextFee;
            }
        }
        if (startDay.Day == dates[dates.Length - 1].Day && startDay.Month == dates[dates.Length - 1].Month)
        {
            if (dayFee > 60) dayFee = 60;
            totalFee += dayFee;
        }
        return totalFee;
    }

    private bool IsTollFreeVehicle(Vehicle vehicle)
    {
        if (vehicle == null) return false;
        string vehicleType = vehicle.Type.ToLower();
        return vehicleType.Equals(TollFreeVehicles.motorcycle.ToString()) ||
               vehicleType.Equals(TollFreeVehicles.bus.ToString()) ||
               vehicleType.Equals(TollFreeVehicles.emergency.ToString()) ||
               vehicleType.Equals(TollFreeVehicles.diplomat.ToString()) ||
               vehicleType.Equals(TollFreeVehicles.foreign.ToString()) ||
               vehicleType.Equals(TollFreeVehicles.military.ToString());
    }

    public int GetTollFee(DateTime date, Vehicle vehicle)
    {
        if (IsTollFreeDate(date) || IsTollFreeVehicle(vehicle)) return 0;
        int hour = date.Hour;
        int minute = date.Minute;

        if (hour == 6 && minute >= 0 && minute <= 29) return 8;
        else if (hour == 6 && minute >= 30 && minute <= 59) return 13;
        else if (hour == 7 && minute >= 0 && minute <= 59) return 18;
        else if (hour == 8 && minute >= 0 && minute <= 29) return 13;
        else if (hour > 8 && hour < 14) return 8;
        else if (hour == 8 && minute >= 30 && minute <= 59) return 8;
        else if (hour == 14 && minute <= 59) return 8;
        else if (hour == 15 && minute >= 0 && minute <= 29) return 13;
        else if ((hour == 15 && minute >= 30) || (hour == 16 && minute <= 59)) return 18;
        else if (hour == 17 && minute >= 0 && minute <= 59) return 13;
        else if (hour == 18 && minute >= 0 && minute <= 29) return 8;
        else return 0;
    }

    private Boolean IsTollFreeDate(DateTime date)
    {
        int year = date.Year;
        int month = date.Month;
        int day = date.Day;

        if (date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday) return true;

        if (year == 2013)
        {
            if (month == 1 && day == 1 ||
                month == 3 && (day == 28 || day == 29) ||
                month == 4 && (day == 1 || day == 30) ||
                month == 5 && (day == 1 || day == 8 || day == 9) ||
                month == 6 && (day == 5 || day == 6 || day == 20 || day == 21) ||
                month == 7 || month == 10 && day == 31 ||
                month == 11 && day == 1 ||
                month == 12 && (day == 24 || day == 25 || day == 26 || day == 31))
            {
                return true;
            }
        }
        return false;
    }

    private enum TollFreeVehicles
    {
        motorcycle = 0,
        bus = 1,
        emergency = 2,
        diplomat = 3,
        foreign = 4,
        military = 5
    }
}