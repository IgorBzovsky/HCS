using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace HCS.Data
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<HcsDbContext>
    {
        public HcsDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<HcsDbContext>();
            builder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=hcsdb;Trusted_Connection=True;MultipleActiveResultSets=true",
                optionsBuilder => optionsBuilder.MigrationsAssembly(typeof(HcsDbContext)
                .GetTypeInfo().Assembly.GetName().Name));
            return new HcsDbContext(builder.Options);
        }
    }
}
