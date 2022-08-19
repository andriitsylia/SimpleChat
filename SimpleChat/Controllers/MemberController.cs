using SimpleChat.Interfaces;
using BLL.Interfaces;
using AutoMapper;
using DTO.Member;


namespace SimpleChat.Controllers
{
    public class MemberController : IMemberController
    {
        private readonly ISimpleChatService<MemberDTO> _service;
        private readonly IMapper _mapper;

        public MemberController(ISimpleChatService<MemberDTO> service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        public void Create(MemberModel member)
        {
            MemberDTO memberDTO = _mapper.Map<MemberDTO>(member);
            _service.Create(memberDTO);
        }

        public void Update(MemberModel member)
        {
            MemberDTO memberDTO = _service.GetById(member.Id);
            if (memberDTO != null)
            {
                _mapper.Map(member, memberDTO);
                _service.Update(memberDTO);
            }
        }

        public void Delete(int id)
        {
            MemberDTO memberDTO = _service.GetById(id);
            if (memberDTO != null)
            {
                _service.Delete(id);
            }
        }

        public MemberModel GetById(int id)
        {
            MemberDTO memberDTO = _service.GetById(id);
            MemberModel result = _mapper.Map<MemberModel>(memberDTO);
            return result;
        }

        public IEnumerable<MemberModel> GetAll()
        {
            IEnumerable<MemberModel> result = _mapper.Map<IEnumerable<MemberModel>>(_service.GetAll());
            return result;
        }
    }
}
