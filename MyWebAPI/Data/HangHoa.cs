using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyWebAPI.Data
{
    [Table("HangHoa")]
    public class HangHoa
    {
        [Key]
        public Guid MaHH { get; set; }

        [Required]
        [MaxLength(100)]
        public string? TenHH { get; set; }

        public string? Mota { get; set; }

        [Range(0, double.MaxValue)]
        public double DonGia { get; set; }

        public byte GiamGia { get; set; }

        public int? MaLoai { set; get; }

        [ForeignKey("MaLoai")]
        public Loai? Loai { set; get; }

        public ICollection<DonHangChiTiet> donHangChiTiets { get; set; }
        public HangHoa()
        {
            donHangChiTiets = new List<DonHangChiTiet>();
        }
    }
}
