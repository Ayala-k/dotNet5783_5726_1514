using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Threading;
using System.Windows;
namespace PL;

/// <summary>
/// simulator stimulat the store dayli working 
/// take the oldest order and Send/Provide it to customer
/// do it until orders end or by clicking the stop button 
/// </summary>
public partial class SimulatorWindow : Window
{
 private readonly Stopwatch stopWatch = new();

 private volatile bool isStopWatchRun;
 private bool closed = false;

 public string updateProgressText
 {
  get { return (string)GetValue(updateProgressTextProperty); }
  set
  {
   this.Dispatcher.Invoke(() =>
  {
   SetValue(updateProgressTextProperty, value);
  });
  }
 }
 public static readonly DependencyProperty updateProgressTextProperty =
     DependencyProperty.Register("updateProgressText", typeof(string), typeof(SimulatorWindow));

 public string stopWatchText
 {
  get { return (string)GetValue(stopWatchTextProperty); }
  set
  {
   this.Dispatcher.Invoke(() =>
   {
    SetValue(stopWatchTextProperty, value);
   });
  }
 }
 public static readonly DependencyProperty stopWatchTextProperty =
     DependencyProperty.Register("stopWatchText", typeof(string), typeof(SimulatorWindow), new PropertyMetadata(null));

 BackgroundWorker worker = new();

 public SimulatorWindow()
 {
  InitializeComponent();

  worker.DoWork += doWork;
  worker.ProgressChanged += updateStopWatch;
  worker.RunWorkerCompleted += doWorkCompleted;
  worker.WorkerReportsProgress = true;
  worker.WorkerSupportsCancellation = true;
  worker.RunWorkerAsync();
 }

 private void doWork(object? sender, DoWorkEventArgs? e)
 {
  Simulator.Simulator.updateProgress += simulatorUpdateProgress;
  Simulator.Simulator.StopRunning += simulatorStopRunning;

  stopWatchText = "00:00:00";
  stopWatch.Start();
  isStopWatchRun = true;

  Simulator.Simulator.Run();

  while (isStopWatchRun)
  {
   worker.ReportProgress(1);
   Thread.Sleep(1000);
  }
 }

 private void updateStopWatch(Object? sender, ProgressChangedEventArgs? e)
 {
  string timerText = stopWatch.Elapsed.ToString();
  timerText = timerText.Substring(0, 8);
  stopWatchText = timerText;
 }

 private void stopStopWatchButton_Click(object sender, RoutedEventArgs e)
 {
  if (isStopWatchRun)
  {
   stopWatch.Stop();
   isStopWatchRun = false;
   closed = true;
  }
  this.Close();
 }

 private void simulatorStopRunning()
 {
  updateProgressText = "no orders waiting";
 }

 private void simulatorUpdateProgress(DateTime startTime, BL.BO.OrderStatus previousStatus, BL.BO.OrderStatus nextStatus, int time, BL.BO.Order order)//handel the event the wake in Simulator class
 {
  updateProgressText = "orderID: " + order.ID
   + "\n previous status: " + previousStatus.ToString()
   + "\n next Status : " + nextStatus.ToString()
   + "\n start time: " + startTime.ToString()
   + "end time: " + startTime.AddSeconds(time).ToString()
   + "\n time: " + time + "seconds";
 }

 private void Window_Closing(object sender, CancelEventArgs e)
 {
  if (isStopWatchRun)
  {
   isStopWatchRun = false;
  }
 }

 private void doWorkCompleted(object? sender, RunWorkerCompletedEventArgs? e)
 {
  Simulator.Simulator.Stop();
  isStopWatchRun = false;
  worker.DoWork -= doWork;
  worker.ProgressChanged -= updateStopWatch;
  worker.RunWorkerCompleted -= doWorkCompleted;
  Simulator.Simulator.updateProgress -= simulatorUpdateProgress;
  Simulator.Simulator.StopRunning -= simulatorStopRunning;
 }

 private void goBackButton_Click(object sender, RoutedEventArgs e)
 {
  new MainWindow().Show();
  this.Close();
 }

 private void onClosing(object sender, CancelEventArgs e)
 {
   if(!closed)
     e.Cancel = true;
 }

}

