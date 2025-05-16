using ResumeBaseBLL.Models;
using ResumeBaseDAL;

namespace ResumeBaseBLL.Mapper
{
    public static class Mapper
    {
        public static ResumeDTO ToDTO(Resume entity)
        {
            return new ResumeDTO
            {
                Id = entity.ID,
                FullName = entity.FullName,
                Description = entity.Description
            };
        }

        public static Resume ToEntity(ResumeDTO dto)
        {
            return new Resume
            {
                ID = dto.Id,
                FullName = dto.FullName,
                Description = dto.Description
            };
        }

        public static VacancyDTO ToDTO(Vacancy entity)
        {
            return new VacancyDTO
            {
                ID = entity.ID,
                Title = entity.Title,
                Salary = entity.Salary,
                Description = entity.Description
            };
        }

        public static Vacancy ToEntity(VacancyDTO dto)
        {
            return new Vacancy
            {
                ID = dto.ID,
                Title = dto.Title,
                Salary = dto.Salary,
                Description = dto.Description
            };
        }

        public static ApplicationDTO ToDTO(Application entity)
        {
            return new ApplicationDTO
            {
                ID = entity.ID,
                ResumeID = entity.ResumeID,
                VacancyID = entity.VacancyID,
                Resume = Mapper.ToDTO(entity.Resume),
                Vacancy = Mapper.ToDTO(entity.Vacancy),
                Status = entity.Status
            };
        }

        public static Application ToEntity(ApplicationDTO dto)
        {
            if (dto == null) return null;

            return new Application
            {
                ID = dto.ID,
                ResumeID = dto.ResumeID,
                VacancyID = dto.VacancyID,
                Status = dto.Status
            };
        }
    }
}
