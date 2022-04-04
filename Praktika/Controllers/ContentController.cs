﻿using Microsoft.AspNetCore.Mvc;
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
    public class ContentController : ControllerBase
    {

        private readonly IContentService contentservice;
        public ContentController(IContentService contentservice)
        {
            this.contentservice = contentservice;
        }

        [HttpPost]
        public async Task<ActionResult<BaseResponse<Content>>> Create([FromForm] ContentCreateDto content)
        {
            var result = await contentservice.CreateAsync(content);

            return StatusCode(result.Error is null ? result.Code : result.Error.Code, result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BaseResponse<Course>>> Get(Guid id)
        {
            var result = await contentservice.GetAsync(p => p.Id == id);

            return StatusCode(result.Error is null ? result.Code : result.Error.Code, result);
        }

        [HttpGet]
        public async Task<ActionResult<BaseResponse<IEnumerable<Content>>>> GetAll([FromQuery] PaginationParams @params)
        {
            var result = await contentservice.GetAllAsync(@params);

            return StatusCode(result.Error is null ? result.Code : result.Error.Code, result);
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<BaseResponse<bool>>> Delete(Guid id)
        {
            var result = await contentservice.DeleteAsync(p => p.Id == id);

            return StatusCode(result.Error is null ? result.Code : result.Error.Code, result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<BaseResponse<Content>>> Update(Guid id, [FromForm] ContentCreateDto courseDto)
        {
            var result = await contentservice.UpdateAsync(id, courseDto);

            return StatusCode(result.Error is null ? result.Code : result.Error.Code, result);
        }
    }
}
