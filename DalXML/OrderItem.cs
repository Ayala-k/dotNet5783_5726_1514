using DalApi;
using DO;
using System.IO;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace Dal;

internal class OrderItem : IOrderItem
{
    //string rootName = "OrdersList";
    string orderItemPath = @"XMLOrderItem.xml";



    public int Add(DO.OrderItem orderItem)
    {
        List<DO.OrderItem?> orderItemsList = XMLTools.LoadListFromXMLSerializer<DO.OrderItem?>(orderItemPath);
        XElement config = XElement.Load(XMLTools.configPath);
        int serialNumber = (int)config.Element("OrderItemID");
        orderItem.ID = serialNumber;
        serialNumber++;
        config.Element("OrderItemID")!.SetValue(serialNumber);
        config.Save(XMLTools.configPath);
        orderItemsList.Add(orderItem);
        XMLTools.SaveListToXMLSerializer(orderItemsList, orderItemPath);
        return orderItem.ID;
    }

    /// <summary>
    /// Deletes an order from the file of orders
    /// </summary>
    /// <param name="id"></param>
    /// <exception cref="NotImplementedException"></exception>
    public void Delete(int orderItemID)
    {
        List<DO.OrderItem?> orderItemsList = XMLTools.LoadListFromXMLSerializer<DO.OrderItem?>(orderItemPath);
        orderItemsList.Remove((orderItemsList.FirstOrDefault(item => item?.ID == orderItemID))
            ?? throw new EntityNotFoundException("order item not found"));
        XMLTools.SaveListToXMLSerializer(orderItemsList, orderItemPath);

    }

    /// <summary>
    /// Update an order in the file of orders
    /// </summary>
    /// <param name="t"></param>
    /// <exception cref="NotImplementedException"></exception>
    public void Update(DO.OrderItem orderItem)
    {
        List<DO.OrderItem?> orderItemsList = XMLTools.LoadListFromXMLSerializer<DO.OrderItem?>(orderItemPath);
        orderItemsList.Remove((orderItemsList.FirstOrDefault(item => item?.ID == orderItem.ID)));
        orderItemsList.Add(orderItem);
        XMLTools.SaveListToXMLSerializer(orderItemsList, orderItemPath);
    }

    /// <summary>
    /// Returns list of all the orders, if gets a condition - by it
    /// </summary>
    /// <param name="func"></param>
    /// <returns>IEnumerable<DO.Order?></returns>
    public IEnumerable<DO.OrderItem?> GetAll(Func<DO.OrderItem?, bool>? predicate = null)
    {
        List<DO.Order?> orderItemsList = XMLTools.LoadListFromXMLSerializer<DO.Order?>(orderItemPath);
        return from DO.OrderItem? orderItem in orderItemsList
               where predicate == null || predicate(orderItem)
               select orderItem;
    }


    /// <summary>
    /// Gets a condition and returns an order with this condition
    /// </summary>
    /// <param name="func"></param>
    /// <returns>DO.Order</returns>
    /// <exception cref="NotFound"></exception>
    public DO.OrderItem GetByCondition(Func<DO.OrderItem?, bool>? predicate)
    {
        List<DO.OrderItem?> orderItemsList = XMLTools.LoadListFromXMLSerializer<DO.OrderItem?>(orderItemPath);
        return (orderItemsList.FirstOrDefault(item => predicate(item)))
                       ?? throw new EntityNotFoundException("order item not found");
    }
}


