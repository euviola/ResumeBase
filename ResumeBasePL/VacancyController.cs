using Microsoft.AspNetCore.Mvc;
using ResumeBaseDAL;
using ResumeBaseBLL.Models;
using ResumeBaseBLL.Mapper;
using System.Linq;

namespace ResumeBaseWebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VacancyController : ControllerBase
    {
        private readonly IRepository<Vacancy> _vacancyRepository;

        public VacancyController(IRepository<Vacancy> vacancyRepository)
        {
            _vacancyRepository = vacancyRepository;
        }

        // GET: api/vacancy
        [HttpGet]
        public IActionResult GetAll()
        {
            var vacancies = _vacancyRepository.GetAll().ToList();
            var dtos = vacancies.Select(Mapper.ToDTO).ToList();
            return Ok(dtos);
        }

        // GET: api/vacancy/{id}
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var vacancy = _vacancyRepository.GetById(id);
            if (vacancy == null)
                return NotFound($"Vacancy with ID {id} not found.");

            var dto = Mapper.ToDTO(vacancy);
            return Ok(dto);
        }

        // POST: api/vacancy
        [HttpPost]
        public IActionResult Create([FromBody] VacancyDTO dto)
        {
            if (dto == null)
                return BadRequest("Vacancy data is required.");

            var entity = Mapper.ToEntity(dto);
            _vacancyRepository.Add(entity);
            _vacancyRepository.SaveChanges();

            dto.ID = entity.ID;
            return CreatedAtAction(nameof(GetById), new { id = entity.ID }, dto);
        }

        // PUT: api/vacancy/{id}
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] VacancyDTO dto)
        {
            if (dto == null || dto.ID != id)
                return BadRequest("Vacancy ID mismatch.");

            var entity = _vacancyRepository.GetById(id);
            if (entity == null)
                return NotFound($"Vacancy with ID {id} not found.");

            entity.Title = dto.Title;
            entity.Salary = dto.Salary;
            entity.Description = dto.Description;

            _vacancyRepository.Update(entity);
            _vacancyRepository.SaveChanges();

            return NoContent();
        }

        // DELETE: api/vacancy/{id}
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var entity = _vacancyRepository.GetById(id);
            if (entity == null)
                return NotFound($"Vacancy with ID {id} not found.");

            _vacancyRepository.Delete(id);
            _vacancyRepository.SaveChanges();

            return NoContent();
        }

        // GET: api/vacancy/search/{term}
        [HttpGet("search/{term}")]
        public IActionResult Search(string term)
        {
            var results = _vacancyRepository.GetAll()
                .Where(v => v.Title != null && v.Title.ToLower().Contains(term.ToLower()))
                .Take(10)
                .Select(Mapper.ToDTO)
                .ToList();

            return Ok(results);
        }
    }
}
