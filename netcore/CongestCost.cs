using System;
using System.Collections.Generic;

public class CongestCost
{
    List<TimeRange> _timeRanges;
    public CongestCost()
    {
        _timeRanges = new List<TimeRange>()
        {
            new TimeRange { Start = new TimeSpan(6, 0, 0), End = new TimeSpan(6, 29, 59), Value = 8 },
            new TimeRange { Start = new TimeSpan(6, 30, 0), End = new TimeSpan(6, 59, 59), Value = 13 },
            new TimeRange { Start = new TimeSpan(7, 0, 0), End = new TimeSpan(7, 59, 59), Value = 18 },
            new TimeRange { Start = new TimeSpan(8, 0, 0), End = new TimeSpan(8, 29, 59), Value = 13 },
            new TimeRange { Start = new TimeSpan(8, 30, 0), End = new TimeSpan(14, 59, 59), Value = 8 },
            new TimeRange { Start = new TimeSpan(15, 0, 0), End = new TimeSpan(15, 29, 59), Value = 13 },
            new TimeRange { Start = new TimeSpan(15, 30, 0), End = new TimeSpan(16, 59, 59), Value = 18 },
            new TimeRange { Start = new TimeSpan(17, 0, 0), End = new TimeSpan(17, 59, 59), Value = 13 },
            new TimeRange { Start = new TimeSpan(18, 0, 0), End = new TimeSpan(18, 29, 59), Value = 8 },
            new TimeRange { Start = new TimeSpan(18, 30, 0), End = new TimeSpan(5, 59, 59), Value = 0 }
        };
    }

    public int getCost(DateTime date)
    {
        // Extract hour and minute components
        int hour = date.Hour;
        int minute = date.Minute;
        int second = date.Second;

        // Create a TimeSpan using the extracted components
        TimeSpan timeOfDay = new TimeSpan(hour, minute, second);

        foreach (var range in _timeRanges)
        {
            if (IsWithinRange(range, timeOfDay))
            {
                return range.Value;
            }
        }

        return 0;
    }
    private bool IsWithinRange(TimeRange range, TimeSpan inputTime)
    {
        return inputTime >= range.Start && inputTime <= range.End;
    }
}