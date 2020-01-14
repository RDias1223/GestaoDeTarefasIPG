using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestaoDeTarefasIPG.Models
{
    public class CargoViewModels
    {
        public IEnumerable<Cargo> Cargo { get; set; }

        public PaginaViewModels Paginacao { get; set; }
        public string Nome { get; set; }
     
    }
}
