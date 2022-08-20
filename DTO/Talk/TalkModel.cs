using DTO.Member;

namespace DTO.Talk
{
    public class TalkModel
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public bool IsPrivate { get; set; }
        public ICollection<MemberModel> Members { get; set; }
    }
}
