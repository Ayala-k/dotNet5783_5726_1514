﻿namespace BL.BO;

public class OrderTracking
{
    public int ID { get; set; }
    public OrderStatus? Status { get; set; }
    public struct DateAndProgressDescription
    {
        public DateTime? ProgressDate { get; set; }
        public string ProgressDescription { get; set; }
    }
    public IEnumerable<DateAndProgressDescription?>? DateAndProgressDescriptionsList;

    public override string ToString()
    {
        string descriptionStr = "";
        foreach (DateAndProgressDescription description in DateAndProgressDescriptionsList)
        {
            descriptionStr += (description.ProgressDate.ToString() + " " + description.ProgressDescription + "\n");
        }
        return (
        $@"
        Order tracking ID={ID},
        Status: {Status},
        Progress:
        {descriptionStr}");
    }
}