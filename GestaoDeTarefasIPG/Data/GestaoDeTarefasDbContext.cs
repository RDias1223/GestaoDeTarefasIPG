using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using GestaoDeTarefasIPG.Models;

    public class GestaoDeTarefasDbContext : DbContext
    {
        public GestaoDeTarefasDbContext (DbContextOptions<GestaoDeTarefasDbContext> options)
            : base(options)
        {
        }

        public DbSet<GestaoDeTarefasIPG.Models.Funcionario> Funcionario { get; set; }

        public DbSet<GestaoDeTarefasIPG.Models.UnidadeOrganizacional> UnidadeOrganizacional { get; set; }

        public DbSet<GestaoDeTarefasIPG.Models.Servico> Servico { get; set; }
    }
