namespace Oprosnik.Models
{
    public class UserAnswer
    {
        public int Id { get; set; }
        public Question question { get; set; }
        public AnswerChoice answerChoice { get; set; }
        public User user { get; set; }
    }
}
