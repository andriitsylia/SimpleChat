
using DTO.Member;

namespace DTO.Talk
{
    public class TalkDTO
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public bool IsPrivate { get; set; }
        public ICollection<MemberDTO> Members { get; set; }
    }
}
