using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Db
{
    public partial class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public virtual DbSet<Account> Accounts { get; set; }
        public virtual DbSet<RoomBill> RoomBills { get; set; }
        public virtual DbSet<HotelRoom> HotelRooms { get; set; }
    }
}
