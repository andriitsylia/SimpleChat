using DTO.Member;
using DTO.Talk;

namespace DTO.Message
{
    public class MessageModel
    {
        public int Id { get; set; }
        public string Sender { get; set; } = string.Empty;
        public string? Text { get; set; }
        public DateTime Created { get; set; }
        public int MemberId { get; set; }
        public MemberDTO? Member { get; set; }
        public int TalkId { get; set; }
        public TalkDTO? Talk { get; set; }
    }
}
