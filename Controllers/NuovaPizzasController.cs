using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using la_mia_pizzeria_ef.Context;
using la_mia_pizzeria_ef.Models;

namespace la_mia_pizzeria_ef.Controllers
{
    public class NuovaPizzasController : Controller
    {
        private readonly NuovaPizzeriaContext _context;

        public NuovaPizzasController(NuovaPizzeriaContext context)
        {
            _context = context;
        }

        // GET: NuovaPizzas
        public async Task<IActionResult> Index()
        {
              return _context.Pizze != null ? 
                          View(await _context.Pizze.ToListAsync()) :
                          Problem("Entity set 'NuovaPizzeriaContext.Pizze'  is null.");
        }

        // GET: NuovaPizzas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Pizze == null)
            {
                return NotFound();
            }

            var nuovaPizza = await _context.Pizze
                .FirstOrDefaultAsync(m => m.Id == id);
            if (nuovaPizza == null)
            {
                return NotFound();
            }

            return View(nuovaPizza);
        }

        // GET: NuovaPizzas/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: NuovaPizzas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description,ImgPath,Price")] NuovaPizza nuovaPizza)
        {
            if (ModelState.IsValid)
            {
                _context.Add(nuovaPizza);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(nuovaPizza);
        }

        // GET: NuovaPizzas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Pizze == null)
            {
                return NotFound();
            }

            var nuovaPizza = await _context.Pizze.FindAsync(id);
            if (nuovaPizza == null)
            {
                return NotFound();
            }
            return View(nuovaPizza);
        }

        // POST: NuovaPizzas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,ImgPath,Price")] NuovaPizza nuovaPizza)
        {
            if (id != nuovaPizza.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(nuovaPizza);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NuovaPizzaExists(nuovaPizza.Id))
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
            return View(nuovaPizza);
        }

        // GET: NuovaPizzas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Pizze == null)
            {
                return NotFound();
            }

            var nuovaPizza = await _context.Pizze
                .FirstOrDefaultAsync(m => m.Id == id);
            if (nuovaPizza == null)
            {
                return NotFound();
            }

            return View(nuovaPizza);
        }

        // POST: NuovaPizzas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Pizze == null)
            {
                return Problem("Entity set 'NuovaPizzeriaContext.Pizze'  is null.");
            }
            var nuovaPizza = await _context.Pizze.FindAsync(id);
            if (nuovaPizza != null)
            {
                _context.Pizze.Remove(nuovaPizza);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NuovaPizzaExists(int id)
        {
          return (_context.Pizze?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
