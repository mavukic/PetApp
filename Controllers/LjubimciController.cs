using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PetApp.Data;
using PetApp.Models;

namespace PetApp.Controllers
{
    public class LjubimciController : Controller
    {
        private readonly ApplicationDbContext _context;
        private IHostingEnvironment _environment;
        public LjubimciController(ApplicationDbContext context,
                                  IHostingEnvironment environment)

        {
            _environment = environment;
            _context = context;
        }

        List<Ljubimac> CheckForAdopterAndShelter()
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
            return listlj;
        }


        public async Task<IActionResult> Index(String searchString, 
            string SearchString2, int SearchString3, String SearchString4)
        {

            _context.Ljubimac.Include(l => l.Skloniste);

            var z = new SelectList(_context.Set<Skloniste>(), "Id", "Naziv", SearchString3);
            ViewData["SkloništeId"] = z;
            SearchString3 = (int)z.SelectedValue;


            foreach (var d in _context.Skloniste)
            {
                if (d.Id == SearchString3)
                {
                    var returnvaluesearch = from l in _context.Ljubimac
                                            where l.SklonisteId == d.Id
                                            select l;

                    return View(returnvaluesearch);
                }
            }
            _context.Ljubimac.Include(l => l.Grad);
            var listgradova = from d in _context.Ljubimac

                              select d;
            var listagradova2 = new List<String>();
            foreach (var f in listgradova)
            {
                if (!listagradova2.Contains(f.Grad))
                    listagradova2.Add(f.Grad);
            }
            var g = new SelectList(listagradova2.ToList(), SearchString4);
            ViewData["Gradovi"] = g;
            SearchString4 = (String)g.SelectedValue;


            foreach (var d in _context.Ljubimac)
            {
                if (d.Grad == SearchString4)
                {
                    var list = new List<Ljubimac>();
                    list.Add(d);

                    return View(list);
                }
            }
            var ljubimcisearch2 = from m in _context.Ljubimac
                                  select m;

            String stringsub = null;
            String stringsub2 = null;

            if (SearchString2 != null)
            {
                stringsub = SearchString2.Substring(1, SearchString2.Length - 1);
                stringsub = stringsub.ToLower();
                String temp = SearchString2.ElementAt(0).ToString();

                stringsub2 = temp.ToUpper() + stringsub;
            }
            if (!String.IsNullOrEmpty(stringsub2) && stringsub2.Contains("Pas"))
            {
                ljubimcisearch2 = ljubimcisearch2.Where(s => s.Vrsta == "Pas");
                return View(ljubimcisearch2.ToList());
            }
            if (!String.IsNullOrEmpty(stringsub2) && (stringsub2.Contains("Macka") || stringsub2.Contains("Mačka")))
            {
                ljubimcisearch2 = ljubimcisearch2.Where(s => s.Vrsta == "Mačka");
                return View(ljubimcisearch2.ToList());
            }


            if (SearchString2 != null)
            {
                ljubimcisearch2 = ljubimcisearch2.Where(s => s.Opis.Contains(SearchString2));
                return View(ljubimcisearch2.ToList());
            }


            var ljubimcisearch = from m in _context.Ljubimac
                                 select m;

            if (!String.IsNullOrEmpty(searchString))
            {
                ljubimcisearch = ljubimcisearch.Where(s => s.Ime.Contains(searchString));
                return View(ljubimcisearch.ToList());
            }

            return View(CheckForAdopterAndShelter());
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Izgubljeni(String searchString, String SearchString2, String SearchString4)
        {

            _context.Ljubimac.Include(l => l.Grad);
            var listgradova = from d in _context.Ljubimac

                              select d;
            var listagradova2 = new List<String>();
            foreach (var f in listgradova)
            {
                if (!listagradova2.Contains(f.Grad))
                    listagradova2.Add(f.Grad);
            }
            var g = new SelectList(listagradova2.ToList(), SearchString4);
            ViewData["Gradovi"] = g;
            SearchString4 = (String)g.SelectedValue;

            foreach (var d in _context.Ljubimac)
            {
                if (d.Grad == SearchString4 && d.SklonisteId == null)
                {
                    var list = new List<Ljubimac>();
                    list.Add(d);
                    if (list.Count != 0)
                        return View(list);
                }
            }
            var applicationDbContext = _context.Ljubimac.Include(l => l.Skloniste).Where(l => l.SklonisteId == null);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Ljubimci/Create

        public IActionResult Create()
        {

            var list = new List<String>();
            list.Add("Mačka");
            list.Add("Pas");
            ViewData["Vrsta"] = new SelectList(list);
            ViewData["SkloništeId"] = new SelectList(_context.Set<Skloniste>(), "Id", "Naziv");

            return View();

        }

        // POST: Ljubimci/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]

        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Ime,Opis,Datum,Grad,SklonisteId,Mjesto,Vrsta,Godine,Slika")] Ljubimac ljubimac, ICollection<IFormFile> files)
        {
            if (ModelState.IsValid)
            {



                ljubimac.Datum = DateTime.Now;
                var uploads = Path.Combine(_environment.WebRootPath, "slike");
                if (ljubimac.Vrsta == "Mačka")
                {
                    uploads = Path.Combine(_environment.WebRootPath, "slike/macke");

                }
                else if (ljubimac.Vrsta == "Pas")
                {
                    uploads = Path.Combine(_environment.WebRootPath, "slike/psi");
                }
                foreach (var file in files)
                {
                    if (file.Length > 0)
                    {
                        using (var fileStream = new FileStream(Path.Combine(uploads, file.FileName), FileMode.Create))
                        {
                            await file.CopyToAsync(fileStream);
                            if (ljubimac.Vrsta == "Mačka")
                            {
                                ljubimac.Slika = "//" + "macke" + "//" + file.FileName;

                            }
                            else if (ljubimac.Vrsta == "Pas")
                            {
                                ljubimac.Slika = "//" + "psi" + "//" + file.FileName;
                            }
                            else
                            {
                                ljubimac.Slika = file.FileName;
                            }

                        }
                    }
                }
                _context.Add(ljubimac);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            var list = new List<String>();
            list.Add("Mačka");
            list.Add("Pas");
            ViewData["Vrsta"] = new SelectList(list);
            ViewData["SkloništeId"] = new SelectList(_context.Set<Skloniste>(), "Id", "Naziv", ljubimac.SklonisteId);

            return View(ljubimac);
        }

        // GET: Ljubimci/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ljubimac = await _context.Ljubimac.FindAsync(id);
            if (ljubimac == null)
            {
                return NotFound();
            }
            var sklon = from d in _context.Skloniste
                        where d.Id == ljubimac.SklonisteId
                        select d.Naziv;
            ViewData["SkloništeId"] = new SelectList(_context.Skloniste, "Id", "Naziv", ljubimac.SklonisteId);
            ViewData["SkloništeNaziv"] = new SelectList(_context.Skloniste, "Naziv", "Naziv", sklon);

            var list = new List<String>();
            list.Add("Mačka");
            list.Add("Pas");
            ViewData["Vrsta"] = new SelectList(list, ljubimac.Vrsta);
            return View(ljubimac);
        }

        // POST: Ljubimci/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Ime,Opis,Datum,Grad,SklonisteId,Mjesto,Vrsta,Godine,PosvajteljId,Slika")] Ljubimac ljubimac, ICollection<IFormFile> files)
        {
            if (id != ljubimac.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    ljubimac.Datum = DateTime.Now;
                    var uploads = Path.Combine(_environment.WebRootPath, "slike");
                    if (ljubimac.Vrsta == "Mačka")
                    {
                        uploads = Path.Combine(_environment.WebRootPath, "slike/macke");

                    }
                    else if (ljubimac.Vrsta == "Pas")
                    {
                        uploads = Path.Combine(_environment.WebRootPath, "slike/psi");
                    }
                    foreach (var file in files)
                    {
                        if (file.Length > 0)
                        {
                            using (var fileStream = new FileStream(Path.Combine(uploads, file.FileName), FileMode.Create))
                            {
                                await file.CopyToAsync(fileStream);
                                if (ljubimac.Vrsta == "Mačka")
                                {
                                    ljubimac.Slika = "//" + "macke" + "//" + file.FileName;

                                }
                                else if (ljubimac.Vrsta == "Pas")
                                {
                                    ljubimac.Slika = "//" + "psi" + "//" + file.FileName;
                                }
                                else
                                {
                                    ljubimac.Slika = file.FileName;
                                }

                            }
                        }
                    }
                    _context.Update(ljubimac);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LjubimacExists(ljubimac.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index", "Ljubimci");
            }
            var list = new List<String>();
            list.Add("Mačka");
            list.Add("Pas");
            ViewData["Vrsta"] = new SelectList(list, ljubimac.Vrsta);
            ViewData["SkloništeId"] = new SelectList(_context.Set<Skloniste>(), "Id", "Naziv", ljubimac.SklonisteId);
            var sklon = from d in _context.Skloniste
                        where d.Id == ljubimac.SklonisteId
                        select d.Naziv;
            ViewData["SkloništeNaziv"] = new SelectList(_context.Set<Skloniste>(), "Naziv", "Naziv", sklon);
            return View(ljubimac);
        }

        // GET: Ljubimci/Delete/5

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ljubimac = await _context.Ljubimac
                .Include(l => l.Skloniste)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ljubimac == null)
            {
                return NotFound();
            }

            return View(ljubimac);
        }

        // POST: Ljubimci/Delete/5
        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ljubimac = await _context.Ljubimac.FindAsync(id);
            _context.Ljubimac.Remove(ljubimac);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LjubimacExists(int id)
        {
            return _context.Ljubimac.Any(e => e.Id == id);
        }
    }
}
