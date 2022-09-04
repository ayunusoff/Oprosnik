namespace Oprosnik.Models
{
    public class Question
    {
        public int Id { get; set; }
        public string Text_Quest { get; set; }
        public uint? Scores { get; set; } = 0;
        public Survey survey { get; set; }
    }
}
