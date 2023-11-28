using System;
using System.Collections.Generic;
using System.Linq;
using congestion.calculator;

public class CongestRule
{
    CongestTaxConfigSet _config;
    public CongestRule(string _configFileName)
    {
        JsonLoader jsonLoader = new JsonLoader(_configFileName);
        this._config = jsonLoader.LoadConfig();
    }


    public bool ISFreeDay(DateTime dateTime)
    {
        int dayOfWeek = (int)dateTime.DayOfWeek + 1; // DayOfWeek start from  zero
        if (this._config.OffWeekendDays.ContainsKey(dayOfWeek))
        {
            return true;
        }

        //check holidays
        int month = dateTime.Month;
        int dayOfMonth = dateTime.Day;

        bool isHoliDay = this._config.OffHolidays.Any(holiday => holiday.Month == month && holiday.DayOFMonth == dayOfMonth);

        if (isHoliDay)
            return true;

        //check july month
        if (month == 7)
            return true;

        return false;
    }
    public bool ISFreeVehicle(Vehicle inputVehicle)
    {
        string vehicleType = inputVehicle.GetVehicleType().ToLower();
        var allowed = this._config.OffFreeVehicles.Any(vehicle => vehicle.ToLower() == vehicleType);
        return allowed;
    }
}