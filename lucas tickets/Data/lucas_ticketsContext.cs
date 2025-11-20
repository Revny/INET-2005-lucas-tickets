using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using lucas_tickets.Models;

namespace lucas_tickets.Data
{
    public class lucas_ticketsContext : DbContext
    {
        public lucas_ticketsContext (DbContextOptions<lucas_ticketsContext> options)
            : base(options)
        {
        }

        public DbSet<lucas_tickets.Models.Shows> Shows { get; set; } = default!;
        public DbSet<lucas_tickets.Models.Category> Category { get; set; } = default!;
    }
}
