using DTO.Member;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IMemberService : ISimpleChatService<MemberDTO>
    {
        public MemberDTO GetByIdWithTalks(int id);
        public IEnumerable<MemberDTO> GetAllWithTalks();

    }
}
