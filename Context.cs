using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace ProjeCore.Models
{
    public class Context : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("server=LAPTOP-H6VBB0G0\\SQLSERVER; database=BirimDB; integrated security=true;TrustServerCertificate = True;");
        }



        public DbSet<Birim> Birims { get; set; }
        public DbSet<Personel> Personels { get; set; }
		public DbSet<Admin> Admins { get; set; }

		public DbSet<Kullanici> Kullanicis { get; set; }

        public DbSet<Randev> Randevs { get; set; }
    }
}
