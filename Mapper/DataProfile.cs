using AutoMapper;
using DTO.Member;
using DTO.Talk;
using DAL.Entities;

namespace Mapper
{
    public class DataProfile : Profile
    {
        public DataProfile()
        {
            CreateMap<Member, MemberDTO>().ReverseMap().ForMember(m => m.Id, options => options.Ignore());
            CreateMap<MemberDTO, MemberModel>().ReverseMap().ForMember(m => m.Id, options => options.Ignore());

            CreateMap<Talk, TalkDTO>().ReverseMap().ForMember(t => t.Id, options => options.Ignore());
            CreateMap<TalkDTO, TalkModel>().ReverseMap().ForMember(t => t.Id, options => options.Ignore());
        }
    }
}