using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GestaoDeTarefasIPG.Models
{
    public class UnidadeOrganizacional
    {

        public int UnidadeOrganizacionalID { get; set; }

        [Required(ErrorMessage = "Por favor, Entroduza o nome")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Por favor, introduza o seu número de telemóvel ou telefone")]
        [RegularExpression(@"(2\d{8})|(9[1236]\d{7})", ErrorMessage = "Contacto inválido")]
        public string Contacto { get; set; }

        [Required(ErrorMessage = "Por favor, introduza o email")]
        [EmailAddress(ErrorMessage = "Email inválido")]
        public string Email { get; set; }
    }
}
