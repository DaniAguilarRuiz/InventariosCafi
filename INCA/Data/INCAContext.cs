using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using INCA.Models;

namespace INCA.Data
{
    public class INCAContext : DbContext
    {
        public INCAContext (DbContextOptions<INCAContext> options)
            : base(options)
        {
        }

        public DbSet<INCA.Models.Empleados> Empleados { get; set; } = default!;

        public DbSet<INCA.Models.Equipos> Equipos { get; set; } = default!;

        public DbSet<INCA.Models.Mantenimientos> Mantenimientos { get; set; } = default!;

    }
}
