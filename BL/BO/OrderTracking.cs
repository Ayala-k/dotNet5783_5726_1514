
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

 public override string ToString()
 {

  string Description = "";
  foreach (DateAndProgressDescription description in DateAndProgressDescriptionsList)
  {
   Description += (description.ToString());
  }
  return (
  $@"
        Order tracking ID={ID},
        Status: {Status},
        Date And Progres sDescriptions List: {Description}");

 }
}