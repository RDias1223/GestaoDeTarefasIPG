using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestaoDeTarefasIPG.Models
{
    public class PaginaViewModels
    {
        public int PaginaCorrente { get; set; }
        public int TotalItens { get; set; }
        public int TamanhoPagina { get; set; }
        public int NumeroPagina => (int)Math.Ceiling((double)TotalItens / TamanhoPagina);
        public String Nome { get; set; }
    }
}
