using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Dot.Net.WebApi.Domain;
using P7CreateRestApi.Repositories;

namespace P7CreateRestApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TradeController : ControllerBase
    {
        private readonly ITradeRepository _tradeRepository;
        private readonly ILogger<TradeController> _logger;
        private object? trade;

        public TradeController(ILogger<TradeController> logger, Repositories.ITradeRepository tradeRepository)
        {
            _logger = logger;
            _tradeRepository = tradeRepository;
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Admin, RH, User")]
        public async Task<IActionResult> Get(int id)
        {
            _logger.LogInformation($"Récupération de la transaction avec l'ID : {id}");

             await _tradeRepository.GetByIdAsync(id);
            
            {
                _logger.LogWarning($"Transaction avec l'ID {id} non trouvée");
                return NotFound();
            }
           
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Trade trade)
        {
            _logger.LogInformation("Ajout d'une nouvelle transaction");

            await _tradeRepository.AddAsync(trade);

            _logger.LogInformation($"Transaction ajoutée avec succès. ID de la transaction : {trade.TradeId}");

            return CreatedAtAction(nameof(Get), new { id = trade.TradeId }, trade);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin, RH")]
        public async Task<IActionResult> Put(int id, [FromBody] Trade trade)
        {
            _logger.LogInformation($"Mise à jour de la transaction avec l'ID : {id}");

            if (id != trade.TradeId)
            {
                _logger.LogError("Incompatibilité dans les ID de transaction. Requête incorrecte.");
                return BadRequest();
            }

            await _tradeRepository.UpdateAsync(trade);

            _logger.LogInformation($"Transaction avec l'ID {id} mise à jour avec succès");
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin, RH")]
        public async Task<IActionResult> Delete(int id)
        {
            _logger.LogInformation($"Suppression de la transaction avec l'ID : {id}");

            await _tradeRepository.DeleteAsync(id);

            _logger.LogInformation($"Transaction avec l'ID {id} supprimée avec succès");
            return NoContent();
        }
    }
}
