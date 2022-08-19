using AutoMapper;
using DTO.Member;
using DAL.Entities;

namespace Mapper
{
    public class DataProfile : Profile
    {
        public DataProfile()
        {
            CreateMap<Member, MemberDTO>().ReverseMap().ForMember(m => m.Id, options => options.Ignore());
            CreateMap<MemberDTO, MemberModel>().ReverseMap().ForMember(m => m.Id, options => options.Ignore());
        }
    }
}