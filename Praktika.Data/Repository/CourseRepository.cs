using Praktika.Data.DbContexts;
using Praktika.Data.IRepository;
using Praktika.Domain.Entities;

namespace Praktika.Data.Repository
{
    public class CourseRepository : GenericRepository<Course>, ICourseRepository
    {
        public CourseRepository(PraktikaContext prcontext) : base(prcontext)
        {
        }
    }
}
