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
        public async Task<IActionResult> Index(FuncionarioViewModels modelo = null, int pagina = 1, string nome = null)
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
        public async Task<IActionResult> Create([Bind("FuncionarioId,Nome,Endereco,CodigoPostal,Data_Nascimento,Contacto,Email")] Funcionario funcionario)
        {
            var email = funcionario.Email;
            var contacto = funcionario.Contacto;

            if (emailInvalido(email) == true)
            {
                //Mensagem de erro se o email for inválido
                ModelState.AddModelError("Email", "Este email já existe");
            }


            if (contactoInvalido(contacto))
            {
                //Mensagem de erro se o nº de CC já existe
                ModelState.AddModelError("Contacto", "Contacto já existente");
            }
           


            /********************************/
            if (ModelState.IsValid)
            {
                if (!contactoInvalido(contacto) || !emailInvalido(email))
                {
                    _context.Add(funcionario);

                    await _context.SaveChangesAsync();

                    ViewBag.Title = " Adicionado!";
                    ViewBag.Message = "Novo funcionario criado Sucesso.";

                    return View("Sucesso");
                }
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
        public async Task<IActionResult> Edit(int id, [Bind("FuncionarioId,Nome,Endereco,CodigoPostal,Data_Nascimento,Contacto,Email")] Funcionario funcionario)
        {
            var email = funcionario.Email;
            var contacto = funcionario.Contacto;
            var funId = funcionario.FuncionarioId;
            if (id != funcionario.FuncionarioId)
            {
                return NotFound();
            }

            if (emailInvalidoEdit(email, funId))
            {
                //Mensagem de erro se o email já existir
                ModelState.AddModelError("Email", "Email já existente");
            }


            //Validar Contacto
            if (contactoInvalidoEdit(contacto,funId))
            {
                //Mensagem de erro se o CC já existir
                ModelState.AddModelError("Contacto", "Contacto já existente");
            }


            if (ModelState.IsValid)
            {
                try
                {
                    if (!contactoInvalidoEdit(contacto,funId) || !emailInvalidoEdit(email, funId))
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

        /*para creat*/

        /*return true se o email já existir no DB  */
        private bool emailInvalido(string email)
        {
            bool invalido = false;

            //Procura na BD se existem  com o mesmo email
            var funcionario = from e in _context.Funcionario
                              where e.Email.Contains(email) 
                              select e;

            if (!funcionario.Count().Equals(0))
            {
                invalido = true;
            }

            return invalido;
        }

        private bool contactoInvalido(string contacto)
        {
            bool invalido= false;


                     var funcionario = from e in _context.Funcionario
                              where e.Contacto.Contains(contacto)
                              select e;

            if (!funcionario.Count().Equals(0))
            {
                invalido = true;
            }

            return invalido;
        }
        /*+++++++++++++++Edit++++++++++++++++*/
 private bool emailInvalidoEdit(string email, int funId)
        {
            bool invalido = false;

            var funcionario = from e in _context.Funcionario
                              where e.Email.Contains(email) && e.FuncionarioId != funId
                              select e;

            if (!funcionario.Count().Equals(0))
            {
                invalido = true;
            }

            return invalido;
        }
        private bool contactoInvalidoEdit(string contacto, int funId)
        {
            bool invalido = false;

            var funcionario = from e in _context.Funcionario
                              where e.Contacto.Contains(contacto) && e.FuncionarioId != funId
                              select e;

            if (!funcionario.Count().Equals(0))
            {
                invalido = true;
            }

            return invalido;
        }
    }
}
 