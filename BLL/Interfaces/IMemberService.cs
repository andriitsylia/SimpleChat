using DTO.Member;

namespace BLL.Interfaces
{
    public interface IMemberService : ISimpleChatService<MemberDTO>
    {
        public MemberDTO GetByIdWithTalks(int id);
        public IEnumerable<MemberDTO> GetAllWithTalks();

    }
}
