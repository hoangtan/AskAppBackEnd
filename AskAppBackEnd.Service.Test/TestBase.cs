using AskAppBackEnd.Autofac.Integration;
using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AskAppBackEnd.Service.Test
{
    public class TestBase
    {
        public IContainer Container;
        public TestBase()
        {
            Container = RegisterComponents();
        }

        private IContainer RegisterComponents()
        {
            var builder = AutofacRegister.Setup();
            return builder.Build();
        }
    }
}
