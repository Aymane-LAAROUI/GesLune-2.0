using GesLune.Api.Models;
using GesLune.Api.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace GesLune.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ArticleController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetAll()
        {
            var result = ArticleRepository.GetAll();
            return Ok(result);
        }

        [HttpGet("codes/{articleId}")]
        public IActionResult GetCodes(int articleId)
        {
            var result = ArticleRepository.GetCodes(articleId);
            return Ok(result);
        }

        [HttpGet("etat-stock")]
        public IActionResult GetEtatStock()
        {
            var result = ArticleRepository.GetEtatStock();
            return Ok(result);
        }

        [HttpGet("fiche-stock/{articleId}")]
        public IActionResult GetFicheStock(int articleId)
        {
            var result = ArticleRepository.GetFicheStock(articleId);
            return Ok(result);
        }

        [HttpPost]
        public IActionResult Enregistrer([FromBody] Model_Article model)
        {
            var result = ArticleRepository.Enregistrer(model);
            return Ok(result);
        }

        [HttpPost("add-code")]
        public IActionResult AddCode([FromQuery] int articleId, [FromQuery] string articleCode)
        {
            var result = ArticleRepository.AddCode(articleId, articleCode);
            return Ok(result);
        }
    }
}
