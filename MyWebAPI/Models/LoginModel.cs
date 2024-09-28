﻿using System.ComponentModel.DataAnnotations;

namespace MyWebAPI.Models
{
    public class LoginModel
    {
        [Required]
        [MaxLength(50)]
        public string? UserName { get; set; }
        [Required]
        [MaxLength(100)]
        public string? Password { get; set; }
    }
}
