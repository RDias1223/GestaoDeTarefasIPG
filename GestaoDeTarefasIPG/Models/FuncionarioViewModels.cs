using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestaoDeTarefasIPG.Models
{
    public class FuncionarioViewModels
    {
        public IEnumerable<Funcionario> Funcionarios { get; set; }

        public int PaginaCorrente { get; set; }
        public int TotalPagina { get; set; }
        public int PrimeiraPagMortrar { get; set; }
        public int UltimaPagMostrar { get; set; }
    }
}
