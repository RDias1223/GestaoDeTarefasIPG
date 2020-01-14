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
            SeedDataCargo(db);



        }

      

        public static async Task EnsurePopulatedAsync (UserManager<IdentityUser> userManager)
        {
            const string Admin_user = "issoandadificil@gmail.com";
            const string Admin_Password = "sorestatentar";

            const string Funcionario_user = "estaafazersentido@gmail.com";
            const string Funcionario_Password = "cotinuaatentar";
            
            // ADMINISTRADOR
            IdentityUser Admin = await userManager.FindByNameAsync(Admin_user);

            if (Admin == null)
            {
                Admin = new IdentityUser { UserName = Admin_user };
                await userManager.CreateAsync(Admin, Admin_Password);
            }

            if (!await userManager.IsInRoleAsync(Admin, "Administrador"))
            {
                await userManager.AddToRoleAsync(Admin, "Administrador");
            }


            //funcionario
            IdentityUser funcionar = await userManager.FindByNameAsync(Funcionario_user);

            if (funcionar == null)
            {
                funcionar = new IdentityUser { 
                    UserName = Funcionario_user };
                await userManager.CreateAsync(funcionar, Funcionario_Password);
            }

            if (!await userManager.IsInRoleAsync(funcionar, "Administrador"))
            {
                await userManager.AddToRoleAsync(funcionar, "Administrador");
            }
            
        }
        public static async Task CreateRolesAsync(RoleManager<IdentityRole> roleManager)
        {
            if (!await roleManager.RoleExistsAsync("Administrador"))
            {
                await roleManager.CreateAsync(new IdentityRole("Administrador"));
            }
            if (!await roleManager.RoleExistsAsync("Funcionario"))
            {
                await roleManager.CreateAsync(new IdentityRole("Funcionario"));
            }
        }

        private static void SeedDataCargo(GestaoDeTarefasDbContext db)
        {
          if (db.Cargo.Any()) return;
          
           
            db.Cargo.AddRange(
            new Cargo {Nome="Presidente",},
            new Cargo { Nome = "Vice Presidente",  },
            new Cargo { Nome = "Director ESTG", },
            new Cargo { Nome = "Director ESS",  },
            new Cargo { Nome = "Director ESECD",  },
            new Cargo { Nome = "Director ESTH",  },
            new Cargo { Nome = "Chefe de Funcionarios ESTG", },
            new Cargo { Nome = "Chefe de Funcionarios ESECD", },
            new Cargo { Nome = "Chefe de Funcionarios ESS",},
            new Cargo { Nome = "Chefe de Funcionarios ESTH",  },
            new Cargo { Nome = "Chefe de Funcionarios Ação Social", }
            );
            db.SaveChanges();
        }



        private static void SeedDataFuncionarios(GestaoDeTarefasDbContext db)
        {   
            if (db.Funcionario.Any()) return;

            Cargo Empregados = GetCargoCreatingIfNeed(db, "Responsavel pela limpeza");

            Cargo Seguranca = GetCargoCreatingIfNeed(db, "Segurança");

            db.Funcionario.AddRange(
            new Funcionario
            {

                Nome = "Fernando Fernandes",
                Endereco = " Resi Masculina 1 Rua Soeirov Viegas Nº6",
                CodigoPostal = "6300-758 GUARDA",
                Data_Nascimento = new DateTime(1990, 4, 19),
                Contacto = "912377773",
                Email = "FF_R45@gmail.com",
                CargoId =Seguranca.CargoId


            },
                 new Funcionario
                 {

                     Nome = "Tiago Mota",
                     Endereco = "AV DA IGREJA  PORTO MOS SAO JOAO BAPTISTA PEDRO, LOTE R1 22 R/C ESQ.",
                     CodigoPostal = ", 2480-301 LEIRIA",
                     Data_Nascimento = new DateTime(1980, 4, 19),
                     Contacto = "916378987",
                     Email = "tmota131@gmail.com",
                     CargoId = Seguranca.CargoId



                 },
                  new Funcionario
                  {

                      Nome = "Sérgio Cardoso",
                      Endereco = " R ELIAS GARCIA 228 SALA 7,  ARCOZELO BARCELOS",
                      CodigoPostal = ",4750-144 BRAGA",
                      Data_Nascimento = new DateTime(1989, 12, 19),
                      Contacto = "915678563",
                      Email = "sergcArd34@hotmail.com",
                      CargoId = Seguranca.CargoId


                  },
                   new Funcionario
                   {

                       Nome = "Rony Dias",
                       Endereco = "Travessa do Açougue a São Vicente  nº 45",
                       CodigoPostal = "1100 - 005 LISBOA",
                       Data_Nascimento = new DateTime(1979, 2, 23),
                       Contacto = "967378563",
                       Email = "rd5462@gmail.com",
                       CargoId = Empregados.CargoId


                   },
                    new Funcionario
                    {

                        Nome = "Francisco Sá ",
                        Endereco = " Resi Masculina 1 Rua Soeirov Viegas Nº6",
                        CodigoPostal = "6300-758 Guarda",
                        Data_Nascimento = new DateTime(1975, 1, 29),
                        Contacto = "937378563",
                        Email = "rtrdj9@sapo.pt",
                        CargoId = Empregados.CargoId


                    },
                 new Funcionario
                 {

                     Nome = "Bárbara Carneiro",
                     Endereco = " Resi Masculina 1 Rua Soeirov Viegas Nº6",
                     CodigoPostal = "6300-758 Guarda",
                     Data_Nascimento = new DateTime(1983, 3, 12),
                     Contacto = "912378563",
                     Email = "barbara_R@sapo.pt",
                     CargoId = Empregados.CargoId


                 },
                    new Funcionario
                    {

                        Nome = "Gustavo Carneiro",
                        Endereco = " Resi Masculina 1 Rua Soeirov Viegas Nº6",
                        CodigoPostal = "6300-758 Guarda",
                        Data_Nascimento = new DateTime(1969, 07, 18),
                        Contacto = "918878563",
                        Email = "gust_C@sapo.pt",
                        CargoId = Seguranca.CargoId


                    },
                       new Funcionario
                       {

                           Nome = "João Carneiro",
                           Endereco = "Rua Francisco Sá Carneiro Nº6",
                           CodigoPostal = "6300-225 Guarda",
                           Data_Nascimento = new DateTime(1980, 8, 19),
                           Contacto = "91098563",
                           Email = "joaocarneiro@gmail.com",
                           CargoId = Seguranca.CargoId


                       },
                       new Funcionario
                       {

                           Nome = "Bárbara Sousa",
                           Endereco = " Rua da Boa Vista Nº76",
                           CodigoPostal = "3000-105 Coimbra",
                           Data_Nascimento = new DateTime(1992, 10, 30),
                           Contacto = "967878563",
                           Email = "hhdfca_R@gmail.com",
                           CargoId = Empregados.CargoId



                       }
                );

            db.SaveChanges();
        }

        private static Cargo GetCargoCreatingIfNeed(GestaoDeTarefasDbContext db, string nome)
        {
           Cargo cargo= db.Cargo.SingleOrDefault(e => e.Nome == nome);

            if(cargo == null)
            {
              cargo = new Cargo { Nome = nome };
                db.Add(cargo);
                db.SaveChanges();
            }

            return cargo;
        }
    }
}
