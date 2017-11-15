using AspNetMvcDemo.Controllers;
using AspNetMvcDemo.Models;
using AspNetMvcDemo.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections;

// Learn more about testing controllers at
// https://docs.microsoft.com/en-us/aspnet/core/mvc/controllers/testing

namespace AspNetMvcDemo.Tests
{
    [TestClass]
    public class TestHeroesController
    {
        [TestMethod]
        public void TestHeroesList()
        {
            var testHeroes = new []
            {
                new Hero() { ID = 1, Name = "Foo", RealName = "Bar" }
            };

            // Note the use of Moq (=mocking framework) here. Learn more at
            // https://github.com/Moq/moq4/wiki/Quickstart
            var mockRepository = new Mock<IHeroRepository>();
            mockRepository.Setup(repo => repo.GetAll()).Returns(testHeroes);

            var controller = new HeroesController(mockRepository.Object);
            var result = controller.Index();

            Assert.IsInstanceOfType(result, typeof(ViewResult));
            CollectionAssert.AreEquivalent(testHeroes, (ICollection)((ViewResult)result).Model);
        }

        [TestMethod]
        public void TestHeroesDelete()
        {
            var testHeroes = new[]
            {
                new Hero() { ID = 1, Name = "Foo", RealName = "Bar" }
            };

            // Note the use of Moq (=mocking framework) here. Learn more at
            // https://github.com/Moq/moq4/wiki/Quickstart
            var mockRepository = new Mock<IHeroRepository>();
            mockRepository.Setup(repo => repo.GetById(0)).Returns(testHeroes[0]);
            mockRepository.Setup(repo => repo.Delete(0));

            var controller = new HeroesController(mockRepository.Object);
            var result = controller.DeleteConfirmed(0);

            // Make sure that Delete(0) has been called on the repository
            mockRepository.Verify(repo => repo.Delete(0), Times.Once);
            Assert.IsInstanceOfType(result, typeof(RedirectToActionResult));
        }
    }
}
