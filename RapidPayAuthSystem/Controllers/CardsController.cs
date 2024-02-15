using Microsoft.AspNetCore.Mvc;
using RapidPayAuthSystem.Data;
using RapidPayAuthSystem.Services;
using IAuthorizationService = RapidPayAuthSystem.Services.IAuthorizationService;


namespace RapidPayAuthSystem.Controllers
{
    public class CardsController : ControllerBase
    {
        private readonly IRepository<Card> _cardRepository;
        private readonly IAuthorizationService _authorizationService;
        private readonly IPaymentService _paymentService;

        public CardsController(IRepository<Card> cardRepository, IAuthorizationService authorizationService, IPaymentService paymentService)
        {
            _cardRepository = cardRepository;
            _authorizationService = authorizationService;
            _paymentService = paymentService;
        }

        [HttpPost("api/cards/create")]
        public IActionResult CreateCard([FromBody] string cardNumber)
        {
            if (!_authorizationService.Authorize(Request.Headers["Username"], Request.Headers["Password"]))
                return Unauthorized();

            var card = new Card { CardNumber = cardNumber, Balance = 0 };
            _cardRepository.Add(card);
            _cardRepository.SaveChanges();
            return Ok(card);
        }

        [HttpGet("{id}/balance")]
        public async Task<IActionResult> GetCardBalance(int id)
        {
            if (!_authorizationService.Authorize(Request.Headers["Username"], Request.Headers["Password"]))
                return Unauthorized();

            var card = await _cardRepository.GetByIdAsync(id);
            if (card == null)
                return NotFound();

            return Ok(card.Balance);
        }

        [HttpPost("{id}/pay")]
        public async Task<IActionResult> Pay(int id, [FromBody] decimal amount)
        {
            if (!_authorizationService.Authorize(Request.Headers["Username"], Request.Headers["Password"]))
                return Unauthorized();

            //Note: for performance bonus
            var card = await _cardRepository.GetByIdAsync(id);
            if (card == null)
                return NotFound();

            decimal fee = _paymentService.GetCurrentFee();

            // Calculate the total payment after deducting the fee
            decimal totalPayment = amount * (1 - fee);

            // Ensure the total payment doesn't make the balance negative
            if (totalPayment < 0)
            {
                return BadRequest("Payment amount cannot make the balance negative.");
            }

            card.Balance += totalPayment;
            _cardRepository.Update(card);
            _cardRepository.SaveChanges();

            return Ok(card.Balance);
        }
    }
}
