using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace GestaoDeTarefasIPG.Models
{
    public class UnidadeOrganizacionalViewModel
    {
        public IEnumerable<UnidadeOrganizacional> UnidadeOrganizacional { get; set; }
        public PaginaViewModels Pagination { get; set; }
       
        public string CurrentName { get; set; }

        public string Nome { get; set; }
      
        public string Contacto { get; set; }
        public string Email { get; set; }
    }

    
}
