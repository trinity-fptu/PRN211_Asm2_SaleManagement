using PRN211_Asm2_Salemanagement_Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PRN211_Asm2_Salemanagement_Library.DAOs;

namespace PRN211_Asm2_Salemanagement_Library.Repos.OrderRepo
{
    public class OrderRepo : IOrderRepo
    {
        public bool AddOrder(Order order) => OrderDAO.Instance.AddOrder(order);

        public bool DeleteOrder(int id) => OrderDAO.Instance.DeleteOrder(id);

        public IEnumerable<Order> GetAllOrders() => OrderDAO.Instance.GetAllOrders();

        public Order GetOrderById(int id) => OrderDAO.Instance.GetOrderById(id);

        public Order GetOrderByMemberId(int id) => OrderDAO.Instance.GetOrderByMemberId(id);

        public IEnumerable<Order> GetOrdersByDateRange(DateTime startDate, DateTime endDate) =>
            OrderDAO.Instance.SearchOrderByDateRange(startDate, endDate);

        public bool UpdateOrder(Order order) => OrderDAO.Instance.UpdateOrder(order);
    }
}
