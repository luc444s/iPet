using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using iPet.Models;

namespace iPet.Models.AjaxSerialization
{
    public class PetFinder
    {
        public string Raca { get; set; }

        public Sexos Sexo { get; set; }

        public Portes Porte { get; set; }

        public Cores Cor { get; set; }

        public float Preco { get; set; }
    }
}