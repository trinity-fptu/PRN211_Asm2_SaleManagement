﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PRN211_Asm2_Salemanagement_WinApp.Mapper;

namespace PRN211_Asm2_Salemanagement_WinApp
{
    public partial class frmOrderDetail : Form
    {
        public frmOrderDetail()
        {
            InitializeComponent();
            OrderDetailMapper orderDetailMapper = new OrderDetailMapper();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Hide();
            frmAdmin frmAdmin = new frmAdmin();
            frmAdmin.Show();
        }
    }
}
