using Microsoft.EntityFrameworkCore;
using Minos.Domain.Aggregates.Model;
using Minos.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Minos.Engine.Contexts
{
    public class MinosContext : DbContext, IUnitOfWork
    {
        public MinosContext(DbContextOptions<MinosContext> options) : base(options) { }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<AdminGroup> AdminsGroups { get; set; }
        public DbSet<AdminMenu> AdminMenus { get; set; }
        public DbSet<Code> Codes { get; set; }
        public DbSet<Company> Companys { get; set; }
        public DbSet<Device> Devices { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserGroup> UserGroups { get; set; }

        public Task<int> SaveChangeAsync(CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<bool> SaveEntityAsync(CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseMySql("Server=localhost;User ID=root;Password=Pa$$w0rd;Database=Minos",
            //    options =>
            //    options.CharSet(CharSet.Utf8)
            //    .CharSetBehavior(CharSetBehavior.AppendToAllAnsiColumns));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {


        }
    }
}
