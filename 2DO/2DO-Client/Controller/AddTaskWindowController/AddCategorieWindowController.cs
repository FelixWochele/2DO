using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _2DO_Client.Framework;
using _2DO_Client.ViewModels.AddWindows;
using _2DO_Client.Views;

namespace _2DO_Client.Controller
{
    public class AddCategorieWindowController
    {


        public AddCategoieWindowViewModel mViewModel;
        public AddCategorieWindowView mView;

        public AddCategorieWindowController()
        {
            mView = new AddCategorieWindowView();
            mViewModel = new AddCategoieWindowViewModel();

            mView.DataContext = mViewModel;

            mViewModel.OkCommand = new RelayCommand(ExecuteOkCommand);
            mViewModel.CancelCommand = new RelayCommand(ExecuteCancelCommand);
        }

        private void ExecuteOkCommand(object obj)
        {
            mView.DialogResult = true;
            mView.Close();
        }

        private void ExecuteCancelCommand(object obj)
        {
            mView.DialogResult = false;
            mView.Close();
        }

        public ServiceReference1.Categorie AddCategorie()
        {
            var ret = mView.ShowDialog();

            if (ret == true)
            {
                return mViewModel.Model;
            }
            else
            {
                return null;
            }
        }

        public ServiceReference1.Categorie ChangeCategorie(ServiceReference1.Categorie cat)
        {
            mViewModel.Model = cat;

            var ret = mView.ShowDialog();

            if (ret == true)
            {
                return mViewModel.Model;
            }
            else
            {
                return null;
            }
        }

    }
}
