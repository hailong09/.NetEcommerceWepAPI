using backend.Dtos;
using backend.Extensions;
using backend.Models;
using backend.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Stripe;

namespace backend.Controllers
{


    [ApiController]
    [Route("payment")]
    public class PaymentController : ControllerBase
    {
        public readonly IOptions<StripeOptions> options;
        public readonly IStripeClient client;



        public PaymentController(IOptions<StripeOptions> options, IOrderRepository orderRepository)
        {
            this.options = options;
            this.client = new StripeClient(this.options.Value.SecretKey);

        }



        [Authorize]
        [HttpGet("config")]
        public ConfigResponseDto GetConfig()
        {
            return new ConfigResponseDto
            {
                PublishableKey = this.options.Value.PublishableKey
            };
        }

        [Authorize]
        [HttpPost("create-payment-content")]
        public async Task<ActionResult> CreatePayment([FromBody] CreatePaymentIntenetRequest createPaymentIntenetRequest)
        {
            var options = new PaymentIntentCreateOptions
            {
                Amount = createPaymentIntenetRequest.Amount,
                Currency = "cad",
                PaymentMethodTypes = new List<string>{
                    "card",
                }

            };

            var service = new PaymentIntentService(this.client);
            try
            {
                var paymentIntent = await service.CreateAsync(options);
                return Ok(new CreatePaymentIntentResponseDto { ClientSecret = paymentIntent.ClientSecret });
            }
            catch (Stripe.StripeException e)
            {
                return BadRequest(new
                {
                    Error = new
                    {
                        Message = e.Message
                    }
                });
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }

        [Authorize]
        [HttpPost("webhook")]
        public async Task<IActionResult> Webhook()
        {
            var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();
            Event stripeEvent;
            try
            {
                stripeEvent = EventUtility.ConstructEvent(
                    json,
                    Request.Headers["Stripe-Signature"],
                    this.options.Value.WebhookSecret
                );
                Console.WriteLine($"Webhook notification with type: {stripeEvent.Type} found for {stripeEvent.Id}");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Something failed {e}");
                return BadRequest();
            }


            if (stripeEvent.Type == "payment_intent.created")
            {
                var paymentIntent = stripeEvent.Data.Object as Stripe.PaymentIntent;
                Console.WriteLine($"[{stripeEvent.Id}] PaymentIntent ({paymentIntent.Id}) created!");
                // Take some action based on session.
            }

            return Ok();
        }

    }
}