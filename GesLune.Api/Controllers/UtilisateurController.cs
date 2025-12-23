using GesLune.Api.Models;
using GesLune.Api.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace GesLune.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UtilisateurController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetAll()
        {
            var result = UtilisateurRepository.GetAll();
            return Ok(result);
        }

        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody] Model_Utilisateur model)
        {
            var user = UtilisateurRepository.Authenticate(model.Utilisateur_Login, model.Utilisateur_Password);
            if (user == null) return Unauthorized();
            return Ok(user);
        }

        [HttpPost]
        public IActionResult Enregistrer([FromBody] Model_Utilisateur model)
        {
            var result = UtilisateurRepository.Enregistrer(model);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var result = UtilisateurRepository.Delete(id);
            return Ok(result);
        }
    }
}
