using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestaoDeTarefasIPG.Models
{
    public class UnidadeOrganizacionalViewModel
    {
        public IEnumerable<UnidadeOrganizacional> UnidadeOrganizacionals { get; set; }
        public PaginaViewModels Pagination { get; set; }
        public string CurrentName { get; set; }
    }

    
}
