
using BlTest.BlApi;
using Dal;
using DalApi;

namespace BlTest.BlImplementation;

internal class Product:IProduct
{
    IDal Dal = new DalList();
}
