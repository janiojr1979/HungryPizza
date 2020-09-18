using HungryPizza.API.VO;
using HungryPizza.Domain.Models;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace HungryPizza.Integration.Tests
{
    public class ClientControllerTest : IClassFixture<WebApplicationFactory<HungryPizza.API.Startup>>
    {
        private readonly WebApplicationFactory<HungryPizza.API.Startup> _factory;
        private readonly JsonSerializerSettings serializerSettings = new JsonSerializerSettings();

        public ClientControllerTest(WebApplicationFactory<HungryPizza.API.Startup> factory)
        {
            _factory = factory;
        }

        [Theory(DisplayName = "Inserir cliente")]
        [Trait("Client", "Hungry Pizza")]
        [InlineData(false, null, null, null, null, null, null, null)]
        [InlineData(true, "Zeca", "@test.com", "Rua ali 555", "SP", "São Paulo", "05406-100", "998887755")]
        public async Task Post(bool success, string name, string email, string address, string state, string city, string zipCode, string phone)
        {
            var random = new Random();
            RequestClient client = new RequestClient() { Address = address, City = city, Email = random.Next(10000).ToString() + email, Name = name, Phone = phone, State = state, ZipCode = zipCode };
            var httpClient = _factory.CreateClient();
            var httpContent = new StringContent(JsonConvert.SerializeObject(client, serializerSettings), Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync("/api/Client", httpContent);

            Assert.Equal(success, response.IsSuccessStatusCode);
        }

        [Theory(DisplayName = "Consultar cliente")]
        [Trait("Client", "Hungry Pizza")]
        [InlineData("Zeca", "@test.com", "Rua ali 555", "SP", "São Paulo", "05406-100", "998887755")]
        public async Task Get(string name, string email, string address, string state, string city, string zipCode, string phone)
        {
            Guid id = Guid.Empty;
            var httpClient = _factory.CreateClient();
            var random = new Random();
            RequestClient client = new RequestClient() { Address = address, City = city, Email = random.Next(10000).ToString() + email, Name = name, Phone = phone, State = state, ZipCode = zipCode };
            var httpContent = new StringContent(JsonConvert.SerializeObject(client, serializerSettings), Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync("/api/Client", httpContent);

            if (response.IsSuccessStatusCode)
            {
                Stream responseStream = await response.Content.ReadAsStreamAsync();
                StreamReader reader = new StreamReader(responseStream);
                string responseString = reader.ReadToEnd();

                id = JsonConvert.DeserializeObject<ResponseAdded>(responseString).Id;
            }

            //Success
            response = await httpClient.GetAsync($"/api/Client/id/{id}");

            Assert.True(response.IsSuccessStatusCode);
            if (response.IsSuccessStatusCode)
            {

                Stream responseStream = await response.Content.ReadAsStreamAsync();
                StreamReader reader = new StreamReader(responseStream);
                string responseString = reader.ReadToEnd();

                var clientById = JsonConvert.DeserializeObject<Client>(responseString);

                response = await httpClient.GetAsync($"/api/Client/email/{clientById.Email}");

                Assert.True(response.IsSuccessStatusCode);
            }

            //Fail
            response = await httpClient.GetAsync($"/api/Client/id/{Guid.NewGuid()}");
            Assert.False(response.IsSuccessStatusCode);

            response = await httpClient.GetAsync($"/api/Client/email/{"t@tests.com"}");
            Assert.False(response.IsSuccessStatusCode);
        }

        [Theory(DisplayName = "Atualizar cliente")]
        [Trait("Client", "Hungry Pizza")]
        [InlineData("Zeca", "@test.com", "Rua ali 555", "SP", "São Paulo", "05406-100", "998887755")]
        public async Task Put(string name, string email, string address, string state, string city, string zipCode, string phone)
        {
            Guid id = Guid.Empty;
            var random = new Random();
            RequestClient client = new RequestClient() { Address = address, City = city, Email = random.Next(10000).ToString() + email, Name = name, Phone = phone, State = state, ZipCode = zipCode };
            var httpClient = _factory.CreateClient();
            var httpContent = new StringContent(JsonConvert.SerializeObject(client, serializerSettings), Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync("/api/Client", httpContent);

            if (response.IsSuccessStatusCode)
            {
                Stream responseStream = await response.Content.ReadAsStreamAsync();
                StreamReader reader = new StreamReader(responseStream);
                string responseString = reader.ReadToEnd();

                id = JsonConvert.DeserializeObject<ResponseAdded>(responseString).Id;
            }
            client.Name = "Joazinho";
            httpContent = new StringContent(JsonConvert.SerializeObject(client, serializerSettings), Encoding.UTF8, "application/json");

            //Success
            response = await httpClient.PutAsync($"/api/Client/{id}", httpContent);
            Assert.True(response.IsSuccessStatusCode);

            //Fail
            response = await httpClient.PutAsync($"/api/Client/{Guid.NewGuid()}", httpContent);
            Assert.False(response.IsSuccessStatusCode);
        }
    }
}
