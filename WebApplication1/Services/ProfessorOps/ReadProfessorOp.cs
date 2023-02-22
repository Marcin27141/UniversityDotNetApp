using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.DataBase;
using WebApplication1.Queries;
using WebApplication1.Services.People;

namespace WebApplication1.Services.ProfessorOps
{
    public class ReadProfessorOp : IReadProfessorOp
    {
        private readonly AppDbContext _context;
        public ReadProfessorOp(AppDbContext context) => _context = context;


        public List<Professor> GetAllProfessors()
        {
            return _context.Professors.Include(p => p.PersonalData).Select(p => Professor.FromEntityProfessor(p)).ToList();
        }

        /*public List<ProfessorDisplayAndKey> GetProfessorsDisplayAndKey()
        {
            return _context.Professors.AsNoTracking().Select(p => new ProfessorDisplayAndKey
            {
                Display = p.ToString() + ", " + p.Subject,
                ProfessorIdCode = p.IdCode,
            }).ToList();
        }*/

        public Professor GetProfessorByKey(int professorID)
        {
            var professor = _context.Professors.AsNoTracking().Include(p => p.PersonalData).SingleOrDefault(p => p.EntityProfessorID == professorID);
            if (professor == null) return null;
            return Professor.FromEntityProfessor(professor);
        }

        public Professor GetProfessorByIdCode(string idCode)
        {
            var professor = _context.Professors.AsNoTracking().Include(p => p.PersonalData).SingleOrDefault(p => p.IdCode.Equals(idCode));
            if (professor == null) return null;
            return Professor.FromEntityProfessor(professor);
        }

        public Professor GetProfessorByUser(string userId)
        {
            var professor = _context.Professors.AsNoTracking()
                .Include(p => p.PersonalData)
                .ThenInclude(pd => pd.ApplicationUser)
                .SingleOrDefault(p => p.PersonalData.ApplicationUser.Id.Equals(userId));
            if (professor == null) return null;
            return Professor.FromEntityProfessor(professor);
        }

        public List<Professor> SortFilterProfessors(ProfessorOrderByOptions orderByOption, ProfessorFilterByOptions filterByOption, string filter)
        {
            return _context.Professors
                .AsNoTracking()
                .Include(s => s.PersonalData)
                .OrderProfessorsBy(orderByOption)
                .FilterProfessorsBy(filterByOption, filter)
                .MapEntitiesToProfessors()
                .ToList();
        }
    }

    public class ProfessorDisplayAndKey
    {
        public string Display { get; set; }
        public string ProfessorIdCode { get; set; }
    }

    public interface IReadProfessorOp
    {
        List<Professor> GetAllProfessors();

        Professor GetProfessorByIdCode(string idCode);
        Professor GetProfessorByUser(string userId);
        List<Professor> SortFilterProfessors(ProfessorOrderByOptions orderByOption, ProfessorFilterByOptions filterByOption, string filter);
    }
}
