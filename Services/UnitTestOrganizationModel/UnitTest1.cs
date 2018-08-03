using System;
using System.Collections.ObjectModel;
using com.sbh.dto.complexobjects;
using com.sbh.dto.simpleobjects;
using com.sbh.srv.implementations;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestOrganizationModel
{
    [TestClass]
    public class UnitTest1
    {

        [TestMethod]
        public void TestMethodGetOrganization()
        {
            OrganizationModel organizationModel = new OrganizationModel();

            organizationModel.GetOrganization();

            Assert.IsNotNull(true);
        }
    }
}
