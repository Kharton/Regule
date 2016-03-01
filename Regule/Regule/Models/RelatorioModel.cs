using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Regule.Models
{
    public class Relatorio
    {
        [Required]
        [Display(Name ="Início")]
        public DateTime Inicio { get; set; }

        [Required]
        [Display(Name = "Final")]
        public DateTime Fim { get; set; }

        public int Produto { get; set; }

        public int Cliente { get; set; }

        public int Fornecedor{ get; set; }
    }

}