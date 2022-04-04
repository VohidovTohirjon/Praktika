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
    public class CourseService : ICourseService
    {
        private readonly IMapper mapper;
        private readonly IUnitOfWork unitofwork;
        private readonly IWebHostEnvironment env;
        private readonly IConfiguration config;
        public CourseService(IMapper mapper, IUnitOfWork unitofwork, IWebHostEnvironment env, IConfiguration config)
        {
            this.unitofwork = unitofwork;
            this.env = env;
            this.config = config;
            this.mapper = mapper;
        }
        public async Task<BaseResponse<Course>> CreateAsync(CourseCreateDto course)
        {
            var response = new BaseResponse<Course>();

            var existStudent = await unitofwork.Course.GetAsync(p => p.CourseName == course.CourseName);
            if (existStudent is not null)
            {
                response.Error = new ErrorModel(400, "Course is exist");
                return response;
            }

            var mappedCourse = mapper.Map<Course>(course);

            mappedCourse.Image = await FileStreamExtension.SavefileAsync(course.Image.OpenReadStream(), course.Image.FileName, config, env);

            var result = await unitofwork.Course.CreateAsync(mappedCourse);

            await unitofwork.SaveChanges();

            response.Data = result;

            return response;
        }

        public async Task<BaseResponse<bool>> DeleteAsync(Expression<Func<Course, bool>> expression)
        {
            var response = new BaseResponse<bool>();
            var entry = await unitofwork.Course.DeleteAsync(expression);
            if (entry == null)
            {
                response.Error = new ErrorModel(404, "Tugadi DeleteAsync");
                return response;
            }

            await unitofwork.SaveChanges();

            response.Data = entry;
            return response;
        }

        public async Task<BaseResponse<IEnumerable<Course>>> GetAllAsync(PaginationParams @params, Expression<Func<Course, bool>> expression = null)
        {
            string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

            var response = new BaseResponse<IEnumerable<Course>>();

            var students = await unitofwork.Course.GetAllAsync(expression);

            response.Data = students.ToPagedList(@params);

            return response;
        }

        public async Task<BaseResponse<Course>> GetAsync(Expression<Func<Course, bool>> expression)
        {
            var response = new BaseResponse<Course>();
            var entry = await unitofwork.Course.GetAsync(expression);
            if (entry == null)
            {
                response.Error = new ErrorModel(404, "Course not found, Tugadi GetAsync");
                return response;
            }

            unitofwork.SaveChanges();

            response.Data = entry;
            return response;
        }

        public async Task<BaseResponse<Course>> UpdateAsync(Guid id, CourseCreateDto courseDto)
        {
            var response = new BaseResponse<Course>();

            var course = await unitofwork.Course.GetAsync(p => p.Id == id && p.State != ItemState.Deleted);
            if (course is null)
            {
                response.Error = new ErrorModel(404, "Course not found, UpdateAsync");
                return response;
            }

            course.CourseName = courseDto.CourseName;
            course.Author = courseDto.Author;
            course.State = courseDto.State;
            course.Update();

            var result = await unitofwork.Course.UpdateAsync(course);

            await unitofwork.SaveChanges();

            response.Data = result;

            return response;
        }

    }
}
