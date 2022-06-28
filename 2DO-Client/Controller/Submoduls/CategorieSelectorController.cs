using _2DO_Client.Framework;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _2DO_Client.ViewModels;
using _2DO_Client.Views;
using ServiceReference1;

namespace _2DO_Client.Controller
{
    public class CategorieSelectorController : SubmoduleControler
    {
        private CategorieSelectorViewModel mViewModel;

        public override ViewModelBase Initialize()
        {
            if (mViewModel == null)
            {
                mViewModel = new CategorieSelectorViewModel();
            }

            return mViewModel;
        }

        public void AddElement(ServiceReference1.Categorie categorieList)
        {
            mViewModel.CategorieModels.Add(categorieList);
        }

        public ServiceReference1.Categorie GetSelectedElement()
        {
            return mViewModel.SelectedItem;
        }

        public void RemoveElement(Categorie getSelectedElement)
        {
            mViewModel.CategorieModels.Remove(getSelectedElement);
        }

        public void ResetModelList()
        {
            mViewModel.CategorieModels.Clear();
        }
    }
}
