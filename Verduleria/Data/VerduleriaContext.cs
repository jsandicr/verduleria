using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Verduleria.Models;

namespace Verduleria.Data
{
    public class VerduleriaContext : DbContext
    {
        public VerduleriaContext (DbContextOptions<VerduleriaContext> options)
            : base(options)
        {
        }

        public DbSet<Verduleria.Models.Promocion> Promocion { get; set; } = default!;

        public DbSet<Verduleria.Models.Producto>? Producto { get; set; }
    }
}
