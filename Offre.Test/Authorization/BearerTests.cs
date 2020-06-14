using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Offre.Logic.Authorize;

namespace Offre.Test.Authorization
{
    [TestClass]
    public class BearerTests
    {
        [TestMethod]
        public void ChecksIfBearerTokenContainsUserIdTest()
        {
            long userId = 12500;
            const string secret =
                "f7dbc58fa686cae84e95e7592d957ddddbfce5dbd469bdf753644e8100949aad12a34bb497d40ddb6716cfaf1398702a3683e43a3544889e9a8fb619f3c31726";

            Mock<IConfiguration> configurationMock = new Mock<IConfiguration>();

            Mock<IConfigurationSection> configurationSectionMockSecret = new Mock<IConfigurationSection>();
            configurationSectionMockSecret.Setup(mock => mock.Value).Returns(secret);

            Mock<IConfigurationSection> configurationSectionMockExpiryTime = new Mock<IConfigurationSection>();
            configurationSectionMockExpiryTime.Setup(mock => mock.Value).Returns("7");

            configurationMock.Setup(mock => mock.GetSection("AppSettings:Secret")).Returns(() => configurationSectionMockSecret.Object);

            configurationMock.Setup(mock => mock.GetSection("AppSettings:TokenExpiryTime")).Returns(() => configurationSectionMockExpiryTime.Object);

            var subject = new AuthorizeLogic(configurationMock.Object);

            var token = subject.GenerateToken(userId);

            var tokenHandler = new JwtSecurityTokenHandler();

            var user = tokenHandler.ValidateToken(token, new TokenValidationParameters
            {
                RequireExpirationTime = true,
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidIssuer = string.Empty,
                ValidAudience = string.Empty,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret))
            }, out _);

            var claim = user.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Name)?.Value;

            Assert.IsNotNull(claim);

            Assert.IsTrue(claim.Contains(userId.ToString()));
        }
    }
}
