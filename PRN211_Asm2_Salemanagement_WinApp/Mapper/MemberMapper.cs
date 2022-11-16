using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN211_Asm2_Salemanagement_WinApp.Mapper
{
    public class MemberMapper
    {
        [DisplayName("Member ID")]
        public int MemberID { get; set; }
        [DisplayName("Email")]
        public string Email { get; set; }
        [DisplayName("Company Name")]
        public string CompanyName { get; set; }
        [DisplayName("Password")]
        public string Password { get; set; }
        [DisplayName("City")]
        public string City { get; set; }
        [DisplayName("Country")]
        public string Country { get; set; }
    }
}
