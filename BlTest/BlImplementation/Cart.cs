
using BlTest.BlApi;
using Dal;
using DalApi;

namespace BlTest.BlImplementation;

internal class Cart:ICart
{
    IDal Dal = new DalList();
}
