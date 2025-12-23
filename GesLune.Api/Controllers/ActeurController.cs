using GesLune.Api.Models;
using GesLune.Api.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace GesLune.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ActeurController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetAll()
        {
            var result = ActeurRepository.GetAll();
            return Ok(result);
        }

        [HttpGet("top/{top}")]
        public IActionResult GetAllTop(int top)
        {
            var result = ActeurRepository.GetAll(top);
            return Ok(result);
        }

        [HttpGet("types")]
        public IActionResult GetTypes()
        {
            var result = ActeurRepository.GetTypes();
            return Ok(result);
        }

        [HttpGet("by-type/{typeId}")]
        public IActionResult GetByTypeId(int typeId)
        {
            var result = ActeurRepository.GetByTypeId(typeId);
            return Ok(result);
        }

        [HttpGet("releve/{acteurId}")]
        public IActionResult GetReleve(int acteurId, [FromQuery] DateTime? dateDu = null, [FromQuery] DateTime? dateAu = null)
        {
            var result = ActeurRepository.GetReleve(acteurId, dateDu, dateAu);
            return Ok(result);
        }

        [HttpPost]
        public IActionResult Enregistrer([FromBody] Model_Acteur model)
        {
            var result = ActeurRepository.Enregistrer(model);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var result = ActeurRepository.Delete(id);
            return Ok(result);
        }
    }
}
