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
    public class TalkConfiguration : IEntityTypeConfiguration<Talk>
    {
        public void Configure(EntityTypeBuilder<Talk> builder)
        {
            //Member m1 = new Member() {NickName = "Andy1", FirstName = "Andy", LastName = "Brown1" };
            //Member m2 = new Member() {NickName = "Andy2", FirstName = "Andy", LastName = "Brown2" };
            //Member m3 = new Member() {NickName = "Andy3", FirstName = "Andy", LastName = "Brown3" };
            //Member m4 = new Member() {NickName = "Andy4", FirstName = "Andy", LastName = "Brown4" };
            //Member m5 = new Member() {NickName = "Andy5", FirstName = "Andy", LastName = "Brown5" };

            //builder.HasData(new Talk() { Id = 1, Name = "Andy1_Andy2", IsPrivate = true, Members = { m1, m2 } },
            //                new Talk() { Id = 2, Name = "Andy3_Andy4", IsPrivate = true, Members = { m3, m4 } },
            //                new Talk() { Id = 3, Name = "Andy1_Andy5", IsPrivate = true, Members = { m1, m5 } },
            //                new Talk() { Id = 4, Name = "Andy5_Andy2", IsPrivate = true, Members = { m2, m5 } },
            //                new Talk() { Id = 5, Name = "Andy2_Andy4", IsPrivate = true, Members = { m2, m4 } },
            //                new Talk() { Id = 6, Name = "Wolfs", IsPrivate = false, Members = { m1, m2, m3 } },
            //                new Talk() { Id = 7, Name = "Sheeps", IsPrivate = false, Members = { m3, m4, m5 } });
        }
    }
}
