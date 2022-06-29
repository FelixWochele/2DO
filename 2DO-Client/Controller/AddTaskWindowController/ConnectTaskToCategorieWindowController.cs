using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
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

        }
    }
}
