﻿using System.ComponentModel.DataAnnotations;

namespace ICTLab_backend.Models
{
    public class User
    {
        [Key]
        public int ID { get; set; }

        [Required]
        [StringLength(60)]
        public string UserName { get; set; }

        [Required]
        [StringLength(64, MinimumLength = 5)]
        public string Password { get; set; }

        public string UserType { get; set; }
        public string fullName { get; set; }
    }
}