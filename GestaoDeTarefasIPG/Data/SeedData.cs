using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GestaoDeTarefasIPG.Models;
namespace GestaoDeTarefasIPG.Data
{
    public class SeedData
    {
        public static void Populate (GestaoDeTarefasDbContext db) {

            SeedDataFuncionarios(db);



        }

        private static void SeedDataFuncionarios(GestaoDeTarefasDbContext db)
        {
          //  if (db.Funcionario.Any()) return;
            db.Funcionario.AddRange(
                new Funcionario {

                    Nome = "Fernando Fernandes",
                    Morada = "Adão,Guarda",
                    Contacto = "912378563",
                    Email = "FF_R45@gmail.com",

                  

                },
                 new Funcionario
                 {

                     Nome = "Tiago Mota",
                     Morada = "Sacavém, lisboa",
                     Contacto = "916378987",
                     Email = "tmota131@gmail.com",

                    

                 },
                  new Funcionario
                  {

                      Nome = "Sérgio Cardoso",
                      Morada = "Baldios,Evora",
                      Contacto = "915678563",
                      Email = "sergcArd34@hotmail.com",

                   

                  },
                   new Funcionario
                   {

                       Nome = "Rony Dias",
                       Morada = "Sacavém, Lisboa",
                       Contacto = "967378563",
                       Email = "rd5462@gmail.com",

                       

                   },
                    new Funcionario
                    {

                        Nome = "Francisco Sá ",
                        Morada = "Guarda, Guarda",
                        Contacto = "937378563",
                        Email = "barbara_R@sapo.pt",

                        

                    },
                 new Funcionario
                 {

                     Nome = "Bárbara Carneiro",
                     Morada = "Azinhoso, Castelo Branco", 
                     Contacto = "912378563",
                     Email = "barbara_R@sapo.pt",

                   

                 }
                );
            db.SaveChanges();
        }
    }
}
