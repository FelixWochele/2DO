using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using _2DO_Client.Framework;
using ServiceReference1;
using Task = ServiceReference1.Task;

namespace _2DO_Client.ViewModels.AddWindows
{
    public class ConnectTaskToCategorieWindowViewModel : ViewModelBase
    {


        public ICommand OkCommand { get; set; }

        public ICommand CancelCommand { get; set; }

        public ObservableCollection<ServiceReference1.Task> TaskModels { get; set; } = new();

        private Task mSelectedItem;

        public Task SelectedItem
        {
            get
            {
                return mSelectedItem;
            }
            set
            {
                mSelectedItem = value;
                OnPropertyChanged();
            }
        }

    }
}
