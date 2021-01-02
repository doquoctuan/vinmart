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
    public class BUS_ChiTietPN
    {
        private static BUS_ChiTietPN instance;

        public BUS_ChiTietPN()
        {
        }

        public static BUS_ChiTietPN Intance
        {
            get { if (instance == null) instance = new BUS_ChiTietPN(); return instance; }
            set => instance = value;
        }

        public bool LuuPhieuNhap(string maPN, string maHang, int sl, int gia)
        {
            return DAO_ChiTietPN.Intance.LuuPhieuNhap(maPN, maHang, sl, gia);
        }

        public DataTable getCTPN(string maPN)
        {
            return DAO_ChiTietPN.Intance.getCTPN(maPN);
        }
    }
}
