using DTO.Member;
using DTO.Talk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface ITalkService : ISimpleChatService<TalkDTO>
    {
        public TalkDTO GetByIdWithMembers(int id);
        public IEnumerable<TalkDTO> GetAllWithMembers();
        public IEnumerable<TalkDTO> GetPrivate();
        public IEnumerable<TalkDTO> GetNonPrivate();
    }
}
