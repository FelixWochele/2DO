using _2DO_Server.Database.Data;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using _2DO_Service.NHibernate;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;

namespace _2DO_Service
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in both code and config file together.
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class ToDoService : IToDoService
    {

        private INHibernateHelper nHibernateHelper { get; set; }

        public bool InitNHibernate()
        {
            nHibernateHelper = new NHibernateHelper();
            //nHibernateHelper.OpenSession();


            return true;
        }

        #region TaskLists
        public bool AddTaskList(TaskList taskList)
        {
            using (var session = nHibernateHelper.OpenSession())
            {
                var transaction = session.BeginTransaction();

                session.SaveOrUpdate(taskList);

                transaction.Commit();
            }

            return true;
        }

        public bool RemoveLTaskist(TaskList taskList)
        {

            using (var session = nHibernateHelper.OpenSession())
            {
                var transaction = session.BeginTransaction();

                session.Delete(taskList);

                transaction.Commit();
            }

            return true;
        }

        public List<TaskList> GetAllTaskLists()
        {

            using (var session = nHibernateHelper.OpenSession())
            {
                Trace.WriteLine("NODE2");

                var transaction = session.BeginTransaction();

                var returnList = session.QueryOver<TaskList>().List();

                transaction.Commit();

                return returnList as List<TaskList>;
            }
        }
        #endregion

        #region Tasks
        public bool AddTask(Task task)
        {

            using (var session = nHibernateHelper.OpenSession())
            {
                var transaction = session.BeginTransaction();

                session.SaveOrUpdate(task);

                transaction.Commit();
            }

            return true;
        }
        public bool RemoveTask(Task task)
        {

            using (var session = nHibernateHelper.OpenSession())
            {
                var transaction = session.BeginTransaction();

                session.Delete(task);

                transaction.Commit();
            }

            return true;
        }
        public List<Task> GetAllTasks()
        {
            using (var session = nHibernateHelper.OpenSession())
            {
                var transaction = session.BeginTransaction();

                var returnList = session.QueryOver<Task>().List();

                transaction.Commit();

                return returnList as List<Task>;
            }
        }
        #endregion

        #region Category
        public bool AddCategorie(Categorie categorie)
        {
            using (var session = nHibernateHelper.OpenSession())
            {
                var transaction = session.BeginTransaction();

                session.SaveOrUpdate(categorie);

                transaction.Commit();
            }

            return true;
        }
        public bool RemoveCategorie(Categorie categorie)
        {
            using (var session = nHibernateHelper.OpenSession())
            {
                var transaction = session.BeginTransaction();

                session.Delete(categorie);

                transaction.Commit();
            }

            return true;
        }
        public List<Categorie> GetAllCategories()
        {
            using (var session = nHibernateHelper.OpenSession())
            {
                var transaction = session.BeginTransaction();

                var returnList = session.QueryOver<Categorie>().List();

                transaction.Commit();

                return returnList as List<Categorie>;
            }
        }
        #endregion

        #region CategoryToTasks
        public bool AddCategorieToTask(TaskToCategorieRelations categorieToTask)
        {
            using (var session = nHibernateHelper.OpenSession())
            {
                var transaction = session.BeginTransaction();

                session.SaveOrUpdate(categorieToTask);

                transaction.Commit();
            }

            return true;
        }
        public bool RemoveCategorieToTask(TaskToCategorieRelations categorieToTask)
        {
            using (var session = nHibernateHelper.OpenSession())
            {
                var transaction = session.BeginTransaction();

                session.Delete(categorieToTask);

                transaction.Commit();
            }

            return true;
        }
        public List<TaskToCategorieRelations> GetAllCategoriesToTasks()
        {
            using (var session = nHibernateHelper.OpenSession())
            {
                var transaction = session.BeginTransaction();

                var returnList = session.QueryOver<TaskToCategorieRelations>().List();

                transaction.Commit();

                return returnList as List<TaskToCategorieRelations>;
            }
        }
        #endregion
    }
}
