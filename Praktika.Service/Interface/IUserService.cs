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
    public interface IUserService
    {
        Task<BaseResponse<IEnumerable<User>>> GetAllAsync(PaginationParams @params, Expression<Func<User, bool>> expression = null);

        Task<BaseResponse<User>> GetAsync(Expression<Func<User, bool>> expression);

        Task<BaseResponse<User>> CreateAsync(UserCreateDto user);

        Task<BaseResponse<User>> UpdateAsync(Guid id, UserCreateDto userDto);

        Task<BaseResponse<bool>> DeleteAsync(Expression<Func<User, bool>> expression);


    }
}
