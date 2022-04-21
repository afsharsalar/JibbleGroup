using System;
using System.Linq;
using System.Threading.Tasks;
using JibbleGroup.Service;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace JibbleGroup.Tests
{
    [TestClass]
    public class PeopleTests
    {
        private readonly IServiceProvider _serviceProvider;

        public PeopleTests()
        {
            var services = new ServiceCollection();
            services.AddHttpClient();
            services.AddScoped<IPeopleService, PeopleService>();

            _serviceProvider = services.BuildServiceProvider();
        }
        
        
        [TestMethod]
        public async Task Test_PeopleList_HasData()
        {
            //arrange
            var service = _serviceProvider.GetService<IPeopleService>();

            //act
            var data =await service.GetAsync("");


            //assert
            Assert.IsTrue(data.Any());
            Assert.AreEqual(20,data.Count());
        }


        [TestMethod]
        public async Task Test_PeopleFilter_HasOneData()
        {
            //arrange
            var service = _serviceProvider.GetService<IPeopleService>();

            //act
            var data = await service.GetAsync("FirstName eq 'Scott'");


            //assert
            Assert.IsTrue(data.Any());
            Assert.AreEqual(1, data.Count());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public async Task Test_PeopleFilter_WrongFilter()
        {
            //arrange
            var service = _serviceProvider.GetService<IPeopleService>();

            //act
            var data = await service.GetAsync("FirstName");
        }


        [TestMethod]
        public async Task Test_SpecificPerson_Exist()
        {
            //arrange
            var service = _serviceProvider.GetService<IPeopleService>();

            //act
            var data = await service.GetOneAsync("russellwhyte");
            
            //assert
            Assert.IsNotNull(data);
        }

        [TestMethod]
        public async Task Test_SpecificPerson_NOTExist()
        {
            //arrange
            var service = _serviceProvider.GetService<IPeopleService>();

            //act
            var data = await service.GetOneAsync("salar");

            //assert
            Assert.IsNull(data);
        }
    }
}
