
using System.Diagnostics;

namespace DO;

/// <summary>
/// Structure for orders
/// </summary>
public struct Order
{
    /// <summary>
    /// Unique ID of ...
    /// </summary>
    public int ID { get; set; }

    /// <summary>
    /// Unique ID of ...
    /// </summary>
    public string CustomerName { get; set; }

    /// <summary>
    /// Unique ID of ...
    /// </summary>
    public string CustomerEmail { get; set; }

    /// <summary>
    /// Unique ID of ...
    /// </summary>
    public string CustomerAddress { get; set; }

    /// <summary>
    /// Unique ID of ...
    /// </summary>
    public DateTime OrderDate { get; set; }

    /// <summary>
    /// Unique ID of ...
    /// </summary>
    public DateTime ShipDate { get; set; }

    /// <summary>
    /// Unique ID of ...
    /// </summary>
    public DateTime DeliveryDate { get; set; }

    public override string ToString() => $@"
        Order ID={ID}: 
        customer name: {CustomerName}
        customer email: {CustomerEmail}
        customer address: {CustomerAddress}
        order date: {OrderDate}
        ship date: {ShipDate}
        delivery date: {DeliveryDate}";
}
