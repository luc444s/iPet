namespace iPet.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Web;

    public enum TiposUsuarios{
        [Display(Name = "Pessoa Física")]
        PessoaFisica,
        [Display(Name = "Pessoa Júridica")]
        PessoaJuridica
    };

    [Table("Usuarios")]
    public partial class User
    {
        [Key]
        public int ID { get; set; }

        [Required]
        [StringLength(20)]
        [Display(Name = "Nome")]
        public string Nome { get; set; }

        [Display(Name = "File")]
        [DataType(DataType.Upload)]
        [NotMapped]
        public HttpPostedFileBase AvatarFile { get; set; }

        [Display(Name = "Avatar")]
        public string Avatar { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Usuário")]
        public string Login { get; set; }

        [Required]
        public string Endereco { get; set; }

        [Phone]
        public string Telefone { get; set; }

        [Required]
        public TiposUsuarios TipoUsuario { get; set; }

        [Required]
        public string Cpf_Cnpj { get; set; }

        [Required]
        public string Senha { get; set; }

        public int GroupID { get; set; }

        [ForeignKey("GroupID")]
        public virtual Group Group { get; set; }

        public virtual IEnumerable<FavoritePet> FavoritesPets { get; set; }

        public virtual IEnumerable<Pet> Pets { get; set; }
    }
}
