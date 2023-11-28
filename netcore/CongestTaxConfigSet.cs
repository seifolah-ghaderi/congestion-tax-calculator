using System.Collections.Generic;

public class CongestTaxConfigSet
{
    public Dictionary<int, string> OffWeekendDays { get; set; }
    public List<HoliDay> OffHolidays { get; set; }
    public int[] OffMonthsForTotal { get; set; }
    public string[] OffFreeVehicles { get; set; }
    public int SingleChargeDurationInMinutes { get; set; }
    public int MaxChargeOfDay { get; set; }
}
