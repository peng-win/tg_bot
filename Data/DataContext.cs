using Microsoft.EntityFrameworkCore;
using Data.Entities;

namespace Data
{
    public class DataContext : DbContext
    {        
        public DbSet<IngridientsEntity> Ingridients { get; set; }
        public DbSet<OrdersEntity> Orders { get; set; }
        public DbSet<UserEntity> Users { get; set; }
        public DbSet<PaymentTypeEntity> PaymentTypes { get; set; }
        public DbSet<PizzaSizeEntity> PizzaSize { get; set; }
        public DbSet<PizzaEntity> Pizzas { get; set; }
        public DbSet<MenuEntity> Menu { get; set; }


        public DataContext(DbContextOptions<DataContext> options): base(options)
        {

        }

        public async Task<int> SaveChangesAsync()
        {
            return await base.SaveChangesAsync();
        }

        public DbSet<T> DbSet<T>() where T : class
        {
            return Set<T>();
        }
    }
}