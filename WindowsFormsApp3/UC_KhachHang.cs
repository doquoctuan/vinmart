﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BUS;
using DTO;

namespace WindowsFormsApp3
{
    public partial class UC_KhachHang : UserControl
    {
        public UC_KhachHang()
        {
            InitializeComponent();
            LoadListKH();
        }

        public void LoadListKH()
        {
            DataTable dt = BUS_KhachHang.Intance.getListKH();
            dgvKhachHang.DataSource = dt;
            dgvKhachHang.Columns["MaHang"].Visible = false;

            DTO_KhachHang khachHang = new DTO_KhachHang();
            khachHang = BUS_KhachHang.Intance.getDataByID(txtDienThoaiKH.Text);
            string query = String.Format("select * from HangThanhVien");
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            cbbHang.DataSource = data;
            cbbHang.ValueMember = "MaHang";
            cbbHang.DisplayMember = "TenHang";
            loadBinding();
        }
           
        void loadBinding()
        {
            txtMaKH.DataBindings.Add(new Binding("Text", dgvKhachHang.DataSource, "MaKH", true, DataSourceUpdateMode.Never));
            txtTenKH.DataBindings.Add(new Binding("Text", dgvKhachHang.DataSource, "TenKH", true, DataSourceUpdateMode.Never));
            txtDienThoaiKH.DataBindings.Add(new Binding("Text", dgvKhachHang.DataSource, "SDT", true, DataSourceUpdateMode.Never));
            txtEmailKH.DataBindings.Add(new Binding("Text", dgvKhachHang.DataSource, "Email", true, DataSourceUpdateMode.Never));
            rtbDiaChiKhachHang.DataBindings.Add(new Binding("Text", dgvKhachHang.DataSource, "DiaChi", true, DataSourceUpdateMode.Never));
        }

        void ClearBinding()
        {
            txtMaKH.DataBindings.Clear();
            txtTenKH.DataBindings.Clear();
            txtDienThoaiKH.DataBindings.Clear();
            txtEmailKH.DataBindings.Clear();
            rtbDiaChiKhachHang.DataBindings.Clear();
        }

        private void btnLamMoiKhachHang_Click(object sender, EventArgs e)
        {
            ClearBinding();
            LoadListKH();
        }
        public bool check = false;
        private void btnThemKhachHang_Click(object sender, EventArgs e)
        {
            check = !check;
            if (check == true)
            {
                txtMaKH.Text = "";
                txtTenKH.Text = "";
                txtDienThoaiKH.Text = "";
                txtEmailKH.Text = "";
                rtbDiaChiKhachHang.Text = "";
                txtMaKH.Enabled = true;
                txtTenKH.Enabled = true;
                txtDienThoaiKH.Enabled = true;
                txtEmailKH.Enabled = true;
                rtbDiaChiKhachHang.Enabled = true;
                dgvKhachHang.Enabled = false;
                btnThemKhachHang.Text = "Xác nhận";
            }
            else
            {
                btnThemKhachHang.Text = "Thêm Mới";
                txtMaKH.Enabled = false;
                txtTenKH.Enabled = false;
                txtDienThoaiKH.Enabled = false;
                txtEmailKH.Enabled = false;
                rtbDiaChiKhachHang.Enabled = false;
                dgvKhachHang.Enabled = true;
                if (txtMaKH.Text == "")
                {
                    MessageBox.Show("Nhập thiếu thông tin! Vui lòng thử lại");
                }
                else
                {
                    if (BUS_KhachHang.Intance.themKH(txtMaKH.Text, txtTenKH.Text, rtbDiaChiKhachHang.Text, txtDienThoaiKH.Text, txtEmailKH.Text))
                    {
                        MessageBox.Show("Thêm khách hàng thành công!", "Thông báo");
                        ClearBinding();
                        LoadListKH();
                    }
                    else MessageBox.Show("Thất bại!", "Thông báo");
                }
            }
        }

        private void btnSuaKhachHang_Click(object sender, EventArgs e)
        {
            check = !check;
            if (check == true)
            {
                txtTenKH.Enabled = true;
                txtDienThoaiKH.Enabled = true;
                txtEmailKH.Enabled = true;
                rtbDiaChiKhachHang.Enabled = true;
                btnThemKhachHang.Enabled = false;
            }
            else
            {
                txtMaKH.Enabled = false;
                txtTenKH.Enabled = false;
                txtDienThoaiKH.Enabled = false;
                txtEmailKH.Enabled = false;
                rtbDiaChiKhachHang.Enabled = false;
                if (BUS_KhachHang.Intance.suaKH(txtMaKH.Text, txtTenKH.Text, rtbDiaChiKhachHang.Text, txtDienThoaiKH.Text, txtEmailKH.Text))
                {
                     MessageBox.Show("Sửa thành công!", "Thông báo");
                    btnThemKhachHang.Enabled = true;
                    ClearBinding();
                    LoadListKH();
                }
            }
        }

        private void btnXoaKhachHang_Click(object sender, EventArgs e)
        {
            if (BUS_KhachHang.Intance.xoaKH(txtMaKH.Text))
            {
                MessageBox.Show("Xóa thành công!", "Thông báo");
                ClearBinding();
                LoadListKH();
            }
        }

        private void txtTimKiemKhachHang_TextChanged(object sender, EventArgs e)
        {
            dgvKhachHang.DataSource = BUS_KhachHang.Intance.TimKiemKH(txtTimKiemKhachHang.Text);
            ClearBinding();
            loadBinding();
        }

        private void dgvKhachHang_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvKhachHang.SelectedCells.Count > 0)
            {
                cbbHang.SelectedValue = dgvKhachHang.SelectedCells[5].Value;
            }
        }
    }
}
