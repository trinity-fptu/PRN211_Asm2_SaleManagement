using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AutoMapper;
using PRN211_Asm2_Salemanagement_Library.Models;
using PRN211_Asm2_Salemanagement_Library.Repos.MemberRepo;
using PRN211_Asm2_Salemanagement_Library.Repos.OrderDetailRepo;
using PRN211_Asm2_Salemanagement_Library.Repos.OrderRepo;
using PRN211_Asm2_Salemanagement_Library.Repos.ProductRepo;
using PRN211_Asm2_Salemanagement_WinApp.Mapper;

namespace PRN211_Asm2_Salemanagement_WinApp
{
    public partial class frmAdmin : Form
    {
        public MemberMapper MemberMapper { get; set; }
        public ProductMapper ProductMapper { get; set; }
        public OrderMapper OrderMapper { get; set; }
        public OrderDetailMapper OrderDetailMapper { get; set; }
        public IMemberRepo MemberRepo { get; set; }
        public IProductRepo ProductRepo { get; set; }
        public IOrderRepo OrderRepo { get; set; }
        public IOrderDetailRepo OrderDetailRepo { get; set; }
        
        private IMapper mapper;

        public frmAdmin()
        {
            InitializeComponent();
            MemberRepo = new MemberRepo();
            ProductRepo = new ProductRepo();
            OrderRepo = new OrderRepo();
            //OrderDetailRepo = new OrderDetailRepo();
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Member, MemberMapper>();
                cfg.CreateMap<Product, ProductMapper>();
                cfg.CreateMap<Order, OrderMapper>();
                //cfg.CreateMap<OrderDetail, OrderDetailMapper>();
            });
            mapper = config.CreateMapper();
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
                rbMemberEmailSearch.Checked = true;
                rbProductNameSearch.Checked = true;
                rbUnitPriceSearch.Checked = true;
                dtpStartDate.Value = DateTime.Now;
                dtpEndDate.Value = DateTime.Now;
                //Get city list and country list from database
                var countryList = MemberRepo.GetCountryList().ToArray();
                cbFilterByCountry.Items.AddRange(countryList);
                
            }


        }

       

        private void dgvMember_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int rowIndex = e.RowIndex;
            txtMemberID.Text = dgvMember.Rows[rowIndex].Cells[0].Value.ToString();
            txtEmail.Text = dgvMember.Rows[rowIndex].Cells[1].Value.ToString();
            txtCompanyName.Text = dgvMember.Rows[rowIndex].Cells[2].Value.ToString();
            txtPassword.Text = dgvMember.Rows[rowIndex].Cells[5].Value.ToString();
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
            
        }

        private void btnMemberSearch_Click(object sender, EventArgs e)
        {
            string searchString = txtMemberSearch.Text;
            //Check if txtMemberSearch is null
            if (string.IsNullOrWhiteSpace(searchString))
            {
                MessageBox.Show("Please enter the keyword to search");
                return;
            }
            else
            {
                IEnumerable<Member> members = null;
                if (rbMemberIDSearch.Checked)
                {
                    //Search member by memberID
                    members = MemberRepo.SearchMemberById(int.Parse(searchString));
                }
                else if (rbMemberEmailSearch.Checked)
                {
                    //Search member by email
                    members = MemberRepo.SearchMemberByEmail(searchString);
                }
                if (members != null)
                {
                    dgvMember.DataSource = members.ToList();
                }
            }
        }

        private void btnProductSearch_Click(object sender, EventArgs e)
        {
            string searchProductString = txtProductSearch.Text;
            string searchFromInput = txtFromInput.Text;
            string searchToInput = txtToInput.Text;
            IEnumerable<Product> sProduct = null;
            if (!string.IsNullOrWhiteSpace(searchProductString))
            {
                if (rbProductIDSearch.Checked)
                {
                    sProduct = ProductRepo.SearchProductById(int.Parse(searchProductString));
                }
                else if (rbProductNameSearch.Checked)
                {
                    sProduct = ProductRepo.SearchProductByName(searchProductString);
                }
            }
            else if (!string.IsNullOrWhiteSpace(searchFromInput) || !string.IsNullOrWhiteSpace(searchToInput))
            {
                if (rbUnitPriceSearch.Checked)
                {
                    sProduct = ProductRepo.SearchProductByPriceRange(int.Parse(searchFromInput), int.Parse(searchToInput));
                }
                else if (rbUnitsInStockSeach.Checked)
                {
                    sProduct = ProductRepo.SearchProductByUnitInStockRange(int.Parse(searchFromInput), int.Parse(searchToInput));
                }
            }
            else
            {
                MessageBox.Show("Please enter the keyword to search");
            }
            if (sProduct != null)
            {
                dgvProduct.DataSource = sProduct.ToList();
            }
        }

        private void btnOrderSearch_Click(object sender, EventArgs e)
        {
            DateTime startDate = dtpStartDate.Value;
            DateTime endDate = dtpEndDate.Value;
            if (startDate > endDate)
            {
                MessageBox.Show("Start date must be less than end date");
            }
            else
            {
                IEnumerable<Order> orders = OrderRepo.GetOrdersByDateRange(startDate, endDate);
                dgvOrder.DataSource = orders.ToList();
            }
        }

        private void btnMemberAdd_Click(object sender, EventArgs e)
        {
            //Check if textbox is null 
            if (string.IsNullOrWhiteSpace(txtMemberID.Text) || string.IsNullOrWhiteSpace(txtEmail.Text) 
                                                            || string.IsNullOrWhiteSpace(txtPassword.Text) 
                                                            || string.IsNullOrWhiteSpace(txtCity.Text) 
                                                            || string.IsNullOrWhiteSpace(txtCountry.Text))
            {
                MessageBox.Show("Please enter all information");
            }
            else
            {
                //Check duplicate member ID 
                if (MemberRepo.CheckDuplicateId(int.Parse(txtMemberID.Text)))
                {
                    MessageBox.Show("Member ID already exists");
                }
                else
                {
                    //Check duplicate email
                    if (MemberRepo.CheckDuplicateEmail(txtEmail.Text))
                    {
                        MessageBox.Show("Email already exists");
                    }
                    else
                    {
                        //Add new member
                        Member member = new Member();
                        member.MemberId = int.Parse(txtMemberID.Text);
                        member.Email = txtEmail.Text;
                        member.CompanyName = txtCompanyName.Text;
                        member.Password = txtPassword.Text;
                        member.City = txtCity.Text;
                        member.Country = txtCountry.Text;
                        MemberRepo.AddMember(member);
                        MessageBox.Show("Member added successfully");
                        //Refresh datagridview
                        using (var db = new SaleManagermentContext())
                        {
                            dgvMember.DataSource = db.Members.ToList();
                        }
                    }
                }
            }
        }

        private void btnMemberUpdate_Click(object sender, EventArgs e)
        {
            //Check if textbox is null 
            if (string.IsNullOrWhiteSpace(txtMemberID.Text) || string.IsNullOrWhiteSpace(txtEmail.Text) 
                                                            || string.IsNullOrWhiteSpace(txtPassword.Text) 
                                                            || string.IsNullOrWhiteSpace(txtCity.Text) 
                                                            || string.IsNullOrWhiteSpace(txtCountry.Text))
            {
                MessageBox.Show("Please enter all information");
            }
            else
            {
                //Check duplicate email
                if (MemberRepo.CheckDuplicateEmail(txtEmail.Text))
                {
                    MessageBox.Show("Email already exists");
                }
                else
                {
                    //Update member
                    Member member = new Member();
                    member.MemberId = int.Parse(txtMemberID.Text);
                    member.Email = txtEmail.Text;
                    member.CompanyName = txtCompanyName.Text;
                    member.City = txtCity.Text;
                    member.Country = txtCountry.Text;
                    MemberRepo.UpdateMember(member);
                    MessageBox.Show("Member updated successfully");
                    //Refresh datagridview
                    using (var db = new SaleManagermentContext())
                    {
                        dgvMember.DataSource = db.Members.ToList();
                    }
                }
            }
        }

        private void btnMemberDelete_Click(object sender, EventArgs e)
        {
            //Check if textbox is null 
            if (string.IsNullOrWhiteSpace(txtMemberID.Text))
            {
                MessageBox.Show("Please enter all information");
            }
            else
            {
                //Check member is existed
                if (MemberRepo.GetMemberById(int.Parse(txtMemberID.Text)) == null)
                {
                    MessageBox.Show("Member not found");
                }
                else
                {
                    //Delete member
                    MemberRepo.DeleteMember(int.Parse(txtMemberID.Text));
                    MessageBox.Show("Member deleted successfully");
                    //Refresh datagridview
                    using (var db = new SaleManagermentContext())
                    {
                        dgvMember.DataSource = db.Members.ToList();
                    }
                }
            }
        }

        private void btnProductAdd_Click(object sender, EventArgs e)
        {
            //Check if textbox is null 
            if (string.IsNullOrWhiteSpace(txtProductID.Text) || string.IsNullOrWhiteSpace(txtCategoryID.Text) 
                                                             || string.IsNullOrWhiteSpace(txtProductName.Text) 
                                                             || string.IsNullOrWhiteSpace(txtWeight.Text) 
                                                             || string.IsNullOrWhiteSpace(txtUnitPrice.Text) 
                                                             || string.IsNullOrWhiteSpace(txtUnitsInStock.Text))
            {
                MessageBox.Show("Please enter all information");
            }
            else
            {
                //Add product
                Product product = new Product();
                product.ProductId = int.Parse(txtProductID.Text);
                product.CategoryId = int.Parse(txtCategoryID.Text);
                product.ProductName = txtProductName.Text;
                product.Weight = txtWeight.Text;
                product.UnitPrice = int.Parse(txtUnitPrice.Text);
                product.UnitslnStock = int.Parse(txtUnitsInStock.Text);
                ProductRepo.AddProduct(product);
                MessageBox.Show("Product added successfully");
                dgvProduct.DataSource = ProductRepo.GetAllProducts();
            }
        }

        private void btnProductUpdate_Click(object sender, EventArgs e)
        {
            //Check if textbox is null 
            if (string.IsNullOrWhiteSpace(txtProductID.Text) || string.IsNullOrWhiteSpace(txtCategoryID.Text) 
                                                             || string.IsNullOrWhiteSpace(txtProductName.Text) 
                                                             || string.IsNullOrWhiteSpace(txtWeight.Text) 
                                                             || string.IsNullOrWhiteSpace(txtUnitPrice.Text) 
                                                             || string.IsNullOrWhiteSpace(txtUnitsInStock.Text))
            {
                MessageBox.Show("Please enter all information");
            }
            else
            {
                //Update product
                Product product = new Product();
                product.ProductId = int.Parse(txtProductID.Text);
                product.CategoryId = int.Parse(txtCategoryID.Text);
                product.ProductName = txtProductName.Text;
                product.Weight = txtWeight.Text;
                product.UnitPrice = int.Parse(txtUnitPrice.Text);
                product.UnitslnStock = int.Parse(txtUnitsInStock.Text);
                ProductRepo.UpdateProduct(product);
                MessageBox.Show("Product updated successfully");
                dgvProduct.DataSource = ProductRepo.GetAllProducts();
            }
        }

        private void btnProductDelete_Click(object sender, EventArgs e)
        {
            //Check if textbox is null 
            if (string.IsNullOrWhiteSpace(txtProductID.Text))
            {
                MessageBox.Show("Please enter all information");
            }
            else
            {
                //Delete product
                ProductRepo.DeleteProduct(int.Parse(txtProductID.Text));
                MessageBox.Show("Product deleted successfully");
                dgvProduct.DataSource = ProductRepo.GetAllProducts();
            }
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            //Close and load login form
            this.Close();
            frmLogin loginForm = new frmLogin();
            loginForm.Show();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            //Close application
            Application.Exit();
        }

        private void cbFilterByCountry_SelectedIndexChanged(object sender, EventArgs e)
        {
            using (var db = new SaleManagermentContext())
            {
                var member = MemberRepo.FilterMemberByCountry(cbFilterByCountry.GetItemText(this.cbFilterByCountry.SelectedItem));
                if (member.Count() > 0)
                {
                    dgvMember.DataSource = member.ToList();
                    var cityList = MemberRepo.GetCityList(cbFilterByCountry.GetItemText(this.cbFilterByCountry.SelectedItem)).ToArray();
                    cbFilterByCity.Items.Clear();
                    cbFilterByCity.Items.AddRange(cityList);

                }
                else
                {
                    dgvMember.DataSource = db.Members.ToList();
                }
            }
        }

        private void cbFilterByCity_SelectedIndexChanged(object sender, EventArgs e)
        {
            using (var db = new SaleManagermentContext())
            {
                var member = MemberRepo.FilterMemberByCity(cbFilterByCity.GetItemText(this.cbFilterByCity.SelectedItem), cbFilterByCountry.GetItemText(this.cbFilterByCountry.SelectedItem));
                if (member.Count() > 0)
                {
                    dgvMember.DataSource = member.ToList();
                }
                else
                {
                    dgvMember.DataSource = db.Members.ToList();
                }
            }

        }

        private void btnDetail_Click(object sender, EventArgs e)
        {

        }

        private void btnMemberRefresh_Click(object sender, EventArgs e)
        {
            //Refresh dgvMember
            using (var db = new SaleManagermentContext())
            {
                dgvMember.DataSource = db.Members.ToList();
            }
        }

        private void btnProductRefresh_Click(object sender, EventArgs e)
        {
            //Refresh dgvProduct
            using (var db = new SaleManagermentContext())
            {
                dgvProduct.DataSource = db.Products.ToList();
            }
        }

        private void btnOrderRefresh_Click(object sender, EventArgs e)
        {
            //Refresh dgvOrder
            using (var db = new SaleManagermentContext())
            {
                dgvOrder.DataSource = db.Orders.ToList();
            }
        }

        private void dgvOrder_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }
    }
}
