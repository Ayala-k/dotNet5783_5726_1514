//using System;
//using DalApi;
//using DO;
//namespace Dal;

//internal class Order:IOrder
//{
//	public Order()
//	{
//	}

//}
using DalApi;
using DO;
using System.IO;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace Dal;

internal class Order : IOrder
{
    string rootName = "OrdersList";
    string orderPath = @"XMLOrder.xml";



    public int Add(DO.Order order)
    {
        List<DO.Order?> ordersList = XMLTools.LoadListFromXMLSerializer<DO.Order?>(orderPath);
        XElement config = XElement.Load(XMLTools.configPath);
        int serialNumber = (int)config.Element("OrderID");
        order.ID = serialNumber;
        serialNumber++;
        config.Element("orderID")!.SetValue(serialNumber);
        config.Save(XMLTools.configPath);
        ordersList.Add(order);
        XMLTools.SaveListToXMLSerializer(ordersList, orderPath);
        return order.ID;
    }

    /// <summary>
    /// Deletes an order from the file of orders
    /// </summary>
    /// <param name="id"></param>
    /// <exception cref="NotImplementedException"></exception>
    public void Delete(int orderID)
    {
        List<DO.Order?> ordersList = XMLTools.LoadListFromXMLSerializer<DO.Order?>(orderPath);
        ordersList.Remove((ordersList.FirstOrDefault(item => item?.ID == orderID))
            ?? throw new EntityNotFoundException("order not found"));
        XMLTools.SaveListToXMLSerializer(ordersList, orderPath);

    }

    /// <summary>
    /// Update an order in the file of orders
    /// </summary>
    /// <param name="t"></param>
    /// <exception cref="NotImplementedException"></exception>
    public void Update(DO.Order order)
    {
        List<DO.Order?> ordersList = XMLTools.LoadListFromXMLSerializer<DO.Order?>(orderPath);
        ordersList.Remove((ordersList.FirstOrDefault(item => item?.ID == order.ID)));
        ordersList.Add(order);
        XMLTools.SaveListToXMLSerializer(ordersList, orderPath);
    }

    /// <summary>
    /// Returns list of all the orders, if gets a condition - by it
    /// </summary>
    /// <param name="func"></param>
    /// <returns>IEnumerable<DO.Order?></returns>
    public IEnumerable<DO.Order?> GetAll(Func<DO.Order?, bool>? predicate = null)
    {
        List<DO.Order?> ordersList = XMLTools.LoadListFromXMLSerializer<DO.Order?>(orderPath);
        return from order in ordersList
               where predicate == null || predicate(order)
               select order;
    }


    /// <summary>
    /// Gets a condition and returns an order with this condition
    /// </summary>
    /// <param name="func"></param>
    /// <returns>DO.Order</returns>
    /// <exception cref="NotFound"></exception>
    public DO.Order GetByCondition(Func<DO.Order?, bool>? predicate)
    {
        List<DO.Order?> ordersList = XMLTools.LoadListFromXMLSerializer<DO.Order?>(orderPath);
        return (ordersList.FirstOrDefault(item => predicate(item)))
                       ?? throw new EntityNotFoundException("order not found");
    }
}


