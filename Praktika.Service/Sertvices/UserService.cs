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

#pragma warning disable

namespace Praktika.Service.Sertvices
{
    public class UserService : IUserService
    {
        private IMapper mapper;
        private readonly IUnitOfWork unitofwork;
        private readonly IWebHostEnvironment env;
        private readonly IConfiguration config;
        public UserService(IUnitOfWork unitofwork, IMapper mapper, IWebHostEnvironment env, IConfiguration config)
        {
            this.mapper = mapper;
            this.unitofwork = unitofwork;
            this.env = env;
            this.config = config;
        }

        public async Task<BaseResponse<User>> CreateAsync(UserCreateDto user)
        {
            var response = new BaseResponse<User>();

            var existStudent = await unitofwork.User.GetAsync(p => p.Email == user.Email);
            if (existStudent is not null)
            {
                response.Error = new ErrorModel(400, "User is exist");
                return response;
            }

            var mappedStudent = mapper.Map<User>(user);

            mappedStudent.Image = await FileStreamExtension.SavefileAsync(user.Image.OpenReadStream(), user.Image.FileName, config, env);

            var result = await unitofwork.User.CreateAsync(mappedStudent);

            await unitofwork.SaveChanges();

            response.Data = result;

            return response;
        }

        public async Task<BaseResponse<bool>> DeleteAsync(Expression<Func<User, bool>> expression)
        {
            var response = new BaseResponse<bool>();
            var entry = await unitofwork.User.DeleteAsync(expression);
            if (entry == null)
            {
                response.Error = new ErrorModel(404, "Tugadi DeleteAsync");
                return response;
            }

            await unitofwork.SaveChanges();

            response.Data = entry;
            return response;
        }

        public async Task<BaseResponse<IEnumerable<User>>> GetAllAsync(PaginationParams @params, Expression<Func<User, bool>> expression = null)
        {
            string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

            var response = new BaseResponse<IEnumerable<User>>();

            var students = await unitofwork.User.GetAllAsync(expression);

            response.Data = students.ToPagedList(@params);

            return response;
        }

        public async Task<BaseResponse<User>> GetAsync(Expression<Func<User, bool>> expression)
        {
            var response = new BaseResponse<User>();
            var entry = await unitofwork.User.GetAsync(expression);
            if (entry == null)
            {
                response.Error = new ErrorModel(404, "Tugadi GetAsync");
                return response;
            }
            response.Data = entry;
            return response;
        }

        public async Task<BaseResponse<User>> UpdateAsync(Guid id, UserCreateDto userDto)
        {
            var response = new BaseResponse<User>();

            var user = await unitofwork.User.GetAsync(p => p.Id == id && p.State != ItemState.Deleted);
            if (user is null)
            {
                response.Error = new ErrorModel(404, "User not found, UpdateAsync");
                return response;
            }

            user.FirstName = userDto.FirstName;
            user.LastName = userDto.LastName;
            user.Email = userDto.Email;
            user.Role = userDto.Role;
            user.State = userDto.State;
            user.Password = userDto.Password;
            user.Update();

            var result = await unitofwork.User.UpdateAsync(user);

            await unitofwork.SaveChanges();

            response.Data = result;

            return response;
        }

    }
}
