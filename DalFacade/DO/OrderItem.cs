
using System.Xml.Linq;

namespace DO;

/// <summary>
/// Structure for order items
/// </summary>
public struct OrderItem
{
    /// <summary>
    /// Unique ID of ...
    /// </summary>
    public int ProductID { get; set; }

    /// <summary>
    /// Unique ID of ...
    /// </summary>
    public int OrderID { get; set; }

    /// <summary>
    /// Unique ID of ...
    /// </summary>
    public double Price { get; set; }

    /// <summary>
    /// Unique ID of ...
    /// </summary>
    public int Amount { get; set; }

    public override string ToString() => $@"
        Order Item={ProductID}: 
        orderId: {OrderID}
        Price: {Price}
        Amount: {Amount}
";


}
