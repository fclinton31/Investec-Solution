using System;
using Xunit;
using Investec.API.Controllers;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using Investec.API.Services;
using System.Collections.Generic;
using Investec.API.Model;
using Investec.UniTest.Fixtures;

namespace Investec.UniTest.Systems.Controllers
{
    public class TestClientController
    {
        #region Practice
        /*
        [Fact]
        public async Task Get_OnSuccess_ReturnsStatusCode200()
        {
            //Arrange
            var mockClientService = new Mock<IClientService>();
            mockClientService.Setup(service => service
           .GetALLClients())
           .ReturnsAsync(new List<Client>());
            var Client = new ClientController(mockClientService.Object);

            //Act
            var response = (OkObjectResult)await Client.Get1();

            //Assert
            response.StatusCode.Should().Be(200);
        }
        [Fact]
        public async Task Get_OnSuccess_InvokeClientService()
        {
            //Arrange
            var mockClientService =new Mock<IClientService>();
            mockClientService.Setup(service => service
            .GetALLClients())
            .ReturnsAsync(new List<Client>());
            var Client = new ClientController(mockClientService.Object);

            //Act
            var response = await Client.Get1();

            //Assert
            mockClientService.Verify(service => service.GetALLClients(), Times.Once());
           
        }
        [Fact]
        public async Task Get_OnSuccess_ReturnsListOfClients()
        {
            //Arrange
            var mockClientService = new Mock<IClientService>();
            mockClientService.Setup(service => service
            .GetALLClients())
            .ReturnsAsync(ClientFixture.GetClients());
            var Client = new ClientController(mockClientService.Object);
            //Act
            var response = await Client.Get1();

            //Assert
            response.Should().BeOfType<OkObjectResult>();
            var results = (OkObjectResult)response;
            results.Value.Should().BeOfType<List<Client>>();
        }
        [Fact]
        public async Task Get_OnNoUserFound_Returns404()
        {
            //Arrange
            var mockClientService = new Mock<IClientService>();
            mockClientService.Setup(service => service
            .GetALLClients())
            .ReturnsAsync(new List<Client>());
            var Client = new ClientController(mockClientService.Object);
            //Act
            var response = await Client.Get1();

            //Assert
            response.Should().BeOfType<NotFoundResult>();
        }
        */
        #endregion

        private readonly Mock<IClientService> ClientServiceMock = new Mock<IClientService>();
        //private readonly Mock<ILogger<ClientController>> loggerStub = new ();
       // private readonly Random rand = new ();

        [Fact]
        public async Task GetClientAsync_WithUnexistingClient_ReturnsNotFound()
        {
            // Arrange
            ClientServiceMock.Setup(repo => repo.SearchClient("0788441885"))
                .ReturnsAsync((Client)null);

            var controller = new ClientController(ClientServiceMock.Object);

            // Act
            var result = await controller.SearchClient("0788441885");

            // Assert
            result.Result.Should().BeOfType<NotFoundResult>();
        }
        [Fact]
        public async Task GetClientAsync_WithExistingClient_ReturnsExpectedClient()
        {
            // Arrange
            Client expectedClient =new Client();

            ClientServiceMock.Setup(repo => repo.SearchClient("9803275804084"))
                .ReturnsAsync(expectedClient);

            var controller = new ClientController(ClientServiceMock.Object);

            // Act
            var result = await controller.SearchClient("9803275804084");

            // Assert
            result.Value.Should().BeEquivalentTo(expectedClient);
        }
        [Fact]
        public async Task CreateClientAsync_WithClientToCreate_ReturnsCreatedClient()
        {
            // Arrange
            Client client = new Client
            {
                FirstName = "Faith1",
                LastName = "lebyane1",
                MobileNumber = "0712472291",
                IDNumber = "9903275804084",
                PhysicalAddress = new Address()
                {
                    street = "3109 Morise tshabalala",
                    city = "pretoria",
                    zipcode = "0152"
                }
            };

            var controller = new ClientController(ClientServiceMock.Object);

            // Act
            var result = await controller.CreateClient(client);

            // Assert
            var createdClient = (result.Result as CreatedAtActionResult).Value as Client;
            client.Should().BeEquivalentTo(
                createdClient,
                options => options.ComparingByMembers<Client>().ExcludingMissingMembers()
            );
            createdClient.IDNumber.Should().NotBeEmpty();
            createdClient.FirstName.Should().NotBeEmpty();
            createdClient.MobileNumber.Should().NotBeEmpty();
        }

        [Fact]
        public async Task UpdateClientAsync_WithExistingClient_ReturnsNoContent()
        {
            // Arrange
            Client existingClient = new Client();
            ClientServiceMock.Setup(repo => repo.SearchClient("0788441885"))
                .ReturnsAsync(existingClient);

            var ClientId = existingClient.IDNumber;
            var ClientToUpdate = new Client {
                FirstName = "Charlene",
                LastName = "Lebyane",
                MobileNumber = "1111111",

                PhysicalAddress = new Address()
                {
                    street ="50008 Zoeknog trust",
                    city = "Nelspruit",
                    zipcode = "1370"
                }
            };

            var controller = new ClientController(ClientServiceMock.Object);

            // Act
            var result = await controller.UpdateClient(ClientToUpdate);

            // Assert
            result.Should().BeOfType<NoContentResult>();
        }


    }
}
