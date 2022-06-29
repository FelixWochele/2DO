using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using ServiceReference1;

namespace Unit_Tests
{
    [TestClass]
    public class List_Test
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

        public TaskList GetTestTaskList()
        {
            var testData = new TaskList();
            testData.Comment = "TestComment";
            testData.Description = "TestDescription";
            testData.ID = 36;
            testData.Version = 37;

            return testData;
        }

        [TestMethod]
        public void AddTest()
        {
            InitNHibernate();

            mToDoService.AddTaskList(GetTestTaskList());

            var dbnObj = mToDoService.GetAllTaskLists();

            Assert.AreEqual(1, dbnObj.Length);

            Assert.AreEqual(GetTestTaskList(), dbnObj.First());
        }

        [TestMethod]
        public void DeleteTest()
        {
            InitNHibernate();

            mToDoService.AddTaskList(GetTestTaskList());

            var ret = mToDoService.GetAllTaskLists().First();

            mToDoService.RemoveLTaskist(ret);

            var nullValue = mToDoService.GetAllTaskLists();

            Assert.AreEqual(null, nullValue);
        }

        [TestMethod]
        public void ChangeTest()
        {
            InitNHibernate();

            mToDoService.AddTaskList(GetTestTaskList());

            var ret = mToDoService.GetAllTaskLists().First();

            ret.Comment = "OtherName";
            ret.Description = "OtherDescription";

            mToDoService.AddTaskList(ret);

            var catRet = mToDoService.GetAllTaskLists();

            Assert.AreEqual(1, catRet.Length);

            Assert.AreEqual(ret, catRet.First());

        }
        
    }
}
