using BunkerGame.Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace BunkerGame.Backend.Data;

    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        
        public DbSet<User> Users { get; set; } = null!;
        public DbSet<Player> Players { get; set; } = null!;
        
        
    }

