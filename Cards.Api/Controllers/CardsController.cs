using Cards.Api.Data;
using Cards.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Cards.Api.Controllers
{
    [ApiController]
    [Route("api/conntroller")]
    public class CardsController : ControllerBase
    {
        private readonly CardsDbContext _context;
        public CardsController(CardsDbContext context)
        {
            _context= context;
        }
        [HttpGet]
        [Route("getall")]
        public async Task<IActionResult> GetAllCards()
        {
            var cards=_context.Cards.ToList();
            return Ok(cards);
        }
        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetCard([FromRoute] Guid id)
        {
            var cards = _context.Cards.FirstOrDefault(x=>x.Id==id);
            if (cards != null)
            {
                return Ok(cards);
            }
            return NotFound("Card Not Found");
        }
        [HttpPost]
        public async Task<IActionResult> AddCard([FromBody] Card card)
        {
            card.Id = Guid.NewGuid();
            await _context.Cards.AddAsync(card);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetCard), new {id= card.Id }, card);
        }

        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> UpdateCard([FromRoute] Guid id,[FromBody] Card? card)
        {
            var existingCard = _context.Cards.FirstOrDefault(x => x.Id == id);
            if(existingCard != null)
            {
                existingCard.CardHolderName = card.CardHolderName;
                existingCard.CardNumber = card.CardNumber;
                existingCard.ExpiryMonth = card.ExpiryMonth;
                existingCard.ExpiryYear = card.ExpiryYear;
                existingCard.CVC = card.CVC;
                    
                await _context.SaveChangesAsync();
                return Ok(existingCard);
            }
            return NotFound("Card Not Found");
        }
        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> DeleteCard([FromRoute] Guid id)
        {
            var existingCard = _context.Cards.FirstOrDefault(x => x.Id == id);
            if (existingCard != null)
            {
                _context.Remove(existingCard);
                await _context.SaveChangesAsync();
                return Ok(existingCard);
            }
            return NotFound("Card Not Found");
        }

    }
}
