using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestaoDeTarefasIPG.Models
{
    public class ProfessorViewModels
    {

        public IEnumerable<Professor> Professors { get; set; }


        public string Nome { get; set; }

        public string Morada { get; set; }
        
        public int Contato { get; set; }

        public string Email { get; set; }

      


    }
}
