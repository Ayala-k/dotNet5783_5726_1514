namespace Dal;

public class XMLLoadException : Exception
{
    public string XMLLoad { get; set; }
    public XMLLoadException(string str) : base(str) { }
}
public class XMLSaveException : Exception
{
    public string XMLSave { get; set; }
    public XMLSaveException(string str) : base(str) { }
}
