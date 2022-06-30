using _2DO_Client.Framework;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
        private CategorieSelectorView mView;

        private MainWindowController mMainController;

        public CategorieSelectorController()
        {
            mViewModel = new();
            mView = new();

            mView.DataContext = mViewModel;

            mViewModel.PropertyChanged += _viewModel_PropertyChanged;
        }

        public void setInstance(MainWindowController mainController)
        {
            mMainController = mainController;
        }


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

        private void _viewModel_PropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(CategorieSelectorViewModel.SelectedItem))
            {
                mMainController.SelectCmd(new object());
            }
        }

        public void ResteSelectedItem()
        {
            mViewModel.SelectedItem = null;
        }
    }
}
