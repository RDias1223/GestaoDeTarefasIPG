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


        [Required]
        [StringLength(250)]
        public string Nome { get; set; }


        [Required]
        public int Contato{ get; set; }


        [Required]
        public int Email { get; set; }

    }
}
