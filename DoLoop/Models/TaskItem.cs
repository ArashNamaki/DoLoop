using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace DoLoop
{
    public enum TaskType
    {
        Normal,Important,Quick
    }
    public class TaskItem : INotifyPropertyChanged
    {
        private string _icon { get; set; }   
        private string _description { get; set; }    
        private TaskType _type { get; set; }
        private readonly bool IsCompleted;

        public string Icon { get => _icon; set { _icon = value; OnPropertyChanged(); } }
        public string Description { get => _description; set { _description = value; OnPropertyChanged(); } }    

        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChanged([CallerMemberName] string name = null) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

    }
}
