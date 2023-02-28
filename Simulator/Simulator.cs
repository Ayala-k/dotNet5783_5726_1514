namespace Simulator;

public static class Simulator
{
 public static BL.BlApi.IBl? bl = BlApi.Factory.Get();

 private static readonly Random rand = new(DateTime.Now.Second);

 private static volatile bool isRunning = false;

 public static BL.BO.Order order;
 public static BL.BO.OrderStatus lastStatus;
 public static BL.BO.OrderStatus nextStatus;

 public delegate void stopRunningEvent();
 public static event stopRunningEvent? StopRunning;

 public delegate void updateProgressEvent(DateTime dateNow, BL.BO.OrderStatus previousStatus, BL.BO.OrderStatus nextStatus, int time, BL.BO.Order order);//weird parameters
 public static event updateProgressEvent? updateProgress;

 public static void Run()
 {
  isRunning = true;
  new Thread(() =>
  {
   while (isRunning)
   {
    int? orderId = bl.Order.SelectOrder();
    if (orderId != null)
    {
     order = bl.Order.GetOrderDetails((int)orderId);
     int time = rand.Next(3, 10);
     if (order.ShipDate == null)
     {
      updateProgress(DateTime.Now, BL.BO.OrderStatus.OrderCommited, BL.BO.OrderStatus.OrderShipped, time, order);
     }
     else if (order.DeliveryDate == null)
     {
      updateProgress(DateTime.Now, BL.BO.OrderStatus.OrderShipped, BL.BO.OrderStatus.OrderDelivered, time, order);
     }
     Thread.Sleep(1000 * time);
     if (order.ShipDate == null)
     {
      bl?.Order.UpdateOrderShipping((int)orderId);
     }

     else if (order.DeliveryDate == null)
     {
      bl?.Order.UpdateOrderDelivering((int)orderId);
     }
    }
    else
    {
     StopRunning();
     Thread.Sleep(1000);
    }
   }
  }).Start();
 }

 public static void Stop() => isRunning = false;
}