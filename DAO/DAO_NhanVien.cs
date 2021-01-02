using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using WindowsFormsApp3;
using DTO;
namespace DAO
{
    public class DAO_NhanVien
    {
        private static DAO_NhanVien instance;

        public DAO_NhanVien()
        {
        }

        public static DAO_NhanVien Intance
        {
            get { if (instance == null) instance = new DAO_NhanVien(); return instance; }
            set => instance = value;
        }

        public bool Login(string userName, string passWord)
        {
            MD5 mh = MD5.Create();
            byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(passWord);
            byte[] hash = mh.ComputeHash(inputBytes);
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("X2"));
            }
            string query = "SELECT * FROM NhanVien WHERE TenDangNhap = N'" + userName + "' AND MatKhau = N'" + sb + "' ";

            DataTable result = DataProvider.Instance.ExecuteQuery(query);

            return result.Rows.Count > 0;
        }

        public DTO_NhanVien getNVByID(string id)
        {
            string query = "SELECT * FROM NhanVien WHERE TenDangNhap = N'" + id + "'";
            DataRow a = DataProvider.Instance.ExecuteQuery(query).Rows[0];
            return new DTO_NhanVien(a);
        }

        public DataTable getListNV()
        {
            string query = "select * from NhanVien";
            return DataProvider.Instance.ExecuteQuery(query);
        }

        public bool themNV(string tenDangnhap, string MatKhau, string TenNgDung, string Quyen)
        {
            string query = String.Format("insert into NhanVien values (N'{0}', N'{1}', N'{2}', N'{3}')", tenDangnhap, MatKhau, TenNgDung, Quyen);
            int result = DataProvider.Instance.ExecuteNonQuery(query);
            return result > 0;
        }

        public bool doiMatKhau(string tenDangNhap, string matKhauMoi)
        {
            MD5 mh = MD5.Create();
            byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(matKhauMoi);
            byte[] hash = mh.ComputeHash(inputBytes);
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("X2"));
            }
            string query = String.Format("update NhanVien set MatKhau = '{0}' where TenDangNhap = '{1}'", sb, tenDangNhap);
            int result = DataProvider.Instance.ExecuteNonQuery(query);
            return result > 0;
        }

        public bool suaNV(string tenDangnhap, string MatKhau, string TenNgDung, string Quyen)
        {
            string query = String.Format("update NhanVien set TenNguoiDung = N'{0}', MatKhau = N'{1}', Quyen = N'{2}' where TenDangnhap = N'{3}'", TenNgDung, MatKhau, Quyen, tenDangnhap);
            int result = DataProvider.Instance.ExecuteNonQuery(query);
            return result > 0;
        }

        public bool xoaNV(string maKH)
        {
            string query = String.Format("delete from NhanVien where tenDangnhap = '{0}'", maKH);
            int result = DataProvider.Instance.ExecuteNonQuery(query);
            return result > 0;
        }

        public DataTable TimKiemNV(string name)
        {
            string query = string.Format("SELECT * FROM NhanVien WHERE dbo.GetUnsignString(TenDangNhap) LIKE N'%' + dbo.GetUnsignString(N'{0}') + '%'", name);
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            return data;
        }
    }
}
