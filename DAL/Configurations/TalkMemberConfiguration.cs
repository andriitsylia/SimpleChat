using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using DAL.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.Configurations
{
    public class TalkMemberConfiguration : IEntityTypeConfiguration<TalkMember>
    {
        public void Configure(EntityTypeBuilder<TalkMember> builder)
        {
            builder.HasKey(tm => new { tm.TalkId, tm.MemberId });
            builder.HasOne(tmo => tmo.Member).WithMany(tmm => tmm.TalkMembers).HasForeignKey(tmf => tmf.MemberId);
            builder.HasOne(tm => tm.Talk).WithMany(tmm => tmm.TalkMembers).HasForeignKey(tmf => tmf.TalkId);

            builder.HasData(new TalkMember() { TalkId = 1, MemberId = 1 },
                            new TalkMember() { TalkId = 1, MemberId = 2 },
                            new TalkMember() { TalkId = 2, MemberId = 2 },
                            new TalkMember() { TalkId = 2, MemberId = 5 },
                            new TalkMember() { TalkId = 3, MemberId = 3 },
                            new TalkMember() { TalkId = 3, MemberId = 4 },
                            new TalkMember() { TalkId = 4, MemberId = 1 },
                            new TalkMember() { TalkId = 4, MemberId = 2 },
                            new TalkMember() { TalkId = 4, MemberId = 3 },
                            new TalkMember() { TalkId = 5, MemberId = 3 },
                            new TalkMember() { TalkId = 5, MemberId = 4 },
                            new TalkMember() { TalkId = 5, MemberId = 5 });
        }
    }
}
