using PRN211_Asm2_Salemanagement_Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PRN211_Asm2_Salemanagement_Library.DAOs;

namespace PRN211_Asm2_Salemanagement_Library.Repos.OrderDetailRepo
{
    public class OrderDetailRepo : IOrderDetailRepo
    {
        public bool AddOrderDetail(OrderDetail orderDetail) => OrderDetailDAO.Instance.AddOrderDetail(orderDetail);

        public bool DeleteOrderDetail(int id) => OrderDetailDAO.Instance.DeleteOrderDetail(id);

        public IEnumerable<OrderDetail> GetAllOrderDetails() => OrderDetailDAO.Instance.GetAllOrderDetails();

        public OrderDetail GetOrderDetailById(int id) => OrderDetailDAO.Instance.GetOrderDetailById(id);

        public OrderDetail GetOrderDetailByProductId(int id) => OrderDetailDAO.Instance.GetOrderDetailByProductId(id);

        public bool UpdateOrderDetail(OrderDetail orderDetail) =>
            OrderDetailDAO.Instance.UpdateOrderDetail(orderDetail);
    }
}
