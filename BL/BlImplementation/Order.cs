
using BL.BlApi;
using Dal;
using DalApi;

namespace BL.BlImplementation;

internal class Order:IOrder
{
    IDal Dal = new DalList();
}
