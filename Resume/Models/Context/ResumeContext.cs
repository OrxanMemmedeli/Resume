﻿using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Resume.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Resume.Models.Context
{
    public class ResumeContext :DbContext
    {

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseSqlServer(@"Server=ILQAR\SQLEXPRESS01; Database=ResumeCore; Integrated Security = true;");
            optionsBuilder.UseSqlServer(@"Server=DESKTOP-TROAMS4; Database=ResumeCore; Integrated Security = true;");

            //optionsBuilder.UseSqlServer("data source=ILQAR\SQLEXPRESS01-DESKTOP-TROAMS4; initial catalog=CoreTicketSales; Integrated Security = true;");
        }

        public DbSet<Blog> Blogs { get; set; }
        public DbSet<BlogCategory> BlogCategories { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Education> Educations { get; set; }
        public DbSet<Experience> Experiences { get; set; }
        public DbSet<FotoList> FotoLists { get; set; }
        public DbSet<Info> Infos { get; set; }
        public DbSet<Portfolio> Portfolios { get; set; }
        public DbSet<PortfolioCategory> PortfolioCategories { get; set; }
        public DbSet<Skill> Skills { get; set; }
        public DbSet<SkillCategory> SkillCategories { get; set; }
        public DbSet<Sosial> Sosials { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<HideTable> HideTables { get; set; }
        public DbSet<ActionUser> ActionUsers { get; set; }
        public DbSet<EmailConfig> EmailConfigs { get; set; }
        public DbSet<ControllerNames> ControllerNames { get; set; }
        public DbSet<ControllerActionUser> ControllerActionUsers { get; set; }
        public DbSet<ActionNames> ActionNames { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ControllerActionUser>().HasKey(x => new {x.ControllerID, x.UserID });
            modelBuilder.Entity<ControllerActionUser>().HasOne<ControllerNames>(x => x.ControllerNames).WithMany(x => x.ControllerActionUsers).HasForeignKey(x => x.ControllerID);
            modelBuilder.Entity<ControllerActionUser>().HasOne<User>(x => x.User).WithMany(x => x.ControllerActionUsers).HasForeignKey(x => x.UserID);
            //modelBuilder.Entity<ControllerActionUser>().HasOne<ActionNames>(x => x.ActionNames).WithMany(x => x.ControllerActionUsers).HasForeignKey(x => x.ActionNamesID);

            modelBuilder.Entity<ActionUser>().HasKey(x => new { x.ActionID, x.UserID });
            modelBuilder.Entity<ActionUser>().HasOne<ActionNames>(x => x.ActionNames).WithMany(x => x.ActionUsers).HasForeignKey(x => x.ActionID);
            modelBuilder.Entity<ActionUser>().HasOne<User>(x => x.User).WithMany(x => x.ActionUsers).HasForeignKey(x => x.UserID);
        }
    
    }
}
