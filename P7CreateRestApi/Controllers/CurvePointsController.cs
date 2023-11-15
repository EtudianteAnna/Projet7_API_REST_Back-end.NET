using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using P7CreateRestApi.Data;
using P7CreateRestApi.Domain;

namespace P7CreateRestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CurvePointsController : ControllerBase
    {
        private readonly LocalDbContext _context;

        public CurvePointsController(LocalDbContext context)
        {
            _context = context;
        }

        // GET: api/CurvePoints
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CurvePoint>>> GetCurvePointss()
        {
          if (_context.CurvePointss == null)
          {
              return NotFound();
          }
            return await _context.CurvePointss.ToListAsync();
        }

        // GET: api/CurvePoints/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CurvePoint>> GetCurvePoint(int id)
        {
          if (_context.CurvePointss == null)
          {
              return NotFound();
          }
            var curvePoint = await _context.CurvePointss.FindAsync(id);

            if (curvePoint == null)
            {
                return NotFound();
            }

            return curvePoint;
        }

        // PUT: api/CurvePoints/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCurvePoint(int id, CurvePoint curvePoint)
        {
            if (id != curvePoint.Id)
            {
                return BadRequest();
            }

            _context.Entry(curvePoint).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CurvePointExists(id))
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

        // POST: api/CurvePoints
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CurvePoint>> PostCurvePoint(CurvePoint curvePoint)
        {
          if (_context.CurvePointss == null)
          {
              return Problem("Entity set 'LocalDbContext.CurvePointss'  is null.");
          }
            _context.CurvePointss.Add(curvePoint);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCurvePoint", new { id = curvePoint.Id }, curvePoint);
        }

        // DELETE: api/CurvePoints/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCurvePoint(int id)
        {
            if (_context.CurvePointss == null)
            {
                return NotFound();
            }
            var curvePoint = await _context.CurvePointss.FindAsync(id);
            if (curvePoint == null)
            {
                return NotFound();
            }

            _context.CurvePointss.Remove(curvePoint);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CurvePointExists(int id)
        {
            return (_context.CurvePointss?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
