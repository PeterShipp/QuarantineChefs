using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QuarantineChefs.Models;

namespace QuarantineChefs.Controllers
{
    public class RecipeCommentsController : Controller
    {
        private readonly RecipeBloggerContext _context;

        public RecipeCommentsController(RecipeBloggerContext context)
        {
            _context = context;
        }

        // GET: RecipeComments
        public async Task<IActionResult> Index()
        {
            var recipeBloggerContext = _context.RecipeComments.Include(r => r.Recipe).Include(r => r.UserProfile);
            return View(await recipeBloggerContext.ToListAsync());
        }

        // GET: RecipeComments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var recipeComments = await _context.RecipeComments
                .Include(r => r.Recipe)
                .Include(r => r.UserProfile)
                .FirstOrDefaultAsync(m => m.RecipeCommentsId == id);
            if (recipeComments == null)
            {
                return NotFound();
            }

            return View(recipeComments);
        }

        // GET: RecipeComments/Create
        public IActionResult Create()
        {
            ViewData["RecipeId"] = new SelectList(_context.Recipe, "RecipeId", "RecipeText");
            ViewData["UserProfileId"] = new SelectList(_context.UserProfile, "UserProfileId", "FirstName");
            return View();
        }

        // POST: RecipeComments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RecipeCommentsId,Comments,UserProfileId,RecipeId")] RecipeComments recipeComments)
        {
            if (ModelState.IsValid)
            {
                _context.Add(recipeComments);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["RecipeId"] = new SelectList(_context.Recipe, "RecipeId", "RecipeText", recipeComments.RecipeId);
            ViewData["UserProfileId"] = new SelectList(_context.UserProfile, "UserProfileId", "FirstName", recipeComments.UserProfileId);
            return View(recipeComments);
        }

        // GET: RecipeComments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var recipeComments = await _context.RecipeComments.FindAsync(id);
            if (recipeComments == null)
            {
                return NotFound();
            }
            ViewData["RecipeId"] = new SelectList(_context.Recipe, "RecipeId", "RecipeText", recipeComments.RecipeId);
            ViewData["UserProfileId"] = new SelectList(_context.UserProfile, "UserProfileId", "FirstName", recipeComments.UserProfileId);
            return View(recipeComments);
        }

        // POST: RecipeComments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("RecipeCommentsId,Comments,UserProfileId,RecipeId")] RecipeComments recipeComments)
        {
            if (id != recipeComments.RecipeCommentsId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(recipeComments);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RecipeCommentsExists(recipeComments.RecipeCommentsId))
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
            ViewData["RecipeId"] = new SelectList(_context.Recipe, "RecipeId", "RecipeText", recipeComments.RecipeId);
            ViewData["UserProfileId"] = new SelectList(_context.UserProfile, "UserProfileId", "FirstName", recipeComments.UserProfileId);
            return View(recipeComments);
        }

        // GET: RecipeComments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var recipeComments = await _context.RecipeComments
                .Include(r => r.Recipe)
                .Include(r => r.UserProfile)
                .FirstOrDefaultAsync(m => m.RecipeCommentsId == id);
            if (recipeComments == null)
            {
                return NotFound();
            }

            return View(recipeComments);
        }

        // POST: RecipeComments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var recipeComments = await _context.RecipeComments.FindAsync(id);
            _context.RecipeComments.Remove(recipeComments);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RecipeCommentsExists(int id)
        {
            return _context.RecipeComments.Any(e => e.RecipeCommentsId == id);
        }
    }
}
