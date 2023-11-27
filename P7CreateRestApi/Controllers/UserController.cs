using Microsoft.AspNetCore.Mvc;
using P7CreateRestApi.Domain;
using P7CreateRestApi.Repositories;

namespace P7CreateRestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IBidListRepository _repository;

        public UserController(IBidListRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<BidList>>> GetUserLists()
        {
            var bidLists = await _repository.GetBidListsAsync();
            if (bidLists == null || !bidLists.Any())
            {
                return NotFound(); // retour erreur 404
            }

            return Ok(bidLists); // retour 200
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BidList>> GetUserList(int id)
        {
            var bidList = await _repository.GetByIdAsync(id);

            if (bidList == null)
            {
                return NotFound();
            }

            return Ok(bidList);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutUserList(int id, BidList bidList)
        {
            if (id != bidList.BidListId)
            {
                return BadRequest();
            }

            await _repository.UpdateAsync(bidList);

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<BidList>> PostUserList(BidList bidList)
        {
            try
            {
                await _repository.AddAsync(bidList);
            }
            catch (Exception)
            {

            }


            return CreatedAtAction(nameof(GetUserList), new { id = bidList.BidListId }, bidList);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteUserList(int id)
        {

            return Ok();
        }
    }
    }




