using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GestaoDeTarefasIPG.Models;

namespace GestaoDeTarefasIPG.Controllers
{
    public class FuncionariosController : Controller

    {
        private const int Tamanho_Pagina = 5;


        private readonly GestaoDeTarefasDbContext _context;

        public FuncionariosController(GestaoDeTarefasDbContext context)
        {
            
            _context = context;
        }

        // GET: Funcionarios
        public async Task<IActionResult> Index(FuncionarioViewModels modelo = null, int pagina = 1, string nome = null, string morada=null, string contacto=null, string email=null)
        {
            if (modelo != null || modelo.Nome!= null)
            {
                nome = modelo.Nome;
             }

            IQueryable<Funcionario>funcionario;
            int numfuncionario;
            IEnumerable<Funcionario> listaFuncionario;

            if (!string.IsNullOrEmpty(nome)) 
            {
                funcionario = _context.Funcionario
                    .Where(p=> p.Nome.Contains(nome.Trim()));

                numfuncionario = await funcionario.CountAsync();

                listaFuncionario = await funcionario
                    .OrderBy(p => p.Nome)
                    .Skip(Tamanho_Pagina * (pagina - 1))
                    .Take(Tamanho_Pagina)
                    .ToListAsync();
            }
            else
            {

                funcionario = _context.Funcionario;

                 numfuncionario = await funcionario.CountAsync();

                listaFuncionario = await funcionario
                    .OrderBy(p => p.Nome)
                    .Skip(Tamanho_Pagina * (pagina - 1))
                    .Take(Tamanho_Pagina)
                    .ToListAsync();
              
            }
            if (pagina>(numfuncionario / Tamanho_Pagina) + 1)
            {
                pagina = 1;
            }


            if (listaFuncionario.Count() == 0)
            {
                TempData["NoItemsFound"] = "Não foram encontrados resultados para a sua pesquisa";
            }   


            return View(new FuncionarioViewModels { 
                Funcionarios= listaFuncionario,
                Paginacao =new PaginaViewModels
                {
                    PaginaCorrente = pagina,
                    TamanhoPagina = Tamanho_Pagina,
                    TotalItens = numfuncionario,
                    
                    Nome=nome
                },
                Nome=nome,
                Morada=morada,
                Contacto=contacto,
                Email=email
               }
            );
        }

        // GET: Funcionarios/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var funcionario = await _context.Funcionario
                .SingleOrDefaultAsync(m => m.FuncionarioId == id);
            if (funcionario == null)
            {
                return NotFound();
            }

            return View(funcionario);
        }

        // GET: Funcionarios/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Funcionarios/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FuncionarioId,Nome,Morada,Contacto,Email")] Funcionario funcionario)
        {
            if (ModelState.IsValid)
            {
                _context.Add(funcionario);
                await _context.SaveChangesAsync();

                ViewBag.Title = " Adicionado!";
                ViewBag.Message = "Novo funcionario criado Sucesso.";

                return View("Sucesso");
            }

            return View(funcionario);
        }

        // GET: Funcionarios/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var funcionario = await _context.Funcionario.FindAsync(id);
            if (funcionario == null)
            {
                return NotFound();
            }
            return View(funcionario);
        }

        // POST: Funcionarios/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("FuncionarioId,Nome,Morada,Contacto,Email")] Funcionario funcionario)
        {
            if (id != funcionario.FuncionarioId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(funcionario);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FuncionarioExists(funcionario.FuncionarioId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                ViewBag.Title = "Editado!";
                ViewBag.Message = "O funcionario foi editado com Sucesso.";

                return View("Sucesso");
            }
            return View(funcionario);
        }

        // GET: Funcionarios/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var funcionario = await _context.Funcionario
                .SingleOrDefaultAsync(m => m.FuncionarioId == id);
            if (funcionario == null)
            {
                return NotFound();
            }

            return View(funcionario);
        }

        // POST: Funcionarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var funcionario = await _context.Funcionario.FindAsync(id);


            if (funcionario == null)
            {
                return NotFound();
            }
            try
            {

                _context.Funcionario.Remove(funcionario);
            await _context.SaveChangesAsync();
            }
            catch
            {
               
                return View("ErrorDeleting");
            }
           
            ViewBag.Title = " Deletado!";
            ViewBag.Message = "Funcionario Deletado com  Sucesso.";

            return View("Sucesso");
        }
    
        private bool FuncionarioExists(int id)
        {
            return _context.Funcionario.Any(e => e.FuncionarioId == id);
        }
    }
}
