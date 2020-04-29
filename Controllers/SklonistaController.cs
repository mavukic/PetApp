using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PetApp.Models;
using PetApp.Data;
using PetApp.Models;

namespace WebApplication12.Controllers
{
    public class SklonistaController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SklonistaController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Skloništa
        public async Task<IActionResult> Index(int? id, int? postID)
        {
            var viewModel = new SklonisteView();
            viewModel.Sklonista = await _context.Skloniste
                  .Include(i => i.PostsSklonista)

                  .AsNoTracking()
                 
                  .ToListAsync();

            if (id != null)
            {
                ViewData["SkloništeID"] = id.Value;
                Skloniste skloniste = viewModel.Sklonista.Where(
                    i => i.Id == id.Value).Single();
                viewModel.PostSklonista = skloniste.PostsSklonista;
            }

            if (postID != null)
            {
                ViewData["PostID"] = postID.Value;
                viewModel.PostSklonista = viewModel.PostSklonista.Where(
                    x => x.Id == postID);
            }


            return View(viewModel);
        }
        public async Task<IActionResult> LjubimciTu(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var list = new List<Ljubimac>();
            var skloniste = await _context.Skloniste
                .FirstOrDefaultAsync(m => m.Id == id);
            if (skloniste == null)
            {
                return NotFound();
            }
            else
            {

                foreach (var c in _context.Ljubimac)
                {
                    if (c.SklonisteId == id)
                        list.Add(c);
                }
            }
            return View(list);
        }
        // GET: Skloništa/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sklonište = await _context.Skloniste
                .FirstOrDefaultAsync(m => m.Id == id);
            if (sklonište == null)
            {
                return NotFound();
            }

            return View(sklonište);
        }

        // GET: Skloništa/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Skloništa/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Naziv,Adresa,Grad,Tel,Email,Web")] Skloniste skloniste)
        {
            if (ModelState.IsValid)
            {
                _context.Add(skloniste);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(skloniste);
        }

        // GET: Skloništa/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var skloniste = await _context.Skloniste.FindAsync(id);
            if (skloniste == null)
            {
                return NotFound();
            }
            return View(skloniste);
        }

        // POST: Skloništa/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Naziv,Adresa,Grad,Tel,Email,Web")] Skloniste skloniste)
        {
            if (id != skloniste.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(skloniste);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SklonisteExists(skloniste.Id))
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
            return View(skloniste);
        }

        // GET: Skloništa/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var skloniste = await _context.Skloniste
                .FirstOrDefaultAsync(m => m.Id == id);
            if (skloniste == null)
            {
                return NotFound();
            }

            return View(skloniste);
        }

        // POST: Skloništa/Delete/5
        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var skloniste = await _context.Skloniste.FindAsync(id);
            _context.Skloniste.Remove(skloniste);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SklonisteExists(int id)
        {
            return _context.Skloniste.Any(e => e.Id == id);
        }
    }
}
