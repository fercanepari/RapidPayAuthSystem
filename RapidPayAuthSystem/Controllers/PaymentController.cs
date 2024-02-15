using Microsoft.AspNetCore.Mvc;
using RapidPayAuthSystem.Services;

namespace RapidPayAuthSystem.Controllers
{
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService _paymentService;

        public PaymentController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        [HttpGet("GetCurrentFee")]
        public IActionResult GetCurrentFee()
        {
            var fee = _paymentService.GetCurrentFee();
            return Ok(fee);
        }
    }
}
