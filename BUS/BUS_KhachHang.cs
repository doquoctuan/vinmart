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
    public class BUS_KhachHang
    {
        private static BUS_KhachHang instance;

        public BUS_KhachHang()
        {
        }

        public static BUS_KhachHang Intance
        {
            get { if (instance == null) instance = new BUS_KhachHang(); return instance; }
            set => instance = value;
        }

        public DataTable getListKH()
        {
            return DAO_KhachHang.Intance.getListKH();
        }

        public bool themKH(string maKH, string tenKH, string DiaChi, string SDT, string email, string maHang, string matKhau)
        {
            return DAO_KhachHang.Intance.themKH(maKH, tenKH,DiaChi,SDT,email, maHang, matKhau);
        }

        public bool suaKH(string maKH, string tenKH, string DiaChi, string SDT, string email, string maHang)
        {
            return DAO_KhachHang.Intance.suaKH(maKH, tenKH, DiaChi, SDT, email, maHang);
        }

        public bool xoaKH(string maKH)
        {
            return DAO_KhachHang.Intance.xoaKH(maKH);
        }

        public DataTable TimKiemKH(string name)
        {
            return DAO_KhachHang.Intance.TimKiemKH(name);
        }

        public string loadMaKH()
        {
            return DAO_KhachHang.Intance.loadMaKH();
        }

        public DTO_KhachHang GetTenBySDT(string id)
        {
            return DAO_KhachHang.Intance.GetTenBySDT(id);
        }

        public bool Login(string username, string pass)
        {
            return DAO_KhachHang.Intance.Login(username, pass);
        }

        public DTO_KhachHang getDataByID(string id)
        {
            return DAO_KhachHang.Intance.getDataByID(id);
        }

        public bool doiMatKhau(string maKH, string matKhauMoi)
        {
            return DAO_KhachHang.Intance.doiMatKhau(maKH, matKhauMoi);
        }
    }
}
