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
        public async Task<IActionResult> Details(int? id)
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
            var email = unidadeOrganizacional.Email;
            var contacto = unidadeOrganizacional.Contacto;

            if (emailInvalido(email) == true)
            {
        
                ModelState.AddModelError("Email", "O email já existe");
            }


            if (contactoInvalido(contacto) == true)
            {
                ModelState.AddModelError("Contacto", "O contacto já existente");
            }


            if (!contactoInvalido(contacto) || !emailInvalido(email))
            {
                ViewBag.Title = " Adicionado.";
                ViewBag.Message = "Unidade Organizacional criada com sucesso.";

                _context.Add(unidadeOrganizacional);
                await _context.SaveChangesAsync();
                return View("Success");
            }
            return View(unidadeOrganizacional);
        }

        // GET: UnidadeOrganizacionals/Edit/5
        public async Task<IActionResult> Edit(int? id)
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
        public async Task<IActionResult> Edit(int id, [Bind("UnidadeOrganizacionalID,Nome,Contacto,Email")] UnidadeOrganizacional unidadeOrganizacional)
        {
            var email = unidadeOrganizacional.Email;
            var contacto = unidadeOrganizacional.Contacto;
            var uniId = unidadeOrganizacional.UnidadeOrganizacionalID;

            if(emailInvalidoEdit(email, uniId))
            {
               
                ModelState.AddModelError("Email", "O email já existe");
            }

            if (contactoInvalidoEdit(contacto, uniId))
            {
              
                ModelState.AddModelError("Contacto", "O contacto já existente");
            }

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

                ViewBag.Title = " Adicionado.";
                ViewBag.Message = "Unidade Organizacional criada com sucesso.";
                return View("Success");
            }
            return View(unidadeOrganizacional);
        }

        // GET: UnidadeOrganizacionals/Delete/5
        public async Task<IActionResult> Delete(int? id)
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
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var unidadeOrganizacional = await _context.UnidadeOrganizacional.FindAsync(id);
            _context.UnidadeOrganizacional.Remove(unidadeOrganizacional);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UnidadeOrganizacionalExists(int id)
        {
            return _context.UnidadeOrganizacional.Any(e => e.UnidadeOrganizacionalID == id);
        }


        //Base de Dados
        private bool contactoInvalido(string contacto)
        {
            bool invalido = false;


            var unidade = from e in _context.UnidadeOrganizacional
                              where e.Contacto.Contains(contacto)
                              select e;

            if (!unidade.Count().Equals(0))
            {
                invalido = true;
            }

            return invalido;
        }


        //Base de Dados
        private bool emailInvalido(string email)
        {
            bool invalido = false;

            //Procura na BD se existem  com o mesmo email
            var unidade = from e in _context.UnidadeOrganizacional
                              where e.Email.Contains(email)
                              select e;

            if (!unidade.Count().Equals(0))
            {
                invalido = true;
            }

            return invalido;
        }


        private bool emailInvalidoEdit(string email, int uniId)
        {
            bool invalido = false;

            var unidade = from e in _context.UnidadeOrganizacional
                              where e.Email.Contains(email) && e.UnidadeOrganizacionalID != uniId
                              select e;

            if (!unidade.Count().Equals(0))
            {
                invalido = true;
            }

            return invalido;
        }
        private bool contactoInvalidoEdit(string contacto, int uniId)
        {
            bool invalido = false;

            var unidade = from e in _context.UnidadeOrganizacional
                              where e.Contacto.Contains(contacto) && e.UnidadeOrganizacionalID != uniId
                              select e;

            if (!unidade.Count().Equals(0))
            {
                invalido = true;
            }

            return invalido;
        }
    }
}
    

