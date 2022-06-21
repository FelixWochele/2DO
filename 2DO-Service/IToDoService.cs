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
        string Test();

        //Lists
        [OperationContract]
        bool AddTaskList(TaskList customer);
        [OperationContract]
        bool RemoveLTaskist(TaskList customer);
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
    }
}