using GrpcService.Models;
using Microsoft.EntityFrameworkCore;

namespace GrpcService.Database
{
    public static class DbExtensions
    {
        public static IQueryable<Grade> IncludeGrade(this IQueryable<Grade> query)
        {
            return query
                .Include(g => g.GradedStudent)
                .Include(g => g.GradedCourse);
        }
    }
}
