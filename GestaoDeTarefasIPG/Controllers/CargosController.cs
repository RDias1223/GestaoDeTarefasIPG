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
    public class CargosController : Controller
    {
        private const int Tamanho_Pagina = 5;

        private readonly GestaoDeTarefasDbContext _context;

        public CargosController(GestaoDeTarefasDbContext context)
        {
            _context = context;
        }

        // GET: Cargos
        public async Task<IActionResult> Index( CargoViewModels modelo = null, int pagina = 1, string nome = null)
        {
            if (modelo != null || modelo.Nome != null)
            {
                nome = modelo.Nome;
            }

            IQueryable<Cargo> cargo;
            int numCargo;
            IEnumerable<Cargo> listaCargo;
            if (!string.IsNullOrEmpty(nome))
            {
                cargo=  _context.Cargo.Include(c => c.CargoChefe)
                    .Where(p => p.Nome.Contains(nome.Trim()));

                numCargo = await cargo.CountAsync();
                listaCargo = await cargo

                .OrderBy(p => p.Nome)
                .Skip(Tamanho_Pagina * (pagina - 1))
                .Take(Tamanho_Pagina)
                .ToListAsync();
        }
          else
            {

                cargo = _context.Cargo.Include(c => c.CargoChefe);

                numCargo = await cargo.CountAsync();

        listaCargo= await cargo
            .OrderBy(p => p.Nome)
                    .Skip(Tamanho_Pagina* (pagina - 1))
                    .Take(Tamanho_Pagina)
                    .ToListAsync();

    }
            if (pagina > (numCargo / Tamanho_Pagina) + 1)
            {
                pagina = 1;
            }


            if (listaCargo.Count() == 0)
            {
                TempData["NoItemsFound"] = "Não foram encontrados resultados para a sua pesquisa";
            }

            return View(new CargoViewModels
            {
                Cargo = listaCargo,

                Paginacao = new PaginaViewModels
                {
                    PaginaCorrente = pagina,
                    TamanhoPagina = Tamanho_Pagina,
                    TotalItens = numCargo,

                    Nome = nome
                },

            }
            );
        

       /* var gestaoDeTarefasDbContext = _context.Cargo.Include(c => c.CargoChefe);
            return View(await gestaoDeTarefasDbContext.ToListAsync());*/
        }

        // GET: Cargos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cargo = await _context.Cargo
                .Include(c => c.CargoChefe)
                .FirstOrDefaultAsync(m => m.CargoId == id);
            if (cargo == null)
            {
                return NotFound();
            }

            return View(cargo);
        }

        // GET: Cargoes/Create
        public IActionResult Create()
        {
            ViewData["CargoChefeId"] = new SelectList(_context.Cargo, "CargoId", "Nome");
            return View();
        }

        // POST: Cargoes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CargoId,Nome,CargoChefeId")] Cargo cargo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cargo);
                await _context.SaveChangesAsync();
                ViewBag.Title = " Adicionado!";
                ViewBag.Message = "Novo funcionario criado Sucesso.";

                return View("Sucesso");
            }
            ViewData["CargoChefeId"] = new SelectList(_context.Cargo, "CargoId", "Nome", cargo.CargoChefeId);
            return View(cargo);
        }

        // GET: Cargoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cargo = await _context.Cargo.FindAsync(id);
            if (cargo == null)
            {
                return NotFound();
            }
            ViewData["CargoChefeId"] = new SelectList(_context.Cargo, "CargoId", "Nome", cargo.CargoChefeId);
            return View(cargo);
        }

        // POST: Cargoes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CargoId,Nome,CargoChefeId")] Cargo cargo)
        {
            if (id != cargo.CargoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cargo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CargoExists(cargo.CargoId))
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
            ViewData["CargoChefeId"] = new SelectList(_context.Cargo, "CargoId", "Nome", cargo.CargoChefeId);
            return View(cargo);
        }

        // GET: Cargoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cargo = await _context.Cargo
                .Include(c => c.CargoChefe)
                .FirstOrDefaultAsync(m => m.CargoId == id);
            if (cargo == null)
            {
                return NotFound();
            }

            return View(cargo);
        }

        // POST: Cargoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cargo = await _context.Cargo.FindAsync(id);
            _context.Cargo.Remove(cargo);
            await _context.SaveChangesAsync();

            ViewBag.Title = " Deletado!";
            ViewBag.Message = "Funcionario Deletado com  Sucesso.";

            return View("Sucesso");
        }

        private bool CargoExists(int id)
        {
            return _context.Cargo.Any(e => e.CargoId == id);
        }
    }
}
