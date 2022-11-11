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
using PRN211_Asm2_Salemanagement_Library.Repos.MemberRepo;
using PRN211_Asm2_Salemanagement_Library.Repos.OrderRepo;
using PRN211_Asm2_Salemanagement_Library.Repos.ProductRepo;
using PRN211_Asm2_Salemanagement_WinApp.Mapper;

namespace PRN211_Asm2_Salemanagement_WinApp
{
    public partial class frmAdmin : Form
    {
        public MemberMapper MemberLogin { get; set; }
        public ProductMapper ProductMapper { get; set; }
        public OrderMapper OrderMapper { get; set; }
        //public OrderDetailMapper OrderDetailMapper { get; set; }
        public IMemberRepo MemberRepo { get; set; }
        public IProductRepo ProductRepo { get; set; }
        public IOrderRepo OrderRepo { get; set; }
        //public IOrderDetailRepo OrderDetailRepo { get; set; }
        
        public frmAdmin()
        {
            InitializeComponent();
            MemberRepo = new MemberRepo();
            ProductRepo = new ProductRepo();
            OrderRepo = new OrderRepo();
            //OrderDetailRepo = new OrderDetailRepo();
        }

        private void frmAdmin_Load(object sender, EventArgs e)
        {
            //Load member, product, order from database and display them to datagridview
            using (var db = new SaleManagermentContext())
            {
                dgvMember.DataSource = db.Members.ToList();
                dgvProduct.DataSource = db.Products.ToList();
                dgvOrder.DataSource = db.Orders.ToList();
                //dgvOrderDetail.DataSource = db.OrderDetails.ToList();
            }


        }

        private void dgvMember_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int rowIndex = e.RowIndex;
            txtMemberID.Text = dgvMember.Rows[rowIndex].Cells[0].Value.ToString();
            txtEmail.Text = dgvMember.Rows[rowIndex].Cells[1].Value.ToString();
            txtPassword.Text = dgvMember.Rows[rowIndex].Cells[2].Value.ToString();
            txtCity.Text = dgvMember.Rows[rowIndex].Cells[3].Value.ToString();
            txtCountry.Text = dgvMember.Rows[rowIndex].Cells[4].Value.ToString();
        }

        private void dgvProduct_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int rowIndex = e.RowIndex;
            txtProductID.Text = dgvProduct.Rows[rowIndex].Cells[0].Value.ToString();
            txtCategoryID.Text = dgvProduct.Rows[rowIndex].Cells[1].Value.ToString();
            txtProductName.Text = dgvProduct.Rows[rowIndex].Cells[2].Value.ToString();
            txtWeight.Text = dgvProduct.Rows[rowIndex].Cells[3].Value.ToString();
            txtUnitPrice.Text = dgvProduct.Rows[rowIndex].Cells[4].Value.ToString();
            txtUnitsInStock.Text = dgvProduct.Rows[rowIndex].Cells[5].Value.ToString();
        }

        private void dgvOrder_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int rowIndex = e.RowIndex;
            txtOrderID.Text = dgvOrder.Rows[rowIndex].Cells[0].Value.ToString();
            txtMemberID_Order.Text = dgvOrder.Rows[rowIndex].Cells[1].Value.ToString();
            dtpOrderDate.Text = dgvOrder.Rows[rowIndex].Cells[2].Value.ToString();
            dtpRequiredDate.Text = dgvOrder.Rows[rowIndex].Cells[3].Value.ToString();
            dtpShippedDate.Text = dgvOrder.Rows[rowIndex].Cells[4].Value.ToString();
            txtFreight.Text = dgvOrder.Rows[rowIndex].Cells[5].Value.ToString();
        }

        private void btnMemberSearch_Click(object sender, EventArgs e)
        {

        }

        private void btnProductSearch_Click(object sender, EventArgs e)
        {

        }

        private void btnOrderSearch_Click(object sender, EventArgs e)
        {

        }

        private void btnMemberAdd_Click(object sender, EventArgs e)
        {

        }

        private void btnMemberUpdate_Click(object sender, EventArgs e)
        {

        }

        private void btnMemberDelete_Click(object sender, EventArgs e)
        {

        }

        private void btnProductAdd_Click(object sender, EventArgs e)
        {

        }

        private void btnProductUpdate_Click(object sender, EventArgs e)
        {

        }

        private void btnProductDelete_Click(object sender, EventArgs e)
        {

        }

        private void btnLogout_Click(object sender, EventArgs e)
        {

        }

        private void btnExit_Click(object sender, EventArgs e)
        {

        }
    }
}
