namespace DAL.Entities
{
    public class Talk
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public bool IsPrivate { get; set; }
        public ICollection<Member> Members { get; set; } = new List<Member>();
        public ICollection<Message> Messages { get; set; } = new List<Message>();

    }
}
