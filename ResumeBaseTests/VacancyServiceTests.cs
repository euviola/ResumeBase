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
    public class VacancyServiceTests
    {
        [Fact]
        public void AddVacancy_ShouldAddAndPrintSuccess()
        {
            var mockRepo = new Mock<IRepository<Vacancy>>();
            var service = new VacancyService(mockRepo.Object);

            string input = "Developer\n12000\nFull-time position\n";
            Console.SetIn(new StringReader(input));
            using var sw = new StringWriter();
            Console.SetOut(sw);

            service.AddVacancy();

            mockRepo.Verify(r => r.Add(It.IsAny<Vacancy>()), Times.Once);
            mockRepo.Verify(r => r.SaveChanges(), Times.Once);
            var output = sw.ToString().ToLower();
            Assert.Contains("vacancy added successfully", output);
        }

        [Fact]
        public void RemoveVacancy_ShouldRemove_WhenVacancyExists()
        {
            var mockRepo = new Mock<IRepository<Vacancy>>();
            var vacancy = new Vacancy { ID = 1, Title = "Dev", Salary = 10000, Description = "Job" };
            mockRepo.Setup(r => r.GetById(1)).Returns(vacancy);

            var service = new VacancyService(mockRepo.Object);
            Console.SetIn(new StringReader("1"));
            using var sw = new StringWriter();
            Console.SetOut(sw);

            service.RemoveVacancy();

            mockRepo.Verify(r => r.Delete(1), Times.Once);
            mockRepo.Verify(r => r.SaveChanges(), Times.Once);
            Assert.Contains("vacancy removed successfully", sw.ToString().ToLower());
        }

        [Fact]
        public void RemoveVacancy_ShouldPrintNotFound_WhenVacancyMissing()
        {
            var mockRepo = new Mock<IRepository<Vacancy>>();
            mockRepo.Setup(r => r.GetById(It.IsAny<int>())).Returns((Vacancy)null);

            var service = new VacancyService(mockRepo.Object);
            Console.SetIn(new StringReader("999"));
            using var sw = new StringWriter();
            Console.SetOut(sw);

            service.RemoveVacancy();

            Assert.Contains("vacancy not found", sw.ToString().ToLower());
        }

        [Fact]
        public void GetAllVacancies_ShouldPrintList_WhenExists()
        {
            var mockRepo = new Mock<IRepository<Vacancy>>();
            mockRepo.Setup(r => r.GetAll()).Returns(new List<Vacancy>
            {
                new Vacancy { ID = 1, Title = "Backend", Salary = 15000, Description = "Node.js" },
                new Vacancy { ID = 2, Title = "Frontend", Salary = 14000, Description = "React" }
            }.AsQueryable());

            var service = new VacancyService(mockRepo.Object);
            using var sw = new StringWriter();
            Console.SetOut(sw);

            service.GetAllVacancies();

            var output = sw.ToString();
            Assert.Contains("list of all vacancies", output.ToLower());
            Assert.Contains("backend", output.ToLower());
            Assert.Contains("frontend", output.ToLower());
        }

        [Fact]
        public void FindVacancy_ShouldPrintMatching_WhenFound()
        {
            var mockRepo = new Mock<IRepository<Vacancy>>();
            mockRepo.Setup(r => r.GetAll()).Returns(new List<Vacancy>
            {
                new Vacancy { ID = 1, Title = "React Developer" },
                new Vacancy { ID = 2, Title = "Angular Dev" }
            }.AsQueryable());

            var service = new VacancyService(mockRepo.Object);
            Console.SetIn(new StringReader("react"));
            using var sw = new StringWriter();
            Console.SetOut(sw);

            service.FindVacancy();

            var output = sw.ToString().ToLower();
            Assert.Contains("matching vacancies", output);
            Assert.Contains("react developer", output);
            Assert.DoesNotContain("angular", output);
        }
    }
}
