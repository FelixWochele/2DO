using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _2DO_Client.Framework;
using ServiceReference1;

namespace _2DO_Client.ViewModels
{
    public class ListSelectorViewModel : ViewModelBase
    {
        public ObservableCollection<ServiceReference1.TaskList> TaskListModels { get; set; } = new ObservableCollection<ServiceReference1.TaskList>();

        public TaskList SelectedItem { get; set; }

    }
}
