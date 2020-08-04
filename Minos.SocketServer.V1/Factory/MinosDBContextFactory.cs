using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Minos.Engine.Contexts;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using Pomelo.EntityFrameworkCore.MySql.Storage;
using System;
using System.Collections.Generic;
using System.Text;

namespace Minos.SocketServer.V1.Factory
{
    class MinosDBContextFactory : IDesignTimeDbContextFactory<MinosContext>
    {
        private static string _connectionString;

        public MinosContext CreateDbContext()
        {
            return CreateDbContext(null);
        }

        public MinosContext CreateDbContext(string[] args)
        {
            if (string.IsNullOrEmpty(_connectionString))
            {
                LoadConnectionString();
            }

            var builder = new DbContextOptionsBuilder<MinosContext>();
            builder.UseMySql(_connectionString,
                   optionsBuilder =>
                   {
                       optionsBuilder.CharSet(CharSet.Utf8).CharSetBehavior(CharSetBehavior.AppendToAllAnsiColumns);
                   }
               );

            return new MinosContext(builder.Options);
        }

        private static void LoadConnectionString()
        {
            var builder = new ConfigurationBuilder();
            builder.AddJsonFile("appsettings.json", optional: false);

            var configuration = builder.Build();

            _connectionString = configuration.GetConnectionString("SqlConnection");
        }
    }
}
