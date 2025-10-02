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
using DoLoop.Models;

namespace DoLoop
{
    /// <summary>
    /// Interaction logic for TaskItemControl.xaml
    /// </summary>
    public partial class TaskItemControl : UserControl
    {
        public TaskItemControl(TaskItem task)
        {
            InitializeComponent();
            this.DataContext = task;
        }

        private void TaskItem_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            TaskItem.Opacity = TaskItem.Opacity == 1 ? 0.4 : 1;
        }

        private void TaskItem_MouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            // Right Click = Task info Edit accesibility
        }

        private void ItemDeleteButton_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            var parent = this.Parent as Panel;
            parent?.Children.Remove(this);
        }
    }
}
