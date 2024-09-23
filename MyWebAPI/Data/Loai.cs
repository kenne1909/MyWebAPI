using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyWebAPI.Data
{
    [Table("Loai")]
    public class Loai
    {
        [Key]
        public int MaLoai {  get; set; }
        [Required]
        [MaxLength(50)]
        public string? TenLoai { set; get; }

        public virtual ICollection<HangHoa>? HangHoas { get; set; }//1 loại có nhìu hàng hóa
    }
}
