using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Hess.Corporate.GHGPortal.Business;
namespace GHGPortal.UnitTests
{
    [TestClass]
    public class DataAccessTests
    {
        [TestMethod]
        public void GetFacilityOwners()
        {
            FacilityDataOwners owners = FacilityDataOwners.GetOwners("Tioga Gas Plant");
            Assert.IsNotNull(owners);
        }

        [TestMethod]
        public void GetCountsForMoves()
        {
            int[] moves = { 181290, 0 };
            Dictionary<string, int> counts = Documents.GetDocumentCountForMoves(moves, "QUAL");
            Assert.IsNotNull(counts);
        }
    }
}
