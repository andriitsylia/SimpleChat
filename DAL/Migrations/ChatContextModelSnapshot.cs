﻿// <auto-generated />
using System;
using DAL.EF;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DAL.Migrations
{
    [DbContext(typeof(ChatContext))]
    partial class ChatContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("DAL.Entities.Member", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NickName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Members");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            FirstName = "Andy",
                            LastName = "Brown1",
                            NickName = "Andy1"
                        },
                        new
                        {
                            Id = 2,
                            FirstName = "Andy",
                            LastName = "Brown2",
                            NickName = "Andy2"
                        },
                        new
                        {
                            Id = 3,
                            FirstName = "Andy",
                            LastName = "Brown3",
                            NickName = "Andy3"
                        },
                        new
                        {
                            Id = 4,
                            FirstName = "Andy",
                            LastName = "Brown4",
                            NickName = "Andy4"
                        },
                        new
                        {
                            Id = 5,
                            FirstName = "Andy",
                            LastName = "Brown5",
                            NickName = "Andy5"
                        });
                });

            modelBuilder.Entity("DAL.Entities.Message", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<int>("MemberId")
                        .HasColumnType("int");

                    b.Property<int>("Sender")
                        .HasColumnType("int");

                    b.Property<int>("TalkId")
                        .HasColumnType("int");

                    b.Property<string>("Text")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("MemberId");

                    b.HasIndex("TalkId");

                    b.ToTable("Messages");
                });

            modelBuilder.Entity("DAL.Entities.Talk", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<bool>("IsPrivate")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Talks");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            IsPrivate = true,
                            Name = ""
                        },
                        new
                        {
                            Id = 2,
                            IsPrivate = true,
                            Name = ""
                        },
                        new
                        {
                            Id = 3,
                            IsPrivate = true,
                            Name = ""
                        },
                        new
                        {
                            Id = 4,
                            IsPrivate = true,
                            Name = ""
                        },
                        new
                        {
                            Id = 5,
                            IsPrivate = true,
                            Name = ""
                        },
                        new
                        {
                            Id = 6,
                            IsPrivate = false,
                            Name = "Wolfs"
                        },
                        new
                        {
                            Id = 7,
                            IsPrivate = false,
                            Name = "Sheeps"
                        });
                });

            modelBuilder.Entity("DAL.Entities.TalkMember", b =>
                {
                    b.Property<int>("TalkId")
                        .HasColumnType("int");

                    b.Property<int>("MemberId")
                        .HasColumnType("int");

                    b.HasKey("TalkId", "MemberId");

                    b.HasIndex("MemberId");

                    b.ToTable("TalkMembers");

                    b.HasData(
                        new
                        {
                            TalkId = 1,
                            MemberId = 1
                        },
                        new
                        {
                            TalkId = 1,
                            MemberId = 2
                        },
                        new
                        {
                            TalkId = 2,
                            MemberId = 2
                        },
                        new
                        {
                            TalkId = 2,
                            MemberId = 5
                        },
                        new
                        {
                            TalkId = 3,
                            MemberId = 3
                        },
                        new
                        {
                            TalkId = 3,
                            MemberId = 4
                        },
                        new
                        {
                            TalkId = 4,
                            MemberId = 1
                        },
                        new
                        {
                            TalkId = 4,
                            MemberId = 2
                        },
                        new
                        {
                            TalkId = 4,
                            MemberId = 3
                        },
                        new
                        {
                            TalkId = 5,
                            MemberId = 3
                        },
                        new
                        {
                            TalkId = 5,
                            MemberId = 4
                        },
                        new
                        {
                            TalkId = 5,
                            MemberId = 5
                        });
                });

            modelBuilder.Entity("DAL.Entities.Message", b =>
                {
                    b.HasOne("DAL.Entities.Member", "Member")
                        .WithMany("Messages")
                        .HasForeignKey("MemberId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DAL.Entities.Talk", "Talk")
                        .WithMany("Messages")
                        .HasForeignKey("TalkId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Member");

                    b.Navigation("Talk");
                });

            modelBuilder.Entity("DAL.Entities.TalkMember", b =>
                {
                    b.HasOne("DAL.Entities.Member", "Member")
                        .WithMany("TalkMembers")
                        .HasForeignKey("MemberId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DAL.Entities.Talk", "Talk")
                        .WithMany("TalkMembers")
                        .HasForeignKey("TalkId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Member");

                    b.Navigation("Talk");
                });

            modelBuilder.Entity("DAL.Entities.Member", b =>
                {
                    b.Navigation("Messages");

                    b.Navigation("TalkMembers");
                });

            modelBuilder.Entity("DAL.Entities.Talk", b =>
                {
                    b.Navigation("Messages");

                    b.Navigation("TalkMembers");
                });
#pragma warning restore 612, 618
        }
    }
}
