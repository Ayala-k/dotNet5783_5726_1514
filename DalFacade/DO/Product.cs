namespace DO;

/// <summary>
/// Structure for Products
/// </summary>
public struct Product
{
    /// <summary>
    /// Unique ID of product
    /// </summary>
    public int ID { get; set; }

    /// <summary>
    /// name of product
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// category of product
    /// </summary>
    public Categories Category { get; set; }

    /// <summary>
    /// price of product
    /// </summary>
    public double Price { get; set; }

    /// <summary>
    /// amount in stock of product
    /// </summary>
    public int InStock { get; set; }

    /// <summary>
    ///  printing a product
    /// </summary>
    /// <returns>product as a string</returns>
    public override string ToString() => $@"
        Product ID={ID}: {Name},
        category - {Category}
        Price: {Price}
        Amount in stock: {InStock}
";
}
