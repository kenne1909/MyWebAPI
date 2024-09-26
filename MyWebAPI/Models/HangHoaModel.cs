using System.ComponentModel.DataAnnotations;

namespace MyWebAPI.Models
{
    public class HangHoaModel
    {
        public Guid MaHH { get; set; }
        public string? TenHH { get; set; }

        public string? Mota { get; set; }

        public double DonGia { get; set; }

        public byte GiamGia { get; set; }
        public string? TenLoai { set; get; }
    }
}
