using ApiDtoLibrary.Professors;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Net.Http;
using UniversityApi.API.Contracts;
using UniversityApi.API.DataBase;
using UniversityApi.API.DataBase.Entities;
using UniversityApi.API.DataBase.Identity;

namespace UniversityApi.API.Repositories
{
    public class ProfessorsRepository : GenericRepository<EntityProfessor>, IProfessorsRepository
    {
        public ProfessorsRepository(UniversityApiDbContext dbContext, UserManager<ApiUser> userManager) : base(dbContext, userManager)
        {
        }
    }
}

