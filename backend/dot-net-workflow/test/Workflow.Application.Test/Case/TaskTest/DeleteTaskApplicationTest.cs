using Workflow.Application.Case.Task.DeleteTask;
using Workflow.Domain.Case.Task.DeleteTask;
using Moq;
using Rom.Result.Extensions;

namespace Application.Test.Case.TaskTest
{
    public class DeleteTaskApplicationTest
    {
        private readonly Mock<IDeleteTaskProvider> _providerMock;
        private readonly DeleteTaskApplication _application;

        public DeleteTaskApplicationTest()
        {
            _providerMock = new Mock<IDeleteTaskProvider>();
            _application = new DeleteTaskApplication(_providerMock.Object);
        }

        [Fact]
        public async Task Execute_ShouldReturnError_WhenIdIsEmpty()
        {
            // Act
            var result = await _application.Execute(Guid.Empty);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal("Invalid Id", result.Message);
        }

        [Fact]
        public async Task Execute_ShouldCallProviderAndReturnResult_WhenValid()
        {
            // Arrange
            var id = Guid.NewGuid();
            var expectedResult = true.GetResultDetailSuccess();

            _providerMock
                .Setup(p => p.DeleteTaskAsync(id))
                .ReturnsAsync(expectedResult);

            // Act
            var result = await _application.Execute(id);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.True(result.ResultData);
            _providerMock.Verify(p => p.DeleteTaskAsync(id), Times.Once);
        }
    }
}
