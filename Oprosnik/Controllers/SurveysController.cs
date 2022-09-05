using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Oprosnik.Data;
using Oprosnik.Models;

namespace Oprosnik.Controllers
{
    public class SurveysController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SurveysController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Surveys
        public async Task<IActionResult> Index()
        {
            return _context.Survey != null ?
                        View(await _context.Survey.ToListAsync()) :
                        Problem("Entity set 'ApplicationDbContext.Survey'  is null.");
        }
        // GET: Surveys/UserId
        public IActionResult MySurveys(int id)
        {
            var surveys = _context.Survey.Join(_context.Question, // второй набор
                 s => s, // свойство-селектор объекта из первого набора
                 q => q.survey, // свойство-селектор объекта из второго набора
                 (s, q) => new // результат
                 {
                     title = s.Title,
                     description = s.Description
                 });

            /*
            var surveys = (
            from survey in _context.Survey
            join question in _context.Question
            on survey equals question.survey
            join userans in _context.UserAnswer
            on question equals userans.question
            join user in _context.User
            on userans.user equals user
            where id.ToString() == user.Id
            select survey
        );
            */

            return View(surveys.ToList());
        }
        // GET: Surveys/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Survey == null)
            {
                return NotFound();
            }


            var quests = _context.Question.Where(s => s.survey.Id == id).ToList();
            /*
            var quests = (
                from surveys in _context.Survey
                join questions in _context.Question
                on surveys equals questions.survey
                where surveys.Id == id
                select questions
                );*/
            if (quests == null)
            {
                return NotFound();
            }
            ViewData["SurveyTitle"] = _context.Survey.First(m => m.Id == id).Title;
                //.FirstOrDefault(m => m.Id == id).Title;

            return View(quests);
        }

        // GET: Surveys/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Surveys/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Description")] Survey survey)
        {
            if (ModelState.IsValid)
            {
                _context.Add(survey);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(survey);
        }

        // GET: Surveys/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Survey == null)
            {
                return NotFound();
            }

            var survey = await _context.Survey.FindAsync(id);
            if (survey == null)
            {
                return NotFound();
            }
            return View(survey);
        }

        // POST: Surveys/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Description")] Survey survey)
        {
            if (id != survey.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(survey);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SurveyExists(survey.Id))
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
            return View(survey);
        }

        // GET: Surveys/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Survey == null)
            {
                return NotFound();
            }

            var survey = await _context.Survey
                .FirstOrDefaultAsync(m => m.Id == id);
            if (survey == null)
            {
                return NotFound();
            }

            return View(survey);
        }

        // POST: Surveys/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Survey == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Survey'  is null.");
            }
            var survey = await _context.Survey.FindAsync(id);
            if (survey != null)
            {
                _context.Survey.Remove(survey);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SurveyExists(int id)
        {
            return (_context.Survey?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
