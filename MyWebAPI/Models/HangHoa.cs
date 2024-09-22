namespace WebAPI.Models
{
    public class HangHoaVM
    {
        public string? TenHangHoa {  get; set; }
        public double DonGia { set; get; }
    }
    public class HangHoa : HangHoaVM 
    { 
        public Guid MaHangHoa {  set; get; }
    }
}
