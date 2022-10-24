using DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal;

internal class DataSource
{
    internal struct Config
    {
        static int _productsEmptyIndex = 0;
        static int _ordersEmptyIndex = 0;
        public int _serialNumber;
    }

    internal Product[] _productsArr = new Product[50];
    internal Order[] _ordersArr = new Order[100];
    internal OrderItem[] _orderItemsArr = new OrderItem[200];

    static void Initialize()
    {
        //_productsArr[0]= ;
        //Config._serialNumber = 3;
    }
}
