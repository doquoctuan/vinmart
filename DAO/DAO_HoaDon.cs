using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsFormsApp3;
using DTO;
using System.Data;

namespace DAO
{
    public class DAO_HoaDon
    {
        private static DAO_HoaDon instance;

        public DAO_HoaDon()
        {
        }

        public static DAO_HoaDon Intance
        {
            get { if (instance == null) instance = new DAO_HoaDon(); return instance; }
            set => instance = value;
        }

        public string LoadMaDHMoi()
        {
            string madh = "";

            madh = "DH" + DateTime.Now.Day.ToString("00") + DateTime.Now.Month.ToString("00") + DateTime.Now.Year.ToString("0000");

            string query = String.Format("SELECT dbo.fn_Get_MaDonHang_Next( @MaHD )");

            object madh_next = DataProvider.Instance.ExecuteScalar(query, new object[] { madh });

            if (madh_next.ToString() == "")
            {
                madh_next = madh + "001";
            }
            return madh_next.ToString();
        }

        public bool LuuDonHang(DTO_HoaDon dh)
        {
            // Convert datetime to date SQL Server 
            string query = String.Format("insert into HoaDon values('{0}','{1}','{2}','{3}','{4}')", dh.MaHD, dh.MaKH, dh.NgayTao, dh.TenDangNhap, dh.TongTien);
            int result = DataProvider.Instance.ExecuteNonQuery(query);
            return result > 0;
        }

        public DataTable LoadDanhSachDonHangTheoKH(string MaKH)
        {
            string query = "select * from HoaDon where MaKH ='" + MaKH + "'";
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            return data;
        }
    }
}
