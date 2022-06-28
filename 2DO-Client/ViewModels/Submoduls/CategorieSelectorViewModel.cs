using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using _2DO_Client.Framework;
using ServiceReference1;

namespace _2DO_Client.ViewModels
{
    public class CategorieSelectorViewModel : ViewModelBase
    {

        public ObservableCollection<ServiceReference1.Categorie> CategorieModels { get; set; } = new();

        public Categorie SelectedItem { get; set; }

    }
}
