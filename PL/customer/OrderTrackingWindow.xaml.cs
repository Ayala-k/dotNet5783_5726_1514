using System.Windows;

namespace PL.customer;

/// <summary>
/// Interaction logic for OrderTrackingWindow.xaml
/// </summary>
public partial class OrderTrackingWindow : Window
{
    BL.BlApi.IBl? bl = BlApi.Factory.Get();

    public int orderID
    {
        get { return (int)GetValue(orderIDProperty); }
        set { SetValue(orderIDProperty, value); }
    }
    public static readonly DependencyProperty orderIDProperty =
        DependencyProperty.Register(nameof(orderID), typeof(int), typeof(OrderTrackingWindow));

    public string trackDetails
    {
        get { return (string)GetValue(trackDetailsProperty); }
        set { SetValue(trackDetailsProperty, value); }
    }
    public static readonly DependencyProperty trackDetailsProperty =
        DependencyProperty.Register(nameof(trackDetails), typeof(string), typeof(OrderTrackingWindow));

    public string errorMessageText
    {
        get { return (string)GetValue(errorMessageTextProperty); }
        set { SetValue(errorMessageTextProperty, value); }
    }
    public static readonly DependencyProperty errorMessageTextProperty =
        DependencyProperty.Register("errorMessageText", typeof(string), typeof(OrderTrackingWindow));

    public OrderTrackingWindow()
    {
        InitializeComponent();
    }

    private void TrackOrderButton_Click(object sender, RoutedEventArgs e)
    {
        trackDetails = "";
        try
        {
            trackDetails = bl.Order.OrderTrack(orderID).ToString();
        }
        catch (BL.BO.EntityNotFoundLogicException exp)
        {
            errorMessageText = exp.Message.ToString();
        }
    }
}
