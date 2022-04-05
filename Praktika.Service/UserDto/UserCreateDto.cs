using Microsoft.AspNetCore.Http;
using Praktika.Domain.Enums;
using Praktika.Service.Extensions;
using System.ComponentModel.DataAnnotations;

namespace Praktika.Service.UserDto
{
    public class UserCreateDto
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required, MinLength(8)]
        public string Password { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; }
        public ItemState State { get; set; }

        [Required(ErrorMessage = "Please select a file.")]
        [DataType(DataType.Upload)]
        [MaxFileSize(5 * 1024 * 1024)]
        [AllowedExtensions(new string[] { ".jpg", ".png" })]
        public IFormFile Image { get; set; }
        public UserRole Role { get; set; }

    }
}
