namespace DO;

/// <summary>
/// Structure for order items
/// </summary>
public struct OrderItem
{
    /// <summary>
    /// Unique ID of order item
    /// </summary>
    public int ID { get; set; }

    /// <summary>
    /// ID of product
    /// </summary>
    public int ProductID { get; set; }

    /// <summary>
    /// ID of order
    /// </summary>
    public int OrderID { get; set; }

    /// <summary>
    /// price
    /// </summary>
    public double Price { get; set; }

    /// <summary>
    /// amount of products
    /// </summary>
    public int Amount { get; set; }

    /// <summary>
    ///  printing an order item
    /// </summary>
    /// <returns>an order item as a string</returns>
    public override string ToString() => $@"
        OrderItemID={ID}
        ProductID: {ProductID}
        orderID: {OrderID}
        Price: {Price}
        Amount: {Amount}
";
}
