using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Oprosnik.Data;
using Oprosnik.Models;

namespace ViewComponents.AnswersList
{
    public class AnswersListViewComponent : ViewComponent
    {
        private readonly ApplicationDbContext _context;
        public AnswersListViewComponent(ApplicationDbContext context) 
        {
            _context = context; 
        }

        public async Task<IViewComponentResult> InvokeAsync(Question quest) 
        {
            var anschoices = 
                (
                    from anschoice in _context.AnswerChoice
                    join question in _context.Question
                    on anschoice.question equals question
                    where question == quest
                    select anschoice
                );
            return View(anschoices);
        }
    }
}
