using FluentAssertions;
using Moq;
using Netherift_Cloud_Server.Authentication;
using Netherift_Cloud_Server.Authentication.JwtEndpoint;
using Unity.Services.CloudCode.Core;
using Xunit;

namespace Netherift_Cloud_Server.Tests.Authentication
{
    public class JwtAuthTests
    {
        private Mock<IJwtEndpoint> _endpointMock = new();

        public JwtAuthTests()
        {
        }

        public JwtAuth GenerateDefaultAuth()
        {
            _endpointMock.Setup(r => r.MakeRequestAsync()).Returns(JwtFactory.GenerateToken);
            var auth = new JwtAuth(_endpointMock.Object);
            return auth;
        }

        [Fact]
        public async void ShouldReturnTokenIFAllOk()
        {
            // Arrange
            var auth = GenerateDefaultAuth();

            // Act
            string token = await auth.GetTokenAsync();

            // Assert
            token.Should().NotBeNullOrEmpty();
        }

        [Fact]
        public async void ShouldContainJwtToken()
        {
            // Arrange
            var rightTokenTask = JwtFactory.GenerateToken();
            _endpointMock.Setup(r => r.MakeRequestAsync()).Returns(rightTokenTask);
            var auth = new JwtAuth(_endpointMock.Object);

            // Act
            string token = await auth.GetTokenAsync();
         
            // Assert
            token.Should().BeEquivalentTo(await rightTokenTask);
        }

        [Fact]
        public async void ShouldStoreJwtToken()
        {
            // Arrange
            var auth = GenerateDefaultAuth();

            // Act
            var firstToken = await auth.GetTokenAsync();
            Thread.Sleep(1000 * 3);
            var secondToken = await auth.GetTokenAsync();

            // Assert
            firstToken.Should().BeEquivalentTo(secondToken);
        }

        [Fact]
        public async void ShouldRefreshJwtToken()
        {
            // Arrange
            var auth = GenerateDefaultAuth();

            // Act
            var firstToken = await auth.GetTokenAsync();
            Thread.Sleep(1000 * 35);
            var secondToken = await auth.GetTokenAsync();

            // Assert
            firstToken.Should().NotBeEquivalentTo(secondToken);
        }
    }
}
