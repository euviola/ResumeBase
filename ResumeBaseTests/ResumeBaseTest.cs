using Xunit;
using NSubstitute;
using Moq;
using ResumeBaseBLL;
using ResumeBaseDAL;
using ResumeBaseBLL.Models;
using ResumeBaseBLL.Mapper;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestPlatform.Utilities;

namespace ResumeBaseTests
{
    public class ResumeServiceTests
    {
        [Fact]
        public void GetAllResume_ShouldPrintResumes_WhenDataExists()
        {
            // Arrange
            var fakeData = new List<Resume>
            {
                new Resume { ID = 1, FullName = "John Doe", Description = "Developer" },
                new Resume { ID = 2, FullName = "Jane Smith", Description = "Designer" }
            };

            var mockRepo = new Mock<IRepository<Resume>>();
            mockRepo.Setup(r => r.GetAll()).Returns(fakeData.AsQueryable());

            var service = new ResumeService(mockRepo.Object);

            // Redirect console output
            using var sw = new StringWriter();
            Console.SetOut(sw);

            // Act
            service.GetAllResume();

            // Assert
            var output = sw.ToString();
            Assert.Contains("John Doe", output);
            Assert.Contains("Jane Smith", output);
            Assert.Contains("List of all resumes", output);
        }

        [Fact]
        public void RemoveResume_ShouldPrintSuccess_WhenResumeExists()
        {
            // Arrange
            var mockRepo = new Mock<IRepository<Resume>>();
            var resume = new Resume { ID = 5, FullName = "Test", Description = "Desc" };

            mockRepo.Setup(r => r.GetById(5)).Returns(resume);

            var service = new ResumeService(mockRepo.Object);

            Console.SetIn(new StringReader("5"));

            using var sw = new StringWriter();
            Console.SetOut(sw);

            // Act
            service.RemoveResume();

            // Assert
            var output = sw.ToString().ToLower();
            mockRepo.Verify(r => r.Delete(5), Times.Once);
            Assert.Contains("resume removed successfully", output);
        }


        [Fact]
        public void RemoveResume_ShouldPrintNotFound_WhenResumeNotExists()
        {
            // Arrange
            var mockRepo = new Mock<IRepository<Resume>>();
            mockRepo.Setup(r => r.GetById(99)).Returns((Resume)null);

            var service = new ResumeService(mockRepo.Object);

            Console.SetIn(new StringReader("99"));
            var output = new StringWriter();
            Console.SetOut(output);

            // Act
            service.RemoveResume();

            // Assert
            string consoleOutput = output.ToString().ToLower();
            Assert.Contains("resume not found", consoleOutput);
        }
    }
}
