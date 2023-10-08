using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NetCore5WebAPI.Models;
using NetCore5WebAPI.Repositories;

namespace NetCore5WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoaisController : ControllerBase
    {
        private readonly ILoaiRepository _loaiRepository;

        public LoaisController(ILoaiRepository loaiRepository)
        {
            _loaiRepository = loaiRepository;
        }

        [HttpGet]
        public IActionResult GetAll(string? search, string? sort, int page = 1)
        {
            try
            {
                return Ok(_loaiRepository.GetAll(search, sort, page));
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            try
            {
                var loai = _loaiRepository.GetById(id);
                if (loai == null)
                {
                    return NotFound();
                }
                return Ok(loai);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost]
        [Authorize]
        public IActionResult Create(LoaiModel model)
        {
            try
            {
                var loai = _loaiRepository.Create(model);
                return StatusCode(StatusCodes.Status201Created, loai);
            }
            catch
            {
                //return BadRequest();
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, LoaiVM loaiModel)
        {
            if(id != loaiModel.MaLoai)
            {
                return BadRequest();
            }

            try
            {
                var loai = _loaiRepository.Update(loaiModel);
                if (loai == null)
                {
                    return NotFound();
                }
                return Ok(loai);
            }
            catch
            {
                //return BadRequest();
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                if (_loaiRepository.Delete(id))
                {
                    return Ok();
                }
                else
                {
                    return NotFound();
                }
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
