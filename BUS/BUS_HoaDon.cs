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
    public class BUS_HoaDon
    {
        private static BUS_HoaDon instance;

        public BUS_HoaDon()
        {
        }

        public static BUS_HoaDon Intance
        {
            get { if (instance == null) instance = new BUS_HoaDon(); return instance; }
            set => instance = value;
        }

        public string LoadMaDHMoi()
        {
            return DAO_HoaDon.Intance.LoadMaDHMoi();
        }

        public bool LuuDonHang(DTO_HoaDon dh)
        {
            return DAO_HoaDon.Intance.LuuDonHang(dh);
        }

        public DataTable LoadDanhSachDonHangTheoKH(string MaKH)
        {
            return DAO_HoaDon.Intance.LoadDanhSachDonHangTheoKH(MaKH);
        }
    }
}
