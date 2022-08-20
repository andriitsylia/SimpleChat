using DTO.TalkMember;

namespace SimpleChat.Interfaces
{
    public interface ITalkMemberController
    {
        void Create(TalkMemberModel talkMember);
        void Update(TalkMemberModel talkMember);
        void Delete(int id);
        TalkMemberModel GetByIds(int talkId, int memberId);
        IEnumerable<TalkMemberModel> GetAll();

    }
}