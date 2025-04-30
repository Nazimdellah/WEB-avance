using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using mock.depart.Controllers;
using mock.depart.Models;
using mock.depart.Services;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace catsController.Tests
{
    [TestClass()]
    public class CatsControllerTests
    {
        [TestMethod()]
        public void CatsControllerTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void Delete_CatNotFound()

        {
            Mock<CatsService> serviceMock = new Mock<CatsService>();
            // Notez l'utilisation de CallBase = true
            // On veut un véritable objet CatsController et changer son comportement seulement pour la propriété UserId!
            Mock<CatsController> controller = new Mock<CatsController>(serviceMock.Object) { CallBase = true };
            serviceMock.Setup(c => c.Get(It.IsAny<int>())).Returns(value: null);
            controller.Setup(c => c.UserId).Returns("1");


            var actionResult = controller.Object.DeleteCat(0);

            var result = actionResult.Result as NotFoundResult;

            Assert.IsNotNull(result);





        }
        [TestMethod()]
        public void Delete_WrongOwner()

        {
            Mock<CatsService> serviceMock = new Mock<CatsService>();
            // Notez l'utilisation de CallBase = true
            // On veut un véritable objet CatsController et changer son comportement seulement pour la propriété UserId!
            Mock<CatsController> controller = new Mock<CatsController>(serviceMock.Object) { CallBase = true };
            controller.Setup(c => c.UserId).Returns("1");
            Cat nouveauChat = new Cat();
            nouveauChat.CatOwner = new CatOwner();
            nouveauChat.CatOwner.Id = "2";
            serviceMock.Setup(c => c.Get(It.IsAny<int>())).Returns(value: nouveauChat);
            controller.Object.DeleteCat(1);
            var actionResult = controller.Object.DeleteCat(0);
            var result = actionResult.Result as BadRequestObjectResult;
            Assert.IsNotNull(result);
            Assert.AreEqual("Cat is not yours", result.Value);







        }
    }
}