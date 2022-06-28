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

        public ObservableCollection<ServiceReference1.Categorie> CategorieModels { get; set; } = new ObservableCollection<ServiceReference1.Categorie>();

        public Categorie SelectedItem { get; set; }

    }
}
