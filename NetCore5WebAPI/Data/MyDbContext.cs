using Microsoft.EntityFrameworkCore;

namespace NetCore5WebAPI.Data
{
    public class MyDbContext : DbContext
    {
        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options) { }

        public DbSet<HangHoa> HangHoas { get; set; }
        public DbSet<Loai> Loais { get; set; }
        public DbSet<DonHang> DonHangs { get; set; }
        public DbSet<ChiTietDonHang> ChiTietDonHangs { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DonHang>(entity =>
            {
                entity.ToTable("DonHang");
                entity.HasKey(dh => dh.MaDH);
            });

            modelBuilder.Entity<ChiTietDonHang>(entity =>
            {
                entity.ToTable("ChiTietDonHang");
                entity.HasKey(ctdh => new { ctdh.MaDH, ctdh.MaHH });
                entity.HasOne(ctdh => ctdh.HangHoa)
                    .WithMany(hh => hh.ChiTietDonHangs)
                    .HasForeignKey(ctdh => ctdh.MaHH)
                    .HasConstraintName("FK_CTDH_HH");
                entity.HasOne(ctdh => ctdh.DonHang)
                    .WithMany(dh => dh.ChiTietDonHangs)
                    .HasForeignKey(ctdh => ctdh.MaDH)
                    .HasConstraintName("FK_CTDH_DH");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasIndex(u => u.Username).IsUnique();
            });
        }
    }
}
