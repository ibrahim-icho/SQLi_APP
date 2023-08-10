﻿using System.ComponentModel.DataAnnotations;

namespace Sqli_CRM_Web_Application.Requests
{
    public class AddRoleRequest
    {
        [Required]
        public string UserId { get; set; } = string.Empty;
        [Required]
        public string Role { get; set; } = string.Empty;
    }
}
