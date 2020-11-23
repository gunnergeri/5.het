
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitTestExample.Controllers;
using UnitTestExample.Abstractions;
using System.Activities;
using Moq;
using UnitTestExample.Entities;

namespace week009
{
    public class AccountControllerTestFixture
    {
        [
                Test,
                TestCase("abcd1234", false),
                TestCase("irf@uni-corvinus", false),
                TestCase("irf.uni-corvinus.hu", false),
                TestCase("irf@uni-corvinus.hu", true)
        ]
        public void TestValidateEmail(string email, bool expectedresult)
        {
            //Arrange
            var accountController = new AccountController();
            //Act
            var actualResult = accountController.ValidateEmail(email);
            //Assert
            Assert.AreEqual(expectedresult, actualResult);

        }



        [
          Test,
          TestCase("Aaaaaaaaaa", false),
          TestCase("AAAAAAAAA", false),
          TestCase("aaaaaaaaa", false),
          TestCase("aAa3", false),
          TestCase("AaAaAaAaA9", true),
        ]

        public void TestPassword(string password, bool expectedResult)
        {

            //Arrange
            AccountController accountController = new AccountController();

            //Act
            var actualResult = accountController.ValidatePassword(password);

            //Assert
            Assert.AreEqual(expectedResult, actualResult);
        }


        [
            Test,
            TestCase("irf@uni-corvinus.hu", "Aaaa10000"),
            TestCase("irf@uni-corvinus.hu", "AaAaAaAa111"),
        ]
        public void TestRegisterHappyPath(string email, string password)
        {
            //Arrange

            var accountController = new AccountController();

            var accountServiceMock = new Mock<IAccountManager>(MockBehavior.Strict);
            accountServiceMock
                .Setup(m => m.CreateAccount(It.IsAny<Account>()))
                .Returns<Account>(a => a);


            //Act

            var actualResult = accountController.Register(email, password);


            //Assert

            Assert.AreEqual(email, actualResult.Email);

            Assert.AreEqual(password, actualResult.Password);

            Assert.AreNotEqual(Guid.Empty, actualResult.ID);

            accountServiceMock.Verify(m => m.CreateAccount(actualResult), Times.Once);
        }
    }


}
