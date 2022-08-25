namespace DAL.Entities
{
    public class Message
    {
        public int Id { get; set; }
        public string Sender { get; set; } = string.Empty;
        public string? Text { get; set; }
        public DateTime Created { get; set; }
        public int MemberId { get; set; }
        public Member? Member { get; set; }
        public int TalkId { get; set; }
        public Talk? Talk { get; set; }
    }
}
