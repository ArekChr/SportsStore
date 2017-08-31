using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SportsStore.WebUI.Controllers;
using SportsStore.WebUI.Infrastructure.Abstract;
using SportsStore.WebUI.Models;
using System.Web.Mvc;

namespace SportsStore.UnitTest
{
    [TestClass]
    public class AdnimSecurityTests
    {
        [TestMethod]
        public void Can_Login_With_Valid_Credentials()
        {
            // przygotowanie - utworzenie imitacji dostawcy uwierzytelniania
            Mock<IAuthProvider> mock = new Mock<IAuthProvider>();
            mock.Setup(m => m.Authenticate("admin", "sekret")).Returns(true);

            // przygotowanie - utworzenie modelu widoku
            LoginViewModel model = new LoginViewModel
            {
                UserName = "admin",
                Password = "sekret"
            };

            // przygotowanie - utworzenie kontrolera
            AccountController target = new AccountController(mock.Object);

            //działanie - uwierzytlenie z użyciem prawidłowych danych
            ActionResult result = target.Login(model, "/MyURL");

            //asercje
            Assert.IsInstanceOfType(result, typeof(RedirectResult));
            Assert.AreEqual("/MyURL", ((RedirectResult)result).Url);
        }

        [TestMethod]
        public void Cannot_Login_With_Invalid_Credentials()
        {
            //przygotowanie - utworzenie imitacji dostawcy uwierzytelniania
            Mock<IAuthProvider> mock = new Mock<IAuthProvider>();
            mock.Setup(m => m.Authenticate("nieprawidłowyUżytkownik", "nieprawidłoweHasło")).Returns(false);

            // przygotowanie - utworzenie modelu widoku
            LoginViewModel model = new LoginViewModel
            {
                UserName = "nieprawidłowyUżytkownik",
                Password = "nieprawidłoweHasło"
            };

            // przygotowanie - utworzenie kontrolera
            AccountController target = new AccountController(mock.Object);

            //działanie - uwierzytelnienie z uzyciem nieprawidłowych danych
            ActionResult result = target.Login(model, "/MyURL");

            //asercje
            Assert.IsInstanceOfType(result, typeof(ViewResult));
            Assert.IsFalse(((ViewResult)result).ViewData.ModelState.IsValid);
        }
    }
}
