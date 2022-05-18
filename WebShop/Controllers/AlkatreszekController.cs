using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebShop.Data;
using WebShop.Models;

namespace WebShop.Controllers
{
    public class AlkatreszekController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AlkatreszekController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Alkatreszek
        public async Task<IActionResult> Index(string Megnevezes, string Tipus)
        {
            var model = new Kereso();
            var alkatresz = _context.Alkatresz.Select(x => x);

            if (!string.IsNullOrEmpty(Megnevezes))
            {
                alkatresz = alkatresz.Where(x => x.Megnevezes.Contains(Megnevezes));
                model.Megnevezes = Megnevezes;
            }

            if (!string.IsNullOrEmpty(Tipus))
            {
                alkatresz = alkatresz.Where(x => x.Tipus.Equals(Tipus));
                model.Tipus = Tipus;
            }

            model.AlkatreszLista = await alkatresz.OrderBy(x => x.Megnevezes).ToListAsync();
            model.TipusLista = new SelectList(await _context.Alkatresz.Select(x => x.Tipus).Distinct().OrderBy(x => x).ToListAsync());

            return View(model);
        }

        // GET: Alkatreszek/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var alkatresz = await _context.Alkatresz
                .FirstOrDefaultAsync(m => m.Id == id);
            if (alkatresz == null)
            {
                return NotFound();
            }

            return View(alkatresz);
        }

        // GET: Alkatreszek/Create
        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Alkatreszek/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Create([Bind("Id,Megnevezes,Gyarto,Tipus,Ar")] Alkatresz alkatresz)
        {
            if (ModelState.IsValid)
            {
                _context.Add(alkatresz);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(alkatresz);
        }

        // GET: Alkatreszek/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var alkatresz = await _context.Alkatresz.FindAsync(id);
            if (alkatresz == null)
            {
                return NotFound();
            }
            return View(alkatresz);
        }

        // POST: Alkatreszek/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Megnevezes,Gyarto,Tipus,Ar")] Alkatresz alkatresz)
        {
            if (id != alkatresz.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(alkatresz);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AlkatreszExists(alkatresz.Id))
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
            return View(alkatresz);
        }

        // GET: Alkatreszek/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var alkatresz = await _context.Alkatresz
                .FirstOrDefaultAsync(m => m.Id == id);
            if (alkatresz == null)
            {
                return NotFound();
            }

            return View(alkatresz);
        }

        // POST: Alkatreszek/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var alkatresz = await _context.Alkatresz.FindAsync(id);
            _context.Alkatresz.Remove(alkatresz);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AlkatreszExists(int id)
        {
            return _context.Alkatresz.Any(e => e.Id == id);
        }
    }
}
