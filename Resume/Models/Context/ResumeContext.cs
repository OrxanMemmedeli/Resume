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
            //optionsBuilder.UseSqlServer(@"Server=ILQAR\SQLEXPRESS01; Database=CoreTicketApp; Integrated Security = true;");
            optionsBuilder.UseSqlServer(@"Server=DESKTOP-TROAMS4; Database=Resume; Integrated Security = true;");

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
    }
}
