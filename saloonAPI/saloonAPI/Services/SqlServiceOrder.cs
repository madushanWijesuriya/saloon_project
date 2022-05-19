using saloonAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace saloonAPI.Services
{
    public class SqlServiceOrder : IOrderRepository
    {
        private readonly ApplicationDbContext _context = new ApplicationDbContext();
        public List<Order> GetAllOrders()
        {
            return _context.Orders.ToList();
        }
        public Order GetOrder(int orderId)
        {
            return _context.Orders.FirstOrDefault(c => c.Id == orderId);
        }

        public Order SaveOrder(Order order)
        {
            _context.Orders.Add(order);
            _context.SaveChanges();

            return _context.Orders.Find(order.Id);
        }

        public void UpdateOrder(Order order)
        {
            _context.SaveChanges();
        }
        public void DeleteOrder(Order order)
        {
            _context.Orders.Remove(order);
            _context.SaveChanges();
        }
        public void UpdateStatus(Order order, int status)
        {
            order.OrderStatus = (OrderStatus)status;
            _context.SaveChanges();
        }
        public List<Order> GetOrdersByUser(string UserId)
        {
            return _context.Orders.Where(o => o.CustomerId == UserId).ToList();
        }
    }
}
