
namespace DO;

/// <summary>
/// Structure for Products
/// </summary>
public struct Product
{
    /// <summary>
    /// Unique ID of ...
    /// </summary>
    public int ID { get; set; }

    /// <summary>
    /// Unique ID of ...
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Unique ID of ...
    /// </summary>
    public double Price { get; set; }

    /// <summary>
    /// Unique ID of ...
    /// </summary>
    // what is category type????
    //public Category Category { get; set; }

    /// <summary>
    /// Unique ID of ...
    /// </summary>
    public int InStock { get; set; }

    public override string ToString() => $@"
        Product ID={ID}: {Name},
        category - Category
        Price: {Price}
        Amount in stock: {InStock}
";
// add {} out of Category
}
