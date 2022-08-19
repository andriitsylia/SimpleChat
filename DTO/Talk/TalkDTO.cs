using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.Talk
{
    public class TalkDTO
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public bool IsPrivate { get; set; }
    }
}
