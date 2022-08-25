namespace DAL.Entities
{
    public class Member
    {
        public int Id { get; set; }
        public string NickName { get; set; } = string.Empty;
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public ICollection<Talk> Talks { get; set; } = new List<Talk>();
        public ICollection<Message> Messages { get; set; } = new List<Message>();
    }
}
