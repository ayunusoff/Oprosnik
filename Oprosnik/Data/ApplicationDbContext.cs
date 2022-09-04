using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Oprosnik.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Oprosnik.Models.AnswerChoice>? AnswerChoice { get; set; }
        public DbSet<Oprosnik.Models.Question>? Question { get; set; }
        public DbSet<Oprosnik.Models.Survey>? Survey { get; set; }
        public DbSet<Oprosnik.Models.User>? User { get; set; }
        public DbSet<Oprosnik.Models.UserAnswer>? UserAnswer { get; set; }
    }
}