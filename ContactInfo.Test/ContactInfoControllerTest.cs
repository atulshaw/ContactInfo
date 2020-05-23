using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Data.Common;
using ContactInfo.Data.Common;
using ContactInfo.Data;
using ContactInfo.Data.Implementation;
using ContactInfo.Service.Implementation;
using ContactInfo.API.Controllers;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Web.Http.Results;
using ContactInfo.API.Models;

namespace ContactInfo.Test
{
    [TestClass]
    public class ContactInfoControllerTest
    {
        private DbConnection _connection;
        private TestContext _testDbContext;
        private UnitOfWork _testUnitOfWork;
        private ContactInfoService _contactInfoService;
        private ContactInfoRepository _contactInfoRepository;
        private ContactInfoController _controller = null;
        tblContactInfo tbl = new tblContactInfo();

        /// <summary>
        /// Initialize
        /// </summary>
        [TestInitialize]
        public void Initialize()
        {
            _connection = Effort.DbConnectionFactory.CreateTransient();
            _testDbContext = new TestContext();
            _testUnitOfWork = new UnitOfWork(_testDbContext);
            _contactInfoRepository = new ContactInfoRepository(_testUnitOfWork);
            _contactInfoService = new ContactInfoService(_contactInfoRepository);
            _controller = new ContactInfoController(_contactInfoService);
        }

        /// <summary>
        /// GetAllContactDetails_Return_Test
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public async Task GetAllContactDetails_Return_Test()
        {
            var result = await _controller.GetDetails();
            Assert.IsNotNull(result);

        }

        /// <summary>
        /// InsertContactDetails_Test
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public async Task InsertContactDetails_Test()
        {
            tbl.FirstName = "Ravi";
            tbl.LastName = "Sri";
            tbl.PhoneNumber = "0987654321";
            tbl.Email = "ravi@testmail.com";
            tbl.Status = true;
            var result = await _controller.InsertContactInfo(tbl);
            Assert.IsNotNull(result);
        }

        /// <summary>
        /// UpdateContactDetails_Test
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public async Task UpdateContactDetails_Test()
        {            
            tbl.FirstName = "Raju";
            tbl.LastName = "Last";
            tbl.PhoneNumber = "1234567890";
            tbl.Email = "raju@testmail.com";
            tbl.Status = false;
            tbl.ID = 1;
            var result = await _controller.UpdateContactInfo(tbl);
            Assert.IsNotNull(result);
        }

        /// <summary>
        /// DeleteContactDetails_Test
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public async Task DeleteContactDetails_Test()
        {
            Deleteparam tbl = new Deleteparam();
            tbl.DelContactID = "1";
            var result = await _controller.RemoveContact(tbl);
            Assert.IsNotNull(result);
        }
    }
}
