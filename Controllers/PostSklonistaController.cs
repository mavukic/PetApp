using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PetApp.Data;
using PetApp.Models;

namespace PetApp.Controllers
{
    public class PostSklonistaController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PostSklonistaController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: PostSkloništa
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.PostSklonista.Include(p => p.Skloniste);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: PostSkloništa/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var postSkloništa = await _context.PostSklonista
                .Include(p => p.Skloniste)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (postSkloništa == null)
            {
                return NotFound();
            }

            return View(postSkloništa);
        }

        // GET: PostSkloništa/Create
        public IActionResult Create()
        {
            ViewData["SkloništeId"] = new SelectList(_context.Set<Skloniste>(), "Id", "Naziv");
            return View();
        }

        // POST: PostSkloništa/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Naziv,Content,Datum,SklonisteId")] PostSklonista postSklonista)
        {
            if (ModelState.IsValid)
            {

                postSklonista.Datum = DateTime.Now;
                _context.Add(postSklonista);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["SkloništeId"] = new SelectList(_context.Set<Skloniste>(), "Id", "Naziv", postSklonista.SklonisteId);
            return View(postSklonista);
        }

        // GET: PostSkloništa/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var postSklonista = await _context.PostSklonista.FindAsync(id);
            if (postSklonista == null)
            {
                return NotFound();
            }
            ViewData["SkloništeId"] = new SelectList(_context.Set<Skloniste>(), "Id", "Naziv", postSklonista.SklonisteId);
            return View(postSklonista);
        }

        // POST: PostSkloništa/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Naziv,Content,Datum,SklonisteId")] PostSklonista postSklonista)
        {
            if (id != postSklonista.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(postSklonista);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PostSkloništaExists(postSklonista.Id))
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
            ViewData["SkloništeId"] = new SelectList(_context.Set<Skloniste>(), "Id", "Naziv", postSklonista.SklonisteId);
            return View(postSklonista);
        }

        // GET: PostSkloništa/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var postSkloništa = await _context.PostSklonista
                .Include(p => p.Skloniste)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (postSkloništa == null)
            {
                return NotFound();
            }

            return View(postSkloništa);
        }

        // POST: PostSkloništa/Delete/5
        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var postSkloništa = await _context.PostSklonista.FindAsync(id);
            _context.PostSklonista.Remove(postSkloništa);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PostSkloništaExists(int id)
        {
            return _context.PostSklonista.Any(e => e.Id == id);
        }
    }
}
