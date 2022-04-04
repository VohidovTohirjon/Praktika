using System;
using System.Threading.Tasks;

namespace Praktika.Data.IRepository
{
    public interface IUnitOfWork : IDisposable
    {
        Task SaveChanges();

        ICourseRepository Course { get; }
        IContentRepository Content { get; }
        IUserRepository User { get; }

    }
}
