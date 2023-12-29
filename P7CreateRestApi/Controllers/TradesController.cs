using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using P7CreateRestApi.Domain;
using P7CreateRestApi.Repositories;
using System.ComponentModel.DataAnnotations;
using System.Data.Common;

namespace P7CreateRestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TradesController : ControllerBase
    {
        private readonly LocalDbContext _context;
        private ILogger<TradesController> object1;
        private ITradeRepository object2;
        private LocalDbContext object3;

    public TradeController(ILogger<TradeController> logger, ITradeRepository tradeRepository)
    {
        _logger = logger;
        _tradeRepository = tradeRepository;
    }

        public TradesController(ILogger<TradesController> object1, ITradeRepository object2, LocalDbContext object3)
        {
            this.object1 = object1;
            this.object2 = object2;
            this.object3 = object3;
        }

        public TradesController(LocalDbContext @object)
        {
        }

        // GET: api/Trades
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Trade>>> GetTrades()
        {
          if (_context.Trades == null)
          {
              return NotFound();
          }
            return await _context.Trades.ToListAsync();
        }

        // GET: api/Trades/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Trade>> GetTrade(int id)
        {
          if (_context.Trades == null)
          {
              return NotFound();
          }
            var trade = await _context.Trades.FindAsync(id);

            if (trade == null)
            {
                return NotFound();
            }

            return trade;
        }

        // PUT: api/Trades/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PostTrade(int id, Trade trade)
        {
            if (id != trade.TradeId)
            {
                return BadRequest();
            }

            try
            {
                // Mettez ici la logique pour mettre à jour le Trade dans votre contexte ou autre moyen
                object v = _context.Entry(trade);

                ; await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TradeExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }


        // POST: api/Trades
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Trade>> PostTrade(Trade trade)
        {
          if (_context.Trades == null)
          {
              return Problem("Entity set 'LocalDbContext.Trades'  is null.");
          }
            _context.Trades.Add(trade);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTrade", new { id = trade.TradeId }, trade);
        }

        // DELETE: api/Trades/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTrade(int id)
        {
            if (_context.Trades == null)
            {
                return NotFound();
            }
            var trade = await _context.Trades.FindAsync(id);
            if (trade == null)
            {
                return NotFound();
            }

            _context.Trades.Remove(trade);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TradeExists(int id)
        {
            return (_context.Trades?.Any(e => e.TradeId == id)).GetValueOrDefault();
        }

        public Task PutTrade(int id, Trade trade)
        {
            throw new NotImplementedException();
        }
    }
}
