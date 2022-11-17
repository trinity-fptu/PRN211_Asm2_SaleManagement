using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PRN211_Asm2_Salemanagement_Library.Models;

namespace PRN211_Asm2_Salemanagement_Library.Repos.OrderRepo
{
    public interface IOrderRepo
    {
        public IEnumerable<Order> GetAllOrders();
        public Order GetOrderById(int id);
        public IEnumerable<Order> GetOrdersByDateRange(DateTime startDate, DateTime endDate);
        public bool AddOrder(Order order);
        public bool UpdateOrder(Order order);
        public bool DeleteOrder(int id);

        public Order GetOrderByMemberId(int id);
    }
}
