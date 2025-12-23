using GesLune.Api.Models;
using GesLune.Api.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace GesLune.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DocumentController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetAll()
        {
            var result = DocumentRepository.GetAll();
            return Ok(result);
        }

        [HttpGet("by-type/{typeId}")]
        public IActionResult GetByTypeId(int typeId)
        {
            var result = DocumentRepository.GetByTypeId(typeId);
            return Ok(result);
        }

        [HttpGet("types")]
        public IActionResult GetTypes()
        {
            var result = DocumentRepository.GetTypes();
            return Ok(result);
        }

        [HttpGet("lignes/{documentId}")]
        public IActionResult GetLignes(int documentId)
        {
            var result = DocumentRepository.GetLignes(documentId);
            return Ok(result);
        }

        [HttpGet("chiffre-affaire-mensuel")]
        public IActionResult GetChiffreAffaireMensuel()
        {
            var result = DocumentRepository.GetChiffreAffaireMensuel();
            return Ok(result);
        }

        [HttpPost]
        public IActionResult Enregistrer([FromBody] Model_Document model)
        {
            var result = DocumentRepository.Enregistrer(model);
            return Ok(result);
        }

        [HttpPost("ligne")]
        public IActionResult EnregistrerLigne([FromBody] Model_Document_Ligne ligne)
        {
            var result = DocumentRepository.EnregistrerLigne(ligne);
            return Ok(result);
        }

        [HttpGet("est-encaisse/{documentId}")]
        public IActionResult EstEncaisse(int documentId)
        {
            var result = DocumentRepository.EstEncaisse(documentId);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var result = DocumentRepository.Delete(id);
            return Ok(result);
        }

        [HttpDelete("lignes/{documentId}")]
        public IActionResult DeleteDocumentLignes(int documentId)
        {
            var result = DocumentRepository.DeleteDocumentLignes(documentId);
            return Ok(result);
        }

        [HttpDelete("ligne/{ligneId}")]
        public IActionResult DeleteLigne(int ligneId)
        {
            var result = DocumentRepository.DeleteLigne(ligneId);
            return Ok(result);
        }
    }
}
