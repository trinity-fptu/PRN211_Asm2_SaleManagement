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
    public partial class frmMember : Form
    {

        public static string MemberEmail { get; set; }

        public MemberMapper MemberMapper { get; set; }
        public ProductMapper ProductMapper { get; set; }
        public OrderMapper OrderMapper { get; set; }
        public OrderDetailMapper OrderDetailMapper { get; set; }
        public IMemberRepo MemberRepo { get; set; }
        public IProductRepo ProductRepo { get; set; }
        public IOrderRepo OrderRepo { get; set; }
        public IOrderDetailRepo OrderDetailRepo { get; set; }

        private IMapper mapper;
        public frmMember()
        {
            MemberRepo = new MemberRepo();
            ProductRepo = new ProductRepo();
            OrderRepo = new OrderRepo();
            OrderDetailRepo = new OrderDetailRepo();
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Member, MemberMapper>();
                cfg.CreateMap<Product, ProductMapper>();
                cfg.CreateMap<Order, OrderMapper>();
                //cfg.CreateMap<OrderDetail, OrderDetailMapper>();
            });
            mapper = config.CreateMapper();
            InitializeComponent();
        }

        private void frmMember_Load(object sender, EventArgs e)
        {
            dgvOrderDetailMember.DataSource = OrderRepo.GetOrderListByMemberId(MemberRepo.GetMemberByEmail(MemberEmail).MemberId).ToList();
            this.dgvOrderDetailMember.Columns["Member"].Visible = false;
            Member m = MemberRepo.GetMemberByEmail(MemberEmail);
            txtMemberId.Text = m.MemberId.ToString();
            txtEmail.Text = MemberEmail;
            txtCompanyName.Text = m.CompanyName;
            txtCountry.Text = m.Country;
            txtCity.Text = m.City;
            txtMemberId.Enabled = false;
        }

        private void btnUpdateMember_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMemberId.Text) || string.IsNullOrWhiteSpace(txtEmail.Text)
                                                           || string.IsNullOrWhiteSpace(txtCompanyName.Text)
                                                           || string.IsNullOrWhiteSpace(txtCity.Text)
                                                           || string.IsNullOrWhiteSpace(txtCountry.Text))
            {
                MessageBox.Show("Please enter all information");
            }
            else
            {
                //Check duplicate email

                if (MemberRepo.CheckDuplicateEmail(txtEmail.Text) && txtEmail.Text != MemberEmail)
                {
                    MessageBox.Show("Email already exists");
                }
                else
                {
                    //Update member
                    Member member = MemberRepo.GetMemberByEmail(MemberEmail);
                    member.Email = txtEmail.Text;
                    member.CompanyName = txtCompanyName.Text;
                    member.City = txtCity.Text;
                    member.Country = txtCountry.Text;
                    MemberRepo.UpdateMember(member);
                    MessageBox.Show("Member updated successfully");
                }
                

            }
        }
    }
}
