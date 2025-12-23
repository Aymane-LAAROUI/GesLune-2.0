using GesLune.Api.Models;
using GesLune.Api.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace GesLune.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PaiementController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetAll()
        {
            var result = PaiementRepository.GetAll();
            return Ok(result);
        }

        [HttpGet("types")]
        public IActionResult GetTypes()
        {
            var result = PaiementRepository.GetTypes();
            return Ok(result);
        }

        [HttpPost]
        public IActionResult Enregistrer([FromBody] Model_Paiement model)
        {
            var result = PaiementRepository.Enregistrer(model);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var result = PaiementRepository.Delete(id);
            return Ok(result);
        }
    }
}
