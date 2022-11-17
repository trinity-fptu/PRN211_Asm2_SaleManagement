using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PRN211_Asm2_Salemanagement_Library.Models;

namespace PRN211_Asm2_Salemanagement_Library.DAOs
{
    public class OrderDAO
    {
        // Singleton pattern 
        private static OrderDAO _instance;
        private static readonly object _lock = new object();

        private OrderDAO()
        {
        }

        public static OrderDAO Instance
        {
            get
            {
                lock (_lock)
                {
                    if (_instance == null)
                    {
                        _instance = new OrderDAO();
                    }

                    return _instance;
                }
            }
        }

        //Get all orders
        public IEnumerable<Order> GetAllOrders()
        {
            try
            {
                using (var db = new SaleManagermentContext())
                {
                    return db.Orders.ToList();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public Order GetOrderByMemberId(int memberId)
        {
            using (var db = new SaleManagermentContext())
            {
                Order od = db.Orders.Where(x => x.MemberId == memberId).FirstOrDefault();
                return od; 
            }
        }


        //Get order by id
        public Order GetOrderById(int id)
        {
            try
            {
                using (var db = new SaleManagermentContext())
                {
                    return db.Orders.Find(id);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        //Add new order
        public bool AddOrder(Order order)
        {
            try
            {
                using (var db = new SaleManagermentContext())
                {
                    //Check if order exists
                    if (db.Orders.Any(o => o.OrderId == order.OrderId))
                    {
                        return false;
                    }
                    else
                    {
                        db.Orders.Add(order);
                        db.SaveChanges();
                        return true;
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        //Update order
        public bool UpdateOrder(Order order)
        {
            try
            {
                using (var db = new SaleManagermentContext())
                {
                    //Check if order exists
                    if (db.Orders.Any(o => o.OrderId == order.OrderId))
                    {
                        db.Orders.Update(order);
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

        //Delete order
        public bool DeleteOrder(int id)
        {
            try
            {
                using (var db = new SaleManagermentContext())
                {
                    //Check if order exists
                    if (db.Orders.Any(o => o.OrderId == id))
                    {
                        db.Orders.Remove(db.Orders.Find(id));
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

        //Search order by date range
        public IEnumerable<Order> SearchOrderByDateRange(DateTime startDate, DateTime endDate)
        {
            try
            {
                using (var db = new SaleManagermentContext())
                {
                    return db.Orders.Where(o => o.OrderDate >= startDate && o.OrderDate <= endDate).ToList();
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
