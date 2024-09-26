using System.ComponentModel.DataAnnotations;

namespace MyWebAPI.Models
{
    public class HanghoaMD
    {
        [Required]
        [MaxLength(100)]
        public string? TenHH { get; set; }

        public string? Mota { get; set; }

        [Range(0, double.MaxValue)]
        public double DonGia { get; set; }

        public byte GiamGia { get; set; }

        public int? MaLoai { set; get; }
    }
}
