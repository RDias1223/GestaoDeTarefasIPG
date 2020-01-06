using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GestaoDeTarefasIPG.Models
{
    public class Funcionario
    {
        public int FuncionarioId { get; set; }

        [Required(ErrorMessage = "Por favor, introduza o nome")]
        [StringLength(maximumLength: 50, MinimumLength = 3)]
        public string Nome { get; set; }


        [Required(ErrorMessage = "Por favor, introduza o seu Endereço")]
        [StringLength(maximumLength: 100, MinimumLength = 3)]
           public string Endereco { get; set; }


        [Required(ErrorMessage = "Por favor, introduza o seu Codigo Postal")]
        [RegularExpression(@"^\d{4}-\d{3}(.+[A-Za-z])$", ErrorMessage = "Codigo postal inválido")]
        public string CodigoPostal { get; set; }

        [Required(ErrorMessage = "Por favor, introduza a data de nascimento")]
        [DataType(DataType.Date, ErrorMessage = "Data de nascimento inválida")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = false)]
        public DateTime Data_Nascimento { get; set; }

        [Required(ErrorMessage = "Por favor, introduza o seu número de telemóvel/telefone")]
        [RegularExpression(@"(2\d{8})|(9[1236]\d{7})", ErrorMessage = "Contacto inválido")]
        public string Contacto { get; set; }

        [Required(ErrorMessage = "Por favor, introduza o email")]
        [EmailAddress(ErrorMessage = "Email inválido")]
        public string Email { get; set; }

    }
}
