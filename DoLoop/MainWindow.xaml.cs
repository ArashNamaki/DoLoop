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

    private void AddTaskInput_MouseDoubleClick(object sender, MouseButtonEventArgs e)
    {
        if (TaskTypeGroup.Visibility == Visibility.Visible)
            TaskTypeGroup.Visibility = Visibility.Collapsed;
        else
            TaskTypeGroup.Visibility = Visibility.Visible;
    }
}
