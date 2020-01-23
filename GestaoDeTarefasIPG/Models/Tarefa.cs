using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GestaoDeTarefasIPG.Models
{
    public class Tarefa
    {
        public int TarefaID { get; set; }

        [Required(ErrorMessage = "Por favor, Introduza o Nome da Tarefa")]
        public string Nome { get; set; }

        [StringLength(1000)]
        [Display(Name = "Descrição")]
        public string Descricao { get; set; }

        public int FuncionarioId { get; set; }
        public Funcionario Funcionario { get; set; }
    }
}
