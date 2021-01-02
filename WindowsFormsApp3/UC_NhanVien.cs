using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Diagnostics;
using System.IO;
using BUS;

namespace WindowsFormsApp3
{
    public partial class UC_NhanVien : UserControl
    {
        public UC_NhanVien()
        {
            InitializeComponent();
            LoadData();
        }

        void loadBinding()
        {
            txtTaiKhoanNV.DataBindings.Add(new Binding("Text", dgvNhanVien.DataSource, "TenDangNhap", true, DataSourceUpdateMode.Never));
            txtTenNV.DataBindings.Add(new Binding("Text", dgvNhanVien.DataSource, "TenNguoiDung", true, DataSourceUpdateMode.Never));
            txtMatKhauNV.DataBindings.Add(new Binding("Text", dgvNhanVien.DataSource, "MatKhau", true, DataSourceUpdateMode.Never));
        }
        void LoadData()
        {
        //    ClearBinding();
            dgvNhanVien.DataSource = BUS_NhanVien.Intance.getListNV();
            dgvNhanVien.Columns["TenDangNhap"].HeaderText = "Tên Đăng Nhập";
            dgvNhanVien.Columns["MatKhau"].HeaderText = "Mật Khẩu";
            dgvNhanVien.Columns["Quyen"].HeaderText = "Chức Vụ";
            dgvNhanVien.Columns["TenNguoiDung"].HeaderText = "Tên Hiển Thị";
            //   loadBinding();
        }

        bool KiemTraNhap()
        {
            if (txtTaiKhoanNV.Text == "")
            {
                MessageBox.Show("Tên đăng nhập không được bỏ trống", "Thông báo");
                txtTaiKhoanNV.Focus();
                return false;
            } else if (txtMatKhauNV.Text == "")
            {
                MessageBox.Show("Mật khẩu không được bỏ trống", "Thông báo");
                txtMatKhauNV.Focus();
                return false;
            } else if (txtTenNV.Text == "")
            {
                MessageBox.Show("Tên người dùng không được bỏ trống", "Thông báo");
                txtTenNV.Focus();
                return false;
            }
            return true;
        }

        void ClearBinding()
        {
            txtTaiKhoanNV.DataBindings.Clear();
            txtTenNV.DataBindings.Clear();
            txtMatKhauNV.DataBindings.Clear();
  
        }
        public bool check = true;

        public void resetData()
        {
            txtTaiKhoanNV.Text = "";
            txtMatKhauNV.Text = "";
            txtTenNV.Text = "";
        }
        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (check == true)
            {
                check = !check;
                btnSua.Enabled = false;
                btnXoa.Enabled = false;
                btnLuu.Text = "Lưu";
                resetData();
                txtTaiKhoanNV.Focus();
            }
            else
            {
                if (KiemTraNhap())
                {
                    check = !check;
                    btnSua.Enabled = true;
                    btnXoa.Enabled = true;
                    btnLuu.Text = "Thêm";
                    if (BUS_NhanVien.Intance.themNV( txtTaiKhoanNV.Text, txtMatKhauNV.Text,txtTenNV.Text, cbbChucVu.SelectedItem.ToString()))
                    {
                        MessageBox.Show("Thêm thành công!", "Thông báo");
                        LoadData();
                    }
                }

            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (dgvNhanVien.SelectedCells.Count > 0)
            {
                if (BUS_NhanVien.Intance.suaNV(txtTaiKhoanNV.Text, txtMatKhauNV.Text, txtTenNV.Text, cbbChucVu.SelectedItem.ToString()))
                {
                    MessageBox.Show("Sửa thành công!", "Thông báo");
                    LoadData();
                }
            }
        }

        private void dgvNhanVien_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvNhanVien.SelectedCells.Count > 0)
            {
                cbbChucVu.SelectedItem = dgvNhanVien.SelectedCells[3].Value;
                ClearBinding();
                loadBinding();
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            DialogResult dlr = MessageBox.Show("Bạn có muốn xóa không?",
            "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dlr == DialogResult.Yes)
            {
                BUS_NhanVien.Intance.xoaNV(txtTaiKhoanNV.Text);
                MessageBox.Show("Xóa thành công!", "Thông báo");
                LoadData();
            }
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            check = !check;
            btnSua.Enabled = true;
            btnXoa.Enabled = true;
            btnLuu.Text = "Thêm";
            LoadData();
        }

        private void txtTimKiemNhanVien_TextChanged(object sender, EventArgs e)
        {
            dgvNhanVien.DataSource = BUS_NhanVien.Intance.TimKiemNV(txtTimKiemNhanVien.Text);
        }
    }
}
