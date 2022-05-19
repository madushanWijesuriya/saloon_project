using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using saloonAPI.Models;
using saloonAPI.Services;
using saloonAPI.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;


namespace saloonAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderRepository _sqlService;
        private readonly IInvoiceRepository _sqlServiceInvoice;
        private readonly IMapper _mapper;

        public OrdersController(IOrderRepository dataAccessRepository, IMapper mapper, IInvoiceRepository invoiceRepository)
        {
            _sqlService = dataAccessRepository;
            _sqlServiceInvoice = invoiceRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IList<Order>> GetAllOrders()
        {
            List<Order> orders = _sqlService.GetAllOrders();
            return Ok(orders);
        }

        [HttpPost, Authorize]
        public ActionResult<Appoinment> CreateOrder(OrderCreateVM orderCreateVM)
        {
            var products = orderCreateVM.Products;
            var UserId = User.Claims.FirstOrDefault(c => c.Type == "userId").Value;
            Order order = new Order()
            {
                CreatedDate = DateTime.Now,
                DeliveryAddress = orderCreateVM.DeliveryAddress,
                MobileNo = orderCreateVM.MobileNo,
                CustomerId = UserId,
                OrderStatus = OrderStatus.Placed,
                TotalAmount = products.Sum(p => p.Price) + 200 //+ delivery fee
            };

            var orderCreated = _sqlService.SaveOrder(order);
            List<OrderDetail> orderDetailsList = new List<OrderDetail>();

            foreach (var product in products)
            {
                orderDetailsList.Add(new OrderDetail
                {
                    CreatedDate = DateTime.Now,
                    Amount = product.Price,
                    OrderId = orderCreated.Id,
                    ProductId = product.Id,
                });
            }
            orderCreated.OrderDetails = orderDetailsList;
            _sqlService.UpdateOrder(orderCreated);

            return Created("Orders", orderCreated);
        }
       
        [HttpGet("my-orders")]
        public ActionResult<IList<Order>> GetMyOrders()
        {
            List<Order> orders = _sqlService.GetAllOrders();
            var UserId = User.Claims.FirstOrDefault(c => c.Type == "userId").Value;
            orders = orders.Where(o => o.CustomerId == UserId).ToList();
            return Ok(orders);
        }
        

        [HttpPut("{orderId}/orderStatusChange"), Authorize]
        public IActionResult ChangeOrderStatus(int orderId, OrderStatusChangeRequestVM request)
        {
            var order = _sqlService.GetOrder(orderId);

            if (order is null)
            {
                return NotFound();
            } else if (order.OrderStatus == OrderStatus.Canceled)
            {
                return BadRequest(new { message = "Cannot change the order status after mark as canceled" });
            } else if ((OrderStatus)request.Status == OrderStatus.Paid)
            {
                return BadRequest(new { message = "Cannot change status to paid, use generate invoice option" });
            }
            
            order.OrderStatus = (OrderStatus)request.Status;
            _sqlService.UpdateOrder(order);

            return NoContent();
        }

        [HttpPost("{orderId}/generateOrderInvoice"), Authorize]
        public IActionResult GenerateOrderInvoice(int orderId, OrderInvoiceInput data)
        {
            var order = _sqlService.GetOrder(orderId);
            if(order is null)
            {
                return BadRequest();
            }

            var invoiceCreated = _sqlServiceInvoice.SaveInvoice(new Invoice()
            {
                Amount = order.TotalAmount + data.Amount,
                AppoinmentId = null,
                CreatedDate = DateTime.Now
            });
            var orderInvoice = new OrderInvoice()
            {
                CreatedDate = DateTime.Now,
                InvoiceId = invoiceCreated.Id,
                OrderId = order.Id
            };
            invoiceCreated.OrderInvoices.Add(orderInvoice);
            Random r = new Random();
            invoiceCreated.InvoiceNo = "Inv-" + invoiceCreated.Id + "-" + invoiceCreated.CreatedDate.ToString("yyyy-MM-dd") + r.Next(0, 100000).ToString();
            _sqlServiceInvoice.UpdateInvoice(invoiceCreated);

            order.OrderStatus = OrderStatus.Paid;
            _sqlService.UpdateOrder(order);

            var invoiceToReturn = _sqlServiceInvoice.GetInvoice(invoiceCreated.Id);
            return Created("Orders", invoiceToReturn);
        }
    }
   

}
