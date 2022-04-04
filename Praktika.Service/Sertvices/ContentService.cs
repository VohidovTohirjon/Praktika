using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Praktika.Data.IRepository;
using Praktika.Domain.Common;
using Praktika.Domain.Configurations;
using Praktika.Domain.Entities;
using Praktika.Domain.Enums;
using Praktika.Service.Extensions;
using Praktika.Service.Interface;
using Praktika.Service.UserDto;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Praktika.Service.Sertvices
{
    public class ContentService : IContentService
    {
        private IMapper mapper;
        private readonly IUnitOfWork unitofwork;
        private readonly IWebHostEnvironment env;
        private readonly IConfiguration config;
        public ContentService(IUnitOfWork unitofwork, IMapper mapper, IWebHostEnvironment env, IConfiguration config)
        {
            this.mapper = mapper;
            this.unitofwork = unitofwork;
            this.env = env;
            this.config = config;
        }
        public async Task<BaseResponse<Content>> CreateAsync(ContentCreateDto content)
        {
            var response = new BaseResponse<Content>();

            var existStudent = await unitofwork.Content.GetAsync(p => p.TypeOfCourse == content.TypeOfCourse);
            if (existStudent is not null)
            {
                response.Error = new ErrorModel(400, "Content is exist");
                return response;
            }

            var mappedContent = mapper.Map<Content>(content);

            mappedContent.Image = await FileStreamExtension.SavefileAsync(content.Image.OpenReadStream(), content.Image.FileName, config, env);

            var result = await unitofwork.Content.CreateAsync(mappedContent);

            await unitofwork.SaveChanges();

            response.Data = result;

            return response;
        }

        public async Task<BaseResponse<bool>> DeleteAsync(Expression<Func<Content, bool>> expression)
        {
            var response = new BaseResponse<bool>();
            var entry = await unitofwork.Content.DeleteAsync(expression);
            if (entry == null)
            {
                response.Error = new ErrorModel(404, "Tugadi DeleteAsync");
                return response;
            }

            await unitofwork.SaveChanges();

            response.Data = entry;
            return response;
        }

        public async Task<BaseResponse<IEnumerable<Content>>> GetAllAsync(PaginationParams @params, Expression<Func<Content, bool>> expression = null)
        {
            string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

            var response = new BaseResponse<IEnumerable<Content>>();

            var students = await unitofwork.Content.GetAllAsync(expression);

            response.Data = students.ToPagedList(@params);

            return response;
        }

        public async Task<BaseResponse<Content>> GetAsync(Expression<Func<Content, bool>> expression)
        {
            var response = new BaseResponse<Content>();
            var entry = await unitofwork.Content.GetAsync(expression);
            if (entry == null)
            {
                response.Error = new ErrorModel(404, "Content not found, Tugadi GetAsync");
                return response;
            }
            response.Data = entry;
            return response;
        }

        public async Task<BaseResponse<Content>> UpdateAsync(Guid id, ContentCreateDto contentDto)
        {
            var response = new BaseResponse<Content>();

            var content = await unitofwork.Content.GetAsync(p => p.Id == id && p.State != ItemState.Deleted);
            if (content is null)
            {
                response.Error = new ErrorModel(404, "Content not found, UpdateAsync");
                return response;
            }

            content.TypeOfCourse = contentDto.TypeOfCourse;
            content.Duration = contentDto.Duration;
            content.State = contentDto.State;
            content.Update();

            var result = await unitofwork.Content.UpdateAsync(content);

            await unitofwork.SaveChanges();

            response.Data = result;

            return response;
        }
    }
}
