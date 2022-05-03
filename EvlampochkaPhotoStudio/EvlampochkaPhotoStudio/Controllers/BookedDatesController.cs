#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EvlampochkaPhotoStudio.Data;
using EvlampochkaPhotoStudio.Models;

namespace EvlampochkaPhotoStudio.Controllers
{
    public class BookedDatesController : Controller
    {
        private readonly EvlampochkaPhotoStudioContext _context;

        public BookedDatesController(EvlampochkaPhotoStudioContext context)
        {
            _context = context;
        }

        // GET: BookedDates
        public async Task<IActionResult> Index()
        {
            var evlampochkaPhotoStudioContext = _context.BookedDates.Include(b => b.Room);
            return View(await evlampochkaPhotoStudioContext.ToListAsync());
        }

        // GET: BookedDates/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bookedDates = await _context.BookedDates
                .Include(b => b.Room)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bookedDates == null)
            {
                return NotFound();
            }

            return View(bookedDates);
        }

        // GET: BookedDates/Create
        public IActionResult Create()
        {
            ViewData["RoomId"] = new SelectList(_context.Room, "Id", "Id");
            return View();
        }

        // POST: BookedDates/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,RoomId,Date")] BookedDates bookedDates)
        {
            if (ModelState.IsValid)
            {
                _context.Add(bookedDates);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["RoomId"] = new SelectList(_context.Room, "Id", "Id", bookedDates.RoomId);
            return View(bookedDates);
        }

        // GET: BookedDates/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bookedDates = await _context.BookedDates.FindAsync(id);
            if (bookedDates == null)
            {
                return NotFound();
            }
            ViewData["RoomId"] = new SelectList(_context.Room, "Id", "Id", bookedDates.RoomId);
            return View(bookedDates);
        }

        // POST: BookedDates/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,RoomId,Date")] BookedDates bookedDates)
        {
            if (id != bookedDates.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(bookedDates);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookedDatesExists(bookedDates.Id))
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
            ViewData["RoomId"] = new SelectList(_context.Room, "Id", "Id", bookedDates.RoomId);
            return View(bookedDates);
        }

        // GET: BookedDates/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bookedDates = await _context.BookedDates
                .Include(b => b.Room)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bookedDates == null)
            {
                return NotFound();
            }

            return View(bookedDates);
        }

        // POST: BookedDates/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var bookedDates = await _context.BookedDates.FindAsync(id);
            _context.BookedDates.Remove(bookedDates);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BookedDatesExists(int id)
        {
            return _context.BookedDates.Any(e => e.Id == id);
        }
    }
}
