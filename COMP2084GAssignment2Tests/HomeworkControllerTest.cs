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
        public void LoadIndexView()
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
    }
}
