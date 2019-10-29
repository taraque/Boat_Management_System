using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BoatManagement.Models
{
    public class LoginModel
    {
        [Required]
        [StringLength(20)]
        public string Username { get; set; }
        [Required]
        [StringLength(50)]
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }
}