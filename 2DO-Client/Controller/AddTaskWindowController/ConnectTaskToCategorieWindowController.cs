using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using _2DO_Client.Framework;
using _2DO_Client.ViewModels.AddWindows;
using _2DO_Client.Views.AddTaskWindowViews;

namespace _2DO_Client.Controller
{
    public class ConnectTaskToCategorieWindowController
    {

        private ConnectTaskToCategoryWindowView mView;
        private ConnectTaskToCategorieWindowViewModel mViewModel;

        public ConnectTaskToCategorieWindowController()
        {
            mViewModel = new();
            mView = new();

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


        public ServiceReference1.Task Test()
        {
            var ret = mView.ShowDialog();

            if (ret == true)
            {
                return mViewModel.SelectedItem;
            }

            return null;
        }

    }
}
