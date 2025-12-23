using GesLune.Api.Models;
using GesLune.Api.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace GesLune.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategorieController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetAll()
        {
            var result = CategorieRepository.GetAll();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var result = CategorieRepository.GetById(id);
            return Ok(result);
        }

        [HttpPost]
        public IActionResult Enregistrer([FromBody] Model_Categorie model)
        {
            var result = CategorieRepository.Enregistrer(model);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var result = CategorieRepository.Delete(id);
            return Ok(result);
        }
    }
}
