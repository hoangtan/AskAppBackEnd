using AskAppBackEnd.Services;
using Autofac;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading.Tasks;

namespace AskAppBackEnd.Service.Test
{
    [TestClass()]
    public class UserServiceTest : TestBase
    {
        private IUserService _userService;
        public UserServiceTest(): base()
        {
            _userService = Container.Resolve<IUserService>();
                
        }

        [TestMethod()]
        public async Task CreateResetTicketAsyncTest()
        {
            //arrange
            //..

            //act 
            var ticket = await _userService.CreateResetTicketAsync(new Guid("4B65F44D-B946-4FCC-A00F-70A255AE5037"));
            //assert
            Assert.IsTrue(ticket != null && !String.IsNullOrEmpty(ticket.TokenHash));
        }
    }
}

