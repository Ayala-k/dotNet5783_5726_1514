﻿
using DO;

namespace DalApi;

public interface IProduct : ICrud<Product>
{
    public void initializeDataSource();
}
