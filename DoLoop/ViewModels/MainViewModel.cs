using DoLoop.Helpers;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace DoLoop.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<TaskItem> Tasks { get; } = new ObservableCollection<TaskItem>();

        private string _newTaskText;
        public string NewTaskText
        {
            get => _newTaskText;
            set { if (_newTaskText != value) { _newTaskText = value; OnPropertyChanged(); } }
        }
        private TaskType _selectedType = TaskType.Normal;
        public TaskType SelectedType
        {
            get => _selectedType;
            set { if (_selectedType != value) { _selectedType = value; OnPropertyChanged(); } }
        }

        public ICommand AddTaskCommand { get; }
        public ICommand DeleteTaskCommand { get; }

        public MainViewModel()
        {
            AddTaskCommand = new RelayCommand(_ => AddTask(), _ => !string.IsNullOrWhiteSpace(NewTaskText));
            DeleteTaskCommand = new RelayCommand(p =>
            {
                if (p is TaskItem t && Tasks.Contains(t)) Tasks.Remove(t);
            });
        }

        private void AddTask()
        {
            var item = new TaskItem
            {
                Description = NewTaskText.Trim(),
                Type = SelectedType,
                IsCompleted = false
            };
            Tasks.Add(item);
            NewTaskText = string.Empty;
        }


        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChanged([CallerMemberName] string n = null) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(n));
    }
}
