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

        public DTO_NhanVien getNVByID(string id)
        {
            return DAO_NhanVien.Intance.getNVByID(id);
        }

        public DataTable getListNV()
        {
            return DAO_NhanVien.Intance.getListNV();
        }
    }
}
