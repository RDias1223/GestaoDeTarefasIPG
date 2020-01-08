using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GestaoDeTarefasIPG.Models
{
    public class Servico
    {
        public int ServicoID { get; set; }

        [Required(ErrorMessage = "Por favor, Introduza o Nome")]
        public string Nome { get; set; }

        [Required(ErrorMessage ="Por favor, Introduza o seu número de telemóvel ou telefone")]
        public string Contacto { get; set; }

        public int UnidadeOrganizacionalID { get; set;}
        public UnidadeOrganizacional UnidadeOrganizacional { get; set; }
    }
}
