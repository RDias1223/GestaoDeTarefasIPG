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
    public class ProfessoresController : Controller

    {

        private const int Tamanho_Pagina = 5;


        private readonly GestaoDeTarefasDbContext _context;

        public ProfessoresController(GestaoDeTarefasDbContext context)
        {
            _context = context;
        }

        // GET: Professores
        public async Task<IActionResult> Index(ProfessorViewModels modelo = null, int pagina = 1, string nome = null)
        {
            if (modelo != null || modelo.Nome != null)
            {
                nome = modelo.Nome;
            }

            IQueryable<Professor> professor;
            int numprofessor;
            IEnumerable<Professor> listaProfessor;


            if (!string.IsNullOrEmpty(nome))
            {
                professor = _context.Professor
                    .Where(p => p.Nome.Contains(nome.Trim()));

                numprofessor = await professor.CountAsync();

                listaProfessor = await professor
                    .OrderBy(p => p.Nome)
                    .Skip(Tamanho_Pagina * (pagina - 1))
                    .Take(Tamanho_Pagina)
                    .ToListAsync();
            }
            else
            {

                professor = _context.Professor;

                numprofessor = await professor.CountAsync();

                listaProfessor = await professor
                    .OrderBy(p => p.Nome)
                    .Skip(Tamanho_Pagina * (pagina - 1))
                    .Take(Tamanho_Pagina)
                    .ToListAsync();

            }
            if (pagina > (numprofessor / Tamanho_Pagina) + 1)
            {
                pagina = 1;
            }


            if (listaProfessor.Count() == 0)
            {
                TempData["NoItemsFound"] = "Não foram encontrados resultados para a sua pesquisa";
            }


            return View(new ProfessorViewModels
            {
                Professores = listaProfessor,
                Paginacao = new PaginaViewModels
                {
                    PaginaCorrente = pagina,
                    TamanhoPagina = Tamanho_Pagina,
                    TotalItens = numprofessor,

                    Nome = nome
                },

            }
            );
        }

        // GET: Professores/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var professor = await _context.Professor
                .FirstOrDefaultAsync(m => m.ProfessorId == id);
            if (professor == null)
            {
                return NotFound();
            }

            return View(professor);
        }

        // GET: Professores/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Professores/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProfessorId,Nome,DataNascimento,Contato,Email,Morada,CodigoPostal")] Professor professor)
        {
            if (ModelState.IsValid)
            {
                _context.Add(professor);
                await _context.SaveChangesAsync();

                ViewBag.Title = "Adicionado";
                ViewBag.Message = "Professor criado com Sucesso!";

                return View("Sucesso");

            }
            return View(professor);
        }

        // GET: Professores/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var professor = await _context.Professor.FindAsync(id);
            if (professor == null)
            {
                return NotFound();
            }
            return View(professor);
        }

        // POST: Professores/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProfessorId,Nome,DataNascimento,Contato,Email,Morada,CodigoPostal")] Professor professor)
        {
            if (id != professor.ProfessorId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(professor);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProfessorExists(professor.ProfessorId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                ViewBag.Title = "Editado";
                ViewBag.Message = "Professor editado com Sucesso!";

                return View("Sucesso");
            }
            return View(professor);
        }

        // GET: Professores/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var professor = await _context.Professor
                .FirstOrDefaultAsync(m => m.ProfessorId == id);
            if (professor == null)
            {
                return NotFound();
            }

            return View(professor);
        }

        // POST: Professores/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var professor = await _context.Professor.FindAsync(id);
            _context.Professor.Remove(professor);
            await _context.SaveChangesAsync();


            ViewBag.Title = "Apagado";
            ViewBag.Message = "Professor apagado com Sucesso!";


            return View("Sucesso");
        }

        private bool ProfessorExists(int id)
        {
            return _context.Professor.Any(e => e.ProfessorId == id);
        }
    }
}
