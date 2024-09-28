using Microsoft.AspNetCore.Authorization;
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
            try
            {
                var dsLoai = _context.loais.ToList();
                return Ok(dsLoai);
            }
            catch
            {
                return BadRequest();
            }
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
        [Authorize]
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
                //return Ok(loai);
                return StatusCode(StatusCodes.Status201Created, loai);
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
            return NoContent();//204
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id, LoaiModel model)
        {
            var loai = _context.loais.FirstOrDefault(l => l.MaLoai == id);

            if (loai != null)
            {
                _context.Remove(loai);
                _context.SaveChanges();
                return StatusCode(StatusCodes.Status200OK);
            }
            else
            {
                return NotFound();
            }    
        }

    }
}
