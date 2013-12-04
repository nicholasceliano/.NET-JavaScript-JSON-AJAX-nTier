using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Hess.Corporate.GHGPortal.Business;
namespace GHGPortal.UnitTests
{
    [TestClass]
    public class ActiveDirectoryTest
    {
        [TestMethod]
        public void CheckByUserId()
        {
            Employees emps = Employees.GetEmployeesByUserId("sttbatch");
            Assert.AreEqual(emps.Count, 1);
        }

        [TestMethod]
        public void CheckByGroup()
        {
            Employees emps = Employees.GetEmployeesByGroup("STTWebProduction");
            Assert.IsNotNull(emps);
        }

        [TestMethod]
        public void CheckBySearchString()
        {
            
        }
    }
}
