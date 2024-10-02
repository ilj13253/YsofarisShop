using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using SOFARI;
using SofariDb.Новая_папка;

namespace SofariDb
{
    public class SOFARIDB : DbContext
    {
        public SOFARIDB(DbContextOptions<SOFARIDB> options) : base(options)
        {
        }
        public DbSet<Product> Products { get; set; }  
        public DbSet<ProductImg> ProductImgs { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<WhishList> WhishLists { get; set; }
        public DbSet<Order> Orders { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ProductConfiguration());
            base.OnModelCreating(modelBuilder);
        }
    }
}
//фильтр 
//админовская панель
//как создать
//mvc 
//web api
//