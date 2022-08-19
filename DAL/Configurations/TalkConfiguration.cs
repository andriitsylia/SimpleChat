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
            builder.HasData(new Talk() { Id = 1, Name = "", IsPrivate = true },
                            new Talk() { Id = 2, Name = "", IsPrivate = true },
                            new Talk() { Id = 3, Name = "", IsPrivate = true },
                            new Talk() { Id = 4, Name = "", IsPrivate = true },
                            new Talk() { Id = 5, Name = "", IsPrivate = true },
                            new Talk() { Id = 6, Name = "Wolfs", IsPrivate = false },
                            new Talk() { Id = 7, Name = "Sheeps", IsPrivate = false });
        }
    }
}
