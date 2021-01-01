using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Data.SqlClient;
using BUS;

namespace WindowsFormsApp3
{
    public partial class UC_KhoHang : UserControl
    {
        public UC_KhoHang()
        {
            InitializeComponent();
            loadData();
        }

        public void loadData()
        {
            dgvHangHoa.DataSource = BUS_HangHoa.Intance.getListSanPham();
        //    dgvHangHoa.Columns["Anh"].Visible = false;
            dgvHangHoa.Columns[0].HeaderText = "Mã Hàng";
            dgvHangHoa.Columns["DonVi"].HeaderText = "Đơn Vị Tính";
            dgvHangHoa.Columns["SoLuong"].HeaderText = "Số Lượng";
            dgvHangHoa.Columns["GiaGoc"].HeaderText = "Giá Gốc";
            dgvHangHoa.Columns["GiaBan"].HeaderText = "Giá Bán";
            dgvHangHoa.Columns[1].HeaderText = "Tên Hàng";

            DataTable dataDVTinh = DataProvider.Instance.ExecuteQuery("select * from DonViTinh");
            cbbDVT.DataSource = dataDVTinh;
            cbbDVT.ValueMember = "MaDVT";
            cbbDVT.DisplayMember = "TenDVT";
    
            dgvHangHoa.AllowUserToAddRows = false;
            dgvHangHoa.EditMode = DataGridViewEditMode.EditProgrammatically;

            pcbHangHoa.SizeMode = PictureBoxSizeMode.StretchImage;
        }

        private void btnThemMatHangMoi_Click(object sender, EventArgs e)
        {
            Form_ThemMoiSanPham tmsp = new Form_ThemMoiSanPham();
            tmsp.ShowDialog();
        }


        string imgLocation = Application.StartupPath + "\\Resources\\hanghoa.png";

        private void guna2Button6_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlgOpen = new OpenFileDialog();
            dlgOpen.Filter = "PNG files(*.png)|*.png|JPEG(*.jpg)|*.jpg|GIF(*.gif)|*.gif|All files(*.*)|*.*";
            dlgOpen.FilterIndex = 2;
            dlgOpen.Title = "Chọn ảnh minh hoạ cho sản phẩm";
            if (dlgOpen.ShowDialog() == DialogResult.OK)
            {
                imgLocation = dlgOpen.FileName.ToString();
                pcbHangHoa.Image = Image.FromFile(dlgOpen.FileName);
            }
        }

        public void resetData()
        {
            txtTenHang.Text = "";
            txtSoLuong.Text = "0";
            txtGiaGoc.Text = "0";
            txtGiaBan.Text = "0";
            pcbHangHoa.Image = null;
        }
        public bool check = true;

