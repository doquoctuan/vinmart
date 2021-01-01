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
    public class BUS_PhieuNhap
    {
        private static BUS_PhieuNhap instance;

        public BUS_PhieuNhap()
        {
        }

        public static BUS_PhieuNhap Intance
        {
            get { if (instance == null) instance = new BUS_PhieuNhap(); return instance; }
            set => instance = value;
        }

        public DataTable GetListPN()
        {
            return DAO_PhieuNhap.Intance.GetListPN();
        }

        public bool themPN(string maPN, string maNCC, DateTime NgayNhap, string TenDangNhap)
        {

            return DAO_PhieuNhap.Intance.themPN(maPN, maNCC,NgayNhap,TenDangNhap);
        }

        public string loadMaPN()
        {
            return DAO_PhieuNhap.Intance.loadMaPN();
        }

        public bool xoaPN(string maPN)
        {
            return DAO_PhieuNhap.Intance.xoaPN(maPN);
        }


        public DataTable TimKiemPN(string maPN)
        {
            return DAO_PhieuNhap.Intance.TimKiemPN(maPN);
        }
    }
}
