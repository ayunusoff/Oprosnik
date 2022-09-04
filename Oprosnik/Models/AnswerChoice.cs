namespace Oprosnik.Models
{
    public class AnswerChoice
    {
        public int Id { get; set; }
        public string Text_Choice { get; set; }
        public Question question { get; set; }
        public bool? correct_answer { get; set; }
    }
}
