using CRUD_application_2.Controllers;
using CRUD_application_2.Models;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.UI;

namespace CRUD_application_2.Tests.Controllers
{
    [TestFixture]
    public class UserControllerTests
    {
        private UserController controller;
        private List<User> users;

        [SetUp]
        public void Setup()
        {
            // Initialize your controller here and setup test data
            UserController.userlist = new List<User>
            {
                new User { Id = 1, Name = "John Doe", Email = "john@example.com" },
                new User { Id = 2, Name = "Jane Doe", Email = "jane@example.com" },
                new User { Id = 3, Name = "Mike Smith", Email = "mike@example.com" }
            };
            controller = new UserController();
        }

        [Test]
        public void Index_ReturnsViewWithUsers()
        {
            // Act
            var result = controller.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            var model = result.Model as List<User>;
            Assert.AreEqual(2, model.Count); // Assuming you have 2 users in your setup
        }

        [Test]
        public void Details_WithValidId_ReturnsUser()
        {
            // Arrange
            int testUserId = 1; // Assuming this ID exists in your setup

            // Act
            var result = controller.Details(testUserId) as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            var model = result.Model as User;
            Assert.AreEqual(testUserId, model.Id); // Assuming User has an Id property
        }

        [Test]
        public void Create_Post_ValidUser_AddsUserAndRedirects()
        {
            // Arrange
            var newUser = new User { /* Initialize user properties */ };

            // Act
            var result = controller.Create(newUser) as RedirectToRouteResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Index", result.RouteValues["action"]);
            Assert.AreEqual(3, UserController.userlist.Count); // Assuming the user was added successfully
        }

        [Test]
        public void Edit_Get_WithValidId_ReturnsUser()
        {
            // Arrange
            int testUserId = 1; // Assuming this ID exists in your setup

            // Act
            var result = controller.Edit(testUserId) as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            var model = result.Model as User;
            Assert.AreEqual(testUserId, model.Id); // Assuming User has an Id property
        }

        [Test]
        public void Edit_Post_WithValidData_UpdatesUserAndRedirects()
        {
            // Arrange
            var updatedUser = new User { /* Initialize user properties with updated data */ };
            int testUserId = 1; // Assuming this ID exists in your setup

            // Act
            var result = controller.Edit(testUserId, updatedUser) as RedirectToRouteResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Index", result.RouteValues["action"]);
            // Additional assertions to verify the user was updated correctly
        }

        [Test]
        public void Delete_Get_WithValidId_ReturnsUser()
        {
            // Arrange
            int testUserId = 1; // Assuming this ID exists in your setup

            // Act
            var result = controller.Delete(testUserId) as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            var model = result.Model as User;
            Assert.AreEqual(testUserId, model.Id); // Assuming User has an Id property
        }

        [Test]
        public void Delete_Post_WithValidId_RemovesUserAndRedirects()
        {
            // Arrange
            int testUserId = 1; // Assuming this ID exists in your setup

            // Act
            var result = controller.Delete(testUserId, new FormCollection()) as RedirectToRouteResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Index", result.RouteValues["action"]);
            // Additional assertions to verify the user was removed correctly
        }

        [Test]
        public void Search_WithMatchingTerm_ReturnsFilteredUsers()
        {
            // Arrange
            string searchTerm = "John";

            // Act
            var result = controller.Search(searchTerm) as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            var model = result.Model as List<User>;
            Assert.IsNotNull(model);
            Assert.AreEqual(1, model.Count); // Expecting one user to match the search term
            Assert.IsTrue(model.Any(u => u.Name.Contains(searchTerm) || u.Email.Contains(searchTerm)));
        }

        [Test]
        public void Search_WithNoMatchingTerm_ReturnsEmptyResult()
        {
            // Arrange
            string searchTerm = "NonExistingUser";

            // Act
            var result = controller.Search(searchTerm) as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            var model = result.Model as List<User>;
            Assert.IsNotNull(model);
            Assert.AreEqual(0, model.Count); // Expecting no users to match the search term
        }
    }
}
