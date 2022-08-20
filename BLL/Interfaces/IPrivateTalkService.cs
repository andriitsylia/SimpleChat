using DTO.Talk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IPrivateTalkService : ISimpleChatService<TalkDTO>
    {
        public IEnumerable<TalkDTO> GetPrivate();
        public IEnumerable<TalkDTO> GetNonPrivate();
    }
}
