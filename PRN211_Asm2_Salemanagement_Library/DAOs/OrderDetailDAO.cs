using PRN211_Asm2_Salemanagement_Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN211_Asm2_Salemanagement_Library.DAOs
{
    public class OrderDetailDAO
    {
        // Singleton pattern 
        private static OrderDetailDAO _instance;
        private static readonly object _lock = new object();

        private OrderDetailDAO()
        {
        }

        public static OrderDetailDAO Instance
        {
            get
            {
                lock (_lock)
                {
                    if (_instance == null)
                    {
                        _instance = new OrderDetailDAO();
                    }

                    return _instance;
                }
            }
        }

        //Get all order details
        public IEnumerable<OrderDetail> GetAllOrderDetails()
        {
            try
            {
                using (var db = new SaleManagermentContext())
                {
                    return db.OrderDetails.ToList();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        //Get order detail by id
        public OrderDetail GetOrderDetailById(int id)
        {
            try
            {
                using (var db = new SaleManagermentContext())
                {
                    return db.OrderDetails.Find(id);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public OrderDetail GetOrderDetailByProductId(int ProductId)
        {
            using (var db = new SaleManagermentContext())
            {
                OrderDetail od =  db.OrderDetails.Where(x=>x.ProductId == ProductId).FirstOrDefault();
                return od;
            }
        }


        //Add order detail
        public bool AddOrderDetail(OrderDetail orderDetail)
        {
            try
            {
                using (var db = new SaleManagermentContext())
                {
                    db.OrderDetails.Add(orderDetail);
                    db.SaveChanges();
                    return true;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        //Update order detail
        public bool UpdateOrderDetail(OrderDetail orderDetail)
        {
            try
            {
                using (var db = new SaleManagermentContext())
                {
                    var orderDetailToUpdate = db.OrderDetails.Find(orderDetail.OrderId);
                    if (orderDetailToUpdate != null)
                    {
                        orderDetailToUpdate.OrderId = orderDetail.OrderId;
                        orderDetailToUpdate.ProductId = orderDetail.ProductId;
                        orderDetailToUpdate.Quantity = orderDetail.Quantity;
                        orderDetailToUpdate.UnitPrice = orderDetail.UnitPrice;
                        orderDetailToUpdate.Discount = orderDetail.Discount;
                        db.SaveChanges();
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        //Delete order detail
        public bool DeleteOrderDetail(int id)
        {
            try
            {
                using (var db = new SaleManagermentContext())
                {
                    var orderDetailToDelete = db.OrderDetails.Find(id);
                    if (orderDetailToDelete != null)
                    {
                        db.OrderDetails.Remove(orderDetailToDelete);
                        db.SaveChanges();
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}
