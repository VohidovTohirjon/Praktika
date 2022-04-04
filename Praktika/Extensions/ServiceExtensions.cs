using Microsoft.Extensions.DependencyInjection;
using Praktika.Data.IRepository;
using Praktika.Data.Repository;
using Praktika.Service.Interface;
using Praktika.Service.Sertvices;

namespace Praktika.Extensions
{
    public static class ServiceExtensions
    {
        public static void AddCustomServices(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IContentService, ContentService>();
            services.AddScoped<ICourseService, CourseService>();
        }
    }
}
