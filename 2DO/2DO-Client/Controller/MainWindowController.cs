using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity.Infrastructure.Design;
using _2DO_Client.ViewModels;
using _2DO_Client.Views;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows;
using _2DO_Client.Framework;
using _2DO_Client.Service;
using Autofac;
using ServiceReference1;
using Task = ServiceReference1.Task;
using System.Xml.Serialization;
using _2DO_Client.Controller.Mapping;
using Microsoft.Win32;

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
        private bool ListBtnActive = true;
        private bool CategoryBtnActive = true;


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

            IntiButtonsAndViews();

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

        #region Init
        public void IntiButtonsAndViews()
        {
            areaCategorysSelectorController = mApplication.Container.Resolve<CategorieSelectorController>();
            areaCategorysSelectorController.Initialize();
            areaCategorysSelectorController.setInstance(this);
            areaListSelectorController = mApplication.Container.Resolve<ListSelectorController>();
            areaListSelectorController.Initialize();
            areaListSelectorController.setInstance(this);

            mMainWindowViewModel.ShowCategorieSelectorCommand = new RelayCommand(ExecuteCategorieSelectorCommand, CanCategoryCommandExecuted);
            mMainWindowViewModel.ShowListSelectorCommand = new RelayCommand(ExecuteListSelectorCommand, CanListCommandExecuted);

            mMainWindowViewModel.ListCategorieTaskListAddButton = new RelayCommand(ExecuteCategorieTaskListAddCommand);
            mMainWindowViewModel.ListCategorieTaskListDeleteButton = new RelayCommand(ExecuteCategorieTaskListDeleteCommand);
            mMainWindowViewModel.ListCategorieTaskListEditButton = new RelayCommand(ExecuteCategorieTaskListEditCommand);

            mMainWindowViewModel.TaskAddButton = new RelayCommand(ExecuteTaskAddCommand, CanExecuteTaskAddCommand);
            mMainWindowViewModel.TaskDeleteButton = new RelayCommand(ExecuteTaskDeleteCommand, CanExecuteTaskDeleteDeleteCommand);
            mMainWindowViewModel.TaskEditButton = new RelayCommand(ExecuteTaskEditCommand, CanExecuteTaskEditCommand);

            mMainWindowViewModel.TaskExportButton = new RelayCommand(ExecuteTaskExportCommand, CanExecuteTaskExport);
            mMainWindowViewModel.TaskImportButton = new RelayCommand(ExecuteTaskImportCommand);

            mMainWindowViewModel.PropertyChanged += _viewModel_PropertyChanged;
        }

        public void InitGetData()
        {
            UpdateTaskListListFromDB();

            UpdateCategoriesFromDB();

            UpdateTasksFromDB();
        }

        #endregion

        #region Sorting

        private void _viewModel_PropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(MainWindowViewModel.SortSelect))
            {

                UpdateTasksFromDB();

                List<ServiceReference1.Task> temp = null;

                switch (mMainWindowViewModel.mSelection)
                {
                    case 1:
                        temp = mMainWindowViewModel.TaskModels.OrderBy(x => x.CreationDate).ToList();
                        mMainWindowViewModel.TaskModels.Clear();
                        break;
                    case 2:
                        temp = mMainWindowViewModel.TaskModels.OrderBy(x => x.Comment).ToList();
                        mMainWindowViewModel.TaskModels.Clear();
                        break;
                    case 3:
                        temp = mMainWindowViewModel.TaskModels.OrderBy(x => x.DueDate).ToList();
                        mMainWindowViewModel.TaskModels.Clear();
                        break;
                    case 4:
                        temp = mMainWindowViewModel.TaskModels.OrderBy(x => x.Priority).ToList();
                        mMainWindowViewModel.TaskModels.Clear();
                        break;
                    case 5:
                        temp = mMainWindowViewModel.TaskModels.OrderBy(x => x.DueDate).ThenByDescending(x => x.Priority).ToList();
                        mMainWindowViewModel.TaskModels.Clear();
                        break;
                    default:
                        break;
                }

                if (temp != null)
                {
                    foreach (var task in temp)
                    {
                        mMainWindowViewModel.TaskModels.Add(task);
                    }
                }
            }
        }
        #endregion

        #region Command Category/TaskList

        //Submoduls
        private void ExecuteCategorieSelectorCommand(object obj)
        {
            ListIsActive = false;
            mMainWindowViewModel.ActiveViewModel = areaCategorysSelectorController.Initialize();
            areaListSelectorController.ResteSelectedItem();
            ListBtnActive = true;
            CategoryBtnActive = false;
        }

        private void ExecuteListSelectorCommand(object obj)
        {
            ListIsActive = true;
            mMainWindowViewModel.ActiveViewModel = areaListSelectorController.Initialize();
            areaListSelectorController.ResteSelectedItem();
            ListBtnActive = false;
            CategoryBtnActive = true;
        }

        private bool CanListCommandExecuted(object obj)
        {
            return !ListIsActive;
        }
        private bool CanCategoryCommandExecuted(object obj)
        {
            return ListIsActive;
        }

        //Category/TaskList
        private void ExecuteCategorieTaskListAddCommand(object obj)
        {
            if (ListIsActive)
            {

                AddTaskListWindowController mAddTaskListWindowController =
                    mApplication.Container.Resolve<AddTaskListWindowController>();

                var tasklist = mAddTaskListWindowController.AddTaskList();

                if(TaskListIO(tasklist))
                {
                    if (tasklist != null)
                    {
                        mServiceController.AddTaskList(tasklist);

                        UpdateTaskListListFromDB();
                    }
                }
                else 
                {
                    MessageBox.Show("Flasche Eingabe!\n" +
                                    "Achten Sie darauf, dass Sie alle Felder ausgefüllt haben.", "Do2 - Error");
                }
            }
            else
            {
                AddCategorieWindowController mAddCategorieWindowController =
                    mApplication.Container.Resolve<AddCategorieWindowController>();

                var category = mAddCategorieWindowController.AddCategorie();

                if (CategoryIO(category))
                {
                    var doubleName = mServiceController.GetAllCategories()
                        .Where(x => x.Name.ToUpper().Equals(category.Name.ToUpper()))
                        .FirstOrDefault();

                    if (doubleName != null)
                    {
                        MessageBox.Show("Name ist bereits Vergeben!", "2Do");
                    }
                    else if (category != null)
                    {
                        if (CategoryIO(category))
                        {
                            mServiceController.AddCategorie(category);

                            UpdateCategoriesFromDB();
                        }
                        else
                        {
                            MessageBox.Show("Flasche Eingabe!\n" +
                                            "Achten Sie darauf, dass Sie alle Felder ausgefüllt haben.", "Do2 - Error");

                        }
                    }
                }
                else
                {
                    MessageBox.Show("Flasche Eingabe!\n" +
                                    "Achten Sie darauf, dass Sie alle Felder ausgefüllt haben.", "Do2 - Error");
                }
            }
        }

        private bool TaskListIO(TaskList taskList)
        {
            return (taskList != null) && (taskList.Comment != null) && (taskList.Description != null);
        }

        private bool CategoryIO(Categorie categorie)
        {
            return (categorie != null) && (categorie.Name != null);
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

                    var tasksID = tasks.Select(x => x.ID);

                    var catRelations = mServiceController.GetAllCategoriesToTasks()
                        .Where(x => tasksID.Contains(x.TaskID)).ToList();

                    foreach (var catRelation in catRelations)
                    {
                        mServiceController.RemoveCategorieToTask(catRelation);
                    }

                    UpdateTasksFromDB();

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

                    if (tasks.Count == 0)
                    {
                        //Eigentlich unnötig :D
                        foreach (var task in tasks)
                        {
                            mServiceController.RemoveCategorieToTask(task);
                        }

                        mServiceController.RemoveCategorie(areaCategorysSelectorController.GetSelectedElement());
                        UpdateCategoriesFromDB();
                    }
                    else
                    {
                        MessageBox.Show("Es sind noch Aufgabe in der Kategorie!\n" +
                                        "Achten Sie darauf, dass Sie erst alle Aufgabe der Kategorie löschen.", "Do2 - Error");
                    }
                    
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

                var temp = mAddTaskListWindowController.ChangeTaskList(taskList);

                if (temp != null)
                {
                    var newTaskList = mAddTaskListWindowController.ChangeTaskList(taskList);

                    mServiceController.AddTaskList(newTaskList);

                    UpdateTaskListListFromDB();
                }
            }
            else
            {
                AddCategorieWindowController mAddCategorieWindowController =
                    mApplication.Container.Resolve<AddCategorieWindowController>();


                var catList = mServiceController.GetAllCategories()
                    .Where(x => x.ID == areaCategorysSelectorController.GetSelectedElement().ID).FirstOrDefault();

                var test = mAddCategorieWindowController.ChangeCategorie(catList);

                var doubleName = mServiceController.GetAllCategories()
                    .Where(x => x.Name.ToUpper().Equals(test.Name.ToUpper()))
                    .FirstOrDefault();

                if (doubleName != null)
                {
                    MessageBox.Show("Name ist bereits Vergeben!", "2Do");
                }
                else if (test != null)
                {
                    var newCatList = mAddCategorieWindowController.ChangeCategorie(catList);

                    mServiceController.AddCategorie(newCatList);
                }
            }
        }
        //If TaskList selection is changed
        public void SelectCmd(object obj)
        {
            UpdateTasksFromDB();
            mMainWindowViewModel.SortSelect = "-";
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

                if(TaskIO(task))
                {
                    if (task != null)
                    {
                        task.TasklistID = areaListSelectorController.GetSelectedElement().ID;

                        mServiceController.AddTask(task);

                        UpdateTasksFromDB();
                    }
                }
                else
                {
                    MessageBox.Show("Flasche Eingabe!\n" +
                                    "Achten Sie darauf, dass Sie alle Felder ausgefüllt haben.", "Do2 - Error");
                }

            }
            else if (areaCategorysSelectorController.GetSelectedElement() != null)
            {

                ConnectTaskToCategorieWindowController mConnectTaskToCategorieWindowController =
                    mApplication.Container.Resolve<ConnectTaskToCategorieWindowController>();

                var listOfCategoryRelationsIDs = mServiceController.GetAllCategoriesToTasks().Where(x =>
                    x.CategoryID == areaCategorysSelectorController.GetSelectedElement().ID).Select(x => x.TaskID).ToList();

                var test = mServiceController.GetAllTasks().Where(x => !listOfCategoryRelationsIDs.Contains(x.ID)).OrderBy(x => x.Comment).ToList();

                mConnectTaskToCategorieWindowController.setList(test);

                var task = mConnectTaskToCategorieWindowController.Test();

                if(TaskIO(task))
                {
                    if (task != null)
                    {
                        var relationA = new ServiceReference1.TaskToCategorieRelations();

                        relationA.CategoryID = areaCategorysSelectorController.GetSelectedElement().ID;
                        relationA.TaskID = task.ID;

                        mServiceController.AddCategorieToTask(relationA);

                        UpdateTasksFromDB();
                    }
                }
                else
                {
                    MessageBox.Show("Flasche Eingabe!\n" +
                                    "Achten Sie darauf, dass Sie alle Felder ausgefüllt haben.", "Do2 - Error");
                }
                
            }
        }

        private bool CanExecuteTaskAddCommand(object obj)
        {
            return (areaCategorysSelectorController.GetSelectedElement() != null) || (areaListSelectorController.GetSelectedElement()!= null);
        }

        private bool TaskIO(ServiceReference1.Task task)
        {
            return (task != null) && (task.Comment != null) && (task.Description != null) && (task.Priority != null) && (task.State != null) && (task.DueDate != null) && (task.CreationDate != null);
        }

        private void ExecuteTaskDeleteCommand(object obj)
        {
            if (areaListSelectorController.GetSelectedElement() != null && mMainWindowViewModel.SelectedItem != null)
            {
                mServiceController.RemoveTask(mMainWindowViewModel.SelectedItem);
                UpdateTasksFromDB();
                
            }
            else if (areaCategorysSelectorController.GetSelectedElement() != null && mMainWindowViewModel.SelectedItem != null)
            {
                var tasksFromCat = mServiceController.GetAllCategoriesToTasks().
                    Where(x => x.CategoryID == areaCategorysSelectorController.GetSelectedElement().ID);

                var toDelete = tasksFromCat.Where(X => X.TaskID == mMainWindowViewModel.SelectedItem.ID).FirstOrDefault();

                mServiceController.RemoveCategorieToTask(toDelete);
                UpdateTasksFromDB();
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

            if (newtask != null && TaskIO(newtask))
            {
                task = newtask;

                mServiceController.AddTask(task);

                UpdateTasksFromDB();
            }
        }

        private bool CanExecuteTaskEditCommand(object obj)
        {
            return mMainWindowViewModel.SelectedItem != null;
        }

        #endregion

        #region Export/Import

        private void ExecuteTaskImportCommand(object obj)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "xml files (*.xml)|*.xml|All files (*.*)|*.*";
            openFileDialog.Title = "Bitte wähle eine Datei aus.";
            openFileDialog.ShowDialog();
            var filename = openFileDialog.FileName;

            XMLExportMap importMap = new XMLExportMap();

            //Deserialize employee
            var serializer = new XmlSerializer(typeof(XMLExportMap));

            if (!File.Exists(filename))
            {
                if (filename != null)
                    MessageBox.Show("Die angegebene Datei exisitiert nicht!", "2Do - Warnung");
            }
            else
            {
                try
                {
                    using (var stream = new FileStream(filename, FileMode.Open))
                    {
                        importMap = serializer.Deserialize(stream) as XMLExportMap;

                    }


                    importMap.TaskList.ID = 0;
                    mServiceController.AddTaskList(importMap.TaskList);

                    var taskWithId = mServiceController.GetAllTaskLists().Where(x =>
                            (x.Comment == importMap.TaskList.Comment) && (x.Description == importMap.TaskList.Description))
                        .FirstOrDefault();

                    foreach (var tasks in importMap.Tasks)
                    {
                        tasks.ID = 0;
                        tasks.TasklistID = taskWithId.ID;
                        mServiceController.AddTask(tasks);
                    }

                    UpdateTaskListListFromDB();
                    UpdateTasksFromDB();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    if (filename != null)
                        MessageBox.Show("Es ist ein Fehler Aufgetreten.\n" +
                                        "Bitte überprüfen Sie die ausgewählte Datei oder versuchen Sie es mit einer anderen Datei.", "2Do - Warnung");
                    
                }
            }
        }

        private void ExecuteTaskExportCommand(object obj)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "xml files (*.xml)|*.xml|All files (*.*)|*.*";
            saveFileDialog.Title = "Bitte wähle eine Datei aus.";
            saveFileDialog.ShowDialog();
            var filename = saveFileDialog.FileName;

            //Get TaskList with Tasks
            var actualTaskList = areaListSelectorController.GetSelectedElement();
            var tasks = mServiceController.GetAllTasks().Where(x => x.TasklistID == actualTaskList.ID).ToList();

            //Create xmlExportMap object
            var xmlObject = new XMLExportMap();
            xmlObject.TaskList = actualTaskList;
            xmlObject.Tasks = tasks;


            if (File.Exists(saveFileDialog.FileName))
            {
                MessageBox.Show("Die angegebene Datei existiert bereits!", "2Do - Warnung");
            }
            else
            {
                //Deserialize employee
                var serializer = new XmlSerializer(typeof(XMLExportMap));

                using (var stream = new FileStream(saveFileDialog.FileName, FileMode.CreateNew))
                {
                    serializer.Serialize(stream, xmlObject);
                }

                MessageBox.Show($"Exportiervorgang abgeschlossen!\nDie Datei wurde in \"{saveFileDialog.FileName}\" gespeichert!", "2Do - Hinweis");
            }
        }

        private bool CanExecuteTaskExport(object arg)
        {
            return (areaListSelectorController.GetSelectedElement() != null);
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
            else if(areaCategorysSelectorController.GetSelectedElement() != null)
            {
                List<int> category2taskId = mServiceController.GetAllCategoriesToTasks().Where(x => x.CategoryID == areaCategorysSelectorController.GetSelectedElement().ID).Select(x => x.TaskID).ToList();

                var allTasks = mServiceController.GetAllTasks().Where(x => category2taskId.Contains(x.ID)).ToList();

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
