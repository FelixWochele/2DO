using System.Collections.Generic;
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
            areaCategorysSelectorController.setInstance(this);
            areaListSelectorController = mApplication.Container.Resolve<ListSelectorController>();
            areaListSelectorController.Initialize();
            areaListSelectorController.setInstance(this);

            mMainWindowViewModel.ShowCategorieSelectorCommand = new RelayCommand(ExecuteCategorieSelectorCommand);
            mMainWindowViewModel.ShowListSelectorCommand = new RelayCommand(ExecuteListSelectorCommand);

            mMainWindowViewModel.ListCategorieTaskListAddButton = new RelayCommand(ExecuteCategorieTaskListAddCommand);
            mMainWindowViewModel.ListCategorieTaskListDeleteButton = new RelayCommand(ExecuteCategorieTaskListDeleteCommand);
            mMainWindowViewModel.ListCategorieTaskListEditButton = new RelayCommand(ExecuteCategorieTaskListEditCommand);

            mMainWindowViewModel.TaskAddButton = new RelayCommand(ExecuteTaskAddCommand, CanExecuteTaskAddCommand);
            mMainWindowViewModel.TaskDeleteButton = new RelayCommand(ExecuteTaskDeleteCommand, CanExecuteTaskDeleteDeleteCommand);
            mMainWindowViewModel.TaskEditButton = new RelayCommand(ExecuteTaskEditCommand, CanExecuteTaskEditCommand);

            //Start WCF Service
            mServiceController = serviceController.mToDoService;
            var test = mServiceController.InitNHibernate();

            //Init Submodule -> List
            ExecuteListSelectorCommand(new object());

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
            areaListSelectorController.ResteSelectedItem();
        }

        private void ExecuteListSelectorCommand(object obj)
        {
            ListIsActive = true;
            mMainWindowViewModel.ActiveViewModel = areaListSelectorController.Initialize();
            areaListSelectorController.ResteSelectedItem();
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
                    var tempEle = areaListSelectorController.GetSelectedElement();

                    var tasks = mServiceController.GetAllTasks()
                        .Where(x => x.TasklistID == areaListSelectorController.GetSelectedElement().ID).ToList();

                    foreach (var task in tasks)
                    {
                        mServiceController.RemoveTask(task);
                    }
                    UpdateTasksFromDB();

                    //areaListSelectorController.RemoveElement(areaListSelectorController.GetSelectedElement());
                    mServiceController.RemoveLTaskist(areaListSelectorController.GetSelectedElement());
                    UpdateTaskListListFromDB();
                }
            }
            else
            {
                if (areaCategorysSelectorController.GetSelectedElement() != null)
                {
                    var tempEle = areaCategorysSelectorController.GetSelectedElement();

                    var tasks = mServiceController.GetAllCategoriesToTasks()
                        .Where(x => x.CategoryID == tempEle.ID).ToList();

                    foreach (var task in tasks)
                    {
                        mServiceController.RemoveCategorieToTask(task);
                    }

                    //TODO: PRÜFEN

                    //areaCategorysSelectorController.RemoveElement(areaCategorysSelectorController.GetSelectedElement());
                    mServiceController.RemoveCategorie(areaCategorysSelectorController.GetSelectedElement());
                    UpdateCategoriesFromDB();
                }
            }
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

                var taskList = mServiceController.GetAllTaskLists()
                    .Where(x => x.ID == areaListSelectorController.GetSelectedElement().ID).FirstOrDefault();

                var newTaskList = mAddTaskListWindowController.ChangeTaskList(taskList);

                mServiceController.AddTaskList(newTaskList);

                UpdateTaskListListFromDB();

                /*
                var retTask = areaListSelectorController.GetSelectedElement();

                mAddTaskListWindowController.ChangeTaskList(retTask);

                if (retTask != null)
                {
                    areaListSelectorController.RemoveElement(areaListSelectorController.GetSelectedElement());
                    areaListSelectorController.AddElement(retTask);
                }
                */
            }
            else
            {
                AddCategorieWindowController mAddCategorieWindowController =
                    mApplication.Container.Resolve<AddCategorieWindowController>();


                var catList = mServiceController.GetAllCategories()
                    .Where(x => x.ID == areaCategorysSelectorController.GetSelectedElement().ID).FirstOrDefault();

                var newCatList = mAddCategorieWindowController.ChangeCategorie(catList);

                mServiceController.AddCategorie(newCatList);

                /*
                var retTask = areaCategorysSelectorController.GetSelectedElement();

                mAddCategorieWindowController.ChangeCategorie(retTask);

                if (retTask != null)
                {
                    areaCategorysSelectorController.RemoveElement(areaCategorysSelectorController.GetSelectedElement());
                    areaCategorysSelectorController.AddElement(retTask);
                }
                */
            }
        }
        //If TaskList selection is changed
        public void SelectCmd(object obj)
        {
            UpdateTasksFromDB();
        }

        #endregion

        #region Commands Tasks

        //Task
        private void ExecuteTaskAddCommand(object obj)
        {
            if (areaListSelectorController.GetSelectedElement() != null)
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
            else if (areaCategorysSelectorController.GetSelectedElement() != null)
            {

                ConnectTaskToCategorieWindowController mConnectTaskToCategorieWindowController =
                    mApplication.Container.Resolve<ConnectTaskToCategorieWindowController>();

                var test = mServiceController.GetAllTasks().ToList();

                mConnectTaskToCategorieWindowController.setList(test);

                var task = mConnectTaskToCategorieWindowController.Test();

                var relationA = new ServiceReference1.TaskToCategorieRelations();

                relationA.CategoryID = areaCategorysSelectorController.GetSelectedElement().ID;
                relationA.TaskID = task.ID;

                mServiceController.AddCategorieToTask(relationA);

            }
        }
        private bool CanExecuteTaskAddCommand(object obj)
        {
            return (areaCategorysSelectorController.GetSelectedElement() != null) || (areaListSelectorController.GetSelectedElement()!= null);
        }

        private void ExecuteTaskDeleteCommand(object obj)
        {
            if (areaListSelectorController.GetSelectedElement() != null)
            {
                if (mMainWindowViewModel.SelectedItem != null)
                {
                    //mMainWindowViewModel.TaskModels.Remove(mMainWindowViewModel.SelectedItem);
                    mServiceController.RemoveTask(mMainWindowViewModel.SelectedItem);
                    UpdateTasksFromDB();
                }
            }
        }

        private bool CanExecuteTaskDeleteDeleteCommand(object obj)
        {
            return (areaCategorysSelectorController.GetSelectedElement() != null) || (areaListSelectorController.GetSelectedElement() != null);
        }

        private void ExecuteTaskEditCommand(object obj)
        {

            AddTaskWindowController mAddTaskWindowController =
                mApplication.Container.Resolve<AddTaskWindowController>();

            var task = mServiceController.GetAllTasks().Where(x => x.ID == mMainWindowViewModel.SelectedItem.ID).FirstOrDefault();

            var newtask = mAddTaskWindowController.ChangeTask(mMainWindowViewModel.SelectedItem);

            task = newtask;

            mServiceController.AddTask(task);

            UpdateTasksFromDB();

            /*
            var retTask = mAddTaskWindowController.ChangeTask(mMainWindowViewModel.SelectedItem);

            if (retTask != null)
            {
                mMainWindowViewModel.TaskModels.Remove(mMainWindowViewModel.SelectedItem);
                mMainWindowViewModel.TaskModels.Add(retTask);
            }
            */
        }

        private bool CanExecuteTaskEditCommand(object obj)
        {
            return mMainWindowViewModel.SelectedItem != null;
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
            areaCategorysSelectorController.ResetModelList();

            var allCategories = mServiceController.GetAllCategories();

            foreach (var category in allCategories)
            {
                areaCategorysSelectorController.AddElement(category);
            }
        }

        private void UpdateTasksFromDB()
        {
            mMainWindowViewModel.TaskModels.Clear();

            if (areaListSelectorController.GetSelectedElement() != null)
            {
                var allTasks = mServiceController.GetAllTasks().Where(x => x.TasklistID == areaListSelectorController.GetSelectedElement().ID).ToList();

                foreach (var tasks in allTasks)
                {
                    mMainWindowViewModel.TaskModels.Add(tasks);
                }
            }
            else if(areaCategorysSelectorController != null)
            {
                List<int> category2taskId = mServiceController.GetAllCategoriesToTasks().Where(x => x.CategoryID == areaCategorysSelectorController.GetSelectedElement().ID).Select(x => x.TaskID).ToList();

                var allTasks = mServiceController.GetAllTasks().Where(x => category2taskId.Contains(x.TasklistID)).ToList();

                foreach (var tasks in allTasks)
                {
                    mMainWindowViewModel.TaskModels.Add(tasks);
                }
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
