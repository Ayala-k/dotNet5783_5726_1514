
using DO;

namespace BL.BO;

public class EntityNotFoundLogicException : Exception
{
    public string EntityNotFound { get; set; }
    public EntityNotFoundLogicException(string str) : base(str) { }
    public EntityNotFoundLogicException(string str, DO.EntityNotFoundException e) : base(str, e) { }

}

public class InvalidDetailsException : Exception
{
    public string InvalidDetails { get; set; }
    public InvalidDetailsException(string str) : base(str) { }
}

public class EntityInUseException : Exception
{
    public string EntityInUse { get; set; }
    public EntityInUseException(string str) : base(str) { }
}

public class EntityAlreadyExistsLogicException : Exception
{
    public string EntityInUse { get; set; }
    public EntityAlreadyExistsLogicException(string str) : base(str) { }
    public EntityAlreadyExistsLogicException(string str, EntityAlreadyExistsException e) : base(str, e) { }
}


public class ProgressAlreadyDoneException : Exception
{
    public string ProgressAlreadyDone { get; set; }
    public ProgressAlreadyDoneException(string str) : base(str) { }
}

public class NotEnoughInStockException : Exception
{
    public string NotEnoughInStock { get; set; }
    public NotEnoughInStockException(string str) : base(str) { }
}