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
    public class ServicoesController : Controller
    {
        private const int PAGE_SIZE = 3;
        private readonly GestaoDeTarefasDbContext _context;

        public ServicoesController(GestaoDeTarefasDbContext context)
        {
            _context = context;
        }

        // GET: Servicoes
        public async Task<IActionResult> Index(ServicoViewModel model = null, int pagina = 1)
        {
            string nome = null;

            if (model != null && model.CurrentName != null)
            {
                nome = model.CurrentName;

            }

            IQueryable<Servico> servico;
            int numServico;
            IEnumerable<Servico> listServico;

            if (!string.IsNullOrEmpty(nome))
            {
                servico = _context.Servico
                .Where(p => p.Nome.Contains(nome.Trim()));

                numServico = await servico.CountAsync();

                listServico = await servico
                    .OrderBy(p => p.Nome)
                    .Skip(PAGE_SIZE * (pagina - 1))
                    .Take(PAGE_SIZE)
                    .ToListAsync();
            }
            else
            {

                servico = _context.Servico;

                numServico = await servico.CountAsync();

                listServico = await servico
                    .OrderBy(p => p.Nome)
                    .Skip(PAGE_SIZE * (pagina - 1))
                    .Take(PAGE_SIZE)
                    .ToListAsync();

            }

            if (pagina > (numServico / PAGE_SIZE) + 1)
            {
                pagina = 1;
            }

            if (listServico.Count() == 0)
            {
                TempData["NoItemsFound"] = "Não foram encontrados resultados para está pesquisa";
            }

            return View(new ServicoViewModel
            {
                Servico = listServico,
                Pagination = new PaginaViewModels
                {
                    PaginaCorrente = pagina,
                    TamanhoPagina = PAGE_SIZE,
                    TotalItens = numServico,
                    Nome = nome
                },
            }
            );
        }

        // GET: Servicoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var servico = await _context.Servico
                .Include(s => s.UnidadeOrganizacional)
                .FirstOrDefaultAsync(m => m.ServicoID == id);
            if (servico == null)
            {
                return NotFound();
            }

            return View(servico);
        }

        // GET: Servicoes/Create
        public IActionResult Create()
        {
            ViewData["UnidadeOrganizacionalID"] = new SelectList(_context.UnidadeOrganizacional, "UnidadeOrganizacionalID", "Nome");
            return View();
        }

        // POST: Servicoes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ServicoID,Nome,Contacto,UnidadeOrganizacionalID")] Servico servico)
        {
            var contacto = servico.Contacto;

            if (contactotIvalid(contacto) == true )
            {
                ModelState.AddModelError("Contacto", "O contacto já existe");
            }

           

            if (!contactotIvalid(contacto))
            {
                 ViewBag.Title = "Adicionado.";
                 ViewBag.Message = "Serviço criado com sucesso.";

                 _context.Add(servico);
                 await _context.SaveChangesAsync();
                 return View("Success");
            }
           
            return View(servico);
        }

        // GET: Servicoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var servico = await _context.Servico.FindAsync(id);
            if (servico == null)
            {
                return NotFound();
            }
            ViewData["UnidadeOrganizacionalID"] = new SelectList(_context.UnidadeOrganizacional, "UnidadeOrganizacionalID", "Nome", servico.UnidadeOrganizacionalID);
            return View(servico);
        }

        // POST: Servicoes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ServicoID,Nome,Contacto,UnidadeOrganizacionalID")] Servico servico)
        {
            if (id != servico.ServicoID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(servico);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ServicoExists(servico.ServicoID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["UnidadeOrganizacionalID"] = new SelectList(_context.UnidadeOrganizacional, "UnidadeOrganizacionalID", "Nome", servico.UnidadeOrganizacionalID);
            return View(servico);
        }

        // GET: Servicoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var servico = await _context.Servico
                .Include(s => s.UnidadeOrganizacional)
                .FirstOrDefaultAsync(m => m.ServicoID == id);
            if (servico == null)
            {
                return NotFound();
            }

            return View(servico);
        }

        // POST: Servicoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var servico = await _context.Servico.FindAsync(id);
            _context.Servico.Remove(servico);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ServicoExists(int id)
        {
            return _context.Servico.Any(e => e.ServicoID == id);
        }

        private bool contactotIvalid(string contacto)
        {
            bool invalido = false;

            var servico = from e in _context.Servico
                          where e.Contacto.Contains(contacto)
                          select e;

            if (!servico.Count().Equals(0))
            {
                invalido = true;
            }
            return invalido;
        }
    }
}
