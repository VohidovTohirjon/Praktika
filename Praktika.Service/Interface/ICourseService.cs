using Praktika.Domain.Common;
using Praktika.Domain.Configurations;
using Praktika.Domain.Entities;
using Praktika.Service.UserDto;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Praktika.Service.Interface
{
    public interface ICourseService
    {
        Task<BaseResponse<IEnumerable<Course>>> GetAllAsync(PaginationParams @params, Expression<Func<Course, bool>> expression = null);

        Task<BaseResponse<Course>> GetAsync(Expression<Func<Course, bool>> expression);

        Task<BaseResponse<Course>> CreateAsync(CourseCreateDto course);

        Task<BaseResponse<Course>> UpdateAsync(Guid id, CourseCreateDto courseDto);

        Task<BaseResponse<bool>> DeleteAsync(Expression<Func<Course, bool>> expression);
    }
}
