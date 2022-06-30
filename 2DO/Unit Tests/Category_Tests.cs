using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ServiceReference1;

namespace Unit_Tests
{
    [TestClass]
    public class Category_Tests
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

        public Categorie GetTestCategory()
        {
            var testData = new Categorie();
            testData.Name = "TestName";
            testData.ID = 16;
            testData.Version = 17;

            return testData;
        }

        [TestMethod]
        public void AddTest()
        {
            InitNHibernate();

            mToDoService.AddCategorie(GetTestCategory());

            var dbnObj = mToDoService.GetAllCategories();

            Assert.AreEqual(1, dbnObj.Length);

            Assert.AreEqual(GetTestCategory(), dbnObj.First());
        }

        [TestMethod]
        public void DeleteTest()
        {
            InitNHibernate();

            mToDoService.AddCategorie(GetTestCategory());

            var ret = mToDoService.GetAllCategories().First();

            mToDoService.RemoveCategorie(ret);

            var nullValue = mToDoService.GetAllCategories();

            Assert.AreEqual(null, nullValue);
        }

        [TestMethod]
        public void ChangeTest()
        {
            InitNHibernate();

            mToDoService.AddCategorie(GetTestCategory());

            var ret = mToDoService.GetAllCategories().First();

            ret.Name = "OtherName";

            mToDoService.AddCategorie(ret);

            var catRet = mToDoService.GetAllCategories();

            Assert.AreEqual(1, catRet.Length);

            Assert.AreEqual(ret, catRet.First());
        }
    }
}
