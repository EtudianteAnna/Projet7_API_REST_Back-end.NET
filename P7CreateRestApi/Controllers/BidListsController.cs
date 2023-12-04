using Microsoft.AspNetCore.Mvc;
using P7CreateRestApi.Domain;
using P7CreateRestApi.Repositories;

namespace P7CreateRestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BidListsController : ControllerBase
    {
        private readonly IBidListRepository _repository;

        public BidListsController(IBidListRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<BidList>>> GetBidLists()
        {
            var bidLists = await _repository.GetBidListsAsync();
            if (bidLists == null || !bidLists.Any())
            {
                return NotFound(); // retour erreur 404
            }

            return Ok(bidLists); // retour 200
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BidList>> GetBidList(int id)
        {
            var bidList = await _repository.GetByIdAsync(id);

            if (bidList == null)
            {
                return NotFound();
            }

            return Ok(bidList);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutBidList(int id, BidList bidList)
        {
            if (id != bidList.BidListId)
            {
                return BadRequest();
            }

            await _repository.UpdateAsync(bidList);

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<BidList>> PostBidList(BidList bidList)
        {
            try
            {
                await _repository.AddAsync(bidList);
            }
            catch(Exception ex )
            {
                //Log de l'exception 
                Console.WriteLine("Une erreur s'est produite lors de l'ajout:{ex:.Message}");
            }
           

            return CreatedAtAction(nameof(GetBidList), new { id = bidList.BidListId }, bidList);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBidList(int id)
        {
            var bidList = await _repository.GetByIdAsync(id);
            if (bidList == null)
            {
                return NotFound();
            }

            await _repository.DeleteAsync(id);

            return NoContent();
        }
    }
}

