using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyWebAPI.Data;
using MyWebAPI.Models;

namespace MyWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoaisController : ControllerBase
    {
        private readonly MyDbContext _context;

        public LoaisController(MyDbContext context) {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var dsLoai = _context.loais.ToList();
            return Ok(dsLoai);
        }

        [HttpGet("{id}")]
        public IActionResult GetByid(int id)
        {
            var loai = _context.loais.FirstOrDefault(l => l.MaLoai == id);

            if (loai == null)
            {
                return NotFound();
            }
            return Ok(loai);
        }

        [HttpPost]
        public IActionResult Create(LoaiModel model)
        {
            try
            {
                var loai = new Loai
                {
                    TenLoai = model.TenLoai,
                };
                _context.Add(loai);
                _context.SaveChanges();
                return Ok(loai);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPut("{id}")]
        public IActionResult Edit(int id,LoaiModel model)
        {
            var loai = _context.loais.FirstOrDefault(l => l.MaLoai == id);

            if (loai == null)
            {
                return NotFound();
            }

            loai.TenLoai=model.TenLoai;
            _context.SaveChanges();
            return NoContent();
        }

    }
}
