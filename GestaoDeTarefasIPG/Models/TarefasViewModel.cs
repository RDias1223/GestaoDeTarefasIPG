using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestaoDeTarefasIPG.Models
{
    public class TarefasViewModel
    {
        public IEnumerable<Tarefa> Tarefa { get; set; }
        public PaginaViewModels Pagination { get; set; }
        public string CurrentName { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }

    }
}