        bool KiemTraNhap()
        {
            int a;
            if (txtTenHang.Text == "")
            {
                MessageBox.Show("Hãy nhập tên hàng hóa", "Thông báo");
                txtTenHang.Focus();
                return false;
            } else if (cbbDVT.SelectedIndex == -1)
            {
                MessageBox.Show("Hãy chọn đơn vị tính", "Thông báo");
                cbbDVT.Focus();
                return false;
            } else if (!int.TryParse(txtGiaGoc.Text, out a))
            {
                MessageBox.Show("Giá gốc phải là một số", "Thông báo");
                txtGiaGoc.Focus();
                return false;
            }
            else if (!int.TryParse(txtGiaBan.Text, out a))
            {
                MessageBox.Show("Giá bán phải là một số", "Thông báo");
                txtGiaBan.Focus();
                return false;
            }
            else if (!int.TryParse(txtSoLuong.Text, out a))
            {
                MessageBox.Show("Số lượng phải là một số", "Thông báo");
                txtSoLuong.Focus();
                return false;
            }
            return true;
        }
        private void guna2Button1_Click(object sender, EventArgs e)
        {
            txtMaHang.Text = BUS_HangHoa.Intance.loadMaHH();
            if (check == true)
            {
                check = !check;
                btnSua.Enabled = false;
                btnXoa.Enabled = false;
                btnThem.Text = "Lưu";
                resetData();
                txtTenHang.Enabled = true;
                txtTenHang.Focus();
                cbbDVT.Enabled = true;
                txtSoLuong.Enabled = true;
                txtGiaBan.Enabled = true;
                txtGiaGoc.Enabled = true;
            } else
            {
                if (KiemTraNhap())
                {
                    check = !check;
                    btnSua.Enabled = true;
                    btnXoa.Enabled = true;
                    btnThem.Text = "Thêm";
                    byte[] images = null;
                    FileStream stream = new FileStream(imgLocation, FileMode.Open, FileAccess.Read);
                    BinaryReader brs = new BinaryReader(stream);
                    images = brs.ReadBytes((int)stream.Length);

                    using (SqlConnection connection = new SqlConnection("Data Source=DESKTOP-E7SCDHU\\SQLEXPRESS;Initial Catalog=QuanLyCuaHangThoiTrang;Integrated Security=True"))
                    {
                        string query = String.Format("Insert into HangHoa Values('{0}', N'{1}', '{2}', {3}, {4}, {5}, @hinh) ", txtMaHang.Text, txtTenHang.Text, cbbDVT.SelectedValue, int.Parse(txtGiaBan.Text), int.Parse(txtSoLuong.Text), int.Parse(txtGiaGoc.Text));
                        SqlCommand cmd = new SqlCommand(query, connection);
                        cmd.Parameters.Add(new SqlParameter("@hinh", images));

                        connection.Open();
                        int n = cmd.ExecuteNonQuery();
                        if (n > 0)
                        {
                            MessageBox.Show("Thêm Thành Công");
                            imgLocation = Application.StartupPath + "\\Resources\\hanghoa.png";
                            resetData();
                            cbbDVT.SelectedValue = dgvHangHoa.Rows[0].Cells["DonVi"].Value;
                            loadData();
                        }
                        connection.Close();
                    }
                }
              
            }
            
        }

        void Binding()
        {
            txtMaHang.DataBindings.Add(new Binding("Text", dgvHangHoa.DataSource, "MaHang", true, DataSourceUpdateMode.Never));
            txtTenHang.DataBindings.Add(new Binding("Text", dgvHangHoa.DataSource, "TenHang", true, DataSourceUpdateMode.Never));
            txtSoLuong.DataBindings.Add(new Binding("Text", dgvHangHoa.DataSource, "SoLuong", true, DataSourceUpdateMode.Never));
            txtGiaGoc.DataBindings.Add(new Binding("Text", dgvHangHoa.DataSource, "GiaGoc", true, DataSourceUpdateMode.Never));
            txtGiaBan.DataBindings.Add(new Binding("Text", dgvHangHoa.DataSource, "GiaBan", true, DataSourceUpdateMode.Never));
        }

        void ClearBinding()
        {
            txtMaHang.DataBindings.Clear();
            txtTenHang.DataBindings.Clear();
            txtSoLuong.DataBindings.Clear();
            txtGiaGoc.DataBindings.Clear();
            txtGiaBan.DataBindings.Clear();
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            if (dgvHangHoa.SelectedCells.Count > 0)
            {
                if (BUS_HangHoa.Intance.suaHH(txtMaHang.Text, txtTenHang.Text, (string)cbbDVT.SelectedValue, int.Parse(txtSoLuong.Text), int.Parse(txtGiaGoc.Text), int.Parse(txtGiaBan.Text) ))
                {
                    if (imgLocation != Application.StartupPath + "\\Resources\\hanghoa.png")
                    {
                        byte[] images = null;
                        FileStream stream = new FileStream(imgLocation, FileMode.Open, FileAccess.Read);
                        BinaryReader brs = new BinaryReader(stream);
                        images = brs.ReadBytes((int)stream.Length);
                        // Update hình nếu có
                        using (SqlConnection connection = new SqlConnection("Data Source=DESKTOP-E7SCDHU\\SQLEXPRESS;Initial Catalog=QuanLyCuaHangThoiTrang;Integrated Security=True"))
                        {
                            string query = String.Format("Update HangHoa set Anh = @hinh where MaHang = '{0}'", txtMaHang.Text);
                            SqlCommand cmd = new SqlCommand(query, connection);
                            cmd.Parameters.Add(new SqlParameter("@hinh", images));
                            connection.Open();
                            cmd.ExecuteNonQuery();
                            connection.Close();
                        }
                    }
                    loadData();
                    cbbDVT.SelectedValue = dgvHangHoa.Rows[0].Cells["DonVi"].Value;
                    imgLocation = Application.StartupPath + "\\Resources\\hanghoa.png";
                    MessageBox.Show("Sửa Thành Công");
                }
            }
        }

