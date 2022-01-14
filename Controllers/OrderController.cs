using backend.Dtos;
using backend.Extensions;
using backend.Models;
using backend.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{

    [ApiController]
    [Route("order")]
    public class OrderController : ControllerBase
    {
        public readonly IOrderRepository orderRepository;
        public OrderController(IOrderRepository orderRepository)
        {
            this.orderRepository = orderRepository;
        }


        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<OrderDto>> GetOrder(string id)
        {
            try
            {
                var order = await orderRepository.GetOrder(id);
                if (order is null)
                {
                    return BadRequest("Order is not found!");
                }

                return order.AsDto();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        [Authorize]
        [HttpPost("save-order")]
        public async Task<ActionResult<OrderDto>> saveOrder([FromBody] OrderRequestDto orderRequestDto)
        {
            if (orderRequestDto is null) return BadRequest();
            OrderModels newOrder = new()
            {
                Id = orderRequestDto.Id,
                UserId = orderRequestDto.UserId,
                CartItems = orderRequestDto.CartItems,
                CustomerInfo = orderRequestDto.CustomerInfo,
                Amount = orderRequestDto.Amount,
                Created = DateTime.UtcNow
            };

            await orderRepository.CreateOrder(newOrder);
            return CreatedAtAction(nameof(saveOrder), new { id = newOrder.Id }, newOrder.AsDto());

        }

        [Authorize]
        [HttpGet("order-history")]
        public async Task<IEnumerable<OrderDto>> GetOrders([FromQuery] string userId)
        {
            var orders = (await orderRepository.GetOrders(userId)).Select(item => item.AsDto());
            return orders;
        }



    }


}