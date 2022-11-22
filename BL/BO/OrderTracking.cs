
using System;

namespace BL.BO;

public class OrderTracking
{
    public int ID { get; set; }
    public OrderStatus status { get; set; }
    public struct DateAndProgressDescription
    {
        DateTime ProgressDate { get; set; }
        string ProgressDescription { get; set; }
    }
    public IEnumerable<DateAndProgressDescription> _dateAndProgressDescriptions;
}
