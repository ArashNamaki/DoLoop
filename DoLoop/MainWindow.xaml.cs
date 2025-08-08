using Accessibility;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DoLoop;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();

        AddTaskInput.GotFocus += AddTaskInput_GotFocus;
    }

    public void AddTaskToList()
    {
        var taskItem = new TaskItem
        {
            Text = AddTaskInput.Text,
            Icon = "O",
            Color = (Brush)FindResource("QuickTask")
        };
        TaskItemList.Children.Add(new TaskItemControl(taskItem));
        AddTaskInput.Clear();
    }

    private void AddTaskInput_GotFocus(object sender, RoutedEventArgs e)
    {

    }

    private void MinimizeBtn_Click(object sender, RoutedEventArgs e)
    {
        this.WindowState = WindowState.Minimized;
    }

    private void CloseBtn_Click(object sender, RoutedEventArgs e)
    {
       Application.Current.Shutdown();
    }

    private void AddTaskInput_KeyDown(object sender, KeyEventArgs e)
    {
        if ((e.Key == Key.Enter))
            AddTaskToList();
            
    }
    private void AddTaskButton_Click(object sender, RoutedEventArgs e)
    {
        AddTaskToList();
    }
    private void AppBar_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
        this.DragMove();
    }

    private void BoardTitleIcon_Click(object sender, RoutedEventArgs e)
    {
        if (SideBar.Visibility == Visibility.Visible)
            SideBar.Visibility = Visibility.Collapsed;
        else
            SideBar.Visibility = Visibility.Visible;
    }

    private void Window_KeyDown(object sender, KeyEventArgs e)
    {

    }


}
