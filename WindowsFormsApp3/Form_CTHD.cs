using BUS;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp3
{
    public partial class Form_CTHD : Form
    {
        string maHD;
        public Form_CTHD(string maHD)
        {
            InitializeComponent();
            this.maHD = maHD;
        }

        private void Form_CTHD_Load(object sender, EventArgs e)
        {
            dgvCTHD.DataSource = BUS_ChiTietHD.Intance.getCTDN(maHD);
        }
    }
}
