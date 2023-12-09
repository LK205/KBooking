using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Db
{
    public partial class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext()
        {
        }

        
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public virtual DbSet<Account> Accounts { get; set; }
        public virtual DbSet<RoomBill> RoomBills { get; set; }
        public virtual DbSet<HotelRoom> HotelRooms { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=LMK205\\SQLEXPRESS; Database=KBOOKING; User Id=sa;Password=Leminhkhoi2003;TrustServerCertificate=True;");
            }
        }

    }
}
