using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyWebAPI.Data;
using MyWebAPI.Models;
using MyWebAPI.Services;

namespace MyWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IHanghoaRepository _hanghoaRepository;

        public ProductController(IHanghoaRepository hanghoaRepository) {
            _hanghoaRepository = hanghoaRepository;
        }

        [HttpGet]
        public IActionResult GetAll(string? search, double? from, double? to,string? sortBy, int page = 1)
        {
            try
            {
                return Ok(_hanghoaRepository.GetAll(search!,from,to,sortBy,page));
            }
            catch
            {
                return BadRequest("We can't get the product");
            }
        }
        [HttpGet("{id}")]
        public IActionResult Get(string id)
        {
            try
            {
                var data = _hanghoaRepository.GetById(id);
                if (data != null)
                {
                    return Ok(data);
                }
                return NotFound();
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        [HttpPost]
        public IActionResult Create(HanghoaMD model)
        {
            try
            {
                return Ok(_hanghoaRepository.Add(model));
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        [HttpPut("{id}")]
        public IActionResult Update(string id, HanghoaMD model)
        {
            try
            {
                _hanghoaRepository.Update(id, model);
                return NoContent();
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpDelete]
        public IActionResult Delete(string id)
        {
            try
            {
                _hanghoaRepository.Delete(id);
                return Ok();
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
