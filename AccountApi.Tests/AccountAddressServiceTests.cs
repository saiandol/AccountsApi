using AccountsApi.Services;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using Newtonsoft.Json.Linq;

namespace AccountApi.Tests
{
    [TestFixture]
    public class AccountAddressServiceTests
    {
        [Test]
        public void HappyPath_GetAddress_Returns_The_Address_ByCallingTheExternalApi()
        {
            //Arrange
            var mockHttpClientFactory = new Mock<IHttpClientFactory>();
            var clientHandlerStub = new DelegatingHandlerStub((request, cancellationToken) =>
            {
                var httpConfiguration = new HttpConfiguration();
                request.SetConfiguration(httpConfiguration);
                request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var response = request.CreateResponse(HttpStatusCode.OK, CannedResponseContent());
                return Task.FromResult(response);
            });
            var client = new HttpClient(clientHandlerStub);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            mockHttpClientFactory.Setup(x => x.CreateClient(It.IsAny<string>())).Returns(client);

            //act
            var accountAddressService = new AccountAddressService(mockHttpClientFactory.Object);

            var address = accountAddressService.GetAddress().GetAwaiter().GetResult();
            
            address.Should().Be("Postcode: CD79 6ST, City: Exeter");
        }

        private string CannedResponseContent()
        {
            // string jsonResult = "{\"results\":[{\"gender\":\"male\",\"name\":{\"title\":\"Mr\",\"first\":\"Russell\",\"last\":\"Gilbert\"},\"location\":{\"street\":{\"number\":1276,\"name\":\"Queens Road\"},\"city\":\"Wakefield\",\"state\":\"Fife\",\"country\":\"United Kingdom\",\"postcode\":\"B5 6AN\",\"coordinates\":{\"latitude\":\"66.0066\",\"longitude\":\"-69.8582\"},\"timezone\":{\"offset\":\"0:00\",\"description\":\"Western Europe Time, London, Lisbon, Casablanca\"}},\"email\":\"russell.gilbert@example.com\",\"login\":{\"uuid\":\"d472ae57-702e-43d3-8a3c-dab3b7c65fd3\",\"username\":\"bluepeacock883\",\"password\":\"pakistan\",\"salt\":\"Eak4o3mF\",\"md5\":\"e3ce11f4dfc3cb56a14df08612334c17\",\"sha1\":\"d96f3d0a2c4afedcb16db2642e85e1fb0f3f6a82\",\"sha256\":\"15ebf180b58ecfb37d98e95b5f17af82b90a81d3d72799435637d3e6995d4793\"},\"dob\":{\"date\":\"1979-01-18T13:56:14.030Z\",\"age\":41},\"registered\":{\"date\":\"2015-04-17T03:05:59.204Z\",\"age\":5},\"phone\":\"01067 880240\",\"cell\":\"0714-122-911\",\"id\":{\"name\":\"NINO\",\"value\":\"TM 61 42 24 E\"},\"picture\":{\"large\":\"24.jpg\",\"medium\":\"24.jpg\",\"thumbnail\":\"24.jpg\"},\"nat\":\"GB\"}],\"info\":{\"seed\":\"8aaae6a6b8cf8828\",\"results\":1,\"page\":1,\"version\":\"1.3\"}}";
            var jsonResult = System.IO.File.ReadAllText(@"MockData\result.json");
            
            return jsonResult;
        }
    }

    public class DelegatingHandlerStub : DelegatingHandler
    {
        private readonly Func<HttpRequestMessage, CancellationToken, Task<HttpResponseMessage>> _handlerFunc;
        public DelegatingHandlerStub()
        {
            _handlerFunc = (request, cancellationToken) => Task.FromResult(request.CreateResponse(HttpStatusCode.OK));
        }

        public DelegatingHandlerStub(Func<HttpRequestMessage, CancellationToken, Task<HttpResponseMessage>> handlerFunc)
        {
            _handlerFunc = handlerFunc;
        }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            return _handlerFunc(request, cancellationToken);
        }
    }


}
