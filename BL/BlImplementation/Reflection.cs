
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Reflection;

namespace BlImplementation;

static public class Reflection
{
 public static string ToStringProperty<T>(this T t)
 {
  string str = "";
  foreach (PropertyInfo item in t.GetType().GetProperties())
   str += "\n" + item.Name
   + ": " + item.GetValue(t, null);
  return str;
 }
 public static A copy<T, A>(this T t, A a)
 {
  PropertyInfo[] propertyInfos = a.GetType().GetProperties();
  foreach (PropertyInfo item in t.GetType().GetProperties().Where(prop => prop.CanRead && prop.CanWrite))
  {
   var same = propertyInfos.Where((prop) => prop.Name == item.Name && prop.PropertyType == item.PropertyType);
   if (same.Count() != 0)
    same.First().SetValue(a, item.GetValue(t));
  }
  return a;
 }
}


