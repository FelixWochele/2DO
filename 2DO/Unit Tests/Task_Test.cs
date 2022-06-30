using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ServiceReference1;
using Task = ServiceReference1.Task;

namespace Unit_Tests
{
    [TestClass]
    public class Task_Test
    {
        public ToDoServiceClient mToDoService { get; set; }
        private string serviceAddress = "http://localhost:8733/2DO-Service/Test";

        public void InitNHibernate()
        {
            //WCF Connetction
            BasicHttpBinding binding = new BasicHttpBinding();
            //binding.Security.Mode = SecurityMode.Message;
            var address = new EndpointAddress(serviceAddress);
            mToDoService = new ToDoServiceClient(binding, address);
        }

        public Task GetTestTask()
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

        [TestMethod]
        public void AddTest()
        {
            InitNHibernate();

            mToDoService.AddTask(GetTestTask());

            var dbnObj = mToDoService.GetAllTasks();

            Assert.AreEqual(1, dbnObj.Length);

            Assert.AreEqual(GetTestTask(), dbnObj.First());
        }

        [TestMethod]
        public void DeleteTest()
        {
            InitNHibernate();

            mToDoService.AddTask(GetTestTask());

            var ret = mToDoService.GetAllTasks().First();

            mToDoService.RemoveTask(ret);

            var nullValue = mToDoService.GetAllTasks();

            Assert.AreEqual(null, nullValue);
        }

        [TestMethod]
        public void ChangeTest()
        {
            InitNHibernate();

            mToDoService.AddTask(GetTestTask());

            var ret = mToDoService.GetAllTasks().First();

            ret.Comment = "OtherName";
            ret.Description = "OtherDescription";

            mToDoService.AddTask(ret);

            var catRet = mToDoService.GetAllTasks();

            Assert.AreEqual(1, catRet.Length);

            Assert.AreEqual(ret, catRet.First());
        }
    }
}
