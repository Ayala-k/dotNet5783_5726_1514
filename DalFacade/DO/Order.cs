namespace DO;

/// <summary>
/// Structure for orders
/// </summary>
public struct Order
{
    /// <summary>
    /// Unique ID of order
    /// </summary>
    public int ID { get; set; }

    /// <summary>
    /// Name of customer
    /// </summary>
    public string CustomerName { get; set; }

    /// <summary>
    /// Email of customer
    /// </summary>
    public string CustomerEmail { get; set; }

    /// <summary>
    /// Address of customer
    /// </summary>
    public string CustomerAddress { get; set; }

    /// <summary>
    /// Date of Order
    /// </summary>
    public DateTime OrderDate { get; set; }

    /// <summary>
    /// Date of shipping
    /// </summary>
    public DateTime ShipDate { get; set; }

    /// <summary>
    /// Date of delivery
    /// </summary>
    public DateTime DeliveryDate { get; set; }

    /// <summary>
    ///  printing an order 
    /// </summary>
    /// <returns>an order as a string</returns>
    public override string ToString() => $@"
        Order ID={ID}: 
        customer name: {CustomerName}
        customer email: {CustomerEmail}
        customer address: {CustomerAddress}
        order date: {OrderDate}
        ship date: {ShipDate}
        delivery date: {DeliveryDate}";
}
