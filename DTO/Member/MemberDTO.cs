using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.Member
{
    public class MemberDTO
    {
        public int Id { get; set; }
        public string NickName { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
    }
}
