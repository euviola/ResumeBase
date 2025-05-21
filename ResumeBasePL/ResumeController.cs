using Microsoft.AspNetCore.Mvc;
using ResumeBaseDAL;
using ResumeBaseBLL.Models;
using System.Linq;
using ResumeBaseBLL.Mapper;

namespace ResumeBaseWebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ResumeController : ControllerBase
    {
        private readonly IRepository<Resume> _resumeRepository;

        public ResumeController(IRepository<Resume> resumeRepository)
        {
            _resumeRepository = resumeRepository;
        }

        // GET: api/resume
        [HttpGet]
        public IActionResult GetAll()
        {
            var entities = _resumeRepository.GetAll().ToList();
            var dtos = entities.Select(Mapper.ToDTO).ToList();
            return Ok(dtos);
        }

        // GET: api/resume/{id}
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var entity = _resumeRepository.GetById(id);
            if (entity == null)
                return NotFound($"Resume with ID {id} not found.");

            var dto = Mapper.ToDTO(entity);
            return Ok(dto);
        }

        // POST: api/resume
        [HttpPost]
        public IActionResult Create([FromBody] ResumeDTO dto)
        {
            if (dto == null)
                return BadRequest("Resume data is required.");

            var entity = Mapper.ToEntity(dto);
            _resumeRepository.Add(entity);
            _resumeRepository.SaveChanges();

            dto.Id = entity.ID; // Повертаємо ID, згенероване БД
            return CreatedAtAction(nameof(GetById), new { id = entity.ID }, dto);
        }

        // PUT: api/resume/{id}
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] ResumeDTO dto)
        {
            if (dto == null || dto.Id != id)
                return BadRequest("Resume ID mismatch.");

            var entity = _resumeRepository.GetById(id);
            if (entity == null)
                return NotFound($"Resume with ID {id} not found.");

            entity.FullName = dto.FullName;
            entity.Description = dto.Description;

            _resumeRepository.Update(entity);
            _resumeRepository.SaveChanges();

            return NoContent();
        }

        // DELETE: api/resume/{id}
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var entity = _resumeRepository.GetById(id);
            if (entity == null)
                return NotFound($"Resume with ID {id} not found.");

            _resumeRepository.Delete(id);
            _resumeRepository.SaveChanges();

            return NoContent();
        }
    }
}
