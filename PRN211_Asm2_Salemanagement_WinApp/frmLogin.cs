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
using PRN211_Asm2_Salemanagement_WinApp.Mapper;

namespace PRN211_Asm2_Salemanagement_WinApp
{
    public partial class frmLogin : Form
    {
        private IMemberRepo memberRepo = new MemberRepo();
        private IMapper mapper;
        
        public frmLogin()
        {
            InitializeComponent();
            var config = new MapperConfiguration(cfg =>
                {
                    cfg.AddProfile(new ProfileMapper());
                });
            mapper = config.CreateMapper();
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            string email = txtEmail.Text;
            string password = txtPassword.Text;
            Member loginMember = memberRepo.Login(email, password);
            MemberMapper memberMapper = mapper.Map<MemberMapper>(loginMember);
            if (loginMember != null)
            {
                MessageBox.Show("Login successfully!", "Login", MessageBoxButtons.OK, MessageBoxIcon.Information);
                string adminEmail = loginMember.Email;
                if (adminEmail == "admin@fstore.com")
                {
                    frmAdmin adminForm = new frmAdmin
                    {
                        MemberMapper = memberMapper,
                        MemberRepo = memberRepo,
                    };
                    adminForm.Closed += (s, args) => this.Close();
                    this.Hide();
                    adminForm.Show();
                }
                else
                {
                    frmMember memberForm = new frmMember();
                    frmMember.MemberEmail = email;
                    memberForm.Show();
                }
            }
            else
            {
                MessageBox.Show("Login failed!", "Login", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            txtEmail.ResetText();
            txtPassword.ResetText();
        }
    }
}
