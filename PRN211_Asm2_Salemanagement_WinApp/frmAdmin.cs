using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PRN211_Asm2_Salemanagement_Library.Repos.MemberRepo;
using PRN211_Asm2_Salemanagement_WinApp.Mapper;

namespace PRN211_Asm2_Salemanagement_WinApp
{
    public partial class frmAdmin : Form
    {
        public MemberMapper MemberLogin { get; set; }
        public IMemberRepo MemberRepo { get; set; }
        public frmAdmin()
        {
            InitializeComponent();
        }

    }
}
