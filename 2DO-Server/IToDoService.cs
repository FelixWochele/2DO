using _2DO_Server.Database.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace _2DO_Server
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IToDoService
    {
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
        bool AddCategory(Categorie customer);
        [OperationContract]
        bool RemoveCategory(Categorie customer);
        [OperationContract]
        List<Categorie> GetAllCategorys();
    }
}
