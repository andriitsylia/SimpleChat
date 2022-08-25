using AutoMapper;
using DAL.Entities;
using DTO.Member;
using DTO.Message;
using DTO.Talk;

namespace Mapper
{
    public class DataProfile : Profile
    {
        public DataProfile()
        {
            CreateMap<Member, MemberDTO>().ReverseMap().ForMember(m => m.Id, options => options.Ignore())
                                                       .ForMember(mt => mt.Talks, options => options.Ignore())
                                                       .ForMember(mm => mm.Messages, options => options.Ignore());
            CreateMap<MemberDTO, MemberModel>().ReverseMap();

            CreateMap<Talk, TalkDTO>().ReverseMap().ForMember(t => t.Id, options => options.Ignore())
                                                   //.ForMember(tm => tm.Members, options => options.Ignore())
                                                   .ForMember(tm => tm.Messages, options => options.Ignore());
            CreateMap<TalkDTO, TalkModel>().ReverseMap();

            CreateMap<Message, MessageDTO>().ReverseMap().ForMember(m => m.Id, options => options.Ignore())
                                                         .ForMember(mm => mm.Member, options => options.Ignore())
                                                         .ForMember(mt => mt.Talk, options => options.Ignore());
            CreateMap<MessageDTO, MessageModel>().ReverseMap();
        }
    }
}