using DTO.Member;
using DTO.Message;

namespace DTO.Talk
{
    public class TalkDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public bool IsPrivate { get; set; }
        public ICollection<MemberDTO> Members { get; set; } = new List<MemberDTO>();
        public ICollection<MessageDTO> Messages { get; set; } = new List<MessageDTO>();

    }
}
