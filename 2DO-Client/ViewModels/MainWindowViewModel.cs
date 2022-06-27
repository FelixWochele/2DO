using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using _2DO_Client.Framework;

namespace _2DO_Client.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {


        private ViewModelBase mActiveViewModel;

        public ICommand ShowListSelectorCommand { get; set; }
        public ICommand ShowCategorieSelectorCommand { get; set; }

		public ICommand ListCategorieAddButton { get; set; }

		public ICommand ListCategorieDeleteButton { get; set; }

		public ICommand TaskAddButton { get; set; }

		public ICommand TaskDeleteButton { get; set; }

        public ObservableCollection<ServiceReference1.Task> TaskModels { get; set; } = new ObservableCollection<ServiceReference1.Task>();

		public ServiceReference1.Task SelectedItem { get; set; }

		public ViewModelBase ActiveViewModel
		{
			get { return mActiveViewModel; }

			set
			{
				if (mActiveViewModel == value)
					return;

				mActiveViewModel = value;
				OnPropertyChanged("ActiveViewModel");
			}
		}
    }
}
