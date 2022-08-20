using DTO.Message;

namespace SimpleChat.Interfaces
{
    public interface IMessageController
    {
        void Create(MessageModel talk);
        void Update(MessageModel talk);
        void Delete(int id);
        MessageModel GetById(int id);
        IEnumerable<MessageModel> GetAll();
    }
}
