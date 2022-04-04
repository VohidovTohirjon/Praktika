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
    public interface IContentService
    {
        Task<BaseResponse<IEnumerable<Content>>> GetAllAsync(PaginationParams @params, Expression<Func<Content, bool>> expression = null);

        Task<BaseResponse<Content>> GetAsync(Expression<Func<Content, bool>> expression);

        Task<BaseResponse<Content>> CreateAsync(ContentCreateDto content);

        Task<BaseResponse<Content>> UpdateAsync(Guid id, ContentCreateDto contentDto);

        Task<BaseResponse<bool>> DeleteAsync(Expression<Func<Content, bool>> expression);
    }
}
