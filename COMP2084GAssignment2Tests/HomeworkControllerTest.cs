using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using COMP2084GAssignment2.Controllers;
using Microsoft.AspNetCore.Mvc;
using COMP2084GAssignment2.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.InMemory;

namespace COMP2084GAssignment2Tests
{
    [TestClass]
    public class HomeworkControllerTest
    {
        //make a moq database context for test methods
        private PlannerContext _context;
        HomeworkController homeworkController;
        List<Homework> homework = new List<Homework>();
        DateTime dateTime = DateTime.MinValue;

        // a global setup that runs before every test
        [TestInitialize]
        public void TestInitialize()
        {
            // make an in memory database to be referenced by the tests
            var workwork = new DbContextOptionsBuilder<PlannerContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            _context = new PlannerContext(workwork);

            // make foriegn key dependant table references
            var course = new Course // fake course
            {
                CourseId = 1,
                Name = "Mainframe"
            };
            var assignment = new Assignment // fake assignment
            {
                AssignmentId = 1,
                Name = "Project"
            };

            // test data
            homework.Add(new Homework
            {
                HomeworkId = 1,
                AssignmentId = assignment.AssignmentId,
                CourseId = course.CourseId,
                Due = dateTime,
                Description = "Lots of work."

            });
            homework.Add(new Homework
            {
                HomeworkId = 2,
                AssignmentId = assignment.AssignmentId,
                CourseId = course.CourseId,
                Due = dateTime,
                Description = "Round 2."

            });
            homework.Add(new Homework
            {
                HomeworkId = 3,
                AssignmentId = assignment.AssignmentId,
                CourseId = course.CourseId,
                Due = DateTime.Now,
                Description = "Round 3."

            });

            //add the moq data to the moq context
            foreach (var work in homework)
            {
                _context.Add(work);
            }
            _context.SaveChanges();

            // make the controller with test data
            homeworkController = new HomeworkController(_context);
        }

        [TestMethod]
        // Test method to see if the view loads
        public void LoadIndexView()
        {
            //setup


            //process
            var result = homeworkController.Index();
            result.Wait();

            var viewResult = (ViewResult)result.Result;


            //check
            Assert.AreEqual(viewResult.ViewName, "Index");
        }
        [TestMethod]
        // Test method to see if the index view loads the data
        public void LoadIndexData()
        {
            //setup


            //process
            var result = homeworkController.Index();
            result.Wait();

            var viewResult = (ViewResult)result.Result;

            List<Homework> model = (List<Homework>)viewResult.Model;

            //check
            CollectionAssert.AreEqual(model, homework);
        }

        //
        [TestMethod]
        // Test method to see if the index loads the data
        public void LoadDetailsView()
        {
            //setup


            //process
            var result = homeworkController.Details(1);
            result.Wait();

            var viewResult = (ViewResult)result.Result;

            //check
            Assert.AreEqual(viewResult.ViewName, "Details");
        }
        [TestMethod]
        // Test method to see if the details view loads the data
        public void LoadDetailsData()
        {
            //setup


            //process
            var result = homeworkController.Details(1);
            result.Wait();

            var viewResult = (ViewResult)result.Result;

            List<Homework> model = new List<Homework>();
            model.Add((Homework)viewResult.Model);

            Homework[] specific = model.ToArray();

            //check
            Assert.IsTrue(homework.Contains(specific[0]));
        }
        [TestMethod]
        // Test method to see if the details view loads the data and gets the correct index
        public void LoadDetailsIndex()
        {
            //setup


            //process
            var result = homeworkController.Details(1);
            result.Wait();

            var viewResult = (ViewResult)result.Result;

            List<Homework> model = new List<Homework>();
            model.Add((Homework)viewResult.Model);

            Homework[] specific = model.ToArray();

            //check
            Assert.IsTrue(homework.IndexOf(specific[0]) == 0);
        }

        [TestMethod]
        // Test method to check for a wrong input returning not found
        public void LoadDetailsNull()
        {
            //setup


            //process
            var result = homeworkController.Details(null).Result;

            //check
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }
        [TestMethod]
        // Test method to check for a wrong input returning not found
        public void LoadDetailsWrong()
        {
            //setup


            //process
            var result = homeworkController.Details(-1).Result;

            //check
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        //
        // A bunch of generic load tests to cover whether the pages not covered load
        //
        [TestMethod]
        // Test method to see if the details view loads the data and gets the correct index
        public void LoadCreateView()
        {
            //setup


            //process
            var result = homeworkController.Create();

            var viewResult = (ViewResult)result;

            //check
            Assert.AreEqual(viewResult.ViewName, "Create");
        }
        [TestMethod]
        // Test method to see if the details view loads the data and gets the correct index
        public void LoadDeleteView()
        {
            //setup


            //process
            var result = homeworkController.Delete(1);
            result.Wait();

            var viewResult = (ViewResult)result.Result;

            //check
            Assert.AreEqual(viewResult.ViewName, "Delete");
        }
        [TestMethod]
        // Test method to see if the details view loads the data and gets the correct index
        public void LoadEditView()
        {
            //setup


            //process
            var result = homeworkController.Edit(1);
            result.Wait();

            var viewResult = (ViewResult)result.Result;

            //check
            Assert.AreEqual(viewResult.ViewName, "Edit");
        }
    }
}
