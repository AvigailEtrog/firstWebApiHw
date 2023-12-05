using AutoMapper;
using DTO;
using Entities;
using Microsoft.AspNetCore.Mvc;
using Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace webApiShopSite.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        IOrderService _orderService;
        IMapper _mapper;
        public OrdersController(IOrderService orderService,IMapper mapper)
        {
            _orderService = orderService;
            _mapper = mapper;
        }
        // POST api/<Orders>
        [HttpPost]
        public async Task<ActionResult<OrderDto>> Post([FromBody] OrderDto orderDto)
        {
            
                Order order = _mapper.Map<OrderDto, Order>(orderDto);
                Order newOrder = await _orderService.createNewOrderAsync(order);
                OrderDto returnOrder = _mapper.Map<Order, OrderDto>(newOrder);
                return newOrder != null ? CreatedAtAction(nameof(Get), new { id = returnOrder.OrderId }, returnOrder) :NoContent();
        }
        // GET api/<Orders>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<OrderDto>> Get(int id)
        {
            Order order = await _orderService.getOrderByIdAsync(id);
            OrderDto orderDto = _mapper.Map<Order, OrderDto>(order);
            return order != null ? Ok(orderDto) : BadRequest("user didnt found");
        }

    }

}
