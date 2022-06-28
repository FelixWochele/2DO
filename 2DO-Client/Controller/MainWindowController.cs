using System.Data.Entity.Infrastructure.Design;
using _2DO_Client.ViewModels;
using _2DO_Client.Views;
using System.Diagnostics;
using System.Linq;
using _2DO_Client.Framework;
using _2DO_Client.Service;
using Autofac;
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

        private bool ListIsActive; 

        public void Initialize()
        {
            mMainWindowView.ShowDialog();
        }

        public MainWindowController(App app, ServiceController serviceController)
        {
            mApplication = app;

            mMainWindowView = new MainWindowView();
            mMainWindowViewModel = new MainWindowViewModel();

            mMainWindowView.DataContext = mMainWindowViewModel;

            areaCategorysSelectorController = mApplication.Container.Resolve<CategorieSelectorController>();
            areaCategorysSelectorController.Initialize();
            areaListSelectorController = mApplication.Container.Resolve<ListSelectorController>();
            areaListSelectorController.Initialize();

            mMainWindowViewModel.ShowCategorieSelectorCommand = new RelayCommand(ExecuteCategorieSelectorCommand);
            mMainWindowViewModel.ShowListSelectorCommand = new RelayCommand(ExecuteListSelectorCommand);

            mMainWindowViewModel.ListCategorieTaskListAddButton = new RelayCommand(ExecuteCategorieTaskListAddCommand);
            mMainWindowViewModel.ListCategorieTaskListDeleteButton = new RelayCommand(ExecuteCategorieTaskListDeleteCommand);
            mMainWindowViewModel.ListCategorieTaskListEditButton = new RelayCommand(ExecuteCategorieTaskListEditCommand);

            mMainWindowViewModel.TaskAddButton = new RelayCommand(ExecuteTaskAddCommand, CanExecuteTaskAddChangeCommand);
            mMainWindowViewModel.TaskDeleteButton = new RelayCommand(ExecuteTaskDeleteCommand, CanExecuteTaskDeleteCommand);
            mMainWindowViewModel.TaskEditButton = new RelayCommand(ExecuteTaskEditCommand, CanExecuteTaskAddChangeCommand);

            //Init Submodule -> List
            ExecuteListSelectorCommand(new object());

            //Start WCF Service
            mServiceController = serviceController.mToDoService;

            var test = mServiceController.InitNHibernate();
            Trace.WriteLine(test);


            //Inital get the Data
            InitGetData();
            
            //Execute Some Test Methods
            //AddTestDataToAllWindows();
        }

        public void InitGetData()
        {
            UpdateTaskListListFromDB();

            UpdateCategoriesFromDB();

            UpdateTasksFromDB();
        }

        #region Command Category/TaskList

        //Submoduls
        private void ExecuteCategorieSelectorCommand(object obj)
        {
            ListIsActive = false;
            mMainWindowViewModel.ActiveViewModel = areaCategorysSelectorController.Initialize();
        }

        private void ExecuteListSelectorCommand(object obj)
        {
            ListIsActive = true;
            mMainWindowViewModel.ActiveViewModel = areaListSelectorController.Initialize();
        }

        //Category/TaskList
        private void ExecuteCategorieTaskListAddCommand(object obj)
        {
            if (ListIsActive)
            {

                AddTaskListWindowController mAddTaskListWindowController =
                    mApplication.Container.Resolve<AddTaskListWindowController>();

                mServiceController.AddTaskList(mAddTaskListWindowController.AddTaskList());

                UpdateTaskListListFromDB();

                /*
                //If DB should not work

                var retTask = mAddTaskListWindowController.AddTaskList();

                if (retTask != null)
                {
                    areaListSelectorController.AddElement(retTask);
                }
                */
            }
            else
            {
                AddCategorieWindowController mAddCategorieWindowController =
                    mApplication.Container.Resolve<AddCategorieWindowController>();

                mServiceController.AddCategorie(mAddCategorieWindowController.AddCategorie());

                UpdateCategoriesFromDB();

                /*
                //If DB should not Work

                var retTask = mAddCategorieWindowController.AddCategorie();

                if (retTask != null)
                {
                    areaCategorysSelectorController.AddElement(retTask);
                }
                */
            }
        }

        private void ExecuteCategorieTaskListDeleteCommand(object obj)
        {
            if (ListIsActive)
            {
                
                if (areaListSelectorController.GetSelectedElement() != null)
                {
                    //areaListSelectorController.RemoveElement(areaListSelectorController.GetSelectedElement());
                    mServiceController.RemoveLTaskist(areaListSelectorController.GetSelectedElement());
                    UpdateTaskListListFromDB();
                }
            }
            else
            {
                if (areaCategorysSelectorController.GetSelectedElement() != null)
                {
                    //areaCategorysSelectorController.RemoveElement(areaCategorysSelectorController.GetSelectedElement());
                    mServiceController.RemoveCategorie(areaCategorysSelectorController.GetSelectedElement());
                    UpdateCategoriesFromDB();
                }
            }

            //TODO: DB NOT ONLY DELETE TASKLIST -> DELTE TASK TO
        }

        private bool CanExecuteCategorieTaskListDeleteCommand(object obj)
        {
            if (ListIsActive)
            {
                return areaListSelectorController.GetSelectedElement() != null;
            }
            else
            {
                return areaCategorysSelectorController.GetSelectedElement() != null;
            }
        }

        private void ExecuteCategorieTaskListEditCommand(object obj)
        {
            if (ListIsActive)
            {
                AddTaskListWindowController mAddTaskListWindowController =
                    mApplication.Container.Resolve<AddTaskListWindowController>();

                var retTask = areaListSelectorController.GetSelectedElement();

                mAddTaskListWindowController.ChangeTaskList(retTask);

                if (retTask != null)
                {
                    areaListSelectorController.RemoveElement(areaListSelectorController.GetSelectedElement());
                    areaListSelectorController.AddElement(retTask);
                }
            }
            else
            {
                AddCategorieWindowController mAddCategorieWindowController =
                    mApplication.Container.Resolve<AddCategorieWindowController>();

                var retTask = areaCategorysSelectorController.GetSelectedElement();

                mAddCategorieWindowController.ChangeCategorie(retTask);

                if (retTask != null)
                {
                    areaCategorysSelectorController.RemoveElement(areaCategorysSelectorController.GetSelectedElement());
                    areaCategorysSelectorController.AddElement(retTask);
                }
            }

            //TODO: DB Delete
        }
        #endregion


        #region Commands Tasks

        //Task
        private void ExecuteTaskAddCommand(object obj)
        {
            AddTaskWindowController mAddTaskWindowController =
                mApplication.Container.Resolve<AddTaskWindowController>();

            // Foreign Key
            var task = mAddTaskWindowController.AddTask();

            task.TasklistID = areaListSelectorController.GetSelectedElement().ID;

            mServiceController.AddTask(task);

            UpdateTasksFromDB();

            /*
            var retTask = mAddTaskWindowController.AddTask();

            if (retTask != null)
            {
                mMainWindowViewModel.TaskModels.Add(retTask);
            }
            */
        }
        private bool CanExecuteTaskAddChangeCommand(object obj)
        {
            return (areaListSelectorController.GetSelectedElement() != null) && (areaCategorysSelectorController != null);
        }

        private void ExecuteTaskDeleteCommand(object obj)
        {
            if (mMainWindowViewModel.SelectedItem != null)
            {
                //mMainWindowViewModel.TaskModels.Remove(mMainWindowViewModel.SelectedItem);
                mServiceController.RemoveTask(mMainWindowViewModel.SelectedItem);
                UpdateTasksFromDB();
            }
        }

        private bool CanExecuteTaskDeleteCommand(object obj)
        {
            return (mMainWindowViewModel.SelectedItem != null);
        }

        private void ExecuteTaskEditCommand(object obj)
        {

            AddTaskWindowController mAddTaskWindowController =
                mApplication.Container.Resolve<AddTaskWindowController>();

            var retTask = mAddTaskWindowController.ChangeTask(mMainWindowViewModel.SelectedItem);

            if (retTask != null)
            {
                mMainWindowViewModel.TaskModels.Remove(mMainWindowViewModel.SelectedItem);
                mMainWindowViewModel.TaskModels.Add(retTask);
            }

            //TODO: DB Delete
        }
        #endregion

        #region DB Operations

        private void UpdateTaskListListFromDB()
        {
            areaListSelectorController.ResetModelList();

            var allTaskLists = mServiceController.GetAllTaskLists();

            foreach (var taskList in allTaskLists)
            {
                areaListSelectorController.AddElement(taskList);
            }
        }

        private void UpdateCategoriesFromDB()
        {
            var allCategories = mServiceController.GetAllCategories();

            foreach (var category in allCategories)
            {
                areaCategorysSelectorController.AddElement(category);
            }
        }

        private void UpdateTasksFromDB()
        {
            mMainWindowViewModel.TaskModels.Clear();

            var allTasks = mServiceController.GetAllTasks();

            foreach (var tasks in allTasks)
            {
                mMainWindowViewModel.TaskModels.Add(tasks);
            }
        }

        #endregion

        #region For debuging reasons only

        private void AddTestDataToAllWindows()
        {
            mMainWindowViewModel.TaskModels.Add(GetTaskTestData());
            areaCategorysSelectorController.AddElement(GetCategorieTestData());
            areaListSelectorController.AddElement(GetTaskListTestData());
        }
        
        public TaskList GetTaskListTestData()
        {
            var testData = new TaskList();
            testData.Comment = "TestComment";
            testData.Description = "TestDescription";
            testData.ID = 36;
            testData.Version = 37;
            return testData;
        }

        public Task GetTaskTestData()
        {
            var test = new Task();

            test.Comment = "TestComment";
            test.Description = "TestDescription";
            test.ID = 26;
            test.Priority = 27;
            test.State = true;
            test.Version = 28;

            return test;
        }

        public Categorie GetCategorieTestData()
        {

            var testData = new Categorie();
            testData.Name = "TestName";
            testData.ID = 16;
            testData.Version = 17;

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
