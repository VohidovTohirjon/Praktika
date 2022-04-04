using Praktika.Data.DbContexts;
using Praktika.Data.IRepository;
using Praktika.Domain.Entities;

namespace Praktika.Data.Repository
{
    public class ContentRepository : GenericRepository<Content>, IContentRepository
    {
        public ContentRepository(PraktikaContext prcontext) : base(prcontext)
        {
        }
    }
}
