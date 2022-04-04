using Microsoft.AspNetCore.Http;
using Praktika.Domain.Enums;
using Praktika.Service.Extensions;
using System;
using System.ComponentModel.DataAnnotations;

namespace Praktika.Service.UserDto
{
    public class CourseCreateDto
    {
        [Required(ErrorMessage = "Please type a CourseName."), MinLength(7)]
        public string CourseName { get; set; }
        [Required(ErrorMessage = "Please type an Author."), MinLength(6)]
        public string Author { get; set; }
        public ItemState State { get; set; }

        [Required(ErrorMessage = "Please select a file.")]
        [DataType(DataType.Upload)]
        [AllowedExtensions(new string[] { ".jpg", ".png", ".jpeg" }), MaxFileSize(5 * 1024 * 1024)]
        public IFormFile Image { get; set; }
        [Required]
        public Guid UserId { get; set; }

    }
}
