using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BUS;
using DTO;

namespace WindowsFormsApp3
{
    public partial class Form_PhieuNhap : Form
    {
        string maPN = BUS_PhieuNhap.Intance.loadMaPN();
        DateTime DatePNSua = DateTime.Now;
        public Form_PhieuNhap()
        {
            InitializeComponent();
            loadData();
            dgvCTPN.Rows.RemoveAt(0);
        }
        public Form_PhieuNhap(string maPN)
        {
            InitializeComponent();
            this.maPN = maPN;
            string query = "exec getCTPN @maPN";
            DataTable data = DataProvider.Instance.ExecuteQuery(query, new object[]{maPN});
            for (int i = 0; i < data.Rows.Count; i++)
            {
                DataGridViewRow row = (DataGridViewRow)dgvCTPN.Rows[0].Clone();
                row.Cells[0].Value = data.Rows[i]["MaHang"].ToString();
                row.Cells[1].Value = data.Rows[i]["TenHang"].ToString();
                row.Cells[2].Value = data.Rows[i]["SoLuong"];
                row.Cells[3].Value = data.Rows[i]["DonGia"];
                tongTien += int.Parse(row.Cells[2].Value.ToString()) * int.Parse(row.Cells[3].Value.ToString());
                row.Cells[4].Value = data.Rows[i]["NgayNhap"];
                DatePNSua = (DateTime)data.Rows[i]["NgayNhap"];
                row.Cells[5].Value = data.Rows[i]["TenDangNhap"].ToString();
                dgvCTPN.Rows.Add(row);
            }
            loadData();
            dgvCTPN.Rows.RemoveAt(data.Rows.Count);
            cbbNCC.SelectedValue = data.Rows[0]["MaNCC"].ToString();
            label4.Text = string.Format(new CultureInfo("vi-VN"), "{0:#,##0.00}", tongTien) + " VNĐ";
        }

        void loadData()
        {
            List<DTO_HangHoa> list = BUS_HangHoa.Intance.getListSanPham();
            AutoCompleteStringCollection arrName = new AutoCompleteStringCollection();
            foreach (DTO_HangHoa item in list)
            {
                arrName.Add(item.TenHang);
            }
            cbbSanPham.AutoCompleteCustomSource = arrName;
            cbbSanPham.DataSource = list;
            cbbSanPham.DisplayMember = "TenHang";
            cbbSanPham.ValueMember = "MaHang";
            cbbSanPham.SelectedIndex = -1;

            List<DTO_NhaCungCap> list2 = BUS_NhaCungCap.Intance.getListNCC();
            AutoCompleteStringCollection arrName2 = new AutoCompleteStringCollection();
            foreach (DTO_NhaCungCap item in list2)
            {
                arrName2.Add(item.TenNCC);
            }
            cbbNCC.AutoCompleteCustomSource = arrName2;
            cbbNCC.DataSource = list2;
            cbbNCC.DisplayMember = "TenNCC";
            cbbNCC.ValueMember = "MaNCC";
            cbbNCC.SelectedIndex = -1;
        }
        int tongTien = 0;

        private void dgvCTPN_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            int temp = 0;
            for (int i = 0; i < dgvCTPN.Rows.Count - 1; i++)
            {
                temp += int.Parse(dgvCTPN.Rows[i].Cells[2].Value.ToString()) * int.Parse(dgvCTPN.Rows[i].Cells[3].Value.ToString());
            }
            label4.Text = string.Format(new CultureInfo("vi-VN"), "{0:#,##0.00}", temp) + " VNĐ";
            tongTien = temp;
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            BUS_PhieuNhap.Intance.xoaPN(maPN);
            if (cbbNCC.SelectedIndex == -1)
            {
                MessageBox.Show("Hãy chọn nhà cung cấp!");
            }
            else
            {
                if (BUS_PhieuNhap.Intance.themPN(maPN, cbbNCC.SelectedValue.ToString(), DatePNSua, Form1.tk))
                {
                    for (int i = 0; i < dgvCTPN.Rows.Count - 1; i++)
                    {
                        string maHang = dgvCTPN.Rows[i].Cells[0].Value.ToString();
                        int soluong = int.Parse(dgvCTPN.Rows[i].Cells[2].Value.ToString());
                        int dongia = int.Parse(dgvCTPN.Rows[i].Cells[3].Value.ToString());
                        if (BUS_ChiTietPN.Intance.LuuPhieuNhap(maPN, maHang, soluong, dongia))
                        {
                            BUS_HangHoa.Intance.capNhatHH(maHang, soluong, dongia);
                        }
                    }
                    MessageBox.Show("Thành Công");
                    this.Close();
                }
            }
        }

        private void Form_PhieuNhap_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'quanLyCuaHangThoiTrangDataSet3.NhaCungCap' table. You can move, or remove it, as needed.
        //    this.nhaCungCapTableAdapter.Fill(this.quanLyCuaHangThoiTrangDataSet3.NhaCungCap);

        }

        private void cbbSanPham_SelectedIndexChanged(object sender, EventArgs e)
        {
            bool check = true;
            
            if (cbbSanPham.SelectedIndex >= 0)
            {
                for (int i = 0; i < dgvCTPN.Rows.Count - 1; i++)
                {
                    string maHang = dgvCTPN.Rows[i].Cells[0].Value.ToString();
                    if (maHang == cbbSanPham.SelectedValue.ToString())
                    {
                        check = false;
                        dgvCTPN.Rows[i].Cells[2].Value = (int)dgvCTPN.Rows[i].Cells[2].Value + 50;
                        tongTien += 50 * int.Parse(dgvCTPN.Rows[i].Cells[3].Value.ToString());
                        label4.Text = string.Format(new CultureInfo("vi-VN"), "{0:#,##0.00}", tongTien) + " VNĐ";
                    }
                }
                if (check)
                {
                    DataGridViewRow row = (DataGridViewRow)dgvCTPN.Rows[0].Clone();
                    row.Cells[0].Value = cbbSanPham.SelectedValue;
                    row.Cells[1].Value = BUS_HangHoa.Intance.getSP(cbbSanPham.SelectedValue.ToString()).TenHang;
                    row.Cells[2].Value = 100;
                    row.Cells[3].Value = BUS_HangHoa.Intance.getSP(cbbSanPham.SelectedValue.ToString()).GiaGoc;
                    row.Cells[4].Value = DateTime.Now;
                    row.Cells[5].Value = Form1.tenNgDung;
                    dgvCTPN.Rows.Add(row);
                    if (row.Cells[2].Value != null && row.Cells[3].Value != null)
                    {
                        tongTien += (int)row.Cells[2].Value * (int)row.Cells[3].Value;
                    }
       
                    label4.Text = string.Format(new CultureInfo("vi-VN"), "{0:#,##0.00}", tongTien) + " VNĐ";
                } 
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            DialogResult dlr = MessageBox.Show("Bạn có muốn xóa không?",
            "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dlr == DialogResult.Yes)
            {
                DataGridViewRow row = dgvCTPN.SelectedCells[0].OwningRow;
                dgvCTPN.Rows.RemoveAt(row.Index);
                int temp = 0;
                for (int i = 0; i < dgvCTPN.Rows.Count - 1; i++)
                {
                    temp += int.Parse(dgvCTPN.Rows[i].Cells[2].Value.ToString()) * int.Parse(dgvCTPN.Rows[i].Cells[3].Value.ToString());
                }
                label4.Text = string.Format(new CultureInfo("vi-VN"), "{0:#,##0.00}", temp) + " VNĐ";
                tongTien = temp;
            }
        }
    }
}
