namespace SimpleChat.Services
{
    public enum ParticipantType
    {
        Member,
        Talk
    }

    public class Participant
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public ParticipantType Type { get; set; }
        public int TalkId { get; set; }
        public string TalkName { get; set; } = String.Empty;
    }
}
