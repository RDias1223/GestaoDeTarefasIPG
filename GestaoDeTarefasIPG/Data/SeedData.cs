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
        private const string Roles_Presidente = "Administrador";
        private const string Roles_Director = "Director";


        public static void Populate(GestaoDeTarefasDbContext db)
        {

            SeedDataFuncionarios(db);
            SeedDataCargo(db);
            SeedDataUnidadeOrganizacional(db);



        }


        public static void SeedDataUnidadeOrganizacional(GestaoDeTarefasDbContext db)
        {
            if (db.UnidadeOrganizacional.Any()) return;

            db.UnidadeOrganizacional.AddRange(
            new UnidadeOrganizacional
            {
                Nome = "Escola Superior de Tecnologia e Gestão",
                Contacto = "270230510",
                Email = "EscolaTecnologia@ipg.pt",
            },
            new UnidadeOrganizacional
            {
                Nome = "Escola Superior de Saúde",
                Contacto = "270230605",
                Email = "EscolaSaude@ipg.pt",
            },
             new UnidadeOrganizacional
             {
                 Nome = "Escola Superior de Turismo e Hotelaria",
                 Contacto = "270230705",
                 Email = "EscolaTurismo@ipg.pt",
             },
             new UnidadeOrganizacional
             {
                 Nome = "Escola Superior de Educação",
                 Contacto = "270230807",
                 Email = "EscolaEducaçao@ipg.pt",
             });
            db.SaveChanges();
        }


        public static async Task PopulateUserAsync (UserManager<IdentityUser> userManager)
        {
            const string Admin_user = "presidente@gmail.com";
            const string Admin_Password = "soresta_Tente1#";

            const string Director_user = "estaafazersentido@gmail.com";
            const string Director_Password = "cotinua_a_tentar2";
            
            // Presidente
            IdentityUser user = await userManager.FindByNameAsync(Admin_user);

            if (user == null)
            {
                user = new IdentityUser {
                    UserName = Admin_user,
                    Email = Admin_user };

                await userManager.CreateAsync(user, Admin_Password);
            }

            if (!await userManager.IsInRoleAsync(user, Roles_Presidente))
            {
                await userManager.AddToRoleAsync(user, Roles_Presidente);
            }


            //Director
            user = await userManager.FindByNameAsync(Director_user);

            if (user == null)
            {
               user= new IdentityUser { 
                    UserName = Director_user, 
                   Email=Director_user };
                await userManager.CreateAsync(user, Director_Password);
            }

            if (!await userManager.IsInRoleAsync(user, Roles_Director))
            {
                await userManager.AddToRoleAsync(user, Roles_Director);
            }

            user = await userManager.FindByNameAsync("test@gmail.com");
            if (user == null){
                user = new IdentityUser{
                    UserName = "test@gmail.com",
                    Email = "test@gmail.com"
                };

                await userManager.CreateAsync(user, Admin_Password);
            }

            if (user == null)
            {
                user = new IdentityUser
                {
                    UserName = "test3@gmail.com",
                    Email = "test3@gmail.com"
                };

                await userManager.CreateAsync(user, Director_Password);
            }
        }
        public static async Task CreateRolesAsync(RoleManager<IdentityRole> roleManager)
        {
            if (!await roleManager.RoleExistsAsync(Roles_Presidente)){
                await roleManager.CreateAsync(new IdentityRole(Roles_Presidente));
            }

            if (!await roleManager.RoleExistsAsync(Roles_Director)){
                await roleManager.CreateAsync(new IdentityRole(Roles_Director));
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
