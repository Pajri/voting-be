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
using VotingBackend.Services.Web;
using VotingBackend.ViewModel;
using System.Net.Http;

namespace VotingBackend.Controllers
{

    [Route("votings")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class VotingsController : Controller
    {
        private readonly ICategoriesService _categoriesService;
        private readonly IVotingsService _votingsService;

        public VotingsController(ICategoriesService categoriesService, IVotingsService votingsService)
        {
            _categoriesService = categoriesService;
            _votingsService = votingsService;
        }

        // GET: Votings

        [Route("")]
        [Route("{page:int}")]
        public async Task<IActionResult> Index([FromRoute] int page, string searchString, Guid selectedCategory)
        {
            if (page > 0) page -= 1;
            if (Request.Method == HttpMethod.Post.Method)
            {
                page = 0;
            }

            var (votings, pageNum) = await _votingsService.Filter(searchString, selectedCategory, page);

            var categories = await _categoriesService.GetAll();
            categories.Insert(0, new CategoryViewModel { ID = Guid.Empty,Name = "--All--" });
            ViewData["Categories"] = new SelectList(categories, "ID", "Name");
            ViewData["PageNum"] = pageNum;
            return View(votings);
        }

        [Route("Details")]
        // GET: Votings/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var voting = await _votingsService.Details((Guid) id);
            if (voting == null)
            {
                return NotFound();
            }

            return View(voting);
        }

        [Route("Create")]
        // GET: Votings/Create
        public async Task<IActionResult> Create()
        {
            var categories = await _categoriesService.GetAll();
            ViewData["CategoryId"] = new SelectList(categories, "ID", "Name");
            return View();
        }

        // POST: Votings/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Route("Create")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Description,DueDate,CategoryId")] VotingViewModel voting)
        {
            if (ModelState.IsValid)
            {
                await _votingsService.Create(voting);
                return RedirectToAction(nameof(Index));
            }
            
             var categories = await _categoriesService.GetAll();
            ViewData["CategoryId"] = new SelectList(categories, "ID", "Name");
            return View(voting);
        }

        // GET: Votings/Edit/5
        [Route("Edit")]
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var voting = await _votingsService.Details((Guid)id);
            if (voting == null)
            {
                return NotFound();
            }

            var categories = await _categoriesService.GetAll();
            ViewData["CategoryId"] = new SelectList(categories, "ID", "Name", voting.CategoryId);
            return View(voting);
        }

        // POST: Votings/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Route("Edit")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("ID,Name,Description,DueDate,CategoryId")] VotingViewModel voting)
        {
            if (id != voting.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _votingsService.Edit(voting);
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

            var categories = await _categoriesService.GetAll();
            ViewData["CategoryId"] = new SelectList(categories, "ID", "Name", voting.CategoryId);
            return View(voting);
        }

        // GET: Votings/Delete/5
        [Route("Delete")]
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var voting = await _votingsService.Details((Guid)id);
            if (voting == null)
            {
                return NotFound();
            }

            return View(voting);
        }

        // POST: Votings/Delete/5
        [Route("Delete")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _votingsService.Delete(id);
            return RedirectToAction(nameof(Index));
        }

        private bool VotingExists(Guid id)
        {
            return _votingsService.IsVotingExists(id);
        }
    }
}
