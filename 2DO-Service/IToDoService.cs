using _2DO_Server.Database.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace _2DO_Service
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IToDoService
    {

        [OperationContract]
        bool InitNHibernate();

        //Lists
        [OperationContract]
        bool AddTaskList(TaskList taskList);
        [OperationContract]
        bool RemoveLTaskist(TaskList taskList);
        [OperationContract]
        List<TaskList> GetAllTaskLists();

        //Items
        [OperationContract]
        bool AddTask(Task customer);
        [OperationContract]
        bool RemoveTask(Task customer);
        [OperationContract]
        List<Task> GetAllTasks();

        //Category
        [OperationContract]
        bool AddCategorie(Categorie customer);
        [OperationContract]
        bool RemoveCategorie(Categorie customer);
        [OperationContract]
        List<Categorie> GetAllCategories();

        //CategoryToTasks
        [OperationContract]
        bool AddCategorieToTask(TaskToCategorieRelations categorieToTask);
        [OperationContract]
        bool RemoveCategorieToTask(TaskToCategorieRelations categorieToTask);
        [OperationContract]
        List<TaskToCategorieRelations> GetAllCategoriesToTasks();
    }
}