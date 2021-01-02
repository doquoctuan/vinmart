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
    public class BUS_NhaCungCap
    {
        private static BUS_NhaCungCap instance;

        public BUS_NhaCungCap()
        {
        }

        public static BUS_NhaCungCap Intance
        {
            get { if (instance == null) instance = new BUS_NhaCungCap(); return instance; }
            set => instance = value;
        }

        public List<DTO_NhaCungCap> getListNCC()
        {
            return DAO_NhaCC.Intance.getListNCC();
        }

        public bool themNCC(DTO_NhaCungCap data)
        {
            return DAO_NhaCC.Intance.themNCC(data);
        }

        public bool suaNCC(DTO_NhaCungCap data)
        {
            return DAO_NhaCC.Intance.suaNCC(data);
        }

        public bool xoaNCC(string maKH)
        {
            return DAO_NhaCC.Intance.xoaNCC(maKH);
        }

        public string loadMaNCC()
        {
            return DAO_NhaCC.Intance.loadMaNCC();
        }

        public DataTable TimKiemNCC(string maPN)
        {
            return DAO_NhaCC.Intance.TimKiemNCC(maPN);
        }
    }
}
