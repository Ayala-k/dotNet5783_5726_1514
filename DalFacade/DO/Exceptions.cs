namespace DO;

public class EntityNotFoundException:Exception
{
    public string EntityNotFound { get; set; }
    public EntityNotFoundException(string str) : base(str) { }
}

public class EntityAlreadyExistsException:Exception
{
    public string EntityAlreadyExists { get; set; }
    public EntityAlreadyExistsException(string str) : base(str) { }
}

[Serializable]
public class DalConfigException : Exception
{
    public DalConfigException(string msg) : base(msg) { }
    public DalConfigException(string msg, Exception ex) : base(msg, ex) { }
}

public class XMLFileLoadCreateException : Exception
{
    public XMLFileLoadCreateException(string msg) : base(msg) { }
    public XMLFileLoadCreateException(string msg, Exception ex) : base(msg, ex) { }
}
