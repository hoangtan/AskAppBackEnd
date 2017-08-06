using AskAppBackEnd.Services;
using Autofac;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AskAppBackEnd.Service.Test
{
    [TestClass]
    public class UtilityServiceTest : TestBase
    {
        private IUtilityService _utilityService;

        public UtilityServiceTest(): base()
        {
            _utilityService = Container.Resolve<IUtilityService>();
        }

        [TestMethod]
        public void SendMailTest()
        {
            //Arrange
            //...
            //Act
            _utilityService.SendEmail("hello@hello.com", "test email");
            //Assert
        }
    }
}
