using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;
using DAO;
namespace BUS
{
    public class BUS_DonViTinh
    {
        private static BUS_DonViTinh instance;

        public BUS_DonViTinh()
        {
        }

        public static BUS_DonViTinh Intance
        {
            get { if (instance == null) instance = new BUS_DonViTinh(); return instance; }
            set => instance = value;
        }

        public DTO_DonViTinh getDataById(string id)
        {
            return DAO_DonViTinh.Intance.getDataById(id);
        }
    }
}
