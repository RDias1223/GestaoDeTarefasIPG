using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GestaoDeTarefasIPG.Models
{
    public class Professor
    {

        public int ProfessorId { get; set; }


        [Required(ErrorMessage = "Nome é Obrigatório")]
        [StringLength(250)]
        public string Nome { get; set; }


        [Required(ErrorMessage = "Adicione seu Contato")]
        [RegularExpression(@"(2\d{8})|(9[1236]\d{7})", ErrorMessage = "Contato Incorreto")]
        public int Contato{ get; set; }


        [Required(ErrorMessage = "Adicione seu Email")]
        [EmailAddress(ErrorMessage = "Email Incorreto")]
        public string Email { get; set; }

    }
}
