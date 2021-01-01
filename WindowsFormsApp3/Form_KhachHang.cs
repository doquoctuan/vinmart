using DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BUS;
namespace WindowsFormsApp3
{
    public partial class Form_KhachHang : Form
    {
        DTO_KhachHang khachHang;
        public Form_KhachHang(DTO_KhachHang khachHang)
        {
            InitializeComponent();
            this.khachHang = khachHang;
        }

        private void Form_KhachHang_Load(object sender, EventArgs e)
        {
            labelHoTen.Text = khachHang.TenKH;
            string query = String.Format("select * from HangThanhVien where MaHang ='{0}'", khachHang.MaHang);
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            if (data.Rows.Count > 0)
            {
                labelHang.Text = "Hạng thành viên: " + data.Rows[0]["TenHang"].ToString();
            }
            txtDiaChi.Text = khachHang.DiaChi;
            txtEmail.Text = khachHang.Email;
            txtSDT.Text = khachHang.SDT1;
            LoadDataDonHang(khachHang.MaKH);
        }

        private void btnXacNhan_Click(object sender, EventArgs e)
        {
            if (BUS_KhachHang.Intance.suaKH(khachHang.MaKH, khachHang.TenKH, txtDiaChi.Text, txtSDT.Text, txtEmail.Text))
            {
                MessageBox.Show("Sửa thành công");
            }
        }

        private void dataGridView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            
        }
        private void LoadDataDonHang(string MaKH)
        {
            dataGridView1.DataSource = BUS_HoaDon.Intance.LoadDanhSachDonHangTheoKH(MaKH);
            dataGridView1.Columns[0].HeaderText = "Mã đơn Hàng";
            dataGridView1.Columns[1].HeaderText = "Mã Khách Hàng";
            dataGridView1.Columns[2].HeaderText = "Ngày tạo";
            dataGridView1.Columns[3].HeaderText = "Người Tạo";
            dataGridView1.Columns[4].HeaderText = "Thành Tiền";
            foreach (DataGridViewColumn col in dataGridView1.Columns)
            {
                col.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            }
            dataGridView1.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }
    }
}
