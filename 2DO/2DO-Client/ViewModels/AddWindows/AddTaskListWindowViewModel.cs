using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using _2DO_Client.Framework;

namespace _2DO_Client.ViewModels.AddWindows
{
    public class AddTaskListWindowViewModel : ViewModelBase
    {

        public ServiceReference1.TaskList Model { get; set; } = new();

        public ICommand OkCommand { get; set; }

        public ICommand CancelCommand { get; set; }

        public string Comment
        {
            get => Model.Comment;
            set
            {
                Model.Comment = value;
                OnPropertyChanged();
            }
        }

        public string Description
        {
            get => Model.Description;
            set
            {
                Model.Description = value;
                OnPropertyChanged();
            }
        }
    }
}
