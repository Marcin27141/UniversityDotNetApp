using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.DataBase;
using WebApplication1.Services.People;

namespace WebApplication1.Services.PeopleOps
{
    public class ReadPeopleOp : IReadPeopleOp
    {
        private readonly AppDbContext _context;
        public ReadPeopleOp(AppDbContext context) => _context = context;

        public List<KeyTypePersonalData> GetAllPersonalData()
        {
            /*var studentOutput = _context.Students.Include(s => s.PersonalData)
                .Select(s => new KeyTypePersonalData
                {
                    Key = s.StudentIndex,
                    Type = PersonType.Student,
                    PersonalData = PersonalData.FromEntityPersonalData(s.PersonalData)
                });

            var professorOutput = _context.Professors.Include(p => p.PersonalData)
                .Select(p => new KeyTypePersonalData
                {
                    Key = p.IdCode,
                    Type = PersonType.Professor,
                    PersonalData = PersonalData.FromEntityPersonalData(p.PersonalData)
                });

            return studentOutput.Concat(professorOutput).ToList();*/

            return _context.PersonalData
                .Select(pd => new KeyTypePersonalData
                {
                    Key = pd.SpecificId,
                    Type = (PersonType)pd.PersonTypeID,
                    PersonalData = PersonalData.FromEntityPersonalData(pd)
                }).ToList();
        }
    }

    public interface IReadPeopleOp
    {
        List<KeyTypePersonalData> GetAllPersonalData();
    }
}
