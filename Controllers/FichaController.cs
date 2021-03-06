using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SolucionCacao.Models;

namespace SolucionCacao.Controllers
{
    public class FichaController : Controller
    {
        private readonly db_concursoContext _context;

        public FichaController(db_concursoContext context)
        {
            _context = context;
        }

        // GET: Ficha
        public async Task<IActionResult> Index()
        {
            var db_concursoContext = _context.Fichas.Include(f => f.IdTecnicoNavigation).Include(f => f.IdZonaNavigation);
            return View(await db_concursoContext.ToListAsync());
        }

        // GET: Ficha/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ficha = await _context.Fichas
                .Include(f => f.IdTecnicoNavigation)
                .Include(f => f.IdZonaNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ficha == null)
            {
                return NotFound();
            }

            return View(ficha);
        }

        // GET: Ficha/Create
        public IActionResult Create()
        {
            ViewData["IdTecnico"] = new SelectList(_context.Tecnicos, "Id", "Id");
            ViewData["IdZona"] = new SelectList(_context.ZonaEstudios, "Id", "Lugar");
            return View();
        }

        // POST: Ficha/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,IdTecnico,IdZona,Arbol,Fruto,Incidencia,Severidad,Fecha")] Ficha ficha)
        {
            if (ModelState.IsValid)
            {
                _context.Add(ficha);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdTecnico"] = new SelectList(_context.Tecnicos, "Id", "Id", ficha.IdTecnico);
            ViewData["IdZona"] = new SelectList(_context.ZonaEstudios, "Id", "Lugar", ficha.IdZona);
            return View(ficha);
        }

        // GET: Ficha/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ficha = await _context.Fichas.FindAsync(id);
            if (ficha == null)
            {
                return NotFound();
            }
            ViewData["IdTecnico"] = new SelectList(_context.Tecnicos, "Id", "Id", ficha.IdTecnico);
            ViewData["IdZona"] = new SelectList(_context.ZonaEstudios, "Id", "Lugar", ficha.IdZona);
            return View(ficha);
        }

        // POST: Ficha/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,IdTecnico,IdZona,Arbol,Fruto,Incidencia,Severidad,Fecha")] Ficha ficha)
        {
            if (id != ficha.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ficha);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FichaExists(ficha.Id))
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
            ViewData["IdTecnico"] = new SelectList(_context.Tecnicos, "Id", "Id", ficha.IdTecnico);
            ViewData["IdZona"] = new SelectList(_context.ZonaEstudios, "Id", "Lugar", ficha.IdZona);
            return View(ficha);
        }

        // GET: Ficha/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ficha = await _context.Fichas
                .Include(f => f.IdTecnicoNavigation)
                .Include(f => f.IdZonaNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ficha == null)
            {
                return NotFound();
            }

            return View(ficha);
        }

        // POST: Ficha/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ficha = await _context.Fichas.FindAsync(id);
            _context.Fichas.Remove(ficha);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FichaExists(int id)
        {
            return _context.Fichas.Any(e => e.Id == id);
        }
    }
}
