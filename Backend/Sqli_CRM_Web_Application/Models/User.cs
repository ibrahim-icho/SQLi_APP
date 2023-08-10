using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Sqli_CRM_Web_Application.Models
{
    public class User : IdentityUser
    {
        [Required, MaxLength(50)]
        public required string Firstname { get; set; } = String.Empty;
        [Required, MaxLength(50)]
        public required string Lastname { get; set; } = String.Empty;
    }
}

