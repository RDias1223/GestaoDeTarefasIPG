using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestaoDeTarefasIPG.Models
{
    public class PaginaViewModels
    {
        public int PaginaCorrente { get; set; }
        public int TotalIntem { get; set; }
        public int TamanhoPagina { get; set; }
        public String Nome { get; set; }
    }
}
