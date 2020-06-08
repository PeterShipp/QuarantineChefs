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
    public class BlogArticlesController : Controller
    {
        private readonly RecipeBloggerContext _context;

        public BlogArticlesController(RecipeBloggerContext context)
        {
            _context = context;
        }

        // GET: BlogArticles
        public async Task<IActionResult> Index()
        {
            return View(await _context.BlogArticle.ToListAsync());
        }

        // GET: BlogArticles/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var blogArticle = await _context.BlogArticle
                .FirstOrDefaultAsync(m => m.BlogArticleId == id);
            if (blogArticle == null)
            {
                return NotFound();
            }

            return View(blogArticle);
        }

        // GET: BlogArticles/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: BlogArticles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BlogArticleId,Title,BlogText")] BlogArticle blogArticle)
        {
            if (ModelState.IsValid)
            {
                _context.Add(blogArticle);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(blogArticle);
        }

        // GET: BlogArticles/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var blogArticle = await _context.BlogArticle.FindAsync(id);
            if (blogArticle == null)
            {
                return NotFound();
            }
            return View(blogArticle);
        }

        // POST: BlogArticles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BlogArticleId,Title,BlogText")] BlogArticle blogArticle)
        {
            if (id != blogArticle.BlogArticleId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(blogArticle);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BlogArticleExists(blogArticle.BlogArticleId))
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
            return View(blogArticle);
        }

        // GET: BlogArticles/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var blogArticle = await _context.BlogArticle
                .FirstOrDefaultAsync(m => m.BlogArticleId == id);
            if (blogArticle == null)
            {
                return NotFound();
            }

            return View(blogArticle);
        }

        // POST: BlogArticles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var blogArticle = await _context.BlogArticle.FindAsync(id);
            _context.BlogArticle.Remove(blogArticle);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BlogArticleExists(int id)
        {
            return _context.BlogArticle.Any(e => e.BlogArticleId == id);
        }
    }
}
