using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.Configurations
{
    public class TalkConfiguration : IEntityTypeConfiguration<Talk>
    {
        public void Configure(EntityTypeBuilder<Talk> builder)
        {
        }
    }
}
