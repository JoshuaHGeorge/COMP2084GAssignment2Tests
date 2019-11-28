using Microsoft.VisualStudio.TestTools.UnitTesting;
using COMP2084GAssignment2.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace COMP2084GAssignment2Tests
{
    [TestClass]
    public class HomeControllerTest
    {
        [TestMethod]
        public void LoadIndexView()
        {
            //setup
            var homeController = new HomeController();

            //process
            var result = (ViewResult)homeController.Index();

            //check
            Assert.AreEqual(result.ViewName, "Index");
        }

        [TestMethod]
        public void LoadPrivacyView()
        {
            //setup
            var homeController = new HomeController();

            //process
            var result = (ViewResult)homeController.Privacy();

            //check
            Assert.AreEqual(result.ViewName, "Privacy");
        }

        //[TestMethod]
        //public void LoadErrorView()
        //{
        //    //setup
        //    var homeController = new HomeController();

        //    //process
        //    var result = (ViewResult)homeController.Error();

        //    //check
        //    Assert.AreEqual(result.ViewName, "Error");
        //}
    }
}
