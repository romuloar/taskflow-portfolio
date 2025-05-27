using Workflow.Application.Case.TaskNotification.NotifyTaskChangeToDone;
using Workflow.Domain.Case.TaskNotification.NotifyTaskChangeToDone;
using Moq;
using Rom.Result.Extensions;

namespace Application.Test.Case.TaskNotification
{
    public class NotifyTaskChangeToDoneApplicationTest
    {
        private readonly Mock<INotifyTaskChangeToDoneProvider> _providerMock;
        private readonly NotifyTaskChangeToDoneApplication _application;

        public NotifyTaskChangeToDoneApplicationTest()
        {
            _providerMock = new Mock<INotifyTaskChangeToDoneProvider>();
            _application = new NotifyTaskChangeToDoneApplication(_providerMock.Object);
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
        public async Task Execute_ShouldReturnError_WhenTaskIdIsEmpty()
        {
            // Arrange
            var notification = new NotifyTaskChangeToDoneDomain
            {
                Id = Guid.Empty,
                Description = "Some description"
            };

            // Act
            var result = await _application.Execute(notification);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal("Invalid notification domain", result.Message);
        }

        [Fact]
        public async Task Execute_ShouldCallProviderAndReturnResult_WhenValid()
        {
            // Arrange
            var notification = new NotifyTaskChangeToDoneDomain
            {
                Id = Guid.NewGuid(),
                Description = "Some description"
            };
            var expectedResult = true.GetResultDetailSuccess();

            _providerMock
                .Setup(p => p.NotifyAsync(notification))
                .ReturnsAsync(expectedResult);

            // Act
            var result = await _application.Execute(notification);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.True(result.ResultData);
            _providerMock.Verify(p => p.NotifyAsync(notification), Times.Once);
        }
    }
}
