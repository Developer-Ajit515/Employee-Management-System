using ERP.APPLICATIONBUSINESS.Interfaces;
using ERP.APPLICATIONBUSINESS.Services;
using ERP.DOMAINMODELS.Dtos;
using ERP.DOMAINMODELS.Dtos.Payment;
using Microsoft.AspNetCore.Mvc;

namespace ERP.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService _service;
        private readonly IStripePaymentService _stripePaymentService;

        public PaymentController(
             IPaymentService paymentService,
             IStripePaymentService stripePaymentService)
        {
            _service = paymentService;
            _stripePaymentService = stripePaymentService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var payments = await _service.GetAllAsync();

            return Ok(payments);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var payment = await _service.GetByIdAsync(id);

            if (payment == null)
            {
                return NotFound(new
                {
                    message = "Payment not found"
                });
            }

            return Ok(payment);
        }

        [HttpPost]
        public async Task<IActionResult> Create(
            [FromBody] CreatePaymentDto dto)
        {
            try
            {
                var payment =
                    await _service.CreateAsync(dto);

                return CreatedAtAction(
                    nameof(GetById),
                    new { id = payment.PaymentId },
                    payment);
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    message = ex.Message
                });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(
            int id,
            [FromBody] CreatePaymentDto dto)
        {
            try
            {
                var payment =
                    await _service.UpdateAsync(id, dto);

                if (payment == null)
                {
                    return NotFound(new
                    {
                        message = "Payment not found"
                    });
                }

                return Ok(payment);
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    message = ex.Message
                });
            }
        }



        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result =
                await _service.DeleteAsync(id);

            if (!result)
            {
                return NotFound(new
                {
                    message = "Payment not found"
                });
            }

            return Ok(new
            {
                message = "Payment deleted successfully"
            });
        }

        [HttpPost("stripe/create-checkout-session")]
        public async Task<IActionResult>
            CreateStripeCheckoutSession(
                [FromBody] CreateStripeCheckoutDto dto)
        {
            try
            {
                var result =
                    await _stripePaymentService
                        .CreateCheckoutSessionAsync(
                            dto.InvoiceId);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    message = ex.Message
                });
            }
        }

        [HttpPost("stripe/webhook")]
        [IgnoreAntiforgeryToken]
        public async Task<IActionResult> StripeWebhook()
        {
            using var reader =
                new StreamReader(
                    Request.Body);

            var json =
                await reader.ReadToEndAsync();

            var signature =
                Request.Headers["Stripe-Signature"]
                    .FirstOrDefault();

            if (string.IsNullOrWhiteSpace(signature))
            {
                return BadRequest(
                    "Missing Stripe signature.");
            }

            try
            {
                await _stripePaymentService
                    .HandleWebhookAsync(
                        json,
                        signature);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    message = ex.Message
                });
            }
        }
    }
}