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

        public ICommand ListCategorieTaskListAddButton { get; set; }
        public ICommand ListCategorieTaskListDeleteButton { get; set; }
        public ICommand ListCategorieTaskListEditButton { get; set; }
		public ICommand TaskAddButton { get; set; }
        public ICommand TaskDeleteButton { get; set; }
        public ICommand TaskEditButton { get; set; }

        public ICommand TaskExportButton { get; set; }
        public ICommand TaskImportButton { get; set; }

        public ObservableCollection<string> SortSelector { get; set; } = new();

        public ObservableCollection<ServiceReference1.Task> TaskModels { get; set; } = new ObservableCollection<ServiceReference1.Task>();

		public ServiceReference1.Task SelectedItem { get; set; }

        public MainWindowViewModel()
        {
            //Set standard values
            SortSelector.Add("-");
            SortSelector.Add("Anlagedatum");
            SortSelector.Add("Alphabetisch");
            SortSelector.Add("Fälligkeitsdatum");
            SortSelector.Add("Priorität");
            SortSelector.Add("Wichtigste");
        }

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

        public int mSelection;

        public string SortSelect
        {
            get => SelectionParserIS(mSelection);
            set
            {
                mSelection = SelectionParserSI(value);
                OnPropertyChanged();
            }
        }

        #region Parser

        private int SelectionParserSI(string value)
        {
            // GANZ KOMISCH HIER!!!!
            // Sobald die Zeilen unten einkommentiert werden, tritt eine Aufofac Nullpointer exception beim build auf???
            // Warum? 

            return value switch
            {
                "-" => 0,
                "Anlagedatum" => 1,
                "Alphabetisch" => 2,
                "Fälligkeitsdatum" => 3,
                "Priorität" => 4,
                "Wichtigste" => 5,
                _ => 0,
            };
        }

        private string SelectionParserIS(int modelReminderMinutes)
        {
            return modelReminderMinutes switch
            {
                0 => "-",
                1 => "Anlagedatum",
                2 => "Alphabetisch",
                3 => "Fälligkeitsdatum",
                4 => "Priorität",
                5 => "Wichtigste",
                _ => "null",
            };
        }
        #endregion
    }
}
