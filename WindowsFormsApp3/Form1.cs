using BUS;
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

namespace WindowsFormsApp3
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public static string tenNgDung, quyen, tk, matkhau;

        private void guna2GradientButton1_Click(object sender, EventArgs e)
        {
            string tenDangNhap = guna2TextBox1.Text;
            string passWord = guna2TextBox2.Text;
            int a;
            if (int.TryParse(tenDangNhap, out a))
            {
                if (LoginKH(tenDangNhap, passWord))
                {
                    Form_KhachHang kh = new Form_KhachHang(BUS_KhachHang.Intance.getDataByID(tenDangNhap));
                    this.Hide();
                    kh.ShowDialog();
                    this.Show();
                }
                else
                {
                    MessageBox.Show("Sai tên tài khoản hoặc mật khẩu!");
                }
            } 
            else
            {
                if (Login(tenDangNhap, passWord))
                {
                    tenNgDung = BUS_NhanVien.Intance.getNVByID(tenDangNhap).TenNguoiDung;
                    tk = tenDangNhap;
                    quyen = BUS_NhanVien.Intance.getNVByID(tenDangNhap).Quyen1;
                    matkhau = BUS_NhanVien.Intance.getNVByID(tenDangNhap).MatKhau;
                    Main f = new Main();
                    this.Hide();
                    f.ShowDialog();
                    this.Show();
                }
                else
                {
                    MessageBox.Show("Sai tên tài khoản hoặc mật khẩu!");
                }
            }
        }

        bool Login(string userName, string passWord)
        {
            return BUS_NhanVien.Intance.Login(userName, passWord);
        }

        bool LoginKH(string userName, string passWord)
        {
            return BUS_KhachHang.Intance.Login(userName, passWord);
        }
    }
}
