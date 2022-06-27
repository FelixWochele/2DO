using _2DO_Client.Framework;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _2DO_Client.ViewModels;
using Microsoft.IdentityModel.Protocols.WsAddressing;

namespace _2DO_Client.Controller
{
    public class ListSelectorController : SubmoduleControler
    {

        private ListSelectorViewModel mViewModel;

        public override ViewModelBase Initialize()
        {
            mViewModel = new ListSelectorViewModel();
            return mViewModel;
        }

        public void AddElement(ServiceReference1.TaskList taskList)
        {
            mViewModel.TaskListModels.Add(taskList);
        }
    }
}
