using DTO.Message;
using DTO.Talk;

namespace DTO.Member
{
    public class MemberDTO
    {
        public int Id { get; set; }
        public string NickName { get; set; } = string.Empty;
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public ICollection<TalkDTO> Talks { get; set; } = new List<TalkDTO>();
        public ICollection<MessageDTO> Messages { get; set; } = new List<MessageDTO>();
    }
}
