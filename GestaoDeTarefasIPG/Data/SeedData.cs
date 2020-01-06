using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using GestaoDeTarefasIPG.Models;
using System.Threading.Tasks;

namespace GestaoDeTarefasIPG.Data
{
    public class SeedData
    {
        public static void Populate(GestaoDeTarefasDbContext db)
        {

            SeedDataFuncionarios(db);



        }


        private static void SeedDataFuncionarios(GestaoDeTarefasDbContext db)
        {   
            if (db.Funcionario.Any()) return;

            db.Funcionario.AddRange(
            new Funcionario
                {

                    Nome = "Fernando Fernandes",
                    Endereco = " Resi Masculina 1 Rua Soeirov Viegas Nº6",
                    CodigoPostal = "6300-758 GUARDA",
                   Data_Nascimento = new DateTime(1990, 4, 19),
                    Contacto = "912377773",
                    Email = "FF_R45@gmail.com",



                },
                 new Funcionario
                 {

                     Nome = "Tiago Mota",
                     Endereco = "AV DA IGREJA  PORTO MOS SAO JOAO BAPTISTA PEDRO, LOTE R1 22 R/C ESQ.",
                     CodigoPostal = ", 2480-301 LEIRIA",
                     Data_Nascimento = new DateTime(1980, 4, 19),
                     Contacto = "916378987",
                     Email = "tmota131@gmail.com",



                 },
                  new Funcionario
                  {

                      Nome = "Sérgio Cardoso",
                      Endereco = " R ELIAS GARCIA 228 SALA 7,  ARCOZELO BARCELOS",
                      CodigoPostal = ",4750-144 BRAGA",
                      Data_Nascimento = new DateTime(1989, 12, 19),
                      Contacto = "915678563",
                      Email = "sergcArd34@hotmail.com",



                  },
                   new Funcionario
                   {

                       Nome = "Rony Dias",
                       Endereco = "Travessa do Açougue a São Vicente  nº 45",
                       CodigoPostal = "1100 - 005 LISBOA",
                       Data_Nascimento = new DateTime(1979, 2, 23),
                       Contacto = "967378563",
                       Email = "rd5462@gmail.com",



                   },
                    new Funcionario
                    {

                        Nome = "Francisco Sá ",
                        Endereco = " Resi Masculina 1 Rua Soeirov Viegas Nº6",
                        CodigoPostal = "6300-758 Guarda",
                        Data_Nascimento = new DateTime(1975, 1, 29),
                        Contacto = "937378563",
                        Email = "rtrdj9@sapo.pt",



                    },
                 new Funcionario
                 {

                     Nome = "Bárbara Carneiro",
                     Endereco = " Resi Masculina 1 Rua Soeirov Viegas Nº6",
                     CodigoPostal = "6300-758 Guarda",
                     Data_Nascimento = new DateTime(1983, 3, 12),
                     Contacto = "912378563",
                     Email = "barbara_R@sapo.pt",



                 },
                    new Funcionario
                    {

                        Nome = "Gustavo Carneiro",
                        Endereco = " Resi Masculina 1 Rua Soeirov Viegas Nº6",
                        CodigoPostal = "6300-758 Guarda",
                        Data_Nascimento = new DateTime(1969, 07, 18),
                        Contacto = "918878563",
                        Email = "gust_C@sapo.pt",



                    },
                       new Funcionario
                       {

                           Nome = "João Carneiro",
                           Endereco = "Rua Francisco Sá Carneiro Nº6",
                           CodigoPostal = "6300-225 Guarda",
                           Data_Nascimento = new DateTime(1980, 8, 19),
                           Contacto = "91098563",
                           Email = "joaocarneiro@gmail.com",



                       },
                       new Funcionario
                       {

                           Nome = "Bárbara Carneiro",
                           Endereco = " Rua da Boa Vista Nº76",
                           CodigoPostal = "3000-105 Coimbra",
                           Data_Nascimento = new DateTime(1992, 10, 30),
                           Contacto = "967878563",
                           Email = "hhdfca_R@gmail.com",



                       }
                );

           db.SaveChanges();
        }
    }
}
