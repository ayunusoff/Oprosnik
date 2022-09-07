using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Oprosnik.Data;
using Oprosnik.Models;

namespace Oprosnik.Controllers
{
    public class AnswerChoicesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AnswerChoicesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: AnswerChoices
        public async Task<IActionResult> Index()
        {
            return _context.AnswerChoice != null ?
                        View(await _context.AnswerChoice.ToListAsync()) :
                        Problem("Entity set 'ApplicationDbContext.AnswerChoice'  is null.");
        }
        // GET: AnswerChoices/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.AnswerChoice == null)
            {
                return NotFound();
            }

            var answerChoice = await _context.AnswerChoice
                .FirstOrDefaultAsync(m => m.Id == id);
            if (answerChoice == null)
            {
                return NotFound();
            }

            return View(answerChoice);
        }

        // GET: AnswerChoices/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: AnswerChoices/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Text_Choice,correct_answer")] AnswerChoice answerChoice)
        {
            if (ModelState.IsValid)
            {
                _context.Add(answerChoice);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(answerChoice);
        }

        // GET: AnswerChoices/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.AnswerChoice == null)
            {
                return NotFound();
            }

            var answerChoice = await _context.AnswerChoice.FindAsync(id);
            if (answerChoice == null)
            {
                return NotFound();
            }
            return View(answerChoice);
        }

        // POST: AnswerChoices/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Text_Choice,correct_answer")] AnswerChoice answerChoice)
        {
            if (id != answerChoice.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(answerChoice);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AnswerChoiceExists(answerChoice.Id))
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
            return View(answerChoice);
        }

        // GET: AnswerChoices/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.AnswerChoice == null)
            {
                return NotFound();
            }

            var answerChoice = await _context.AnswerChoice
                .FirstOrDefaultAsync(m => m.Id == id);
            if (answerChoice == null)
            {
                return NotFound();
            }

            return View(answerChoice);
        }

        // POST: AnswerChoices/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.AnswerChoice == null)
            {
                return Problem("Entity set 'ApplicationDbContext.AnswerChoice'  is null.");
            }
            var answerChoice = await _context.AnswerChoice.FindAsync(id);
            if (answerChoice != null)
            {
                _context.AnswerChoice.Remove(answerChoice);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AnswerChoiceExists(int id)
        {
            return (_context.AnswerChoice?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
