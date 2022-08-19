using DTO.Talk;

namespace SimpleChat.Interfaces
{
    public interface ITalkController
    {
        void Create(TalkModel talk);
        void Update(TalkModel talk);
        void Delete(int id);
        TalkModel GetById(int id);
        IEnumerable<TalkModel> GetAll();
    }
}