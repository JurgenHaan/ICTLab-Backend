using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ICTLab_backend.Models
{
    public class UserSession
    {
        [ForeignKey("UserID")]
        public User User { get; set; }
        [Key]
        public int UserID { get; set; }
        public string SessionKey { get; set; }
        public DateTime ExpireDate { get; set; }
    }
}