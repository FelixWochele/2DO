using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _2DO_Client.Framework;
using _2DO_Client.ViewModels.AddWindows;
using _2DO_Client.Views;
using _2DO_Client.Views.AddTaskWindowViews;

namespace _2DO_Client.Controller
{
    public class AddTaskListWindowController
    {


        public AddTaskListWindowViewModel mViewModel;
        public AddTaskListWindowView mView;

        public AddTaskListWindowController()
        {
            mView = new AddTaskListWindowView();
            mViewModel = new AddTaskListWindowViewModel();

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

        public ServiceReference1.TaskList AddTaskList()
        {
            var ret = mView.ShowDialog();

            if (ret == true)
            {
                return mViewModel.Model;
            }

            return null;
        }

        public ServiceReference1.TaskList ChangeTaskList(ServiceReference1.TaskList cat)
        {
            mViewModel.Model = cat;

            var ret = mView.ShowDialog();

            if (ret == true)
            {
                return mViewModel.Model;
            }

            return null;
        }
    }
}
