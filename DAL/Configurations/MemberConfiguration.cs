using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Threading.Tasks;
using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.Configurations
{
    public class MemberConfiguration : IEntityTypeConfiguration<Member>
    {
        public void Configure(EntityTypeBuilder<Member> builder)
        {
            //Talk t1 = new Talk() {Name = "Andy1_Andy2", IsPrivate = true };
            //Talk t2 = new Talk() {Name = "Andy3_Andy4", IsPrivate = true };
            //Talk t3 = new Talk() {Name = "Andy1_Andy5", IsPrivate = true };
            //Talk t4 = new Talk() {Name = "Andy5_Andy2", IsPrivate = true };
            //Talk t5 = new Talk() {Name = "Andy2_Andy4", IsPrivate = true };
            //Talk t6 = new Talk() {Name = "Wolfs", IsPrivate = false };
            //Talk t7 = new Talk() {Name = "Sheeps", IsPrivate = false };

            //builder.HasData(new Member() { Id = 1, NickName = "Andy1", FirstName = "Andy", LastName = "Brown1", Talks = { t1, t3, t6 } },
            //                new Member() { Id = 2, NickName = "Andy2", FirstName = "Andy", LastName = "Brown2", Talks = { t1, t4, t5, t6 } },
            //                new Member() { Id = 3, NickName = "Andy3", FirstName = "Andy", LastName = "Brown3", Talks = { t2, t6, t7 } },
            //                new Member() { Id = 4, NickName = "Andy4", FirstName = "Andy", LastName = "Brown4", Talks = { t2, t5, t7 } },
            //                new Member() { Id = 5, NickName = "Andy5", FirstName = "Andy", LastName = "Brown5", Talks = { t3, t4, t7 } });
        }
    }
}