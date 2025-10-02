using System;
using DoLoop.Helpers;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using DoLoop.Services;
using DoLoop.Models;




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


        // Debounce helpers so we don't save on every tiny change at once

        private CancellationTokenSource _saveCts;
        private readonly TimeSpan _debounceDelay = TimeSpan.FromMilliseconds(450);
        public MainViewModel()
        {
            AddTaskCommand = new RelayCommand(_ => AddTask(), _ => !string.IsNullOrWhiteSpace(NewTaskText));
            DeleteTaskCommand = new RelayCommand(p =>
            {
                if (p is TaskItem t && Tasks.Contains(t)) Tasks.Remove(t);
            });

            var loaded = TaskStorage.Load();
            foreach(var task in loaded)
            {
                AttachItemHandler(task);
                Tasks.Add(task);
            }

            Tasks.CollectionChanged += Tasks_CollectionChanged;

            if (Application.Current != null)
                Application.Current.Exit += (s, e) => SaveNow();
        }

        private void Tasks_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            // Attach handlers for new items
            if (e.NewItems != null)
                foreach (TaskItem item in e.NewItems)
                    AttachItemHandler(item);

            // Detach handlers for removed items
            if (e.OldItems != null)
                foreach (TaskItem item in e.OldItems)
                    DetachItemHandler(item);

            // Debounce save
            DebounceSave();
        }

        private void AttachItemHandler(TaskItem item)
        {
            if (item == null) return;
            item.PropertyChanged += TaskItem_PropertyChanged;
        }

        private void DetachItemHandler(TaskItem item)
        {
            if (item == null) return;
            item.PropertyChanged -= TaskItem_PropertyChanged;
        }

        private void TaskItem_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            // Save whenever a property changes (IsCompleted, Description, Type)
            DebounceSave();
        }

        private void DebounceSave()
        {
            // Cancel previous scheduled save and schedule new one
            _saveCts?.Cancel();
            _saveCts = new CancellationTokenSource();
            var token = _saveCts.Token;

            _ = Task.Run(async () =>
            {
                try
                {
                    await Task.Delay(_debounceDelay, token);
                    if (token.IsCancellationRequested) return;
                    // do actual save on background thread (TaskStorage uses file IO)
                    TaskStorage.Save(Tasks);
                }
                catch (TaskCanceledException) { /* ignore */ }
                catch (Exception ex)
                {
                    // optionally log or report error. Avoid throwing on UI thread.
                    System.Diagnostics.Debug.WriteLine("Save failed: " + ex);
                }
            }, token);
        }

        // Force immediate save (used on app exit)
        private void SaveNow()
        {
            try
            {
                _saveCts?.Cancel(); // cancel pending debounce
                TaskStorage.Save(Tasks);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("SaveNow failed: " + ex);
            }
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
