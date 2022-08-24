using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.EntityFrameworkCore;
using DAL.EF;
using DAL.Configurations;
using AutoMapper;
using DAL.Interfaces;
using DAL.Entities;
using DAL.Repositories;
using BLL.Interfaces;
using DTO.Member;
using DTO.Talk;
using BLL.Services;
using SimpleChat.Hubs;
using SimpleChat.Interfaces;
using Mapper;
using SimpleChat.Controllers;

namespace SimpleChat
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddRazorPages();
            builder.Services.AddServerSideBlazor();

            string connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
            builder.Services.AddDbContext<ChatContext>(options => options.UseSqlServer(connectionString));

            var mapperConfig = new MapperConfiguration(cfg => cfg.AddProfile(new DataProfile()));
            builder.Services.AddSingleton(mapperConfig.CreateMapper());

            builder.Services.AddScoped<ISimpleChatRepository<Member>, SimpleChatRepository<Member>>();
            builder.Services.AddScoped<ISimpleChatRepository<Talk>, SimpleChatRepository<Talk>>();
            builder.Services.AddScoped<ISimpleChatRepository<Message>, SimpleChatRepository<Message>>();

            builder.Services.AddScoped<IMemberService, MemberService>();
            builder.Services.AddScoped<ITalkService, TalkService>();
            builder.Services.AddScoped<IMessageService, MessageService>();

            builder.Services.AddScoped<IMemberController, MemberController>();
            builder.Services.AddScoped<ITalkController, TalkController>();
            builder.Services.AddScoped<IMessageController, MessageController>();

            builder.Services.AddSingleton<ChatManager>();
            var app = builder.Build();

            app.MigrateDatabase();

            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseRouting();
            
            app.MapBlazorHub();

            app.MapHub<SimpleChatHub>("/SimpleChatHub");

            app.MapFallbackToPage("/_Host");

            app.Run();
        }
    }
}