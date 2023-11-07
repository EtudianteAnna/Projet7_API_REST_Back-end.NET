using Dot.Net.WebApi.Domain;
using Microsoft.AspNetCore.Mvc;

namespace P7CreateRestApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BidListController : ControllerBase
    {
        private readonly Dot.Net.WebApi.Repositories.IUserRepository.UserRepository _userRepository;
        private string userName;

        public BidListController(Dot.Net.WebApi.Repositories.IUserRepository.UserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpGet]
        [Route("validate")]
        public IActionResult Validate([FromBody] BidList bidList)
        {
            // TODO: check data valid and save to db, after saving return bid list
            // Your validation and saving logic here

            return Ok();
        }

        [HttpGet]
        [Route("update/{id}")]
        public IActionResult ShowUpdateForm(int id)
        {
            // TODO: Implement logic to retrieve a bid list for updating
            // You may need to call a service method to retrieve the bid list by ID

            var bidList = _userRepository.FindByUserName(userName);

            if (bidList == null)
            {
                // Return a 404 Not Found if the bid list is not found
                return NotFound();
            }

            return Ok(bidList);
        }

        [HttpPost]
        [Route("update/{id}")]
        public IActionResult UpdateBid(int id, [FromBody] BidList bidList)
        {
            // TODO: check required fields, if valid call service to update Bid and return list Bid
            // Your validation and updating logic here

            return Ok();
        }

        [HttpDelete]
        [Route("{id}")]
        public IActionResult UpdateCurvePoint([FromBody] CurvePoint curvePoint)
        {
            // TODO: check required fields, if valid call service to update Curve and return Curve list
            return Ok();
        }

    }
}

