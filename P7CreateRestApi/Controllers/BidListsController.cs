﻿
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using P7CreateRestApi.Domain;
using P7CreateRestApi.Repositories;

namespace Dot.Net.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BidListsController : ControllerBase
    {
        private readonly IBidListRepository _bidListRepository;
        private readonly ILogger<BidListsController> _logger;

        public BidListsController(ILogger<BidListsController> logger, IBidListRepository bidListRepository)
        {
            _logger = logger;
            _bidListRepository = bidListRepository;
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Admin, RH, User")]
        [ProducesResponseType(StatusCodes.Status200OK)] // OK
        [ProducesResponseType(StatusCodes.Status404NotFound)] // Not Found
        public async Task<IActionResult> Get(int id)
        {
            _logger.LogInformation($"Récupération de la liste d'offres avec l'ID : {id}");

            var bidList = await _bidListRepository.GetByIdAsync(id);
            if (bidList == null)
            {
                _logger.LogWarning($"Liste d'offres avec l'ID {id} non trouvée");
                return NotFound();
            }

            _logger.LogInformation($"Liste d'offres avec l'ID {id} récupérée avec succès");
            return Ok(bidList);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)] // Created
        [ProducesResponseType(StatusCodes.Status400BadRequest)] // Bad Request
        public async Task<IActionResult> Post([FromBody] BidList bidList)
        {
            try
            {
                _logger.LogInformation("Ajout d'une nouvelle liste d'offres");

               if (!ModelState.IsValid)
                {
                    _logger.LogError("Modèle invalideb lors de l'ajout de la liste d'offres");
                    return BadRequest(ModelState);
                
                }
                await _bidListRepository.AddAsync(bidList);

                _logger.LogInformation($"Liste d'offres ajoutée avec succès. ID de la liste d'offres : {bidList.BidListId}");

                return CreatedAtAction(nameof(Get), new { id = bidList.BidListId }, bidList);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Erreur lors de l'ajout de la liste d'offres : {ex.Message}");
                return BadRequest();
            }
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin, RH")]
        [ProducesResponseType(StatusCodes.Status204NoContent)] // No Content
        [ProducesResponseType(StatusCodes.Status400BadRequest)] // Bad Request
        public async Task<IActionResult> Put(int id, [FromBody] BidList bidList)
        {
            _logger.LogInformation($"Mise à jour de la liste d'offres avec l'ID : {id}");

            if (id != bidList.BidListId)
            {
                _logger.LogError("Incompatibilité dans les ID de liste d'offres. Requête incorrecte.");
                return BadRequest();
            }

            await _bidListRepository.UpdateAsync(bidList);

            _logger.LogInformation($"Liste d'offres avec l'ID {id} mise à jour avec succès");
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin, RH")]
        [ProducesResponseType(StatusCodes.Status204NoContent)] // No Content
        [ProducesResponseType(StatusCodes.Status404NotFound)] // Not Found
        public async Task<IActionResult> Delete(int id)
        {
            _logger.LogInformation($"Tentative d'élimination � {DateTime.Now} de la liste d'offres avec l'ID {id}");

            await _bidListRepository.DeleteAsync(id);

            _logger.LogInformation($"Liste d'offres avec l'ID {id} supprimée avec succès");
            return NoContent();
        }
    }
}

