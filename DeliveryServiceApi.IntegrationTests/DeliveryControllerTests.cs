using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace DeliveryServiceApi.IntegrationTests
{
    [TestFixture]
    public class DeliveryControllerTests
    {
        [Test]
        public async Task CheckStatusSendResultShouldReturnOk()
        {
            // Arange

            WebApplicationFactory<Program> webHost = new WebApplicationFactory<Program>().WithWebHostBuilder(_ => { });
            HttpClient httpClient = webHost.CreateClient();

            // Act

            HttpResponseMessage response = await httpClient.GetAsync("api/delivery/check-status");

            // Assert

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }
    }
}
