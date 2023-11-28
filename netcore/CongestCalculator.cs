using System;
using System.Collections.Generic;
using System.Linq;
using congestion.calculator;

/// <summary>
/// This class calculate Congest Tax but not for only One day. it can accept mutiple record for multiple days and calculate both total task
/// and list of each daya tax in details/// 
/// </summary>
public class CongestCalculator
{
    CongestRule _congestRule = null;
    DateTime[] _dates = null;
    CongestCost _congestCost = null;
    Vehicle _vehicle = null;


    public CongestCalculator(Vehicle vehicle, CongestRule congestRule, CongestCost congestCost, DateTime[] dates)
    {
        _congestRule = congestRule;
        _dates = dates;
        _congestCost = congestCost;
        _vehicle = vehicle;
    }

    public int CalculateCost()
    {
        List<HourCost> costList = new List<HourCost>();

        foreach (DateTime tollDate in _dates)
        {
            if (_congestRule.ISFreeVehicle(_vehicle) || _congestRule.ISFreeDay(tollDate))
                continue;

            costList.Add(new HourCost()
            {
                TollDate = tollDate,
                Cost = _congestCost.getCost(tollDate)
            });


        }
        costList.Sort((x, y) => x.TollDate.CompareTo(y.TollDate));

        var lst = applySingleCharge(costList);

        var lst2 = applyDayMaxRule(lst);
        return lst.Sum(p => p.Cost);
    }

    private List<HourCost> applySingleCharge(List<HourCost> dataList)
    {
        // Create a new list to store the modified items
        List<HourCost> modifiedList = new List<HourCost>();

        var groupedByDateAndHour = dataList.GroupBy(item => new { item.TollDate.Date, item.TollDate.Hour });

        // Process each group
        foreach (var dateHourGroup in groupedByDateAndHour)
        {
            // Identify the item with the maximum value within the date and hour group
            var maxItem = dateHourGroup.OrderByDescending(item => item.Cost).FirstOrDefault();

            // Modify the Cost property based on the specified conditions
            foreach (var gitem in dateHourGroup)
            {
                // Create a new instance with the same properties as the original item
                var newItem = new HourCost
                {
                    TollDate = gitem.TollDate,
                    Cost = (gitem.Cost != maxItem.Cost) ? 0 : gitem.Cost,
                    // Set all other properties from maxItem if needed
                };

                // Add the new item to the modified list
                modifiedList.Add(newItem);
            }
        }

        // Return the new list with modified items
        return modifiedList;
    }


    private List<HourCost> applyDayMaxRule(List<HourCost> dataList)
    {

        var groupedByDay = dataList
            .GroupBy(item => item.TollDate.Date)
            .Select(group => new HourCost { TollDate = group.Key, Cost = group.Sum(item => item.Cost) })
            .ToList();

        // groupedByDay.Where(w => w.TotalCost>60).FirstOrDefault().TotalCost = 60;
        var toUpdate = groupedByDay.Where(x => x.Cost > 60);

        foreach (var item in toUpdate)
            item.Cost = 60;

        return groupedByDay;

    }
}
public class HourCost
{
    public DateTime TollDate { get; set; }
    public int Cost { get; set; }
}