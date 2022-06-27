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
using _2DO_Client.Framework;
using _2DO_Client.Service;
using ServiceReference1;
using Task = ServiceReference1.Task;

namespace _2DO_Client.Controller
{
    public class MainWindowController
    {

        public MainWindowView mMainWindowView;
        public MainWindowViewModel mMainWindowViewModel;
        public App mApplication;

        private ToDoServiceClient mServiceController;

        public void Initialize()
        {
            mMainWindowView.ShowDialog();
        }

        public MainWindowController(App app, ServiceController serviceController)
        {
            Trace.WriteLine("Test");

            mApplication = app;

            mMainWindowView = new MainWindowView();
            mMainWindowViewModel = new MainWindowViewModel();

            mMainWindowView.DataContext = mMainWindowViewModel;

            mMainWindowViewModel.ShowCategorieSelectorCommand = new RelayCommand(ExecuteCategorieSelectorCommand);
            mMainWindowViewModel.ShowListSelectorCommand = new RelayCommand(ExecuteListSelectorCommand);
            mMainWindowViewModel.ListCategorieAddButton = new RelayCommand(ExecuteCategorieTaskAddCommand);
            mMainWindowViewModel.ListCategorieDeleteButton = new RelayCommand(ExecuteCategorieTaskDeleteCommand);
            mMainWindowViewModel.TaskAddButton = new RelayCommand(ExecuteTaskAddCommand);
            mMainWindowViewModel.TaskDeleteButton = new RelayCommand(ExecuteTaskDeleteCommand);

            //Init Submodule -> List
            ExecuteListSelectorCommand(new object());
            
            mMainWindowViewModel.TaskModels.Add(GetTaskTestData());

            


            //Start WCF Service
            //mServiceController = serviceController.mToDoService;

            //Test();



            //var task = Task.Run(async () => await Test());


            //mViewModel.AddCommand = new RelayCommand(ExecuteAddCommand);
            //mViewModel.DeleteCommand = new RelayCommand(ExecuteDeleteCommand, CanExecuteDeleteCommand);
        }


        private void ExecuteCategorieSelectorCommand(object obj)
        {
            CategorieSelectorController areaViewController = new CategorieSelectorController();
            mMainWindowViewModel.ActiveViewModel = areaViewController.Initialize();


            areaViewController.AddElement(GetCategorieTestData());
        }

        private void ExecuteListSelectorCommand(object obj)
        {
            ListSelectorController areaViewController = new ListSelectorController();
            mMainWindowViewModel.ActiveViewModel = areaViewController.Initialize();

            

            areaViewController.AddElement(GetTaskListTestData());
        }


        private void ExecuteCategorieTaskAddCommand(object obj)
        {
            throw new NotImplementedException();
            //TODO: DB ADD
        }

        private void ExecuteCategorieTaskDeleteCommand(object obj)
        {
            throw new NotImplementedException();
            //TODO: DB DELETE
        }

        private void ExecuteTaskAddCommand(object obj)
        {




            throw new NotImplementedException();
            //TODO: DB ADD
        }

        private void ExecuteTaskDeleteCommand(object obj)
        {
            //TODO: DB Delete

        }

        #region For Debuging Reasons

        public TaskList GetTaskListTestData()
        {
            var testData = new TaskList();
            testData.Comment = "TestComment";
            testData.Description = "Test";
            testData.ID = 10;
            testData.Version = 1;
            return testData;
        }

        public Task GetTaskTestData()
        {
            var test = new Task();

            test.Comment = "TestCom";
            test.Description = "TestDes";
            test.ID = 10;
            test.Priority = 10;
            test.State = true;
            test.Version = 1;

            return test;
        }

        public Categorie GetCategorieTestData()
        {

            var testData = new Categorie();
            testData.Name = "Felix";
            testData.ID = 1;
            testData.Version = 2;

            return testData;
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
        #endregion
    }
}
