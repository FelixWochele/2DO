using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;
using _2DO_Client.Controller;
using Autofac;
using Autofac.Core;

namespace _2DO_Client
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {

        public IContainer Container { get; set; }

        protected override void OnStartup(StartupEventArgs e)
        {
            ContainerBuilder containerBuilder = new ContainerBuilder();

            containerBuilder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
                .Where(t => t.IsClass && (t.Namespace.Contains("Controller")
                                          || t.Namespace.Contains("ViewModel")
                                          || t.Namespace.Contains("Service")
                                          || t.Namespace.Contains("View")));

            //This
            containerBuilder.RegisterInstance(this);

            Container = containerBuilder.Build();

            var main = Container.Resolve<MainWindowController>();

            main.Initialize();
        }

    }
}
