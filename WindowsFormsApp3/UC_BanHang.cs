using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Globalization;
using ZXing;
using AForge.Video;
using DTO;
using BUS;
namespace WindowsFormsApp3
{
    public partial class UC_BanHang : UserControl
    {
    //    MJPEGStream stream;
        public UC_BanHang()
        {
            InitializeComponent();
            list = BUS_HangHoa.Intance.getListSanPham();
            AutoCompleteStringCollection arrName = new AutoCompleteStringCollection();
            foreach (DTO_HangHoa item in list)
            {
                arrName.Add(item.MaHang);
            }
            cbbMaHang.AutoCompleteCustomSource = arrName;
            cbbMaHang.DataSource = list;
            cbbMaHang.DisplayMember = "MaHang";
            cbbMaHang.ValueMember = "MaHang";

            List<DTO_KhachHang> listKH = new List<DTO_KhachHang>();
            DataTable data2 = BUS_KhachHang.Intance.getListKH();
            foreach (DataRow item2 in data2.Rows)
            {
                DTO_KhachHang kh = new DTO_KhachHang(item2);
                listKH.Add(kh);
            }
            AutoCompleteStringCollection arrName2 = new AutoCompleteStringCollection();
            foreach (DTO_KhachHang itemKH in listKH)
            {
               arrName2.Add(itemKH.SDT1);
            }
            txtInPutNumberPhone.AutoCompleteCustomSource = arrName2;
            resetInfoProduct();
            pictureBox1.Visible = false;

        }

        List<DTO_HangHoa> list;
 

        DTO_KhachHang khachHang = new DTO_KhachHang()
        {
            MaKH = null
        };
            
