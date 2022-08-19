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
            builder.HasData(new Member() { Id = 1, NickName = "Andy1", FirstName = "Andy", LastName = "Brown1" },
                            new Member() { Id = 2, NickName = "Andy2", FirstName = "Andy", LastName = "Brown2" },
                            new Member() { Id = 3, NickName = "Andy3", FirstName = "Andy", LastName = "Brown3" },
                            new Member() { Id = 4, NickName = "Andy4", FirstName = "Andy", LastName = "Brown4" },
                            new Member() { Id = 5, NickName = "Andy5", FirstName = "Andy", LastName = "Brown5" });
        }
    }
}
