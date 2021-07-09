using KCSit.SalesforceAcademy.Kappify.Data;
using KCSit.SalesforceAcademy.Kappify.Logic;
using KCSit.SalesforceAcademy.Kappify.WebApp.ViewModels.Artists;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace KCSit.SalesforceAcademy.Kappify.WebApp.Controllers
{
    public class ArtistsController : Controller
    {
        private readonly academykcsContext _context;
        private readonly IGenericLogic _genericLogic;

        public ArtistsController(IGenericLogic genericLogic, academykcsContext context)
        {
            _context = context;
            _genericLogic = genericLogic;
        }

        // GET: Artists
        public async Task<IActionResult> Index()
        {
            var genericReturn = await _genericLogic.GetAll<Artist>();

            if(!genericReturn.Succeeded)
            {
                return View("Error");
            }

            return View(genericReturn.Result);
        }

        //public IActionResult Index()
        //{
        //    return View( _context.Artists.ToList());
        //}

        // GET: Artists/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var genericResult = await _genericLogic.Get<Artist>(id.Value);

            if (!genericResult.Succeeded) return View("Error");

            if (genericResult.Result == null)
            {
                return NotFound();
            }

            return View(genericResult.Result);
        }

        // GET: Artists/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Artists/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Bio,Genre,Id,Uuid,ImageUrl")] Artist artist)
        {
            if (ModelState.IsValid)
            {
                _context.Add(artist);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(artist);
        }

        // GET: Artists/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var artist = await _context.Artists.FindAsync(id);
            if (artist == null)
            {
                return NotFound();
            }

            var editViewModel = new EditViewModel();
            editViewModel.Bio = artist.Bio;
            editViewModel.Genre = artist.Genre;
            editViewModel.Name = artist.Name;


            return View(editViewModel);
        }

        // POST: Artists/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid uuid, EditViewModel editViewModel)
        {

            if (uuid != editViewModel.Uuid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                //try
                //{
                //    _context.Update(editViewModel);
                //    await _context.SaveChangesAsync();
                //}
                //catch (DbUpdateConcurrencyException)
                //{
                //    if (!ArtistExists(editViewModel.Id))
                //    {
                //        return NotFound();
                //    }
                //    else
                //    {
                //        throw;
                //    }
                //}
                return RedirectToAction(nameof(Index));
            }
            return View(editViewModel);
        }

        // GET: Artists/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var artist = await _context.Artists
                .FirstOrDefaultAsync(m => m.Id == id);
            if (artist == null)
            {
                return NotFound();
            }

            return View(artist);
        }

        // POST: Artists/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var artist = await _context.Artists.FindAsync(id);
            _context.Artists.Remove(artist);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ArtistExists(int id)
        {
            return _context.Artists.Any(e => e.Id == id);
        }
    }
}
