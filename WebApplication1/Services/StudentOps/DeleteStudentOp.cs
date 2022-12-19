using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.DataBase;

namespace WebApplication1.Services.StudentOps
{
    public class DeleteStudentOp : IDeleteStudentOp
    {
        private readonly AppDbContext _context;
        public DeleteStudentOp(AppDbContext context) => _context = context;

        public async Task DeleteStudentByIndexAsync(string studentIndex)
        {
            var entityStudent = _context.Students.Include(s => s.PersonalData).SingleOrDefault(s => s.StudentIndex.Equals(studentIndex));
            if (entityStudent == null) throw new Exception("Student doesn't exist");
            entityStudent.SoftDeleted = true;
            entityStudent.PersonalData.SoftDeleted = true;
            await _context.SaveChangesAsync();
        }
    }

    public interface IDeleteStudentOp
    {
        Task DeleteStudentByIndexAsync(string studentIndex);
    }
}
