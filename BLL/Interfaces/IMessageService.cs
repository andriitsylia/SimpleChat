using DTO.Member;
using DTO.Message;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IMessageService : ISimpleChatService<MessageDTO>
    {
        public MessageDTO GetByIdWithTalks(int id);
        public IEnumerable<MessageDTO> GetAllWithTalks();
    }
}
