using System;
using System.IO;
using System.Reflection.Metadata;
using System.Runtime.Serialization.Json;
using System.Xml;
using System.Xml.Serialization;

public class Sample
{
 public static void Main()
 {
  List<BL.BO.Order> list = new List<BL.BO.Order>();
  BL.BO.Order order = new BL.BO.Order()
  {
   ID=101
  };
  list.Add(order);
  FileStream fs = new FileStream(@"C:\Users\WIN-10\source\repos\dotNet5783_5726_1514\xml\XMLOrder.xml", FileMode.OpenOrCreate);
  XmlSerializer xs = new XmlSerializer(typeof(List<BL.BO.Order>));
  xs.Serialize(fs, list);
  fs.Close();


 }
}