
using BlTest.BlApi;
using Dal;
using DalApi;

namespace BlTest.BlImplementation;

internal class Order:IOrder
{
    IDal Dal = new DalList();
}
