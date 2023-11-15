using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using P7CreateRestApi.Data;
using P7CreateRestApi.Domain;

namespace P7CreateRestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BidListsController : ControllerBase
    {
        private readonly LocalDbContext _context;

        public BidListsController(LocalDbContext context)
        {
            _context = context;
        }

        // GET: api/BidLists
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BidList>>> GetBidLists()
        {
          if (_context.BidLists == null)
          {
              return NotFound();
          }
            return await _context.BidLists.ToListAsync();
        }

        // GET: api/BidLists/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BidList>> GetBidList(int id)
        {
          if (_context.BidLists == null)
          {
              return NotFound();
          }
            var bidList = await _context.BidLists.FindAsync(id);

            if (bidList == null)
            {
                return NotFound();
            }

            return bidList;
        }

        // PUT: api/BidLists/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBidList(int id, BidList bidList)
        {
            if (id != bidList.BidListId)
            {
                return BadRequest();
            }

            _context.Entry(bidList).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BidListExists(id))
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

        // POST: api/BidLists
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<BidList>> PostBidList(BidList bidList)
        {
          if (_context.BidLists == null)
          {
              return Problem("Entity set 'LocalDbContext.BidLists'  is null.");
          }
            _context.BidLists.Add(bidList);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBidList", new { id = bidList.BidListId }, bidList);
        }

        // DELETE: api/BidLists/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBidList(int id)
        {
            if (_context.BidLists == null)
            {
                return NotFound();
            }
            var bidList = await _context.BidLists.FindAsync(id);
            if (bidList == null)
            {
                return NotFound();
            }

            _context.BidLists.Remove(bidList);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BidListExists(int id)
        {
            return (_context.BidLists?.Any(e => e.BidListId == id)).GetValueOrDefault();
        }
    }
}
