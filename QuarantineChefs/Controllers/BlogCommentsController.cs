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
    public class BlogCommentsController : Controller
    {
        private readonly RecipeBloggerContext _context;

        public BlogCommentsController(RecipeBloggerContext context)
        {
            _context = context;
        }

        // GET: BlogComments
        public async Task<IActionResult> Index()
        {
            var recipeBloggerContext = _context.BlogComments.Include(b => b.Blog).Include(b => b.UserProfile);
            return View(await recipeBloggerContext.ToListAsync());
        }

        // GET: BlogComments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var blogComments = await _context.BlogComments
                .Include(b => b.Blog)
                .Include(b => b.UserProfile)
                .FirstOrDefaultAsync(m => m.BlogCommentsId == id);
            if (blogComments == null)
            {
                return NotFound();
            }

            return View(blogComments);
        }

        // GET: BlogComments/Create
        public IActionResult Create()
        {
            ViewData["BlogId"] = new SelectList(_context.BlogArticle, "BlogArticleId", "BlogText");
            ViewData["UserProfileId"] = new SelectList(_context.UserProfile, "UserProfileId", "FirstName");
            return View();
        }

        // POST: BlogComments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BlogCommentsId,Comment,UserProfileId,BlogId")] BlogComments blogComments)
        {
            if (ModelState.IsValid)
            {
                _context.Add(blogComments);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BlogId"] = new SelectList(_context.BlogArticle, "BlogArticleId", "BlogText", blogComments.BlogId);
            ViewData["UserProfileId"] = new SelectList(_context.UserProfile, "UserProfileId", "FirstName", blogComments.UserProfileId);
            return View(blogComments);
        }

        // GET: BlogComments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var blogComments = await _context.BlogComments.FindAsync(id);
            if (blogComments == null)
            {
                return NotFound();
            }
            ViewData["BlogId"] = new SelectList(_context.BlogArticle, "BlogArticleId", "BlogText", blogComments.BlogId);
            ViewData["UserProfileId"] = new SelectList(_context.UserProfile, "UserProfileId", "FirstName", blogComments.UserProfileId);
            return View(blogComments);
        }

        // POST: BlogComments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BlogCommentsId,Comment,UserProfileId,BlogId")] BlogComments blogComments)
        {
            if (id != blogComments.BlogCommentsId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(blogComments);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BlogCommentsExists(blogComments.BlogCommentsId))
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
            ViewData["BlogId"] = new SelectList(_context.BlogArticle, "BlogArticleId", "BlogText", blogComments.BlogId);
            ViewData["UserProfileId"] = new SelectList(_context.UserProfile, "UserProfileId", "FirstName", blogComments.UserProfileId);
            return View(blogComments);
        }

        // GET: BlogComments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var blogComments = await _context.BlogComments
                .Include(b => b.Blog)
                .Include(b => b.UserProfile)
                .FirstOrDefaultAsync(m => m.BlogCommentsId == id);
            if (blogComments == null)
            {
                return NotFound();
            }

            return View(blogComments);
        }

        // POST: BlogComments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var blogComments = await _context.BlogComments.FindAsync(id);
            _context.BlogComments.Remove(blogComments);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BlogCommentsExists(int id)
        {
            return _context.BlogComments.Any(e => e.BlogCommentsId == id);
        }
    }
}
