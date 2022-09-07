using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace Oprosnik.Models
{
    public class UserAnswer
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public Question question { get; set; }
        public AnswerChoice answerChoice { get; set; }
        public IdentityUser user { get; set; }
    }
}
