using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN211_Asm2_Salemanagement_WinApp.Mapper
{
    public class OrderMapper
    {
        [DisplayName("Order ID")]
        public int OrderId { get; set; }
        [DisplayName("Member ID")]
        public int? MemberId { get; set; }

        [DisplayName("Order Date")]
        public DateTime OrderDate
        {
            get
            {
                return OrderDate.Date;
            }
            set
            {
                OrderDate = value.Date;
            }
        }

        [DisplayName("Required Date")]
        public DateTime? RequiredDate { get; set; }
            [DisplayName("Shipped Date")]
        public DateTime? ShippedDate { get; set; }
        [DisplayName("Freight")]
        public decimal? Freight { get; set; }
        
    }
}
