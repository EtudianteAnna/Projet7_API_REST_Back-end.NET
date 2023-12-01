using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using P7CreateRestApi.Data;
using P7CreateRestApi.Domain;

namespace P7CreateRestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RuleNamesController : ControllerBase
    {
        private readonly LocalDbContext _context;

        public RuleNamesController(LocalDbContext context)
        {
            _context = context;
        }

        // GET: api/RuleNames
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RuleName>>> GetRuleNames()
        {
          if (_context.RuleNames == null)
          {
              return NotFound();
          }
            return await _context.RuleNames.ToListAsync();
        }

        // GET: api/RuleNames/5
        [HttpGet("{id}")]
        public async Task<ActionResult<RuleName>> GetRuleName(int id)
        {
          if (_context.RuleNames == null)
          {
              return NotFound();
          }
            var ruleName = await _context.RuleNames.FindAsync(id);

            if (ruleName == null)
            {
                return NotFound();
            }

            return ruleName;
        }

        // PUT: api/RuleNames/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        

        // POST: api/RuleNames
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<RuleName>> PostRuleName(RuleName ruleName)
        {
          if (_context.RuleNames == null)
          {
              return Problem("Entity set 'LocalDbContext.RuleNames'  is null.");
          }
            _context.RuleNames.Add(ruleName);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRuleName", new { id = ruleName.Id }, ruleName);
        }

        // DELETE: api/RuleNames/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRuleName(int id)
        {
            if (_context.RuleNames == null)
            {
                return NotFound();
            }
            var ruleName = await _context.RuleNames.FindAsync(id);
            if (ruleName == null)
            {
                return NotFound();
            }

            _context.RuleNames.Remove(ruleName);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RuleNameExists(int id)
        {
            return (_context.RuleNames?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
