using Xunit;
using NSubstitute;
using Moq;
using ResumeBaseBLL;
using ResumeBaseDAL;
using ResumeBaseBLL.Models;
using ResumeBaseBLL.Mapper;
using System.Collections.Generic;
using System.Linq;

namespace ResumeBaseTests
{
    public class ResumeServiceTests
    {
        [Fact]
        public void GetAllResume_ShouldReturnResumes_WhenDataExists()
        {
            // Arrange
            var fakeData = new List<Resume>
            {
                new Resume { ID = 1, FullName = "John Doe", Description = "Developer" },
                new Resume { ID = 2, FullName = "Jane Smith", Description = "Designer" }
            };

            var mockRepo = new Mock<IRepository<Resume>>();
            mockRepo.Setup(r => r.GetAll()).Returns(fakeData.AsQueryable()) ;

            var service = new ResumeService(mockRepo.Object);

            // Act
            var result = service.GetAllResume(); // Метод, який повертає DTO-шки

            // Assert
            Assert.Equal(2, result.Count());
            Assert.Contains(result, r => r.FullName == "John Doe");
        }
    }
}
