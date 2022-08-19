using DTO.Member;

namespace SimpleChat.Interfaces
{
    public interface IMemberController
    {
        void Create(MemberModel member);
        void Update(MemberModel member);
        void Delete(int id);
        MemberModel GetById(int id);
        IEnumerable<MemberModel> GetAll();
    }
}
