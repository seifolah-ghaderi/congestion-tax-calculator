using System.Dynamic;

public class HoliDay
{
    public int Month { get; set; }
    public int DayOFMonth { get; set; }

    public HoliDay(int month, int dayOfMonth)
    {
        this.Month = month;
        this.DayOFMonth = dayOfMonth;
    }


}