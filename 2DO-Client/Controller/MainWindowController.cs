using _2DO_Client.ViewModels;
using _2DO_Client.Views;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http.Headers;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using _2DO_Client.Service;
using ServiceReference1;

namespace _2DO_Client.Controller
{
    public class MainWindowController
    {

        public MainWindowView mView;
        public MainWindowViewModel mViewModel;
        public App mApplication;

        private ToDoServiceClient mServiceController;

        public void Initialize()
        {
            mView.ShowDialog();
        }

        public MainWindowController(App app, ServiceController serviceController)
        {
            Trace.WriteLine("Test");

            mApplication = app;

            mView = new MainWindowView();
            mViewModel = new MainWindowViewModel();

            mView.DataContext = mViewModel;

            mServiceController = serviceController.mToDoService;

            Test();

            //var task = Task.Run(async () => await Test());


            //mViewModel.AddCommand = new RelayCommand(ExecuteAddCommand);
            //mViewModel.DeleteCommand = new RelayCommand(ExecuteDeleteCommand, CanExecuteDeleteCommand);
        }

        public async void Test()
        {
            Trace.WriteLine("Node1");

            var test = new TaskList();
            test.Comment = "TestComment";
            test.Description = "TestDesc";
            test.Version = 63;

            var res = await mServiceController.TestAsync();
            Trace.WriteLine(res);
            Trace.WriteLine("Node2");

        }
    }
}
