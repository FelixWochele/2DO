using _2DO_Client.ViewModels;
using _2DO_Client.Views;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http.Headers;
using System.ServiceModel;
using System.Text;
using System.Threading;
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

        private CategorieSelectorController areaCategorysSelectorController;
        private ListSelectorController areaListSelectorController; 

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

            areaCategorysSelectorController = new CategorieSelectorController();
            areaListSelectorController = new ListSelectorController();

            mMainWindowView.DataContext = mMainWindowViewModel;

            mMainWindowViewModel.ShowCategorieSelectorCommand = new RelayCommand(ExecuteCategorieSelectorCommand);
            mMainWindowViewModel.ShowListSelectorCommand = new RelayCommand(ExecuteListSelectorCommand);
            mMainWindowViewModel.ListCategorieAddButton = new RelayCommand(ExecuteCategorieTaskAddCommand);
            mMainWindowViewModel.ListCategorieDeleteButton = new RelayCommand(ExecuteCategorieTaskDeleteCommand);
            mMainWindowViewModel.TaskAddButton = new RelayCommand(ExecuteTaskAddCommand);
            mMainWindowViewModel.TaskDeleteButton = new RelayCommand(ExecuteTaskDeleteCommand);

            //Init Submodule -> List
            ExecuteListSelectorCommand(new object());

            //Start WCF Service
            mServiceController = serviceController.mToDoService;

            var test = mServiceController.InitNHibernate();
            Trace.WriteLine(test);

            Thread.Sleep(5000);

            //Inital get the Data
            InitGetData();

            //Execute Some Test Methods
            //Test();
        }



        #region InitGetData
        public void InitGetData()
        {
            
            var allTasks = mServiceController.GetAllTasks();

            foreach (var task in allTasks)
            {
                mMainWindowViewModel.TaskModels.Add(task);
            }

            var allCategories = mServiceController.GetAllCategories();

            foreach (var category in allCategories)
            {
                areaCategorysSelectorController.AddElement(category);
            }

            var allTaskLists = mServiceController.GetAllTaskLists();

            foreach (var tasks in allTaskLists)
            {
                areaListSelectorController.AddElement(tasks);
            }
        }
        #endregion

        #region Commands
        private void ExecuteCategorieSelectorCommand(object obj)
        {
            mMainWindowViewModel.ActiveViewModel = areaCategorysSelectorController.Initialize();
        }

        private void ExecuteListSelectorCommand(object obj)
        {
            mMainWindowViewModel.ActiveViewModel = areaListSelectorController.Initialize();
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
        #endregion

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

        public void Test()
        {
            Trace.WriteLine("Node1");

            

            var res = mServiceController.InitNHibernate();




            Trace.WriteLine(res);
            Trace.WriteLine("Node2");

        }
        #endregion
    }
}
