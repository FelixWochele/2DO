using _2DO_Server.Database.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace _2DO_Service
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in both code and config file together.
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class ToDoService : IToDoService
    {
        //Lists
        public bool AddTaskList(TaskList customer)
        {
            return true;
        }

        public bool RemoveLTaskist(TaskList customer)
        {
            throw new NotImplementedException();
        }

        public List<TaskList> GetAllTaskLists()
        {
            throw new NotImplementedException();
        }

        //Items
        public bool AddTask(Task customer)
        {
            throw new NotImplementedException();
        }
        public bool RemoveTask(Task customer)
        {
            throw new NotImplementedException();
        }
        public List<Task> GetAllTasks()
        {
            throw new NotImplementedException();
        }

        //Category
        public bool AddCategorie(Categorie customer)
        {
            throw new NotImplementedException();
        }
        public bool RemoveCategorie(Categorie customer)
        {
            throw new NotImplementedException();
        }
        public List<Categorie> GetAllCategories()
        {
            throw new NotImplementedException();
        }

    }
}
