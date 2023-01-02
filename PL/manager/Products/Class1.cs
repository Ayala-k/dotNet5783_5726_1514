using BL.BO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace PL.Products;

static class Repository
{
    public static ObservableCollection<ProductForList> People { get; set; }

    static Repository()
    {
        People = new ObservableCollection<ProductForList>()
            {
                //new Person("111"){ FirstName="Avraham", LastName="Kohen", Age=9},
                //new Person("222"){ FirstName="Yizchak", LastName="Levi", Age=12},
                //new Person("333"){ FirstName="Yaakov", LastName="Levin", Age=4},
                //new Person("444"){ FirstName="Moshe", LastName="Kahana", Age=21}
            };
    }
}
