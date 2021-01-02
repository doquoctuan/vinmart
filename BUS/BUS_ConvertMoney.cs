using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAO;
namespace BUS
{
    public class BUS_ConvertMoney
    {
        private static BUS_ConvertMoney instance;

        public BUS_ConvertMoney()
        {

        }
        public static BUS_ConvertMoney Instance
        {
            get { if (instance == null) instance = new BUS_ConvertMoney(); return BUS_ConvertMoney.instance; }
            private set { BUS_ConvertMoney.instance = value; }
        }

        public string chuyenDoi(double gnumber)
        {
            return ConvertMoney.Instance.So_chu(gnumber);
        }
    }
}
