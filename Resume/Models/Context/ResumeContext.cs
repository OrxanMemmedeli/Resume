using Microsoft.AspNetCore.Http;
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
            optionsBuilder.UseSqlServer(@"Server=ILQAR\SQLEXPRESS01; Database=Resume; Integrated Security = true;");
            //optionsBuilder.UseSqlServer(@"Server=DESKTOP-TROAMS4; Database=Resume; Integrated Security = true;");


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
        public DbSet<RoleControl> RoleControls { get; set; }
        public DbSet<UserRoleControl> UserRoleControls { get; set; }
        public DbSet<EmailConfig> EmailConfigs { get; set; }
        public DbSet<ControllerNames> ControllerNames { get; set; }
        public DbSet<ControllerActionUser> ControllerActionUsers { get; set; }
        public DbSet<ActiomNames> ActiomNames { get; set; }
        public DbSet<ControllerAction> ControllerActions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserRoleControl>().HasKey(sc => new { sc.UserID, sc.RoleID });  
            modelBuilder.Entity<UserRoleControl>().HasOne<User>(sc => sc.User).WithMany(s => s.UserRoleControls).HasForeignKey(sc => sc.UserID);
            modelBuilder.Entity<UserRoleControl>().HasOne<RoleControl>(sc => sc.RoleControl).WithMany(s => s.UserRoleControls).HasForeignKey(sc => sc.RoleID);

            modelBuilder.Entity<ControllerAction>().HasKey(sc => new { sc.ControllerID, sc.ActionID });  
            modelBuilder.Entity<ControllerAction>().HasOne<ControllerNames>(sc => sc.ControllerNames).WithMany(s => s.ControllerActions).HasForeignKey(sc => sc.ControllerID);
            modelBuilder.Entity<ControllerAction>().HasOne<ActiomNames>(sc => sc.ActiomNames).WithMany(s => s.ControllerActions).HasForeignKey(sc => sc.ActionID);

            modelBuilder.Entity<ControllerActionUser>().HasKey(sc => new { sc.ControllerActionID, sc.UserID });  
            modelBuilder.Entity<ControllerActionUser>().HasOne<ControllerAction>(sc => sc.ControllerAction).WithMany(s => s.ControllerActionUsers).HasForeignKey(sc => sc.ControllerActionID).HasPrincipalKey(x => x.ID);
            modelBuilder.Entity<ControllerActionUser>().HasOne<User>(sc => sc.User).WithMany(s => s.ControllerActionUsers).HasForeignKey(sc => sc.UserID);
        }
    
    }
}
