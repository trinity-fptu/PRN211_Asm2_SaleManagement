using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PRN211_Asm2_Salemanagement_Library.Models;
using PRN211_Asm2_Salemanagement_Library.Repos.OrderDetailRepo;
using PRN211_Asm2_Salemanagement_WinApp.Mapper;

namespace PRN211_Asm2_Salemanagement_WinApp
{
    public partial class frmOrderDetail : Form
    {
        IOrderDetailRepo orderDetailRepo = new OrderDetailRepo();
        public int OrderID { get; set; }
        public frmOrderDetail()
        {
            InitializeComponent();
            
        }
        
        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Hide();
            frmAdmin frmAdmin = new frmAdmin();
            frmAdmin.Show();
        }

        private void frmOrderDetail_Load(object sender, EventArgs e)
        {
        }
    }
}
