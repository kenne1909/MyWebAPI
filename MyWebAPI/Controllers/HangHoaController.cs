using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HangHoaController : ControllerBase
    {
        public List<HangHoa> hangHoas { get; set; } = new List<HangHoa>();

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(hangHoas);
        }

        [HttpGet("{id}")]
        public IActionResult GetByid(string id)
        {
            try
            {
                var hanghoa = (from h in hangHoas
                               where h.MaHangHoa == Guid.Parse(id)
                               select h).FirstOrDefault();
                if (hanghoa == null)
                    return NotFound();
                return Ok(hanghoa);
            }
            catch
            {
                return BadRequest();
            }
        }


        [HttpPost]
        public IActionResult Create(HangHoaVM hangHoaVM)
        {
            var hanghoa= new HangHoa()
            {
                MaHangHoa= Guid.NewGuid(),
                TenHangHoa=hangHoaVM.TenHangHoa,
                DonGia=hangHoaVM.DonGia,
            };
            hangHoas.Add(hanghoa);
            return Ok(new
            {
                Success=true,
                Data = hanghoa
            });
        }

        [HttpPut("{id}")]
        public IActionResult Edit(string id,HangHoa hangHoaEdit)
        {
            try
            {
                var hanghoa = (from h in hangHoas
                               where h.MaHangHoa == Guid.Parse(id)
                               select h).FirstOrDefault();
                if (hanghoa == null)
                    return NotFound();
                if (id != hanghoa.MaHangHoa.ToString())            
                    return BadRequest();
                //update
                hanghoa.TenHangHoa = hangHoaEdit.TenHangHoa;
                hanghoa.DonGia = hangHoaEdit.DonGia;
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Remove(string id)
        {
            try
            {
                //tìm id
                var hanghoa = hangHoas.SingleOrDefault(hh => hh.MaHangHoa == Guid.Parse(id));
                if (hanghoa == null)
                {
                    return NotFound();
                }
                //update
                hangHoas.Remove(hanghoa);
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }

    }
}
