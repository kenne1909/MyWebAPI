using Microsoft.EntityFrameworkCore;

namespace MyWebAPI.Data
{
    public class MyDbContext : DbContext
    {
        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options) { }

        //DBset
        #region DbSet
        public DbSet<HangHoa> hangHoas { get; set; }
        public DbSet<Loai> loais { get; set; }
        public DbSet<DonHang> donHangs { get; set; }
        public DbSet<DonHangChiTiet> donHangsChiTiet { get; set; }
        public DbSet<NguoiDung> NguoiDungs { get; set; }
        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<DonHang>(e =>
            {
                e.ToTable("DonHang");
                e.HasKey(dh => dh.MaDh);
                e.Property(dh => dh.NgayDat).HasDefaultValueSql("getutcdate()");
            });
            modelBuilder.Entity<DonHangChiTiet>(e => {
                e.ToTable("ChiTietDonHang");
                e.HasKey(e => new
                {
                    e.MaDh,
                    e.MaHH
                });
                e.HasOne(e => e.DonHang)
                .WithMany(e => e.donHangChiTiets)
                .HasForeignKey(e => e.MaDh);

                e.HasOne(e => e.HangHoa)
                    .WithMany(e => e.donHangChiTiets)
                    .HasForeignKey(e => e.MaHH);
            });
            modelBuilder.Entity<NguoiDung>(e =>
            {
                e.HasIndex(e => e.UserName).IsUnique();
                e.Property(e => e.HoTen).IsRequired().HasMaxLength(150);
            });
        }
    }
}