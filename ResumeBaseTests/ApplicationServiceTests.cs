using Moq;
using ResumeBaseBLL;
using ResumeBaseDAL;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Xunit;

namespace ResumeBaseTests
{
    public class ApplicationServiceTests
    {
        [Fact]
        public void AddApplication_ShouldAdd_WhenValidInput()
        {
            var resume = new Resume { ID = 1, FullName = "Alice" };
            var vacancy = new Vacancy { ID = 2, Title = "QA" };
            var mockAppRepo = new Mock<IRepository<Application>>();
            var mockResumeRepo = new Mock<IRepository<Resume>>();
            var mockVacancyRepo = new Mock<IRepository<Vacancy>>();

            mockResumeRepo.Setup(r => r.GetAll()).Returns(new List<Resume> { resume }.AsQueryable());
            mockVacancyRepo.Setup(r => r.GetAll()).Returns(new List<Vacancy> { vacancy }.AsQueryable());

            var service = new ApplicationService(mockAppRepo.Object, mockResumeRepo.Object, mockVacancyRepo.Object);
            Console.SetIn(new StringReader("1\n2"));
            using var sw = new StringWriter();
            Console.SetOut(sw);

            service.AddApplication();

            mockAppRepo.Verify(r => r.Add(It.IsAny<Application>()), Times.Once);
            mockAppRepo.Verify(r => r.SaveChanges(), Times.Once);
            Assert.Contains("application added successfully", sw.ToString().ToLower());
        }

        [Fact]
        public void RemoveApplication_ShouldRemove_WhenFound()
        {
            var app = new Application { ID = 3 };
            var mockRepo = new Mock<IRepository<Application>>();
            mockRepo.Setup(r => r.GetAll()).Returns(new List<Application> { app }.AsQueryable());

            var service = new ApplicationService(mockRepo.Object, null, null);
            Console.SetIn(new StringReader("3"));
            using var sw = new StringWriter();
            Console.SetOut(sw);

            service.RemoveApplication();

            mockRepo.Verify(r => r.Delete(3), Times.Once);
            mockRepo.Verify(r => r.SaveChanges(), Times.Once);
            Assert.Contains("removed successfully", sw.ToString().ToLower());
        }

        [Fact]
        public void GetAllApplications_ShouldPrintApplications_WhenExist()
        {
            var mockRepo = new Mock<IRepository<Application>>();
            mockRepo.Setup(r => r.GetAll()).Returns(new List<Application>
            {
                new Application
                {
                    ID = 1,
                    Resume = new Resume { FullName = "Ivan" },
                    Vacancy = new Vacancy { Title = "Dev" },
                    Status = "Pending"
                }
            }.AsQueryable());

            var service = new ApplicationService(mockRepo.Object, null, null);
            using var sw = new StringWriter();
            Console.SetOut(sw);

            service.GetAllApplications();

            var output = sw.ToString();
            Assert.Contains("ivan", output.ToLower());
            Assert.Contains("dev", output.ToLower());
            Assert.Contains("pending", output.ToLower());
        }

        [Fact]
        public void FindApplication_ShouldPrintOne_WhenExists()
        {
            var mockRepo = new Mock<IRepository<Application>>();
            mockRepo.Setup(r => r.GetAll()).Returns(new List<Application>
            {
                new Application { ID = 7, Resume = new Resume { FullName = "Bob" }, Vacancy = new Vacancy { Title = "HR" }, Status = "Approved" }
            }.AsQueryable());

            var service = new ApplicationService(mockRepo.Object, null, null);
            Console.SetIn(new StringReader("7"));
            using var sw = new StringWriter();
            Console.SetOut(sw);

            service.FindApplication();

            var output = sw.ToString().ToLower();
            Assert.Contains("bob", output);
            Assert.Contains("approved", output);
        }

        [Fact]
        public void SetApplicationStatus_ShouldApprove_WhenOption1()
        {
            var app = new Application { ID = 5, Status = "Pending" };
            var mockRepo = new Mock<IRepository<Application>>();
            mockRepo.Setup(r => r.GetAll()).Returns(new List<Application> { app }.AsQueryable());

            var service = new ApplicationService(mockRepo.Object, null, null);
            Console.SetIn(new StringReader("5\n1"));
            using var sw = new StringWriter();
            Console.SetOut(sw);

            service.SetApplicationStatus();

            Assert.Equal("Approved", app.Status);
            mockRepo.Verify(r => r.Update(app), Times.Once);
            mockRepo.Verify(r => r.SaveChanges(), Times.Once);
        }
    }
}
