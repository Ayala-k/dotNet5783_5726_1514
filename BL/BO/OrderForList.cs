﻿
using System;
using System.Diagnostics;

namespace BL.BO;

public class OrderForList
{
    public int ID { get; set; }
    public string CustomerName { get; set; }
    public OrderStatus Status { get; set; }
    public int AmountOfItems { get; set; }
    public double TotalPrice { get; set; }


    public override string ToString() => $@"
        Order For List ID:{ID},
        Customer name: {CustomerName},
        Status: {Status},
        Amount Of Items: {AmountOfItems},
        Total Price: {TotalPrice}";

}

