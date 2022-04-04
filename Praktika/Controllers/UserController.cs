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
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService userservice;
        private readonly IWebHostEnvironment env;
        public UserController(IUserService userService, IWebHostEnvironment env)
        {
            this.userservice = userService;
            this.env = env;
        }

        [HttpPost]
        public async Task<ActionResult<BaseResponse<User>>> Create([FromForm] UserCreateDto user)
        {
            var result = await userservice.CreateAsync(user);

            return StatusCode(result.Error is null ? result.Code : result.Error.Code, result);
        }

        [HttpGet("{user-id}")]
        public async Task<ActionResult<BaseResponse<User>>> Get(Guid id)
        {
            var result = await userservice.GetAsync(p => p.Id == id);

            return StatusCode(result.Error is null ? result.Code : result.Error.Code, result);
        }

        [HttpGet]
        public async Task<ActionResult<BaseResponse<IEnumerable<User>>>> GetAll([FromQuery] PaginationParams @params)
        {
            var result = await userservice.GetAllAsync(@params);

            return StatusCode(result.Error is null ? result.Code : result.Error.Code, result);
        }
        [HttpDelete("{user-id}")]
        public async Task<ActionResult<BaseResponse<bool>>> Delete(Guid id)
        {
            var result = await userservice.DeleteAsync(p => p.Id == id);

            return StatusCode(result.Error is null ? result.Code : result.Error.Code, result);
        }

        [HttpPut("{user-id}")]
        public async Task<ActionResult<BaseResponse<User>>> Update(Guid id, [FromForm] UserCreateDto userDto)
        {
            var result = await userservice.UpdateAsync(id, userDto);

            return StatusCode(result.Error is null ? result.Code : result.Error.Code, result);
        }

    }
}
