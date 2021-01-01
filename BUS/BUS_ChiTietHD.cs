using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAO;
using DTO;
namespace BUS
{
    public class BUS_ChiTietHD
    {
        private static BUS_ChiTietHD instance;

        public BUS_ChiTietHD()
        {
        }

        public static BUS_ChiTietHD Intance
        {
            get { if (instance == null) instance = new BUS_ChiTietHD(); return instance; }
            set => instance = value;
        }

        public bool LuuDonHang(string madh, string masp, int sl, int gia)
        {
            return DAO_ChiTietHD.Intance.LuuDonHang(madh, masp, sl, gia);
        }
    }
}
