using HungryPizza.API.VO;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using NUnit.Framework;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace HungryPizza.Tests
{
    public class ClientControllerTest : IClassFixture<WebApplicationFactory<HungryPizza.API.Startup>>
    {
        private readonly WebApplicationFactory<HungryPizza.API.Startup> _factory;
        private readonly JsonSerializerSettings serializerSettings = new JsonSerializerSettings();
        private Guid _id;

        public ClientControllerTest(WebApplicationFactory<HungryPizza.API.Startup> factory)
        {
            _factory = factory;
        }

        [Fact(DisplayName = "Inserir cliente")]
        [Trait("Client", "Hungry Pizza")]
        [InlineData(false, null, null, null, null, null, null, null)]
        public async Task Post(bool success, string name, string email, string address, string state, string city, string zipCode, string phone)
        {
            RequestClient client = new RequestClient() { Address = address, City = city, Email = email, Name = name, Phone = phone, State = state, ZipCode = zipCode };
            var httpClient = _factory.CreateClient();
            var httpContent = new StringContent(JsonConvert.SerializeObject(client, serializerSettings), Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync("/api/Client", httpContent);

            Assert.AreEqual(success, response.EnsureSuccessStatusCode());
        }

        //[Fact(DisplayName = "Consultar cliente por email")]
        //[Trait("Client", "Hungry Pizza")]
        //public async Task Get()
        //{

        //}
    }
}