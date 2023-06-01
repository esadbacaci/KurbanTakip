using EntityLayer.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Concrete
{
    public class Context:DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseMySql("server=178.18.200.167;database=kurbantakipoto;user=kurbantakipoto;password=W9hT#Z$XzuE",
                new MySqlServerVersion(new Version(8, 0, 23)));
            }
        }
        public virtual DbSet<Cariislem> Cariislems { get; set; } = null!;
        public virtual DbSet<Carikart> Carikarts { get; set; } = null!;
        public virtual DbSet<Hissecarikart> Hissecarikarts { get; set; } = null!;
        public virtual DbSet<Kasa> Kasas { get; set; } = null!;
        public virtual DbSet<Stok> Stoks { get; set; } = null!;
        public virtual DbSet<Admin> Admins { get; set; } = null!;

    }
}
