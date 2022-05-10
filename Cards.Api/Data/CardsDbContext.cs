using Cards.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Cards.Api.Data
{

    public class CardsDbContext : DbContext
    {
        public CardsDbContext(DbContextOptions<CardsDbContext> options):base(options)
        {

        }
        //DbSet
        public DbSet<Card> Cards { get; set; }

    }
}
