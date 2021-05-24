using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace iPet.Models
{
    public class Login
    {
        [Display(Name = "Email ou Usuário")]
        [Required]
        public string Email { get; set; }

        [Display(Name = "Senha")]
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}