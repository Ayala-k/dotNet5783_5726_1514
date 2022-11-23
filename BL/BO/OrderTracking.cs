
using System;

namespace BL.BO;

public class OrderTracking
{
    public int ID { get; set; }
    public OrderStatus Status { get; set; }
    public struct DateAndProgressDescription
    {
        public DateTime ProgressDate { get; set; }
        public string ProgressDescription { get; set; }
    }
    public IEnumerable<DateAndProgressDescription> DateAndProgressDescriptionsList;
}
