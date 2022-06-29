using _2DO_Client.Framework;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Entity.Core.Metadata.Edm;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _2DO_Client.ViewModels;
using _2DO_Client.Views;
using Microsoft.IdentityModel.Protocols.WsAddressing;
using ServiceReference1;

namespace _2DO_Client.Controller
{
    public class ListSelectorController : SubmoduleControler
    {

        private ListSelectorViewModel mViewModel;
        private ListSelectorView mView;

        private MainWindowController mMainController;

        public ListSelectorController()
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
            return mViewModel;
        }

        public void AddElement(ServiceReference1.TaskList taskList)
        {
            mViewModel.TaskListModels.Add(taskList);
        }

        public ServiceReference1.TaskList GetSelectedElement()
        {
            return mViewModel.SelectedItem;
        }

        public void RemoveElement(TaskList getSelectedElement)
        {
            mViewModel.TaskListModels.Remove(getSelectedElement);
        }

        public void ResetModelList()
        {
            mViewModel.TaskListModels.Clear();
        }

        private void _viewModel_PropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(ListSelectorViewModel.SelectedItem))
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
