using _2DO_Client.Framework;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _2DO_Client.ViewModels;
using Microsoft.IdentityModel.Protocols.WsAddressing;
using ServiceReference1;

namespace _2DO_Client.Controller
{
    public class ListSelectorController : SubmoduleControler
    {

        private ListSelectorViewModel mViewModel;

        public override ViewModelBase Initialize()
        {
            if (mViewModel == null)
            {
                mViewModel = new ListSelectorViewModel();
            }
            
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
    }
}
