using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using _2DO_Client.Framework;
using Task = ServiceReference1.Task;

namespace _2DO_Client.ViewModels
{
    public class AddTaskWindowViewModel : ViewModelBase
    {
        public Task Model { get; set; } = new();

        public ICommand OkCommand { get; set; }

        public ICommand CancelCommand { get; set; }

        public ObservableCollection<string> ReminderMinutesSelector { get; set; } = new();

        public ObservableCollection<string> PrioritySelector { get; set; } = new();

        public AddTaskWindowViewModel()
        {
            ReminderMinutesSelector.Add("-");
            ReminderMinutesSelector.Add("Bei Erinnerung");
            ReminderMinutesSelector.Add("5 Minuten vorher");
            ReminderMinutesSelector.Add("10 Minuten vorher");
            ReminderMinutesSelector.Add("1 Stunde vorher");
            ReminderMinutesSelector.Add("12 Stunden vorher");
            //ReminderMinutesSelector.Add("1 Tag vorher");
            //ReminderMinutesSelector.Add("2 Tage vorher");

            PrioritySelector.Add("-");
            PrioritySelector.Add("25%");
            PrioritySelector.Add("50%");
            PrioritySelector.Add("75%");
            PrioritySelector.Add("100%");

            CreationDate = DateTime.Now;
            DueDate = DateTime.Now + TimeSpan.FromDays(2);
        }

        public int Version
        {
            get => Model.Version;
            set
            {
                Model.Version = value;
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


        public string Comment
        {
            get => Model.Comment;
            set
            {
                Model.Comment = value;
                OnPropertyChanged();
            }
        }

        public bool State
        {
            get => Model.State;
            set
            {
                Model.State = value;
                OnPropertyChanged();
            }
        }


        public DateTime CreationDate
        {
            get => Model.CreationDate;
            set
            {
                Model.CreationDate = value;
                OnPropertyChanged();
            }
        }

        public DateTime DueDate
        {
            get => Model.DueDate;
            set
            {
                Model.DueDate = value;
                OnPropertyChanged();
            }
        }

        public string ReminderMinutes
        {
            get => parseReminderMinutesSI(Model.ReminderMinutes);
            set
            {
                Model.ReminderMinutes = parseReminderMinutesIS(value);
                OnPropertyChanged();
            }
        }

        #region ReminderMinutes Helper
        private int parseReminderMinutesIS(string value)
        {
            // GANZ KOMISCH HIER!!!!
            // Sobald die Zeilen unten einkommentiert werden, tritt eine Aufofac Nullpointer exception beim build auf???
            // Warum? 

            return value switch
            {
                "-" => 0,
                "Bei Erinnerung" => 1,
                "5 Minuten vorher" => 2,
                "10 Minuten vorher" => 3,
                "1 Stunde vorher" => 4,
                "12 Stunden vorher" => 5,
                //"1 Tag vorher" => 6,
                //"2 Tage vorher" => 7,
                _ => 0,
            };
        }

        private string parseReminderMinutesSI(int modelReminderMinutes)
        {
            return modelReminderMinutes switch
            {
                0 => "-",
                1 => "Bei Erinnerung",
                2 => "5 Minuten vorher",
                3 => "10 Minuten vorher",
                4 => "1 Stunde vorher",
                5 => "12 Stunden vorher",
                6 => "1 Tag vorher",
                7 => "2 Tage vorher",
                _ => "null",
            };
        }
        #endregion

        public string Priority
        {
            get => parsePriorityIS(Model.Priority);
            set
            {
                Model.Priority = parsePrioritySI(value);
                OnPropertyChanged();
            }
        }

        #region Priority Helper

        public int parsePrioritySI(string val)
        {
            switch (val)
            {
                case "-":
                    return 0;
                case "25%":
                    return 25;
                case "50%":
                    return 50;
                case "75%":
                    return 75;
                case "100%":
                    return 100;
            }
            return 0;
        }

        public string parsePriorityIS(int val)
        {
            switch (val)
            {
                case 0:
                    return "-";
                case 25:
                    return "25%";
                case 50:
                    return "50%";
                case 75:
                    return "75%";
                case 100:
                    return "100%";
            }

            return null;
        }

        #endregion
    }
}
