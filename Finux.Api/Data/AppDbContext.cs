using System.Reflection;
using Finux.Api.Models;
using Finux.Core.Models;
using Finux.Core.Models.Reports;
using Finux.Core.Models.Stocks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Finux.Api.Data
{
    public class AppDbContext 
        : IdentityDbContext
        <
            User, 
            IdentityRole<long>, 
            long,
            IdentityUserClaim<long>,
            IdentityUserRole<long>,
            IdentityUserLogin<long>,
            IdentityRoleClaim<long>,
            IdentityUserToken<long>
        >
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }
        public DbSet<Category> Categories { get; set; } = null!;
        public DbSet<Transaction> Transactions { get; set; } = null!;
        public DbSet<Stock> Stocks { get; set; } = null!;
        public DbSet<Order> Orders { get; set; } = null!;
        public DbSet<Product> Products { get; set; } = null!;
        public DbSet<Voucher> Vouchers { get; set; } = null!;


        public DbSet<IncomesAndExpenses> IncomesAndExpenses { get; set; } = null!;
        public DbSet<ExpensesByCategory> ExpensesByCategory { get; set; } = null!;
        public DbSet<IncomesByCategory> IncomesByCategory { get; set; } = null!;
        public DbSet<AssetsInWallet> AssetsInWallets { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            builder.Entity<IncomesAndExpenses>()
                .HasNoKey()
                .ToView("vwgetincomesandexpenses");

            builder.Entity<IncomesByCategory>()
                .HasNoKey()
                .ToView("vwgetincomesbycategory");

            builder.Entity<ExpensesByCategory>()
                .HasNoKey()
                .ToView("vwgetexpensesbycategory");

            builder.Entity<AssetsInWallet>()
                .HasNoKey()
                .ToView("vwgetassestinwallet");
        }	
    }
}