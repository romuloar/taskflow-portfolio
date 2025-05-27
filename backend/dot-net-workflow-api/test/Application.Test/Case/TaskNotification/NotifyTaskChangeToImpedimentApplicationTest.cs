using Application.Case.TaskNotification.NotifyTaskChangeToImpediment;
using Domain.Case.TaskNotification.NotifyTaskChangeToImpediment;
using Moq;
using Rom.Result.Extensions;

namespace Application.Test.Case.TaskNotification
{
    public class NotifyTaskChangeToImpedimentApplicationTest
    {
        private readonly Mock<INotifyTaskChangeToImpedimentProvider> _providerMock;
        private readonly NotifyTaskChangeToImpedimentApplication _application;

        public NotifyTaskChangeToImpedimentApplicationTest()
        {
            _providerMock = new Mock<INotifyTaskChangeToImpedimentProvider>();
            _application = new NotifyTaskChangeToImpedimentApplication(_providerMock.Object);
        }

        [Fact]
        public async Task Execute_ShouldReturnError_WhenNotificationIsNull()
        {
            // Act
            var result = await _application.Execute(null);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal("Invalid notification domain", result.Message);
        }

        [Fact]
        public async Task Execute_ShouldReturnError_WhenDomainIsInvalid()
        {
            // Arrange
            var invalidDomain = new NotifyTaskChangeToImpedimentDomain(); // Not valid (missing required fields)

            // Act
            var result = await _application.Execute(invalidDomain);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal("Invalid notification domain", result.Message);
        }

        [Fact]
        public async Task Execute_ShouldCallProviderAndReturnResult_WhenValid()
        {
            // Arrange
            var validDomain = new NotifyTaskChangeToImpedimentDomain
            {
                Id = Guid.NewGuid(),
                Description = "Some description"
            };           

            var expectedResult = true.GetResultDetailSuccess();

            _providerMock
                .Setup(p => p.NotifyAsync(validDomain))
                .ReturnsAsync(expectedResult);

            // Act
            var result = await _application.Execute(validDomain);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.True(result.ResultData);
            _providerMock.Verify(p => p.NotifyAsync(validDomain), Times.Once);
        }
    }
}
