using DTO.Talk;

namespace BLL.Interfaces
{
    public interface ITalkService : ISimpleChatService<TalkDTO>
    {
        public TalkDTO GetByName(string name);
        public TalkDTO GetByIdWithMembers(int id);
        public IEnumerable<TalkDTO> GetAllWithMembers();
        public IEnumerable<TalkDTO> GetPrivateWithMembers();
        public IEnumerable<TalkDTO> GetNonPrivateWithMembers();
    }
}
