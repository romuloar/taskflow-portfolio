using Workflow.Application.Case.TaskNotification.NotifyTaskDeleted;
using Workflow.Domain.Case.TaskNotification.NotifyTaskDeleted;
using Moq;
using Rom.Result.Extensions;

namespace Application.Test.Case.TaskNotification
{
    public class NotifyTaskDeletedApplicationTest
    {
        private readonly Mock<INotifyTaskDeletedProvider> _providerMock;
        private readonly NotifyTaskDeletedApplication _application;

        public NotifyTaskDeletedApplicationTest()
        {
            _providerMock = new Mock<INotifyTaskDeletedProvider>();
            _application = new NotifyTaskDeletedApplication(_providerMock.Object);
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
            var invalidDomain = new NotifyTaskDeletedDomain(); // Not valid (missing required fields)

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
            var validDomain = new NotifyTaskDeletedDomain
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
