using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using P7CreateRestApi.Domain;
using P7CreateRestApi.Repositories;



[ApiController]
[Route("api/[controller]")]
public class TradeController : ControllerBase
{
    private readonly ITradeRepository _tradeRepository;
    private readonly ILogger<TradeController> _logger;

    public TradeController(ILogger<TradeController> logger, ITradeRepository tradeRepository)
    {
        _logger = logger;
        _tradeRepository = tradeRepository;
    }

    [HttpGet("{id}")]
    [Authorize(Roles = "Admin, RH, User")]
    [ProducesResponseType(StatusCodes.Status200OK)] // OK
    [ProducesResponseType(StatusCodes.Status404NotFound)] // Not Found
    public async Task<IActionResult> Get(int id)
    {
        _logger.LogInformation($"R�cup�ration de la transaction avec l'ID : {id}");

        var trade = await _tradeRepository.GetByIdAsync(id);
        if (trade == null)
        {
            _logger.LogWarning($"Transaction avec l'ID {id} non trouv�e");
            return NotFound();
        }

        _logger.LogInformation($"Transaction avec l'ID {id} r�cup�r�e avec succ�s");
        return Ok(trade);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)] // Created
    [ProducesResponseType(StatusCodes.Status400BadRequest)] // Bad Request
    public async Task<IActionResult> Post([FromBody] Trade trade)
    {
        _logger.LogInformation("Ajout d'une nouvelle transaction");

        await _tradeRepository.AddAsync(trade);

        _logger.LogInformation($"Transaction ajout�e avec succ�s. ID de la transaction : {trade.TradeId}");

        return CreatedAtAction(nameof(Get), new { id = trade.TradeId }, trade);
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "Admin, RH")]
    [ProducesResponseType(StatusCodes.Status204NoContent)] // No Content
    [ProducesResponseType(StatusCodes.Status400BadRequest)] // Bad Request
    public async Task<IActionResult> Put(int id, [FromBody] Trade trade)
    {
        _logger.LogInformation($"Mise � jour de la transaction avec l'ID : {id}");

        if (id != trade.TradeId)
        {
            _logger.LogError("Incompatibilit� dans les ID de transaction. Requ�te incorrecte.");
            return BadRequest();
        }

        await _tradeRepository.UpdateAsync(trade);

        _logger.LogInformation($"Transaction avec l'ID {id} mise � jour avec succ�s");
        return NoContent();
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin, RH")]
    [ProducesResponseType(StatusCodes.Status204NoContent)] // No Content
    [ProducesResponseType(StatusCodes.Status404NotFound)] // Not Found
    public async Task<IActionResult> Delete(int id)
    {
        _logger.LogInformation($"Suppression de la transaction avec l'ID : {id}");

        await _tradeRepository.DeleteAsync(id);

        _logger.LogInformation($"Transaction avec l'ID {id} supprim�e avec succ�s");
        return NoContent();
    }
}