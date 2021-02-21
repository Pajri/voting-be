using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using VotingBackend.Data;
using VotingBackend.Models;
using System.Linq;

namespace VotingBackend.Controllers
{
    public class VotingsController : Controller
    {
        private readonly VotingDbContext _context;

        public VotingsController(VotingDbContext context)
        {
            _context = context;
        }

        // GET: Votings
        public async Task<IActionResult> Index(string searchString, string selectedCategory)
        {
            IQueryable<Voting> votings = _context.Voting.Include(v => v.Category);

            if(searchString != null && searchString != "")
            {
                votings = votings.Where(v => v.Name.ToLower().Contains(searchString.ToLower()));
            }

            if(selectedCategory != null && selectedCategory != "")
            {
                votings = votings.Where(v => v.Category.ID == new Guid(selectedCategory));

            }

            return View(await votings.ToListAsync());
        }

        // GET: Votings/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var voting = await _context.Voting
                .Include(v => v.Category)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (voting == null)
            {
                return NotFound();
            }

            return View(voting);
        }

        // GET: Votings/Create
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_context.Category, "ID", "Name");
            return View();
        }

        // POST: Votings/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Name,Description,CreatedDate,VotersCount,DueDate,CategoryId")] Voting voting)
        {
            if (ModelState.IsValid)
            {
                voting.ID = Guid.NewGuid();
                _context.Add(voting);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.Category, "ID", "Name", voting.CategoryId);
            return View(voting);
        }

        // GET: Votings/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var voting = await _context.Voting.FindAsync(id);
            if (voting == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(_context.Category, "ID", "Name", voting.CategoryId);
            return View(voting);
        }

        // POST: Votings/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("ID,Name,Description,CreatedDate,VotersCount,DueDate,CategoryId")] Voting voting)
        {
            if (id != voting.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(voting);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VotingExists(voting.ID))
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
            ViewData["CategoryId"] = new SelectList(_context.Category, "ID", "Name", voting.CategoryId);
            return View(voting);
        }

        // GET: Votings/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var voting = await _context.Voting
                .Include(v => v.Category)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (voting == null)
            {
                return NotFound();
            }

            return View(voting);
        }

        // POST: Votings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var voting = await _context.Voting.FindAsync(id);
            _context.Voting.Remove(voting);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VotingExists(Guid id)
        {
            return _context.Voting.Any(e => e.ID == id);
        }
    }
}
