using Moq;
using ResumeBaseDAL;
using ResumeBaseBLL;

namespace ResumeBaseTests
{
    public class ResumeTest
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

            // Підставити консольне введення ID = 5
            Console.SetIn(new StringReader("5"));

            // Перехопити консольний вивід
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
        public void RemoveResume_ShouldPrintNotFound_WhenResumeDoesNotExist()
        {
            // Arrange
            var mockRepo = new Mock<IRepository<Resume>>();
            mockRepo.Setup(r => r.GetById(99)).Returns((Resume)null);

            var service = new ResumeService(mockRepo.Object);

            Console.SetIn(new StringReader("99"));
            using var sw = new StringWriter();
            Console.SetOut(sw);

            // Act
            service.RemoveResume();

            // Assert
            var output = sw.ToString().ToLower();
            Assert.Contains("resume not found", output);
        }

    }
}