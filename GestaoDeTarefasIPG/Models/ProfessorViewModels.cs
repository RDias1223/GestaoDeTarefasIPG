using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestaoDeTarefasIPG.Models
{
    public class ProfessorViewModels
    {


        public IEnumerable<Funcionario> Funcionarios { get; set; }

        public PaginaViewModels Paginacao { get; set; }
        public string Nome { get; set; }

    }
}
