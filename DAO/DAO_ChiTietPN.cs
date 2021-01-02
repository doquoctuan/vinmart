using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsFormsApp3;

namespace DAO
{
    public class DAO_ChiTietPN
    {
        private static DAO_ChiTietPN instance;

        public DAO_ChiTietPN()
        {
        }

        public static DAO_ChiTietPN Intance
        {
            get { if (instance == null) instance = new DAO_ChiTietPN(); return instance; }
            set => instance = value;
        }

        public bool LuuPhieuNhap(string maPN, string maHang, int sl, int gia)
        {
            string query = String.Format("insert into ChiTietPN values('{0}','{1}',{2},{3})", maPN, maHang, sl, gia);
            int result = DataProvider.Instance.ExecuteNonQuery(query);
            return result > 0;
        }

        public DataTable getCTPN(string maPN)
        {
            string query = "exec getCTPN @maPN";
            return DataProvider.Instance.ExecuteQuery(query, new object[] { maPN });
        }
    }
}
