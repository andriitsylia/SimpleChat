using DTO.Talk;

namespace SimpleChat.Interfaces
{
    public interface ITalkController : IController<TalkModel>
    {
        TalkModel GetByName(string name);
        IEnumerable<TalkModel> GetPrivate();
        IEnumerable<TalkModel> GetNonPrivate();

    }
}