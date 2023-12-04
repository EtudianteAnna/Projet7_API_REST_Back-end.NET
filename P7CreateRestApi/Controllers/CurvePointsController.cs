using Microsoft.AspNetCore.Mvc;
using P7CreateRestApi.Data;
using P7CreateRestApi.Domain;
using P7CreateRestApi.Repositories;


namespace P7CreateRestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CurvePointsController : ControllerBase
    {
        private readonly LocalDbContext _context;
        private readonly ICurvePointRepository _curvePointRepository;

        public CurvePointsController(LocalDbContext context, ICurvePointRepository curvePointRepository)
        {
            _context = context;
            _curvePointRepository = curvePointRepository;
        }               

        [HttpGet("{id}")]
        public async Task<ActionResult<CurvePoints>> GetCurvePoint(int id)
        {
            if (_context.CurvePoints == null)
            {
                return NotFound();
            }

            var curvePoint = await _curvePointRepository.GetByIdAsync(id);

            if (curvePoint == null)
            {
                return NotFound();
            }

            return curvePoint;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutCurvePoint(int id, CurvePoints curvePoint)
        {
            if (id != curvePoint.Id)
            {
                return BadRequest();
            }

            await _curvePointRepository.UpdateAsync(curvePoint);

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<CurvePoints>> PostCurvePoint(CurvePoints curvePoint)
        {
            if (_context.CurvePoints == null)
            {
                return Problem("Entity set 'LocalDbContext.CurvePoints' is null.");
            }

            await _curvePointRepository.AddAsync(curvePoint);

            return CreatedAtAction("GetCurvePoint", new { id = curvePoint.Id }, curvePoint);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCurvePoint(int id)
        {
            if (_context.CurvePoints == null)
            {
                return NotFound();
            }

            var curvePoint = await _curvePointRepository.GetByIdAsync(id);

            if (curvePoint == null)
            {
                return NotFound();
            }

            await _curvePointRepository.DeleteAsync(id);

            return NoContent();
        }

       
    }
}

