using System;
using System.Threading;
using System.Threading.Tasks;
using MFTShop.Models.DbModels;
using Microsoft.EntityFrameworkCore;

namespace MFTShop.Data
{
    public interface IApplicationDbContext:IDisposable
    {
        DbSet<Category> Categories { get; set; }
        DbSet<Customer> Customers { get; set; }
        DbSet<OrderDetail> OrderDetails { get; set; }
        DbSet<Order> Orders { get; set; }
        DbSet<ProductCategory> ProductCategories { get; set; }
        DbSet<Product> Products { get; set; }
        DbSet<Setting> Settings { get; set; }

        void Dispose();
        ValueTask DisposeAsync();
        int SaveChanges();
        Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default);
    }
}