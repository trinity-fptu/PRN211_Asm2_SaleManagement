using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN211_Asm2_Salemanagement_WinApp.Mapper
{
    public class ProductMapper
    {
        [DisplayName("Product ID")]
        public int ProductId { get; set; }
        [DisplayName("Category ID")]
        public int CategoryId { get; set; }
        [DisplayName("Product Name")]
        public string ProductName { get; set; }
        [DisplayName("Weight")]
        public string Weight { get; set; }
        [DisplayName("Unit Price")]
        public decimal UnitPrice { get; set; }
        [DisplayName("Units in Stock")]
        public int UnitslnStock { get; set; }
    }
}
