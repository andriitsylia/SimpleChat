using AutoMapper;
using BLL.Interfaces;
using DAL.Entities;
using DAL.Interfaces;
using DTO.Member;

namespace BLL.Services
{
    public class MemberService : IMemberService
    {
        private readonly ISimpleChatRepository<Member> _repository;
        private readonly IMapper _mapper;

        public MemberService(ISimpleChatRepository<Member> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public void Create(MemberDTO item)
        {
            Member member = _mapper.Map<Member>(item);
            _repository.Create(member);
            _repository.Save();
        }

        public void Delete(int id)
        {
            Member member = _repository.GetById(id);
            if (member != null)
            {
                _repository.Delete(member);
                _repository.Save();
            }
        }

        public IEnumerable<MemberDTO> GetAll()
        {
            IEnumerable<Member> members = _repository.GetAll();
            IEnumerable<MemberDTO> result = _mapper.Map<IEnumerable<MemberDTO>>(members);
            return result;
        }

        public IEnumerable<MemberDTO> GetAllWithTalks()
        {
            IEnumerable<Member> members = _repository.Get(includeProperties: "Talks");
            IEnumerable<MemberDTO> result = _mapper.Map<IEnumerable<MemberDTO>>(members);
            return result;
        }

        public MemberDTO GetById(int id)
        {
            Member member = _repository.GetById(id);
            MemberDTO result = _mapper.Map<MemberDTO>(member);
            return result;
        }

        public MemberDTO GetByIdWithTalks(int id)
        {
            Member member = _repository.Get(filter: m => m.Id == id, includeProperties: "Talks").FirstOrDefault();
            MemberDTO result = _mapper.Map<MemberDTO>(member);
            return result;
        }

        public void Update(MemberDTO item)
        {
            Member member = _repository.GetById(item.Id);
            if (member != null)
            {
                _mapper.Map(item, member);
                _repository.Update(member);
                _repository.Save();
            }
        }
    }
}
