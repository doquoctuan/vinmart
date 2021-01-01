using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;
using DAO;
using System.Data;

namespace BUS
{
    public class BUS_HangHoa
    {
        private static BUS_HangHoa instance;

        public BUS_HangHoa()
        {
        }

        public static BUS_HangHoa Intance
        {
            get { if (instance == null) instance = new BUS_HangHoa(); return instance; }
            set => instance = value;
        }

        public List<DTO_HangHoa> getListSanPham()
        {
            return DAO_HangHoa.Intance.getListSanPham();
        }

        public DTO_HangHoa getSP(string maSP)
        {
            return DAO_HangHoa.Intance.getSP(maSP);
        }

        public bool suaHH(string MaHang, string TenHH, string DVT, int SoLuong, int GiaGoc, int GiaBan)
        {
            return DAO_HangHoa.Intance.suaHH(MaHang, TenHH, DVT, SoLuong, GiaGoc, GiaBan);
        }

        public bool kiemtraXoa(string maHang)
        {
            return DAO_HangHoa.Intance.kiemtraXoa(maHang);
        }

        public bool capNhatHH(string maHang, int SL, int DonGia)
        {
            return DAO_HangHoa.Intance.capNhatHH(maHang, SL, DonGia);
        }

        public bool xoaHang(string maKH)
        {
            return DAO_HangHoa.Intance.xoaHang(maKH);
        }

        public string loadMaHH()
        {
            return DAO_HangHoa.Intance.loadMaHH();
        }

        public DataTable TimKiemHH(string maPN)
        {
            return DAO_HangHoa.Intance.TimKiemHH(maPN);
        }
    }
}
