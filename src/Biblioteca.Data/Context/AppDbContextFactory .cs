using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Text;

namespace Biblioteca.Data.Context
{
    public class AppDbContextFactory : IDesignTimeDbContextFactory<BibliotecaContext>
    {
        public BibliotecaContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<BibliotecaContext>();
            optionsBuilder.UseSqlServer("Server=localhost;Database=Biblioteca;User Id=SA;Password=*UHB*UHB");

            return new BibliotecaContext(optionsBuilder.Options);
        }
    }
}
