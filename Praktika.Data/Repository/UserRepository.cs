using Praktika.Data.DbContexts;
using Praktika.Data.IRepository;
using Praktika.Domain.Entities;

namespace Praktika.Data.Repository
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(PraktikaContext prcontext) : base(prcontext)
        {
        }
    }
}