        int i;
        private void cbbMaHang_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbbMaHang.SelectedIndex >= 0)
            {
                i = cbbMaHang.SelectedIndex;
                txtTenHang.Text = list[i].TenHang;
                txtDonViTinh.Text = list[i].DonVi;
                txtGia.Text = list[i].GiaBan.ToString();
            }
        }

        int tongTien = 0;

        private void btnThemMatHang_Click(object sender, EventArgs e)
        {
            if (cbbMaHang.SelectedIndex >= 0 && txtSoLuong.Value > 0)
            {
                bool check = false;
                foreach (ListViewItem item in lvSanPhamBan.Items)
                {
                    if (item.SubItems[0].Text == cbbMaHang.SelectedValue.ToString())
                    {
                        check = true;
                    }
                    if (check)
                    {
                        int temp = Int32.Parse(item.SubItems[2].Text) + Int32.Parse(txtSoLuong.Value.ToString());
                        item.SubItems[2].Text = temp.ToString();
                        item.SubItems[3].Text = (Int32.Parse(item.SubItems[2].Text)*Int32.Parse(txtGia.Text)).ToString();
                      
                        break;
                    }
                }
                int gia = Int32.Parse(txtGia.Text) * Int32.Parse(txtSoLuong.Value.ToString());
                if (!check)
                {
                    string[] arr = new string[4];
                    arr[0] = cbbMaHang.SelectedValue.ToString();
                    arr[1] = txtTenHang.Text;
                    arr[2] = txtSoLuong.Value.ToString();
                    arr[3] = gia.ToString();
                    ListViewItem listViewItem = new ListViewItem(arr);
                    lvSanPhamBan.Items.Add(listViewItem);
                }
                tongTien += gia;
                lbTienBangSo.Text = string.Format(new CultureInfo("vi-VN"), "{0:#,##0.00}", tongTien) + " VNĐ";
                lbTienBangChu.Text = ConvertMoney.Instance.So_chu(tongTien);
                resetInfoProduct();
               /* if (button2.Text == "QR")
                {
                    stream = new MJPEGStream("http://172.16.1.140:4747/video");
                    stream.NewFrame += stream_NewFrame;
                    stream.Start();
                    timer1.Enabled = true;
                    timer1.Start();
                    button2.Text = "...";
                }
                else
                {
                    button2.Text = "QR";
                    timer1.Stop();
                    stream.Stop();
                }*/
            }
        }

        private void resetInfoProduct()
        {
            cbbMaHang.SelectedIndex = -1;
            txtTenHang.Text = "";
            txtSoLuong.Value = 1;
            txtDonViTinh.Text = "";
            txtGia.Text = "";
        }

        private void btnXoaMatHang_Click(object sender, EventArgs e)
        {
            
            for (int i = 0; i < lvSanPhamBan.Items.Count; i++) //duyệt tất cả các item trong list
            {
                if (lvSanPhamBan.Items[i].Checked)//nếu item đó dc check
                {
                    string tien = lvSanPhamBan.Items[i].SubItems[3].Text.ToString();
                    tongTien -= Int32.Parse(tien);
                    lbTienBangSo.Text = string.Format(new CultureInfo("vi-VN"), "{0:#,##0.00}", tongTien) + " VNĐ";
                    lbTienBangChu.Text = ConvertMoney.Instance.So_chu(tongTien);
                    lvSanPhamBan.Items[i].Remove();//xóa item đó đi
                    i--;
                }
            }
        }

        private void btnThanhToan_Click(object sender, EventArgs e)
        {
            if (lvSanPhamBan.Items.Count > 0)
            {
                DTO_HoaDon hoaDon = new DTO_HoaDon();
                hoaDon.MaHD = BUS_HoaDon.Intance.LoadMaDHMoi();
                hoaDon.MaKH = khachHang.MaKH;
                DateTime oDate = DateTime.Now;
                hoaDon.NgayTao = oDate;
                hoaDon.TenDangNhap = "ADMIN";
                hoaDon.TongTien = tongTien;

                if (BUS_HoaDon.Intance.LuuDonHang(hoaDon))
                {
                    Form_Report rp = new Form_Report(hoaDon.MaHD, ConvertMoney.Instance.So_chu(tongTien), txtTenKH.Text);
                    foreach (ListViewItem item in lvSanPhamBan.Items)
                    {
                        BUS_ChiTietHD.Intance.LuuDonHang(hoaDon.MaHD, item.SubItems[0].Text, Int32.Parse(item.SubItems[2].Text), Int32.Parse(item.SubItems[3].Text) / Int32.Parse(item.SubItems[2].Text));
                        string query = "update hanghoa set SoLuong = SoLuong - " + Int32.Parse(item.SubItems[2].Text) + "where MaHang ='" + item.SubItems[0].Text + "'";
                        DataProvider.Instance.ExecuteNonQuery(query);
                    }
                    lvSanPhamBan.Items.Clear();
                    lbTienBangSo.Text = "0 VNĐ";
                    lbTienBangChu.Text = "Không đồng";
                    txtInPutNumberPhone.Text = "";
                    khachHang.MaKH = null;
                    txtTenKH.Text = "UNKNOW NAME";
                    tongTien = 0;
                    rp.ShowDialog();
                }
            } else MessageBox.Show("Bạn chưa chọn sản phẩm nào!", "Thông báo");

        }

        private void txtInPutNumberPhone_TextChanged_1(object sender, EventArgs e)
        {
            khachHang = BUS_KhachHang.Intance.GetTenBySDT(txtInPutNumberPhone.Text);
            txtTenKH.Text = khachHang.TenKH;
            if (txtInPutNumberPhone.Text.Length == 10 && txtTenKH.Text == "")
            {
                txtTenKH.Text = "Không Tìm Thấy";
            }
        }

        private void btnThemMoiKH_Click(object sender, EventArgs e)
        {
            Form_ThemKH form = new Form_ThemKH(BUS_KhachHang.Intance.loadMaKH(), txtInPutNumberPhone.Text, this);
            form.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            /*if (button2.Text == "QR")
            {
                stream = new MJPEGStream("http://172.16.1.140:4747/video");
                stream.NewFrame += stream_NewFrame;
                stream.Start();
                timer1.Enabled = true;
                timer1.Start();
                button2.Text = "...";
            }
            else
            {
                button2.Text = "QR";
                timer1.Stop();
                stream.Stop();
            }*/
        }
        public void stream_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            Bitmap bmp = (Bitmap)eventArgs.Frame.Clone();
            pictureBox1.Image = bmp;
            
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            /*Bitmap img = (Bitmap)pictureBox1.Image;
            if (img != null)
            {
                ZXing.BarcodeReader Reader = new ZXing.BarcodeReader();
                Result result = Reader.Decode(img);
                try
                {
                    if (result != null)
                    {
                        string decoded = result.ToString().Trim();
                        cbbMaHang.SelectedValue = decoded;
                        button2.Text = "QR";
                        timer1.Stop();
                        
                        img.Dispose();
                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message + "");
                }

            }*/
        }


        private void UC_BanHang_Load(object sender, EventArgs e)
        {
            /*if (button2.Text == "QR")
            {
                stream = new MJPEGStream("http://172.16.1.140:4747/video");
                stream.NewFrame += stream_NewFrame;
                stream.Start();
                timer1.Enabled = true;
                timer1.Start();
                button2.Text = "...";
            }
            else
            {
                button2.Text = "QR";
                timer1.Stop();
                stream.Stop();
            }*/
        }

    }
}
