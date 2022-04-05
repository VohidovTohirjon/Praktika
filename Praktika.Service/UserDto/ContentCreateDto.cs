using Microsoft.AspNetCore.Http;
using Praktika.Domain.Enums;
using Praktika.Service.Extensions;
using System;
using System.ComponentModel.DataAnnotations;

namespace Praktika.Service.UserDto
{
    public class ContentCreateDto
    {
        [Required]
        public int Duration { get; set; }

        [Required]
        public string TypeOfCourse { get; set; }
        public ItemState State { get; set; }

        [Required(ErrorMessage = "Please select a file.")]
        [DataType(DataType.Upload)]
        [MaxFileSize(5 * 1024 * 1024)]
        [AllowedExtensions(new string[] { ".jpg", ".png" })]
        public IFormFile Image { get; set; }

        [Required]
        public Guid CourseId { get; set; }
    }
}
