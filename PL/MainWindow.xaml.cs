using BL.BlApi;
using BL.BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using BL.BlImplementation;
using PL.manager;
using PL.customer;
//using PL.Orders;

namespace PL
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private IBl bl = BlApi.Factory.Get();
        void moveToManager_Click(object sender, RoutedEventArgs e) => new CustomerMainWindow().Show();

        void moveToTrackOrder_Click(object sender, RoutedEventArgs e) => new OrderTrackingWindow().Show();
    }
}
