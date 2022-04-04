using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Praktika.Domain.Common;
using Praktika.Domain.Configurations;
using Praktika.Domain.Entities;
using Praktika.Service.Interface;
using Praktika.Service.UserDto;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Praktika.Controllers
{
    [ApiController]
    [Route("Api/[controller]")]
    public class CourseController : ControllerBase
    {

        private readonly ICourseService courseservice;
        private readonly IWebHostEnvironment env;
        public CourseController(ICourseService courseservice, IWebHostEnvironment env)
        {
            this.courseservice = courseservice;
            this.env = env;
        }

        [HttpPost]
        public async Task<ActionResult<BaseResponse<Course>>> Create([FromForm] CourseCreateDto course)
        {
            var result = await courseservice.CreateAsync(course);

            return StatusCode(result.Error is null ? result.Code : result.Error.Code, result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BaseResponse<Course>>> Get(Guid id)
        {
            var result = await courseservice.GetAsync(p => p.UserId == id);

            return StatusCode(result.Error is null ? result.Code : result.Error.Code, result);
        }

        [HttpGet]
        public async Task<ActionResult<BaseResponse<IEnumerable<Course>>>> GetAll([FromQuery] PaginationParams @params)
        {
            var result = await courseservice.GetAllAsync(@params);

            return StatusCode(result.Error is null ? result.Code : result.Error.Code, result);
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<BaseResponse<bool>>> Delete(Guid id)
        {
            var result = await courseservice.DeleteAsync(p => p.Id == id);

            return StatusCode(result.Error is null ? result.Code : result.Error.Code, result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<BaseResponse<Course>>> Update(Guid id, [FromForm] CourseCreateDto courseDto)
        {
            var result = await courseservice.UpdateAsync(id, courseDto);

            return StatusCode(result.Error is null ? result.Code : result.Error.Code, result);
        }
    }
}
