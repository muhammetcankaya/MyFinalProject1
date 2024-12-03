using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Entities.Concrete;
using Entities.Absrract;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;


namespace DataAcces.Concrete.EntityFramework
{
    //Context: db tabloları ile prohe classlarını bağlamak
    public class NorthwindContext : DbContext
    {
        // veri tabanına bağlandık 
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\MSSQLLocalDB ;Database=Northwind;Trusted_Connection=true ");
        }
        //Burada bizim nesneler veri tabanıyla eşledik
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OperationClaim> OperationClaims { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserOperationClaim> UserOperationClaims { get; set; }
    }
}
