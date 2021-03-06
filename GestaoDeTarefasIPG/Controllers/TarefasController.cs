﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GestaoDeTarefasIPG.Models;

namespace GestaoDeTarefasIPG.Controllers
{
    public class TarefasController : Controller
    {   private const int PAGE_SIZE = 3;

        private readonly GestaoDeTarefasDbContext _context;

        public TarefasController(GestaoDeTarefasDbContext context)
        {
            _context = context;
        }

        // GET: Tarefas
        public async Task<IActionResult> Index(TarefasViewModel model = null, int pagina=1)
        {
            string nome = null;

            if (model != null && model.CurrentName != null)
            {
                nome = model.CurrentName;

            }


            IQueryable<Tarefa> tarefas;
            int numTarefa;
            IEnumerable<Tarefa> listTarefa;

            if (!string.IsNullOrEmpty(nome))
            {
                tarefas = _context.Tarefa.Include(s => s.Funcionario)
                .Where(p => p.Nome.Contains(nome.Trim()));

                numTarefa = await tarefas.CountAsync();

                listTarefa = await tarefas
                    .OrderBy(p => p.Nome)
                    .Skip(PAGE_SIZE * (pagina - 1))
                    .Take(PAGE_SIZE)
                    .ToListAsync();
            }
            else
            {

                tarefas = _context.Tarefa.Include(s => s.Funcionario);

                numTarefa = await tarefas.CountAsync();

                listTarefa = await tarefas
                    .OrderBy(p => p.Nome)
                    .Skip(PAGE_SIZE * (pagina - 1))
                    .Take(PAGE_SIZE)
                    .ToListAsync();

            }

            if (pagina > (numTarefa / PAGE_SIZE) + 1)
            {
                pagina = 1;
            }

            if (listTarefa.Count() == 0)
            {
                TempData["NoItemsFound"] = "Não foram encontrados resultados para está pesquisa";
            }

            return View(new TarefasViewModel
            {
                Tarefa = listTarefa,
                Pagination = new PaginaViewModels
                {
                    PaginaCorrente = pagina,
                    TamanhoPagina = PAGE_SIZE,
                    TotalItens = numTarefa,
                    Nome = nome
                },
            }
            );
        }

        // GET: Tarefas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tarefa = await _context.Tarefa
                .Include(t => t.Funcionario)
                .FirstOrDefaultAsync(m => m.TarefaID == id);
            if (tarefa == null)
            {
                return NotFound();
            }

            return View(tarefa);
        }

        // GET: Tarefas/Create
        public IActionResult Create()
        {
            ViewData["FuncionarioId"] = new SelectList(_context.Funcionario, "FuncionarioId", "Nome");
            return View();
        }

        // POST: Tarefas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TarefaID,Nome,Descricao,FuncionarioId")] Tarefa tarefa)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tarefa);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["FuncionarioId"] = new SelectList(_context.Funcionario, "FuncionarioId", "Nome", tarefa.FuncionarioId);
            return View(tarefa);
        }

        // GET: Tarefas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tarefa = await _context.Tarefa.FindAsync(id);
            if (tarefa == null)
            {
                return NotFound();
            }
            ViewData["FuncionarioId"] = new SelectList(_context.Funcionario, "FuncionarioId", "Nome", tarefa.FuncionarioId);
            return View(tarefa);
        }

        // POST: Tarefas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TarefaID,Nome,Descricao,FuncionarioId")] Tarefa tarefa)
        {
            if (id != tarefa.TarefaID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tarefa);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TarefaExists(tarefa.TarefaID))
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
            ViewData["FuncionarioId"] = new SelectList(_context.Funcionario, "FuncionarioId", "Nome", tarefa.FuncionarioId);
            return View(tarefa);
        }

        // GET: Tarefas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tarefa = await _context.Tarefa
                .Include(t => t.Funcionario)
                .FirstOrDefaultAsync(m => m.TarefaID == id);
            if (tarefa == null)
            {
                return NotFound();
            }

            return View(tarefa);
        }

        // POST: Tarefas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tarefa = await _context.Tarefa.FindAsync(id);
            _context.Tarefa.Remove(tarefa);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TarefaExists(int id)
        {
            return _context.Tarefa.Any(e => e.TarefaID == id);
        }
    }
}
