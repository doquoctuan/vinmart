using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsFormsApp3;

namespace DAO
{
    public class DAO_ChiTietHD
    {
        private static DAO_ChiTietHD instance;

        public DAO_ChiTietHD()
        {
        }

        public static DAO_ChiTietHD Intance
        {
            get { if (instance == null) instance = new DAO_ChiTietHD(); return instance; }
            set => instance = value;
        }

        public bool LuuDonHang(string madh, string masp, int sl, int gia)
        {

            string query = String.Format("insert into ChiTietHD values('{0}','{1}','{2}','{3}')", madh, masp, sl, gia);
            int result = DataProvider.Instance.ExecuteNonQuery(query);
            return result > 0;
        }

        public DataTable getCTDN(string maHD)
        {
            string query = String.Format("select HangHoa.MaHang, TenHang, ChiTietHD.SoLuong, DonGia, DonGia * ChiTietHD.SoLuong As 'TongTien' from ChiTietHD, HangHoa where MaHD = '{0}'and ChiTietHD.MaHang = HangHoa.MaHang", maHD);

            return DataProvider.Instance.ExecuteQuery(query);
        }
    }
}
