﻿namespace BL.BO;

public class Product
{
 public int ID { get; set; }
 public string? Name { get; set; }
 public Categories? Category { get; set; }
 public double Price { get; set; }
 public int InStock { get; set; }
 public override string ToString() => $@"
        Product ID:{ID},
        Product name: {Name},
        category: {Category},
        price: {Price},
        Amount in stock: {InStock}";
}
