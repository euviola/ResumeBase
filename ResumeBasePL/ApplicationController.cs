using Microsoft.AspNetCore.Mvc;
using ResumeBaseDAL;
using ResumeBaseBLL.Models;
using ResumeBaseBLL;
using System.Linq;

namespace ResumeBaseUI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ApplicationController : ControllerBase
    {
        private readonly ApplicationService _service;

        public ApplicationController(
            IRepository<Application> applicationRepository,
            IRepository<Resume> resumeRepository,
            IRepository<Vacancy> vacancyRepository)
        {
            _service = new ApplicationService(applicationRepository, resumeRepository, vacancyRepository);
        }

        [HttpPost]
        public IActionResult Add([FromBody] ApplicationDTO dto)
        {
            if (!int.TryParse(dto.ResumeID, out int resumeId) || !int.TryParse(dto.VacancyID, out int vacancyId))
                return BadRequest("Invalid ResumeID or VacancyID.");

            var result = _service.AddApplicationWeb(dto);

            if (!result.Success)
                return BadRequest(result.Message);

            return Ok(result.Message);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var result = _service.RemoveApplicationWeb(id);

            if (!result.Success)
                return NotFound(result.Message);

            return Ok(result.Message);
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var apps = _service.GetAllApplicationsWeb();
            return Ok(apps);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var app = _service.FindApplicationWeb(id);

            if (app == null)
                return NotFound($"Application with ID {id} not found.");

            return Ok(app);
        }

        [HttpPut("{id}/status")]
        public IActionResult SetStatus(int id, [FromQuery] string status)
        {
            var result = _service.SetApplicationStatusWeb(id, status);

            if (!result.Success)
                return BadRequest(result.Message);

            return Ok(result.Message);
        }
    }
}
