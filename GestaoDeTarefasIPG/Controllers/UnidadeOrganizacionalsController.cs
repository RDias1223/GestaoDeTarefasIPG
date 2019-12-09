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
    public class UnidadeOrganizacionalsController : Controller
    {
        private readonly GestaoDeTarefasDbContext _context;

        public UnidadeOrganizacionalsController(GestaoDeTarefasDbContext context)
        {
            _context = context;
        }

        // GET: UnidadeOrganizacionals
        public async Task<IActionResult> Index()
        {
            return View(await _context.UnidadeOrganizacional.ToListAsync());
        }

        // GET: UnidadeOrganizacionals/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var unidadeOrganizacional = await _context.UnidadeOrganizacional
                .FirstOrDefaultAsync(m => m.UnidadeOrganizacionalID == id);
            if (unidadeOrganizacional == null)
            {
                return NotFound();
            }

            return View(unidadeOrganizacional);
        }

        // GET: UnidadeOrganizacionals/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: UnidadeOrganizacionals/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UnidadeOrganizacionalID,Nome,Contacto,Email")] UnidadeOrganizacional unidadeOrganizacional)
        {
            if (ModelState.IsValid)
            {
                _context.Add(unidadeOrganizacional);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(unidadeOrganizacional);
        }

        // GET: UnidadeOrganizacionals/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var unidadeOrganizacional = await _context.UnidadeOrganizacional.FindAsync(id);
            if (unidadeOrganizacional == null)
            {
                return NotFound();
            }
            return View(unidadeOrganizacional);
        }

        // POST: UnidadeOrganizacionals/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("UnidadeOrganizacionalID,Nome,Contacto,Email")] UnidadeOrganizacional unidadeOrganizacional)
        {
            if (id != unidadeOrganizacional.UnidadeOrganizacionalID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(unidadeOrganizacional);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UnidadeOrganizacionalExists(unidadeOrganizacional.UnidadeOrganizacionalID))
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
            return View(unidadeOrganizacional);
        }

        // GET: UnidadeOrganizacionals/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var unidadeOrganizacional = await _context.UnidadeOrganizacional
                .FirstOrDefaultAsync(m => m.UnidadeOrganizacionalID == id);
            if (unidadeOrganizacional == null)
            {
                return NotFound();
            }

            return View(unidadeOrganizacional);
        }

        // POST: UnidadeOrganizacionals/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var unidadeOrganizacional = await _context.UnidadeOrganizacional.FindAsync(id);
            _context.UnidadeOrganizacional.Remove(unidadeOrganizacional);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UnidadeOrganizacionalExists(string id)
        {
            return _context.UnidadeOrganizacional.Any(e => e.UnidadeOrganizacionalID == id);
        }
    }
}
