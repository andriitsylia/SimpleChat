using DTO.Message;

namespace BLL.Interfaces
{
    public interface IMessageService : ISimpleChatService<MessageDTO>
    {
        public MessageDTO GetByIdWithMemberAndTalks(int id);
        public IEnumerable<MessageDTO> GetAllWithMembersAndTalks();
    }
}
