using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using DAL.Entities;
using DAL.Configurations;

namespace DAL.EF
{
    public class ChatContext : DbContext
    {
        public DbSet<Member> Members { get; set; }
        public DbSet<Talk> Talks { get; set; }
        public DbSet<Message> Messages { get; set; }

        public ChatContext(DbContextOptions<ChatContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration<Member>(new MemberConfiguration());
            modelBuilder.ApplyConfiguration<Talk>(new TalkConfiguration());
        }

    }
}
