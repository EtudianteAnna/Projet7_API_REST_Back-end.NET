using Dot.Net.WebApi.Models;
using Dot.Net.WebApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace Dot.Net.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CurveController : ControllerBase
    {
        private readonly CurvePointService _curvePointService;

        public CurveController(CurvePointService curvePointService)
        {
            _curvePointService = curvePointService;
        }

        [HttpGet]
        [Route("list")]
        public IActionResult Home()
        {
            var curvePoints = _curvePointService.GetAllCurvePoints();
            return Ok(curvePoints);
        }

        [HttpPost]
        [Route("add")]
        public IActionResult AddCurvePoint([FromBody] CurvePoint curvePoint)
        {
            if (ModelState.IsValid)
            {
                var savedCurvePoint = _curvePointService.AddCurvePoint(curvePoint);
                return Ok(savedCurvePoint);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        // ... Les autres actions du contrôleur (validate, update, delete, etc.)
    }
}
