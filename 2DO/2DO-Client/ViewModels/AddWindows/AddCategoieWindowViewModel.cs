using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using _2DO_Client.Framework;

namespace _2DO_Client.ViewModels.AddWindows
{
    public class AddCategoieWindowViewModel : ViewModelBase
    {
        public ServiceReference1.Categorie Model { get; set; } = new();

        public ICommand OkCommand { get; set; }

        public ICommand CancelCommand { get; set; }

        public string Name
        {
            get => Model.Name;
            set
            {
                Model.Name = value;
                OnPropertyChanged();
            }
        }
    }
}
