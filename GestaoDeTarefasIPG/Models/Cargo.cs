using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GestaoDeTarefasIPG.Models
{
    public class Cargo
    {

            public int CargoId { get; set; }

        [Required(ErrorMessage = "Por favor, introduza o nome")]
        [StringLength(maximumLength: 50, MinimumLength = 3)]
        public string Nome { get; set; }

        //anotação para chave estrangeira
        public int CargoChefeId { get; set; }

        [ForeignKeyAttribute("CargoId")]
        public Cargo CargoChefe { get; set; }
    }
}
