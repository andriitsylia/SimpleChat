using AutoMapper;
using BLL.DTOs;
using DAL.Entities;

namespace Mapper
{
    public class DataProfile : Profile
    {
        public DataProfile()
        {
            CreateMap<Member, MemberDTO>().ReverseMap().ForMember(m => m.Id, options => options.Ignore());

        }
    }
}