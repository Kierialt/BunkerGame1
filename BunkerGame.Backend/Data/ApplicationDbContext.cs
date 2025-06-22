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
        public DbSet<GameRoom> GameRooms { get; set; } = null!;
        public DbSet<RoomPlayer> RoomPlayers { get; set; } = null!;
        public DbSet<VotingRound> VotingRounds { get; set; } = null!;
        public DbSet<Vote> Votes { get; set; } = null!;
        
        
    }

