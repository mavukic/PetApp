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

    public class PosvajateljiController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PosvajateljiController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Posvajatelji
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Posvajatelj.Include(p => p.Ljubimac);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Posvajatelji/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var posvajatelj = await _context.Posvajatelj
                .Include(p => p.Ljubimac)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (posvajatelj == null)
            {
                return NotFound();
            }

            return View(posvajatelj);
        }

        // GET: Posvajatelji/Create
        public IActionResult Create()
        {
            var list = new List<Ljubimac>();
            var listpos = new List<Posvajatelj>();
            var listlj = new List<Ljubimac>();
            foreach (var d in _context.Ljubimac)
            {
                list.Add(d);

            }
            foreach (var d in _context.Posvajatelj)
            {
                listpos.Add(d);

            }

            foreach (var b in list)
            {
                if (listpos.Count != 0)
                {
                    foreach (var c in listpos)
                    {
                        if (!listpos.Exists(x => x.LjubimacId == b.Id) && b.SklonisteId != null)
                        {
                            if (!listlj.Contains(b))
                                listlj.Add(b);
                        }



                    }
                }
                else
                {
                    if (b.SklonisteId != null)
                        if (!listlj.Contains(b))
                            listlj.Add(b);
                }
            }
            ViewData["LjubimacId"] = new SelectList(listlj, "Id", "Ime");
            return View();
        }

        // POST: Posvajatelji/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Ime,Prezime,Mjesto,Datum,BrMob,Email,LjubimacId")] Posvajatelj posvajatelj)
        {
            if (ModelState.IsValid)
            {
                posvajatelj.Datum = DateTime.Now;
                _context.Add(posvajatelj);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            var list = new List<Ljubimac>();
            var listpos = new List<Posvajatelj>();
            var listlj = new List<Ljubimac>();
            foreach (var d in _context.Ljubimac)
            {
                list.Add(d);

            }
            foreach (var d in _context.Posvajatelj)
            {
                listpos.Add(d);

            }

            foreach (var b in list)
            {
                if (listpos.Count != 0)
                {
                    foreach (var c in listpos)
                    {
                        if (!listpos.Exists(x => x.LjubimacId == b.Id) && b.SklonisteId != null)
                        {
                            if (!listlj.Contains(b))
                                listlj.Add(b);
                        }



                    }
                }
                else
                {
                    if (b.SklonisteId != null)
                        if (!listlj.Contains(b))
                            listlj.Add(b);
                }
            }
            ViewData["LjubimacId"] = new SelectList(listlj, "Id", "Ime", posvajatelj.LjubimacId);
            return View(posvajatelj);
        }

        // GET: Posvajatelji/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var posvajatelj = await _context.Posvajatelj.FindAsync(id);
            if (posvajatelj == null)
            {
                return NotFound();
            }
            ViewData["LjubimacId"] = new SelectList(_context.Ljubimac, "Id", "Ime", posvajatelj.LjubimacId);
            return View(posvajatelj);
        }

        // POST: Posvajatelji/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Ime,Prezime,Mjesto,Datum,BrMob,Email,LjubimacId")] Posvajatelj posvajatelj)
        {
            if (id != posvajatelj.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(posvajatelj);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PosvajateljExists(posvajatelj.Id))
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
            var list = new List<Ljubimac>();
            var listpos = new List<Posvajatelj>();
            var listlj = new List<Ljubimac>();
            foreach (var d in _context.Ljubimac)
            {
                list.Add(d);

            }
            foreach (var d in _context.Posvajatelj)
            {
                listpos.Add(d);

            }

            foreach (var b in list)
            {
                if (listpos.Count != 0)
                {
                    foreach (var c in listpos)
                    {
                        if (!listpos.Exists(x => x.LjubimacId == b.Id) && b.SklonisteId != null)
                        {
                            if (!listlj.Contains(b))
                                listlj.Add(b);
                        }



                    }
                }
                else
                {
                    if (b.SklonisteId != null)
                        if (!listlj.Contains(b))
                            listlj.Add(b);
                }
            }
            ViewData["LjubimacId"] = new SelectList(listlj, "Id", "Ime", posvajatelj.LjubimacId);
            return View(posvajatelj);
        }

        // GET: Posvajatelji/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var posvajatelj = await _context.Posvajatelj
                .Include(p => p.Ljubimac)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (posvajatelj == null)
            {
                return NotFound();
            }

            return View(posvajatelj);
        }

        // POST: Posvajatelji/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var posvajatelj = await _context.Posvajatelj.FindAsync(id);
            _context.Posvajatelj.Remove(posvajatelj);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PosvajateljExists(int id)
        {
            return _context.Posvajatelj.Any(e => e.Id == id);
        }
    }
}
