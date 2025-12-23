using GesLune.Api.Models;
using GesLune.Api.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace GesLune.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VilleController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetAll()
        {
            var result = VilleRepository.GetAll();
            return Ok(result);
        }
    }
}
