using Microsoft.EntityFrameworkCore;
using tic_tac_toe_api.Models;

namespace tic_tac_toe_api
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions options): base(options)
        {

        }

        public DbSet<Game> Games { get; set; }
        public DbSet<MoveData> MovesData { get; set; }
    }
}