        private void dgvHangHoa_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvHangHoa.SelectedCells.Count > 0)
            {
                ClearBinding();
                Binding();
                DataGridViewRow row = dgvHangHoa.SelectedCells[0].OwningRow;
                try
                {
                    string maHang = row.Cells["MaHang"].Value.ToString();
                    string query = String.Format("select Anh from HangHoa where MaHang = '{0}'", maHang);
                    DataRow data = DataProvider.Instance.ExecuteQuery(query).Rows[0];
                    byte[] img = ((byte[])data["Anh"]);
                    if (img == null)
                    {
                        pcbHangHoa.Image = null;
                    }
                    else
                    {
                        MemoryStream ms = new MemoryStream(img);
                        pcbHangHoa.Image = Image.FromStream(ms);
                    }
                }
                catch (Exception) { }

                cbbDVT.SelectedValue = row.Cells["DonVi"].Value;
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            DialogResult dlr = MessageBox.Show("Bạn có muốn xóa không?",
            "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dlr == DialogResult.Yes)
            {
                if (BUS_HangHoa.Intance.kiemtraXoa(txtMaHang.Text))
                {
                    BUS_HangHoa.Intance.xoaHang(txtMaHang.Text);
                    MessageBox.Show("Xóa thành công!", "Thông báo");
                    loadData();
                } else
                {
                    MessageBox.Show("Bạn không được xóa bản ghi này!", "Thông báo");
                }
                
            }
        }

        private void guna2Button5_Click(object sender, EventArgs e)
        {
            Form_DVT form_DVT = new Form_DVT(this);
            form_DVT.ShowDialog();
        }

        private void txtTimKiem_TextChanged(object sender, EventArgs e)
        {
            dgvHangHoa.DataSource = BUS_HangHoa.Intance.TimKiemHH(txtTimKiem.Text);
        }

        private void dgvHangHoa_SelectionChanged_1(object sender, EventArgs e)
        {
            if (dgvHangHoa.SelectedCells.Count > 0)
            {
                ClearBinding();
                Binding();
                DataGridViewRow row = dgvHangHoa.SelectedCells[0].OwningRow;
                try
                {
                    string maHang = row.Cells["MaHang"].Value.ToString();
                    string query = String.Format("select Anh from HangHoa where MaHang = '{0}'", maHang);
                    DataRow data = DataProvider.Instance.ExecuteQuery(query).Rows[0];
                    byte[] img = ((byte[])data["Anh"]);
                    if (img == null)
                    {
                        pcbHangHoa.Image = null;
                    }
                    else
                    {
                        MemoryStream ms = new MemoryStream(img);
                        pcbHangHoa.Image = Image.FromStream(ms);
                    }
                }
                catch (Exception) { }

                cbbDVT.SelectedValue = row.Cells["DonVi"].Value;
            }
        }

        private void dgvMatHang_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void guna2Button1_Click_1(object sender, EventArgs e)
        {
            check = !check;
            btnSua.Enabled = true;
            btnXoa.Enabled = true;
            btnThem.Text = "Thêm";
            loadData();
        }
    }
}
