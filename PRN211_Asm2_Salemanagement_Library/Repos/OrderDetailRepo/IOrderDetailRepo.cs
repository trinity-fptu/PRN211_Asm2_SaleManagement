using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PRN211_Asm2_Salemanagement_Library.Models;

namespace PRN211_Asm2_Salemanagement_Library.Repos.OrderDetailRepo
{
    public interface IOrderDetailRepo
    {
        public IEnumerable<OrderDetail> GetAllOrderDetails();
        public OrderDetail GetOrderDetailById(int id);
        public bool AddOrderDetail(OrderDetail orderDetail);
        public bool UpdateOrderDetail(OrderDetail orderDetail);
        public bool DeleteOrderDetail(int id);

        public OrderDetail GetOrderDetailByProductId(int id);


    }
}
