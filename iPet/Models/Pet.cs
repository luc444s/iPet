namespace iPet.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Web;
    
    [Table("Pets")]
    public partial class Pet
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
        public HttpPostedFileBase PetImageFile { get; set; }

        [Display(Name = "Foto")]
        public string PetImage { get; set; }

        [Required]
        public Portes Porte { get; set; }

        [Display(Name = "Raça")]
        [Required]
        public string Raca { get; set; }

        public bool Castrado { get; set; }

        [Required]
        [Display(Name = "Preço")]
        public float Preco { get; set; }

        public Cores Cor { get; set; }

        public Sexos Sexo { get; set; }

        public bool Vacinado { get; set; }

        [Display(Name = "Descrição")]
        public string Description { get; set; }

        public int UserID { get; set; }

        [ForeignKey("UserID")]
        public virtual User User { get; set; }

        [NotMapped]
        public double MatchPercent { get; set; }

        public void GetMatch(string raca, Portes porte, Sexos sexo, Cores cor, float preco) {
            double score = 0;

            // Influencia em 35%
            if (raca == Raca)
            {
                score += 35;
            }

            // Influencia em 20%
            if (sexo == Sexo)
            {
                score += 20;
            }

            // Influencia em 25%
            if (porte == Porte)
            {
                score += 25;
            }
            else
            {
                if (porte == Portes.Pequeno || porte == Portes.Grande)
                {
                    if (Porte == Portes.Medio)
                    {
                        score += 12.5;
                    }
                }
            }

            // Influencia em 10%
            if (cor == Cor && cor != Cores.Outro)
            {
                score += 10;
            }

            if (cor == Cores.Outro && Cor == Cores.Outro) {
                score += 5;
            }

            // Influencia em 10%
            if (Preco <= preco)
            {
                score += 10;
            }
            else
            {
                if (Preco - 100 <= preco)
                {
                    score += 8;
                }
                else
                {
                    if (Preco - 150 <= preco)
                    {
                        score += 7;
                    }
                    else
                    {
                        if (Preco - 150 <= preco)
                        {
                            score += 5;
                        }
                        else
                        {
                            if (Preco - 200 <= preco)
                            {
                                score += 3;
                            }
                        }
                    }                    
                }
            }

            MatchPercent = score;
        }
    }
}
