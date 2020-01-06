using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GestaoDeTarefasIPG.Models
{
    public class Tarefa
    {
        public int TarefaId { get; set; } 

        [Required(ErrorMessage = "Por favor, introduza o nome")]
        [StringLength(maximumLength: 50, MinimumLength = 3)]
        public string Nome{ get; set; }
        [Required(ErrorMessage = "Por favor, introduza o valor da prioridade")]
        [RegularExpression(@"([0-5]\d{0})", ErrorMessage = "prioridade inválida")]
        public int Prioridade { get; set; }
        
    }
}
