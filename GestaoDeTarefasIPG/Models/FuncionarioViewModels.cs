using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestaoDeTarefasIPG.Models
{
    public class FuncionarioViewModels
    {
        public IEnumerable<Funcionario> Funcionarios { get; set; }
            
        public PaginaViewModels Paginacao { get; set; }
        public string Nome { get; set; }
        public string Morada { get; set; }
        public string Contacto { get; set; }
        public string Email { get; set; }
    }
}
