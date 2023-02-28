namespace BlImplementation;

sealed internal class Bl : BL.BlApi.IBl
{
 public BL.BlApi.IOrder Order => new BL.BlImplementation.Order();
 public BL.BlApi.IProduct Product => new BL.BlImplementation.Product();
 public BL.BlApi.ICart Cart => new BL.BlImplementation.Cart();
}
