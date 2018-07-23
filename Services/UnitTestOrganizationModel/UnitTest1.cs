using System;
using System.Collections.ObjectModel;
using ComplexObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SrvOrganizationModel;

namespace UnitTestOrganizationModel
{
    [TestClass]
    public class UnitTest1
    {

        [TestMethod]
        public void TestMethodGetOrganization()
        {
            OrganizationModel organizationModel = new OrganizationModel();

            ObservableCollection<ComplexOrganization> result = organizationModel.GetOrganization();

            Assert.IsNotNull(result);
        }
    }
}
