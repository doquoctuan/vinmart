using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAO;
using DTO;
namespace BUS
{
    public class BUS_NhanVien
    {
        private static BUS_NhanVien instance;

        public BUS_NhanVien()
        {
        }

        public static BUS_NhanVien Intance
        {
            get { if (instance == null) instance = new BUS_NhanVien(); return instance; }
            set => instance = value;
        }

        public bool Login(string userName, string passWord)
        {
            return DAO_NhanVien.Intance.Login(userName, passWord);
        }

        public bool doiMatKhau(string tenDangNhap, string matKhauMoi)
        {
            return DAO_NhanVien.Intance.doiMatKhau(tenDangNhap, matKhauMoi);
        }

        public DTO_NhanVien getNVByID(string id)
        {
            return DAO_NhanVien.Intance.getNVByID(id);
        }

        public DataTable getListNV()
        {
            return DAO_NhanVien.Intance.getListNV();
        }

        public bool themNV(string tenDangnhap, string MatKhau, string TenNgDung, string Quyen)
        {
            return DAO_NhanVien.Intance.themNV(tenDangnhap, MatKhau, TenNgDung, Quyen);
        }

        public bool suaNV(string tenDangnhap, string MatKhau, string TenNgDung, string Quyen)
        {
            return DAO_NhanVien.Intance.suaNV(tenDangnhap, MatKhau, TenNgDung, Quyen);
        }

        public bool xoaNV(string maKH)
        {
            return DAO_NhanVien.Intance.xoaNV(maKH);
        }

        public DataTable TimKiemNV(string name)
        {
            return DAO_NhanVien.Intance.TimKiemNV(name);
        }
    }
}
