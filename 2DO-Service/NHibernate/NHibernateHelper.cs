// //////////////////////////////////////////////////////////////////////////////////////////////////////
// FileName: NHibernateHelper.cs
// Author : Felix Wochele
// Created On : 21062022
// Description : Helper for DB Access
// //////////////////////////////////////////////////////////////////////////////////////////////////////

using System;
using System.Diagnostics;
using System.Reflection;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;

namespace _2DO_Service.NHibernate
{
    public class NHibernateHelper : INHibernateHelper
    {

        private static ISessionFactory mSessionFactory;

        public static string DatabaseFile = "Assets/Wonderlist.db3";


        private ISessionFactory SessionFactory
        {
            get
            {
                if (mSessionFactory == null)
                    InitializeSessionFactory();

                return mSessionFactory;
            }
        }

        public ISession OpenSession()
        {
            return SessionFactory.OpenSession();
        }

        private void InitializeSessionFactory()
        {
            Trace.WriteLine("test");
            mSessionFactory = Fluently.Configure()
                .Database(SQLiteConfiguration.Standard.UsingFile(DatabaseFile).ShowSql)
                .Mappings(m => m.FluentMappings.AddFromAssembly(Assembly.GetExecutingAssembly())
                    .Conventions.Add(FluentNHibernate.Conventions.Helpers.DefaultLazy.Never()))
                .BuildSessionFactory();

            Trace.WriteLine("DB Connection established!");
        }

    }
}