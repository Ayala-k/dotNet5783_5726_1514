using System.Xml.Linq;
using System.Xml.Serialization;

namespace Dal;

internal class XMLTools
{
    static string dir = @"..\xml\";
    public static string configPath = dir + @"Config.xml";
    static XMLTools()
    {
        if (!Directory.Exists(dir))
            Directory.CreateDirectory(dir);
    }

    #region SaveLoadWithXElement
    public static void SaveListToXMLElement(XElement rootElem, string filePath)
    {
        try
        {
            rootElem.Save(dir + filePath);
        }
        catch (Exception ex)
        {
            throw new XMLSaveException(ex.Message);
        }
    }
    public static XElement LoadListFromXMLElement(string filePath)
    {
        try
        {
            if (File.Exists(dir + filePath))
            {
                return XElement.Load(dir + filePath);
            }
            else
            {
                XElement rootElem = new XElement(dir + filePath);
                rootElem.Save(dir + filePath);
                return rootElem;
            }
        }
        catch (Exception ex)
        {
            throw new XMLLoadException(ex.Message);
        }
    }
    #endregion

    #region SaveLoadWithXMLSerializer
    public static void SaveListToXMLSerializer<T>(List<T> list, string filePath)
    {
        try
        {
            FileStream file = new FileStream(dir + filePath, FileMode.Create);
            XmlSerializer x = new XmlSerializer(list.GetType());
            x.Serialize(file, list);
            file.Close();
        }
        catch (Exception ex)
        {
            throw new XMLSaveException(ex.Message);
        }
    }
    public static List<T> LoadListFromXMLSerializer<T>(string filePath)
    {
        try
        {
            if (File.Exists(dir + filePath))
            {
                List<T> list;
                XmlSerializer x = new XmlSerializer(typeof(List<T>)); 
                FileStream file = new FileStream(dir + filePath, FileMode.Open);
                list = (List<T>)x.Deserialize(file);
                file.Close();
                return list;
            }
            else
                return new List<T?>();
        }
        catch (Exception ex)
        {
            throw new XMLLoadException(ex.Message);
        }
    }
    #endregion
}