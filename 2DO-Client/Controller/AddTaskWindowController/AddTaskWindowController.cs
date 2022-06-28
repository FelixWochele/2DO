using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _2DO_Client.Annotations;
using _2DO_Client.Framework;
using _2DO_Client.ViewModels;
using _2DO_Client.Views;

namespace _2DO_Client.Controller
{
    internal class AddTaskWindowController
    {

        public AddTaskWindowViewModel mViewModel;
        public AddTaskWindowView mView;

        public AddTaskWindowController()
        {
            mView = new AddTaskWindowView();
            mViewModel = new AddTaskWindowViewModel();

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

        public ServiceReference1.Task AddTask()
        {
            var ret = mView.ShowDialog();

            if (ret == true)
            {
                return mViewModel.Model;
            }

            return null;
        }

        public ServiceReference1.Task ChangeTask(ServiceReference1.Task task)
        {
            mViewModel.Model = task;

            var ret = mView.ShowDialog();

            if (ret == true)
            {
                return mViewModel.Model;
            }

            return null;
        }
    }
}
