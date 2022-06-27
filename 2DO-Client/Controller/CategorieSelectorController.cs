using _2DO_Client.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _2DO_Client.ViewModels;
using _2DO_Client.Views;

namespace _2DO_Client.Controller
{
    public class CategorieSelectorController : SubmoduleControler
    {
        private CategorieSelectorViewModel mCategorieSelectorViewModel;

        public override ViewModelBase Initialize()
        {
            mCategorieSelectorViewModel = new CategorieSelectorViewModel();
            return mCategorieSelectorViewModel;
        }

        public void AddElement(ServiceReference1.Categorie categorieList)
        {
            mCategorieSelectorViewModel.CategorieModels.Add(categorieList);
        }
    }
}
