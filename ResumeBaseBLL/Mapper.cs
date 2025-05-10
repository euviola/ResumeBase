using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
                Id = entity.Id,
                FullName = entity.FullName,
                Description = entity.Description
            };
        }

        public static Resume ToEntity(ResumeDTO dto)
        {
            return new Resume
            {
                Id = dto.Id,
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
            return new Application
            {
                ID = dto.ID,
                ResumeID = dto.ResumeID,
                VacancyID = dto.VacancyID,
                Resume = Mapper.ToEntity(dto.Resume),
                Vacancy = Mapper.ToEntity(dto.Vacancy),
                Status = dto.Status
            };
        }
    }
}
