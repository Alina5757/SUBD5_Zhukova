using DataBaseImplement.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBaseImplement
{
    public class Database : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (optionsBuilder.IsConfigured == false)
            {
                optionsBuilder.UseSqlServer(@"Data Source=DESKTOP-H7FNSK3\SQLEXPRESS;
Initial Catalog=SUBD5Database;Integrated Security=True;MultipleActiveResultSets=True;");
            }
            base.OnConfiguring(optionsBuilder);
        }
        public virtual DbSet<GSM> GSMs { set; get; }
        public virtual DbSet<Sell> Sells { set; get; }
        public virtual DbSet<Smena> Smenas { set; get; }
        public virtual DbSet<Worker> Workers { set; get; }
        public virtual DbSet<WorkerGSM> WorkerGSMs { set; get; }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            modelBuilder.Entity<Smena>().HasKey(s => new { s.SmenaDate, s.SmenaNumber, s.WorkerId});
            modelBuilder.Entity<WorkerGSM>().HasKey(wg => new { wg.GSMId, wg.WorkerId});
        }
    }
}
