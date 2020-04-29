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
    public class PostUdrugeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PostUdrugeController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: PostUdruge
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.PostUdruge.Include(p => p.Udruga);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: PostUdruge/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var postUdruge = await _context.PostUdruge
                .Include(p => p.Udruga)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (postUdruge == null)
            {
                return NotFound();
            }

            return View(postUdruge);
        }

        // GET: PostUdruge/Create
        public IActionResult Create()
        {
            ViewData["UdrugaId"] = new SelectList(_context.Set<Udruga>(), "Id", "Naziv");
            return View();
        }

        // POST: PostUdruge/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Naziv,Content,Datum,UdrugaId")] PostUdruge postUdruge)
        {
            if (ModelState.IsValid)
            {
                postUdruge.Datum = DateTime.Now;
                _context.Add(postUdruge);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["UdrugaId"] = new SelectList(_context.Set<Udruga>(), "Id", "Naziv", postUdruge.UdrugaId);
            return View(postUdruge);
        }

        // GET: PostUdruge/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var postUdruge = await _context.PostUdruge.FindAsync(id);
            if (postUdruge == null)
            {
                return NotFound();
            }
            ViewData["UdrugaId"] = new SelectList(_context.Set<Udruga>(), "Id", "Naziv", postUdruge.UdrugaId);
            return View(postUdruge);
        }

        // POST: PostUdruge/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Naziv,Content,Datum,UdrugaId")] PostUdruge postUdruge)
        {
            if (id != postUdruge.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {

                    _context.Update(postUdruge);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PostUdrugeExists(postUdruge.Id))
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
            ViewData["UdrugaId"] = new SelectList(_context.Set<Udruga>(), "Id", "Naziv", postUdruge.UdrugaId);
            return View(postUdruge);
        }

        // GET: PostUdruge/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var postUdruge = await _context.PostUdruge
                .Include(p => p.Udruga)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (postUdruge == null)
            {
                return NotFound();
            }

            return View(postUdruge);
        }

        // POST: PostUdruge/Delete/5
        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var postUdruge = await _context.PostUdruge.FindAsync(id);
            _context.PostUdruge.Remove(postUdruge);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PostUdrugeExists(int id)
        {
            return _context.PostUdruge.Any(e => e.Id == id);
        }
    }
}
