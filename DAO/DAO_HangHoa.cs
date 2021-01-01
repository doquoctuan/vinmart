using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;
using WindowsFormsApp3;

namespace DAO
{
    public class DAO_HangHoa
    {
        private static DAO_HangHoa instance;

        public DAO_HangHoa()
        {
        }

        public static DAO_HangHoa Intance
        {
            get { if (instance == null) instance = new DAO_HangHoa(); return instance; }
            set => instance = value;
        }

        public List<DTO_HangHoa> getListSanPham()
        {
            List<DTO_HangHoa> list = new List<DTO_HangHoa>();
            DataTable data = DataProvider.Instance.ExecuteQuery("select * from HangHoa");
            foreach (DataRow item in data.Rows)
            {
                DTO_HangHoa hangHoa = new DTO_HangHoa(item);
                list.Add(hangHoa);
            }
            return list;
        }

        public bool suaHH(string MaHang, string TenHH, string DVT, int SoLuong, int GiaGoc, int GiaBan)
        {
            string query = String.Format("update HangHoa set SoLuong = {0}, GiaGoc = {1}, GiaBan = {2}, TenHang = N'{3}', DonVi = '{4}'  where MaHang = '{5}'", SoLuong, GiaGoc, GiaBan, TenHH, DVT, MaHang);
            int result = DataProvider.Instance.ExecuteNonQuery(query);
            return result > 0;
        }

        public bool kiemtraXoa(string maHang)
        {
            string query = String.Format("select * from ChiTietHD where MaHang = '{0}'", maHang);
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            if (data.Rows.Count > 0)
            {
                return false;
            }
            query = String.Format("select * from ChiTietPN where MaHang = '{0}'", maHang);
            data = DataProvider.Instance.ExecuteQuery(query);
            if (data.Rows.Count > 0)
            {
                return false;
            }
            return true;
        }

        public bool capNhatHH(string maHang, int SL, int DonGia)
        {
            string query = String.Format("update HangHoa set SoLuong = {0} + SoLuong, GiaGoc = (GiaGoc + {1})/2 where MaHang = '{2}'", SL, DonGia, maHang);
            int result = DataProvider.Instance.ExecuteNonQuery(query);
            return result > 0;
        }

        public bool xoaHang(string maKH)
        {
            string query = String.Format("delete from HangHoa where MaHang = '{0}'", maKH);
            int result = DataProvider.Instance.ExecuteNonQuery(query);
            return result > 0;
        }

        public string loadMaHH()
        {
            string maKHnext = "SP001";
            string query = "select top 1 MaHang from HangHoa order by MaHang desc";
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            if (data.Rows.Count > 0)
            {
                DataRow data2 = data.Rows[0];
                string maKH = data2["MaHang"].ToString();
                maKHnext = maKH.Substring(2);
                int i = Convert.ToInt32(maKHnext);
                i = i + 1;
                if (i < 100 && i > 9)
                {
                    maKHnext = "SP0" + i;
                }
                else if (i < 10) maKHnext = "SP00" + (i);
                else
                {
                    maKHnext = "SP" + i;
                }
            }

            return maKHnext;
        }

        public DataTable TimKiemHH(string maPN)
        {
            string query = "exec usp_timHangHoa @maPN";
            DataTable data = DataProvider.Instance.ExecuteQuery(query, new object[] { maPN });
            return data;
        }

        public DTO_HangHoa getSP(string maSP)
        {
            DTO_HangHoa a = new DTO_HangHoa();
            string query = String.Format("select * from HangHoa where MaHang = N'{0}'", maSP);
            if (DataProvider.Instance.ExecuteQuery(query).Rows.Count > 0)
            {
                DataRow data = DataProvider.Instance.ExecuteQuery(query).Rows[0];
                a.TenHang = data["TenHang"].ToString();
                a.MaHang = maSP;
                a.GiaGoc = int.Parse(data["GiaGoc"].ToString());
                a.GiaBan = int.Parse(data["GiaBan"].ToString());
            }
            return a;
        }

        public bool temHH(DTO_HangHoa data, string imgLocation)
        {
            byte[] images = null;
            FileStream stream = new FileStream(imgLocation, FileMode.Open, FileAccess.Read);
            BinaryReader brs = new BinaryReader(stream);
            images = brs.ReadBytes((int)stream.Length);

            using (SqlConnection connection = new SqlConnection("Data Source=DESKTOP-E7SCDHU\\SQLEXPRESS;Initial Catalog=QuanLyCuaHangThoiTrang;Integrated Security=True"))
            {
                string query = String.Format("Insert into HangHoa Values('{0}', N'{1}', '{2}', {3}, {4}, {5}, @hinh) ", data.MaHang, data.TenHang, data.DonVi, data.GiaBan, data.SoLuong, data.GiaGoc);
                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.Parameters.Add(new SqlParameter("@hinh", images));

                connection.Open();
                int n = cmd.ExecuteNonQuery();
                if (n > 0)
                {
                    return true;
                }
                connection.Close();
            }
            return false;
        }

        public void capNhatHinh(string imgLocation, string maHang)
        {
            byte[] images = null;
            FileStream stream = new FileStream(imgLocation, FileMode.Open, FileAccess.Read);
            BinaryReader brs = new BinaryReader(stream);
            images = brs.ReadBytes((int)stream.Length);
            // Update hình nếu có
            using (SqlConnection connection = new SqlConnection("Data Source=DESKTOP-E7SCDHU\\SQLEXPRESS;Initial Catalog=QuanLyCuaHangThoiTrang;Integrated Security=True"))
            {
                string query = String.Format("Update HangHoa set Anh = @hinh where MaHang = '{0}'", maHang);
                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.Parameters.Add(new SqlParameter("@hinh", images));
                connection.Open();
                cmd.ExecuteNonQuery();
                connection.Close();
            }
        }

        public byte[] getAnhByID(string ID)
        {
            string query = String.Format("select Anh from HangHoa where MaHang = '{0}'", ID);
            DataRow data = DataProvider.Instance.ExecuteQuery(query).Rows[0];
            byte[] img = ((byte[])data["Anh"]);
            return img;
        }
    }
}
