using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsFormsApp3;
using DTO;
namespace DAO
{
    public class DAO_DonViTinh
    {
        private static DAO_DonViTinh instance;

        public DAO_DonViTinh()
        {
        }

        public static DAO_DonViTinh Intance
        {
            get { if (instance == null) instance = new DAO_DonViTinh(); return instance; }
            set => instance = value;
        }

        public DTO_DonViTinh getDataById(string id)
        {
            DTO_DonViTinh item = new DTO_DonViTinh();
            string query = "select * from DonViTinh where MaDVT='" + id + "'";
            DataTable data = DataProvider.Instance.ExecuteQuery(query);

            if (data.Rows.Count > 0)
            {
                item = new DTO_DonViTinh(data.Rows[0]);
            }

            return item;
        }
    }
}
