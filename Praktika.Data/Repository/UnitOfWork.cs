using Praktika.Data.DbContexts;
using Praktika.Data.IRepository;
using System;
using System.Threading.Tasks;

namespace Praktika.Data.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        public IUserRepository User { get; private set; }

        public ICourseRepository Course { get; private set; }

        public IContentRepository Content { get; private set; }

        private PraktikaContext erer;
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
        public UnitOfWork(PraktikaContext erer)
        {
            this.erer = erer;
            User = new UserRepository(erer);
            Content = new ContentRepository(erer);
            Course = new CourseRepository(erer);
        }
        public async Task SaveChanges()
        {
            await erer.SaveChangesAsync();
        }
    }
}
