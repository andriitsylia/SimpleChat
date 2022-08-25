using DTO.Member;
using DTO.Message;

namespace DTO.Talk
{
    public class TalkModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public bool IsPrivate { get; set; }
        public ICollection<MemberModel> Members { get; set; } = new List<MemberModel>();
        public ICollection<MessageModel> Messages { get; set; } = new List<MessageModel>();
    }
}
