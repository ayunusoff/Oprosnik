using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Oprosnik.Data;
using Oprosnik.Models;
using System.Net;

namespace Oprosnik.Controllers
{
    [Authorize]
    public class UserAnswersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public UserAnswersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: UserAnswers
        public async Task<IActionResult> Index()
        {
            return _context.UserAnswer != null ?
                        View(await _context.UserAnswer.ToListAsync()) :
                        Problem("Entity set 'ApplicationDbContext.UserAnswer'  is null.");
        }

        // GET: UserAnswers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.UserAnswer == null)
            {
                return NotFound();
            }

            var userAnswer = await _context.UserAnswer
                .FirstOrDefaultAsync(m => m.Id == id);
            if (userAnswer == null)
            {
                return NotFound();
            }

            return View(userAnswer);
        }

        // GET: UserAnswers/Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] string userId, [FromForm] int anschoiceId, [FromForm] int questionId)
        {
            var quest = await _context.Question.FindAsync(questionId);
            var usr = await _context.Users.FindAsync(userId);
            var anschoice = await _context.AnswerChoice.FindAsync(anschoiceId);

            if (usr == null || quest == null || anschoice == null)
            {
                return StatusCode((int)HttpStatusCode.NotAcceptable);
            }

            UserAnswer userAnswer = new UserAnswer
            {
                question = quest,
                answerChoice = anschoice,
                user = usr
            };

            if (ModelState.IsValid)
            {
                await _context.UserAnswer.AddAsync(userAnswer);
                await _context.SaveChangesAsync();
                return Redirect(Request.Headers["Referer"].ToString());
            }
            return NoContent();
        }

        // GET: UserAnswers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.UserAnswer == null)
            {
                return NotFound();
            }

            var userAnswer = await _context.UserAnswer.FindAsync(id);
            if (userAnswer == null)
            {
                return NotFound();
            }
            return View(userAnswer);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, UserAnswer userAnswer)
        {
            if (id != userAnswer.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(userAnswer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserAnswerExists(userAnswer.Id))
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
            return View(userAnswer);
        }

        // GET: UserAnswers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.UserAnswer == null)
            {
                return NotFound();
            }

            var userAnswer = await _context.UserAnswer
                .FirstOrDefaultAsync(m => m.Id == id);
            if (userAnswer == null)
            {
                return NotFound();
            }

            return View(userAnswer);
        }

        // POST: UserAnswers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.UserAnswer == null)
            {
                return Problem("Entity set 'ApplicationDbContext.UserAnswer'  is null.");
            }
            var userAnswer = await _context.UserAnswer.FindAsync(id);
            if (userAnswer != null)
            {
                _context.UserAnswer.Remove(userAnswer);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserAnswerExists(int id)
        {
            return (_context.UserAnswer?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
